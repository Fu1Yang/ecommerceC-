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

        public PiecesAutoModel(DataContext context)
        {
            _context = context;
        }

        public List<Produits> ProduitsListe { get; set; }

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
                ? new Dictionary<int, int>()
                : JsonConvert.DeserializeObject<Dictionary<int, int>>(cartJson);

            if (cart.ContainsKey(productId))
                cart[productId]++;
            else
                cart[productId] = 1;

            HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cart));

            // DEBUG: Afficher le contenu du panier
            Console.WriteLine("Contenu du panier:");
            foreach (var item in cart)
            {
                Console.WriteLine($"- Produit {item.Key}: {item.Value} unité(s)");
            }

            return new JsonResult(new { success = true, message = "Produit ajouté au panier" });
        }
        public class CartItemDto
        {
            public int ProductId { get; set; }
            public int Quantity { get; set; } = 1;
        }
    }
}