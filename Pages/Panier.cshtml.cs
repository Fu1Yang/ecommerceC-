using ecommerce.Data;
using ecommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ecommerce.Pages
{
    public class PanierModel : PageModel
    {
        private readonly DataContext _context;

        public PanierModel(DataContext context)
        {
            _context = context;
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

        public async Task OnGetAsync()
        {
            var sessionCart = HttpContext.Session.GetString("Cart");

         
            //Console.WriteLine($"Contenu session: {sessionCart}");

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

        }

        [HttpPost]
        public IActionResult Vider()
        {
            HttpContext.Session.Remove("panier");
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        [IgnoreAntiforgeryToken]
        public IActionResult OnPostValider()
        {
            //HttpContext.Session.Remove("Cart");
            return RedirectToPage("/Client");
        }

    }
}