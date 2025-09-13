﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ecommerce.Data;
using ecommerce.Models;
using Microsoft.AspNetCore.Authorization;

namespace ecommerce.Pages.Admin.Rdvs
{
    [Authorize(Roles ="Admin")]
    public class DeleteModel : PageModel
    {
        private readonly ecommerce.Data.DataContext _context;

        public DeleteModel(ecommerce.Data.DataContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Rdv Rdv { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rdv = await _context.Rdvs.FirstOrDefaultAsync(m => m.Id == id);

            if (rdv is not null)
            {
                Rdv = rdv;

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

            var rdv = await _context.Rdvs.FindAsync(id);
            if (rdv != null)
            {
                Rdv = rdv;
                _context.Rdvs.Remove(Rdv);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
