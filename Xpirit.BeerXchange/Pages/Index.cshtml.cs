using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Xpirit.BeerXchange.Model;

namespace Xpirit.BeerXchange
{
	public class IndexModel : PageModel
    {
        private readonly BeerXchangeContext _context;

        public IndexModel(BeerXchangeContext context)
        {
            _context = context;
        }

        public IList<Beer> CurrentBeers { get;set; }
        public IList<Beer> HistoricalBeers { get; set; }

        public int Credits { get; set; }

        public async Task OnGetAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            var allBeers = await _context.Beer.ToListAsync();
            CurrentBeers = allBeers.Where(b => b.RemovedBy == null).ToList();
            HistoricalBeers = allBeers.Where(b => b.RemovedDate != null || b.RemovedBy != null).OrderByDescending(b => b.RemovedDate).ToList();

            var user = User.FindFirst("name").Value;
            Credits = allBeers.Count(b => b.CreatedBy == user) - allBeers.Count(b => b.RemovedBy == user);
        }
    }
}
