using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ecommerce.Data;
using ecommerce.Models;

namespace ecommerce.Pages.Admin.Tuto
{
    public class DetailsModel : PageModel
    {
        private readonly ecommerce.Data.DataContext _context;

        public DetailsModel(ecommerce.Data.DataContext context)
        {
            _context = context;
        }

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
    }
}
