#nullable disable
using AzuriteSample.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AzuriteSample.Pages.Workflows
{
    public class DetailsModel : PageModel
    {
        private readonly Data.AzuriteSampleContext _context;

        public DetailsModel(Data.AzuriteSampleContext context)
        {
            _context = context;
        }

        public Workflow Workflow { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Workflow = await _context.Workflow.FirstOrDefaultAsync(m => m.Id == id);

            if (Workflow == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
