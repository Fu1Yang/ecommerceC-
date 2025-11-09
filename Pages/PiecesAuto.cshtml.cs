using ecommerce.Data;
using ecommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ecommerce.Pages
{
    public class PiecesAutoModel : PageModel
    {
        private readonly DataContext _context;
        public List<Produits> ProduitsListe { get; set; }
        public PiecesAutoModel(DataContext context)
        {
            _context = context;
        }

        public async Task OnGet()
        {
            ProduitsListe = await _context.Produits.ToListAsync();
            Console.WriteLine("OnGet appelé - Produits chargés");
        }

        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> OnPostAddToCart([FromForm] int productId)
        {
            Console.WriteLine($"=== Ajout au panier - Produit ID: {productId} ===");

            var cartJson = HttpContext.Session.GetString("Cart");
            var cart = string.IsNullOrEmpty(cartJson)
                ? new List<CartItemDto>()
                : JsonConvert.DeserializeObject<List<CartItemDto>>(cartJson);

            // Check if the product already exists
            var existingItem = cart.FirstOrDefault(c => c.ProductId == productId);
            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                cart.Add(new CartItemDto { ProductId = productId, Quantity = 1 });
            }

            HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cart));

            return new JsonResult(new { success = true, message = "Produit ajouté au panier" });
        }

        public class CartItemDto
        {
            public int ProductId { get; set; }
            public int Quantity { get; set; } = 1;
        }
    }
}