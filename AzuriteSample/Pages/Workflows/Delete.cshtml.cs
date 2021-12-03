#nullable disable
using Azure.Storage.Blobs;
using AzuriteSample.Data;
using AzuriteSample.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AzuriteSample.Pages.Workflows
{
    public class DeleteModel : PageModel
    {
        private readonly AzuriteSampleContext _context;
        private readonly BlobContext _blobContext;

        public DeleteModel(AzuriteSampleContext context, BlobServiceClient blobServiceClient)
        {
            _context = context;
            _blobContext = new BlobContext(blobServiceClient);
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Workflow = await _context.Workflow.FindAsync(id);

            if (Workflow != null)
            {
                _context.Workflow.Remove(Workflow);
                await _context.SaveChangesAsync();
                await _blobContext.DeleteBlobAsync(Workflow.FileName);
            }

            return RedirectToPage("./Index");
        }
    }
}
