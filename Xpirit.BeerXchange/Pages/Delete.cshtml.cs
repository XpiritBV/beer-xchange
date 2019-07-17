using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Xpirit.BeerXchange.Model;

namespace Xpirit.BeerXchange
{
	public class DeleteModel : PageModel
    {
        private readonly BeerXchangeContext _context;

        public DeleteModel(BeerXchangeContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Beer = await _context.Beer.FindAsync(id);

            if (Beer != null)
            {
                _context.Beer.Remove(Beer);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
