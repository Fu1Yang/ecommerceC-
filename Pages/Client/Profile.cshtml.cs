using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ecommerce.Pages.Client
{
    [Authorize]
    public class ProfileModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
