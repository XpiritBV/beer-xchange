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
    public class AddModel : PageModel
    {
        private readonly Xpirit.BeerXchange.BeerXchangeContext _context;

        public AddModel(Xpirit.BeerXchange.BeerXchangeContext context)
        {
            _context = context;
        }

        public List<Beer> SwitchedForBeers { get; set; }

        public async Task<IActionResult> OnGet()
        {
            SwitchedForBeers = await _context.Beer.Where(b => b.RemovedBy == null).ToListAsync();
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