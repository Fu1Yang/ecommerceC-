using ecommerce.Data;
using ecommerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ecommerce.Pages.Client
{
    [Authorize]
    public class ProfileModel : PageModel
    {
        private readonly DataContext _context;
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public string? Firstname { get; set; }
        public string? Age { get; set; }
        public string? PathPhoto { get; set; }
        public string? Adresse { get; set; }
        public string? Email { get; set; }

        public ProfileModel(DataContext context)
        {
            _context = context;
        }

        public async Task OnGet()
        {
            UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            UserName = User.Identity?.Name;

            Firstname = User.FindFirst("Firstname")?.Value;
            Age = User.FindFirst("Age")?.Value;
            Adresse = User.FindFirst("Adresse")?.Value;
            PathPhoto = User.FindFirst("PathPhoto")?.Value;

            Console.WriteLine($"Profil chargé pour {UserName}");
        }


    }
}
