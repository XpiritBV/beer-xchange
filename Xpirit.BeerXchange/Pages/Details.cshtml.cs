using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Xpirit.BeerXchange.Model;

namespace Xpirit.BeerXchange
{
	public class DetailsModel : PageModel
    {
        private readonly BeerXchangeContext _context;

        public DetailsModel(BeerXchangeContext context)
        {
            _context = context;
        }

        public Beer Beer { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Beer = await _context.Beer.FirstOrDefaultAsync(m => m.Id == id);

            if (Beer == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
