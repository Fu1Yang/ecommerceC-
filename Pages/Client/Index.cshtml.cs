using ecommerce.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace ecommerce.Pages.Client
{
    public class IndexModel : PageModel
    {
        private readonly ecommerce.Data.DataContext _context;

        public IndexModel(ecommerce.Data.DataContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            // Si l'utilisateur est déjà connecté, on le redirige
            if (User.Identity.IsAuthenticated)
                Response.Redirect("/Client/Index");
        }

        [BindProperty] public string Email { get; set; }
        [BindProperty] public string Password { get; set; }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == Email);

            if(user == null || !BCrypt.Net.BCrypt.Verify(Password, user.Password))
            {
                ModelState.AddModelError(string.Empty, "Email ou mot de passe incorrect.");
                return Page();
            }

            // Récupérer le profil correspondant
            var profile = _context.Profiles.FirstOrDefault(p => p.IdUser == user.Id);
            // Création des claims
            var claims = new List<Claim>
            {
                // permet d’identifier l’utilisateur
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
            };
            // Ajouter les infos du profil si elles existent
            if (profile.IdUser != null )
            {
                claims.Add(new Claim("Name", profile.Name ?? ""));
                claims.Add(new Claim("Firstname", profile.Firstname ?? ""));
                claims.Add(new Claim("Age", profile.Age.ToString()));
                claims.Add(new Claim("Adresse", profile.Adresse ?? ""));
                claims.Add(new Claim("PathPhoto", profile.PathPhoto ?? ""));
            }
       
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            return Redirect(returnUrl ?? "/Client/Profile");
        }

        public async Task<IActionResult> OnGetLogout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToPage("/Client/Index");
        }

    }
}
