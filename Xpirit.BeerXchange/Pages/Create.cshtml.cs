using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Xpirit.BeerXchange.Model;

namespace Xpirit.BeerXchange
{
    public class CreateModel : PageModel
    {
        private readonly Xpirit.BeerXchange.BeerXchangeContext _context;

        public CreateModel(Xpirit.BeerXchange.BeerXchangeContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
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

            Beer.AddedDate = DateTime.Now;

            _context.Beer.Add(Beer);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}