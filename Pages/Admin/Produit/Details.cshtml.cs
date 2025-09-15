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

namespace ecommerce.Pages.Admin.Produit
{
    [Authorize(Roles = "Admin")]
    public class DetailsModel : PageModel
    {
        private readonly ecommerce.Data.DataContext _context;

        public DetailsModel(ecommerce.Data.DataContext context)
        {
            _context = context;
        }

        public Produits Produits { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produits = await _context.Produits.FirstOrDefaultAsync(m => m.Id == id);

            if (produits is not null)
            {
                Produits = produits;

                return Page();
            }

            return NotFound();
        }
    }
}
