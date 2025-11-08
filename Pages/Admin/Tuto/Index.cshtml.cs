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
    public class IndexModel : PageModel
    {
        private readonly ecommerce.Data.DataContext _context;

        public IndexModel(ecommerce.Data.DataContext context)
        {
            _context = context;
        }

        public IList<Models.Tuto> Tuto { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Tuto = await _context.Tuto.ToListAsync();
        }
    }
}
