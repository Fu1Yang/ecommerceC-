using ecommerce.Data;
using ecommerce.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.Pages
{
    public class PanierModel : PageModel
    {
        private readonly DataContext _context;

        public PanierModel(DataContext context)
        {
            _context = context;
        }

        // Liste des articles du panier avec les infos produit
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
            if (string.IsNullOrEmpty(sessionCart))
            {
                CartItems = new List<CartItemViewModel>();
                return;
            }

            // CORRECTION: Désérialiser en Dictionary<int, int>
            var cartDict = JsonConvert.DeserializeObject<Dictionary<int, int>>(sessionCart);

            // Récupérer les produits depuis la base
            var productIds = cartDict.Keys.ToList();
            var produits = await _context.Produits
                                         .Where(p => productIds.Contains(p.Id))
                                         .ToListAsync();

            // Créer les items à afficher
            CartItems = cartDict.Select(c =>
            {
                var produit = produits.FirstOrDefault(p => p.Id == c.Key);
                return new CartItemViewModel
                {
                    ProductId = c.Key,
                    Name = produit?.Name ?? "Produit inconnu",
                    Price = produit?.Price ?? 0,
                    Quantity = c.Value
                };
            }).ToList();
        }
    }
}
