using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ecommerce.Data;
using ecommerce.Models;

namespace ecommerce.Pages.Admin.Tuto
{
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
        public Models.Tuto Tuto { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Tuto.Add(Tuto);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
