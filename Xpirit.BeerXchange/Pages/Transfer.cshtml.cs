using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Xpirit.BeerXchange.Model;

namespace Xpirit.BeerXchange
{
	public class TransferModel : PageModel
	{
		private readonly BeerXchangeContext _context;

		public List<Beer> MyBeers { get; set; }

		public List<string> AllOtherUsers { get; set; }

		[BindProperty]
		[Required(ErrorMessage = "Please select a beer")]
		public int SelectedBeer { get; set; }

		[BindProperty]
		[Required(ErrorMessage = "Please select a user")]
		public string SelectedUser { get; set; }

		public TransferModel(BeerXchangeContext context)
		{
			_context = context;
		}

		public async Task<IActionResult> OnGet()
		{
			var user = User.FindFirst("name").Value;
			var allBeers = await _context.Beer.OrderBy(b => b.Name).ToListAsync();

			var credits = allBeers.Count(b => b.CreatedBy == user) - allBeers.Count(b => b.RemovedBy == user);
			if (credits <= 0)
			{
				return RedirectToPage("./Index");
			}

			MyBeers = allBeers.Where(b => b.CreatedBy == user && b.RemovedBy == null).ToList();
			AllOtherUsers = allBeers.Select(b => b.CreatedBy)
				.Except(new[] { user })
				.Distinct()
				.OrderBy(s => s)
				.ToList();

			if (AllOtherUsers.Count == 0)
			{
				return RedirectToPage("./Index");
			}

			SelectedBeer = MyBeers.First().Id;
			SelectedUser = AllOtherUsers.First();

			return Page();
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			var beer = await _context.Beer.FindAsync(SelectedBeer);
			beer.CreatedBy = SelectedUser;
			await _context.SaveChangesAsync();

			return RedirectToPage("./Index");
		}
	}
}
