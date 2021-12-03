#nullable disable
using Azure.Storage.Blobs;
using AzuriteSample.Data;
using AzuriteSample.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AzuriteSample.Pages.Workflows
{
    public class IndexModel : PageModel
    {
        private readonly AzuriteSampleContext _context;
        private readonly BlobContext _blobContext;

        public IndexModel(AzuriteSampleContext context, BlobServiceClient blobServiceClient)
        {
            _context = context;
            _blobContext = new BlobContext(blobServiceClient);
        }

        public IList<Workflow> Workflow { get;set; }

        public IList<Workflow> UnAssignedWorkflow { get; set; }

        public async Task OnGetAsync()
        {
            Workflow = await _context.Workflow.ToListAsync();
        }

        public async Task<ActionResult> OnGetDownloadFileAsync(int id)
        {
            var selectedWorkflow = _context.Workflow.FirstOrDefault(workflowItem => id == workflowItem.Id);
            if (selectedWorkflow == null)
            {
                return NotFound();
            }

            var fileByes = await _blobContext.DownloadBlobAsync(selectedWorkflow.FileName);
            if (fileByes == null)
            {
                return NotFound();
            }

            return File(fileByes, "application/octet-stream", selectedWorkflow.FileName);
        }
    }
}
