using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ecommerce.Models;
using Microsoft.AspNetCore.Authorization;

namespace ecommerce.Pages.Admin.Estimates
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        private readonly ecommerce.Data.DataContext _context;

        public DeleteModel(ecommerce.Data.DataContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Estimate Estimate { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estimate = await _context.Estimate.FirstOrDefaultAsync(m => m.Id == id);

            if (estimate is not null)
            {
                Estimate = estimate;

                return Page();
            }

            return NotFound();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estimate = await _context.Estimate.FindAsync(id);
            if (estimate != null)
            {
                Estimate = estimate;
                _context.Estimate.Remove(Estimate);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
