using ecommerce.Data;
using ecommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

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

        }

        public void AddCart()
        {
            Console.WriteLine("oki");
        }
    }
}
