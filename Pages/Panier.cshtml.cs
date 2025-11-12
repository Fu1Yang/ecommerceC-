using ecommerce.Data;
using ecommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Stripe.Checkout;

namespace ecommerce.Pages
{
    public class PanierModel : PageModel
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public PanierModel(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public List<CartItemViewModel> CartItems { get; set; } = new();

        public class CartItemViewModel
        {
            public int ProductId { get; set; }
            public string Name { get; set; }
            public decimal Price { get; set; }
            public int Quantity { get; set; }
            public decimal Total => Price * Quantity;
        }

        // ✅ UNE SEULE méthode OnGetAsync
        public async Task OnGetAsync()
        {
            await LoadCartAsync();
        }

        [HttpPost]
        public IActionResult OnPostVider()
        {
            HttpContext.Session.Remove("Cart");
            HttpContext.Session.Clear();
            return RedirectToPage("/Index");
        }

        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> OnPostValider()
        {
            // Charger le panier depuis la base de données
            await LoadCartAsync();

            // Vérifier que le panier n'est pas vide
            if (!CartItems.Any())
            {
                TempData["Error"] = "Votre panier est vide";
                return RedirectToPage();
            }

            var domain = $"{Request.Scheme}://{Request.Host}";
            var lineItems = new List<SessionLineItemOptions>();

            // Parcourir les articles du panier réel
            foreach (var item in CartItems)
            {
                // DEBUG: Afficher les valeurs
                Console.WriteLine($"Article: {item.Name}, Prix: {item.Price}€, Quantité: {item.Quantity}");

                // Convertir le prix en centimes
                var unitAmount = (long)Math.Round(item.Price * 100);

                Console.WriteLine($"Prix en centimes: {unitAmount}");

                // Vérification du montant minimum
                if (unitAmount < 1)
                {
                    TempData["Error"] = $"Le prix de '{item.Name}' est invalide ({item.Price}€ = {unitAmount} centimes, minimum 0.01€)";
                    return RedirectToPage();
                }

                if (item.Quantity < 1)
                {
                    TempData["Error"] = $"La quantité de '{item.Name}' est invalide";
                    return RedirectToPage();
                }

                lineItems.Add(new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = "eur",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Name,
                            Description = $"Produit - {item.Name}",
                        },
                        UnitAmount = unitAmount, // Prix en centimes
                    },
                    Quantity = item.Quantity,
                });
            }

            Console.WriteLine($"Nombre d'articles à envoyer à Stripe: {lineItems.Count}");

            try
            {
                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = new List<string> { "card" },
                    LineItems = lineItems,
                    Mode = "payment",
                    SuccessUrl = domain + "/success",
                    CancelUrl = domain + "/panier",
                };

                var service = new SessionService();
                Session session = await service.CreateAsync(options);

                // Rediriger vers Stripe Checkout
                return Redirect(session.Url);
            }
            catch (Stripe.StripeException ex)
            {
                Console.WriteLine($"Erreur Stripe: {ex.Message}");
                Console.WriteLine($"Stripe Error: {ex.StripeError?.Message}");
                TempData["Error"] = $"Erreur lors de la création du paiement: {ex.Message}";
                return RedirectToPage();
            }
        }

        // Méthode corrigée qui charge depuis la base de données
        private async Task LoadCartAsync()
        {
            var sessionCart = HttpContext.Session.GetString("Cart");

            if (string.IsNullOrEmpty(sessionCart))
            {
                CartItems = new List<CartItemViewModel>();
                return;
            }

            List<PiecesAutoModel.CartItemDto> cartDto;

            try
            {
                cartDto = JsonConvert.DeserializeObject<List<PiecesAutoModel.CartItemDto>>(sessionCart);
            }
            catch (JsonSerializationException ex)
            {
                Console.WriteLine($"Erreur désérialisation: {ex.Message}");
                CartItems = new List<CartItemViewModel>();
                return;
            }

            if (cartDto == null || !cartDto.Any())
            {
                CartItems = new List<CartItemViewModel>();
                return;
            }

            // Récupérer les produits depuis la base
            var productIds = cartDto.Select(c => c.ProductId).ToList();
            var produits = await _context.Produits
                                         .Where(p => productIds.Contains(p.Id))
                                         .ToListAsync();

            // Créer les items à afficher
            CartItems = cartDto.Select(c =>
            {
                var produit = produits.FirstOrDefault(p => p.Id == c.ProductId);
                return new CartItemViewModel
                {
                    ProductId = c.ProductId,
                    Name = produit?.Name ?? "Produit inconnu",
                    Price = produit?.Price ?? 0,
                    Quantity = c.Quantity
                };
            }).ToList();

            // DEBUG: Afficher ce qui a été chargé
            Console.WriteLine($"=== PANIER CHARGÉ ===");
            foreach (var item in CartItems)
            {
                Console.WriteLine($"ID: {item.ProductId}, Nom: {item.Name}, Prix: {item.Price}, Qté: {item.Quantity}");
            }
        }
    }
}