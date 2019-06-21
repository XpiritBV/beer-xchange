using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Xpirit.BeerXchange.Model;

namespace Xpirit.BeerXchange
{
    public class IndexModel : PageModel
    {
        private readonly Xpirit.BeerXchange.BeerXchangeContext _context;

        public IndexModel(Xpirit.BeerXchange.BeerXchangeContext context)
        {
            
            _context = context;
        }

        public IList<Beer> CurrentBeers { get;set; }
        public IList<Beer> HistoricalBeers { get; set; }

        public async Task OnGetAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            CurrentBeers = await _context.Beer.Where(b => b.RemovedBy == null).ToListAsync();
            HistoricalBeers = await _context.Beer.Where(b => b.RemovedDate != null || b.RemovedBy != null).OrderByDescending(b => b.RemovedDate).ToListAsync();
        }
    }
}
