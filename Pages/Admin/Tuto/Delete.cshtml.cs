using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ecommerce.Data;
using ecommerce.Models;
using Microsoft.AspNetCore.Authorization;

namespace ecommerce.Pages.Admin.Tuto
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
        public Models.Tuto Tuto { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tuto = await _context.Tuto.FirstOrDefaultAsync(m => m.Id == id);

            if (tuto is not null)
            {
                Tuto = tuto;

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

            var tuto = await _context.Tuto.FindAsync(id);
            if (tuto != null)
            {
                Tuto = tuto;
                _context.Tuto.Remove(Tuto);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
