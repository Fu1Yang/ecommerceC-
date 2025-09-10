using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ecommerce.Pages.Client
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }

        public void OnPost(string username, string password)
        {
            Console.WriteLine(username, password);
        }
    }
}
