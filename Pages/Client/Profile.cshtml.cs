using ecommerce.Data;
using ecommerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ecommerce.Pages.Client
{
    [Authorize]
    public class ProfileModel : PageModel
    {
        private readonly DataContext _context;

        public ProfileModel(DataContext context)
        {
            _context = context;
        }
        public List<Profiles> UserProfile {  get; set; }
      
        public async Task OnGet()
        {
            UserProfile = await _context.Profiles.ToListAsync();

            Console.WriteLine("OnGet appelé - Profiles chargés");
        }

    }
}
