#nullable disable
using AzuriteSample.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AzuriteSample.Pages.Workflows
{
    public class EditModel : PageModel
    {
        private readonly Data.AzuriteSampleContext _context;

        public EditModel(Data.AzuriteSampleContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Workflow).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkflowExists(Workflow.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool WorkflowExists(int id)
        {
            return _context.Workflow.Any(e => e.Id == id);
        }
    }
}
