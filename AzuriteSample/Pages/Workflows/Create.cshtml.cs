#nullable disable
using Azure.Storage.Blobs;
using AzuriteSample.Data;
using AzuriteSample.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AzuriteSample.Pages.Workflows
{
    public class CreateModel : PageModel
    {
        private readonly AzuriteSampleContext _context;
        private readonly BlobContext _blobContext;

        public CreateModel(AzuriteSampleContext context, BlobServiceClient blobServiceClient)
        {
            _context = context;
            _blobContext = new BlobContext(blobServiceClient);
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Workflow Workflow { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (Workflow.File == null)
            {
                ModelState.AddModelError("Workflow.File", "File must be provided");
                return Page();
            }

            // Get the file name and size from the uploaded file.
            Workflow.FileName = Workflow.File.FileName;
            Workflow.FileSize = Workflow.File.Length;

            if (!ModelState.IsValid)
            {
                return Page();
            }

            // if the file name is already available in the container, don't allow to upload another.
            var existingBlobs = await _blobContext.ListBlobsAsync();
            if (existingBlobs.Contains(Workflow.FileName))
            {
                ModelState.AddModelError("Workflow.File", "File is already present in the storage.");
                return Page();
            }

            _context.Workflow.Add(Workflow);
            await _context.SaveChangesAsync();
            using (MemoryStream stream = new MemoryStream())
            {
                await Workflow.File.CopyToAsync(stream);
                await _blobContext.UploadBlobAsync(Workflow.FileName, stream.ToArray());
            }

            return RedirectToPage("./Index");
        }
    }
}
