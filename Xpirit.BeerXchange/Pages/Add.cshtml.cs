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
	public class AddModel : PageModel
    {
        private readonly BeerXchangeContext _context;

        public AddModel(BeerXchangeContext context)
        {
            _context = context;
        }

        public List<Beer> SwitchedForBeers { get; set; }

		public List<string> AllUsers { get; set; }

		public async Task<IActionResult> OnGet()
        {
			var user = User.FindFirst("name").Value;
			SwitchedForBeers = await _context.Beer.Where(b => b.RemovedBy == null).ToListAsync();

			var users = await _context.Beer.Select(b => b.CreatedBy).ToListAsync();
			users.Add(user);
			AllUsers = users.Distinct().OrderBy(u => u).ToList();

			CreditTo = user;

            return Page();
        }

        [BindProperty]
        public Beer Beer { get; set; }

		[BindProperty]
		public string CreditTo { get; set; }

		public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Beer.SwitchedForId != null && Beer.SwitchedForId != -1)
            {
                var switchedBeer = _context.Beer.Single(b => b.Id == Beer.SwitchedForId);

                switchedBeer.RemovedBy = CreditTo;
                switchedBeer.RemovedDate = DateTime.Now;
            }
            else
            {
                Beer.SwitchedFor = null;
                Beer.SwitchedForId = null;
            }

            Beer.AddedDate = DateTime.Now;
            Beer.CreatedBy = CreditTo;
            _context.Beer.Add(Beer);

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}