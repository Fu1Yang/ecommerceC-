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

namespace ecommerce.Pages.Admin.Rdvs
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly ecommerce.Data.DataContext _context;

        public IndexModel(ecommerce.Data.DataContext context)
        {
            _context = context;
        }

        public IList<Rdv> Rdv { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Rdv = await _context.Rdvs.ToListAsync();
        }
    }
}
