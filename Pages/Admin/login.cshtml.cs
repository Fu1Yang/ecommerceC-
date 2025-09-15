using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;

namespace ecommerce.Pages.Admin
{
    public class loginModel : PageModel
    {
        IConfiguration configuration;
        public loginModel(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public IActionResult OnGet()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return Redirect("/admin/users");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string adminName, string password, string ReturnUrl)
        {
            var authSection = configuration.GetSection("Auth");

            string adminLogin = authSection["AdminLogin"];
            string adminPassword = authSection["AdminPassword"];

            if (adminName == adminLogin && password == adminPassword)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, adminName),
                    new Claim(ClaimTypes.Role, "Admin")
                };
                var claimsIdentity = new ClaimsIdentity(claims, "Login");
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                return Redirect(ReturnUrl == null ? "/admin/login" : ReturnUrl);
            }
            return Page();
        }

        public async Task<IActionResult>  OnGetLogout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }
    }
}
