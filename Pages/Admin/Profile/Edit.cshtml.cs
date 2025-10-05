using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ecommerce.Data;
using ecommerce.Models;
using Microsoft.AspNetCore.Authorization;

namespace ecommerce.Pages.Admin.Profile
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly ecommerce.Data.DataContext _context;

        public EditModel(ecommerce.Data.DataContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Profiles Profiles { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profiles =  await _context.Profiles.FirstOrDefaultAsync(m => m.Id == id);
            if (profiles == null)
            {
                return NotFound();
            }
            Profiles = profiles;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Profiles).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfilesExists(Profiles.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ProfilesExists(int id)
        {
            return _context.Profiles.Any(e => e.Id == id);
        }
    }
}
