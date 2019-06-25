using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Xpirit.BeerXchange.Model;

namespace Xpirit.BeerXchange
{
    public class TakeModel : PageModel
    {
        private readonly Xpirit.BeerXchange.BeerXchangeContext _context;

        public TakeModel(Xpirit.BeerXchange.BeerXchangeContext context)
        {
            _context = context;
        }

        public List<Beer> SwitchedForBeers { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var allBeers = await _context.Beer.ToListAsync();
            var user = User.FindFirst("name").Value;
            var credits = allBeers.Count(b => b.CreatedBy == user) - allBeers.Count(b => b.RemovedBy == user);

            if (credits <= 0)
            {
                return RedirectToPage("./Index");
            }

            SwitchedForBeers = allBeers.Where(b => b.RemovedBy != "").ToList();
            return Page();
        }

        [BindProperty]
        public Beer Beer { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Beer.SwitchedForId != null && Beer.SwitchedForId != -1)
            {
                var switchedBeer = _context.Beer.Single<Beer>(b => b.Id == Beer.SwitchedForId);

                switchedBeer.RemovedBy = User.FindFirst("name").Value;
                switchedBeer.RemovedDate = DateTime.Now;
            }
            else
            {
                Beer.SwitchedFor = null;
                Beer.SwitchedForId = null;
            }

            Beer.AddedDate = DateTime.Now;
            Beer.CreatedBy = User.FindFirst("name").Value;
            _context.Beer.Add(Beer);

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}