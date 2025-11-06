using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ecommerce.Pages
{
    public class SuccessModel : PageModel
    {
        public void OnGet()
        {
            // Vider le panier
            HttpContext.Session.Remove("Cart");

            // Vous pouvez récupérer session_id pour vérifier le paiement
            var sessionId = Request.Query["session_id"];
        }
    }
}
