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
	public class TakeModel : PageModel
    {
        private readonly BeerXchangeContext _context;

        public TakeModel(BeerXchangeContext context)
        {
            _context = context;
        }

        public List<Beer> AvailableBeers { get; set; }

        [BindProperty]
        public int SelectedBeer { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var allBeers = await _context.Beer.OrderBy(b => b.Name).ToListAsync();
            var user = User.FindFirst("name").Value;
            var credits = allBeers.Count(b => b.CreatedBy == user) - allBeers.Count(b => b.RemovedBy == user);

            if (credits <= 0)
            {
                return RedirectToPage("./Index");
            }

            AvailableBeers = allBeers.Where(b => b.RemovedBy == null).ToList();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var beer = _context.Beer.Single(b => b.Id == SelectedBeer);
            if (beer == null)
            {
                return Page();
            }

            var user = User.FindFirst("name").Value;
            beer.RemovedDate = DateTime.Now;
            beer.RemovedBy = user;
            beer.SwitchedFor = await _context.Beer.FirstAsync(b => b.CreatedBy == user && b.RemovedDate == null);

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}