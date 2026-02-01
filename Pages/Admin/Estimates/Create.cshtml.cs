using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ecommerce.Models;
using Microsoft.AspNetCore.Authorization;

namespace ecommerce.Pages.Admin.Estimates
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly ecommerce.Data.DataContext _context;

        public CreateModel(ecommerce.Data.DataContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Estimate Estimate { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Estimate.Add(Estimate);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
