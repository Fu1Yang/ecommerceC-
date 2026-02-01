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

namespace ecommerce.Pages.Admin.Estimates
{
    [Authorize(Roles = "Admin")]
    public class DetailsModel : PageModel
    {
        private readonly ecommerce.Data.DataContext _context;

        public DetailsModel(ecommerce.Data.DataContext context)
        {
            _context = context;
        }

        public Estimate Estimate { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Estimate = await _context.Estimate.FirstOrDefaultAsync(m => m.Id == id);

            if (Estimate is not null)
            {
                Estimate = Estimate;

                return Page();
            }

            return NotFound();
        }
    }
}
