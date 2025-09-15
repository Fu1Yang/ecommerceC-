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

namespace ecommerce.Pages.Admin.Produit
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
        public Produits Produits { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produits =  await _context.Produits.FirstOrDefaultAsync(m => m.Id == id);
            if (produits == null)
            {
                return NotFound();
            }
            Produits = produits;
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

            _context.Attach(Produits).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProduitsExists(Produits.Id))
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

        private bool ProduitsExists(int id)
        {
            return _context.Produits.Any(e => e.Id == id);
        }
    }
}
