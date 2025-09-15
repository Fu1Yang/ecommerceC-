using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ecommerce.Data;
using ecommerce.Models;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.Pages.Client
{
    
    public class RegisterModel : PageModel
    {
        /*Connection DB*/
        private readonly ecommerce.Data.DataContext _context;

        public RegisterModel(ecommerce.Data.DataContext context)
        {
            _context = context;
        }
        
        public void OnGet()
        {

        }
        [BindProperty]
        public Users Users { get; set; } = new Users();

        public async Task<IActionResult> OnPostAsync(string username, string password, string verificationPassword, string email, string phone = "0660752322", bool emailConfirmed = true)
        {
     
                if (password != verificationPassword)
                {
                    ModelState.AddModelError(string.Empty, "Les mots de passe ne correspondent pas.");
                    return Page();
                }

                if (await _context.Users.AnyAsync(u => u.Email == email))
                {
                    ModelState.AddModelError(string.Empty, "Cet email est déjà utilisé.");
                    return Page();
                }
            try
            {
                var user = new Users
                {
                    Name = username,
                    Password = BCrypt.Net.BCrypt.HashPassword(password),
                    Email = email,
                    Phone = phone,
                    EmailConfirmed = emailConfirmed
                };

                    _context.Users.Add(user);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Une erreur est survenue : "+ex.Message);
                    return Page();
                }
                return RedirectToPage("/Client/Index");
        }
    }
}
