using ecommerce.Data;
using ecommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ecommerce.Pages
{
   
    public class tutoModel : PageModel
    {
        private readonly DataContext _context;
        public List<Tuto> TutoListe { get; set; }
        public tutoModel(DataContext context)
        {
            _context = context;
        }
        public async Task OnGet()
        {
            TutoListe = await _context.Tuto.ToListAsync();
        }
    }
}
