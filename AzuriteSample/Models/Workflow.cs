using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AzuriteSample.Models
{
    public class Workflow
    {
        public int Id { get; set; }

        [Display(Name = "File Name")]
        [StringLength(60, MinimumLength = 3)]
        public string? FileName { get; set; }
        
        [Display(Name = "File")]
        [NotMapped]
        public IFormFile? File { get; set; }

        [Required]
        [Display(Name = "Assigned To")]
        [StringLength(60, MinimumLength = 3)]
        public string? AssignedTo { get; set; }

        [Display(Name = "Reviewed?")]
        public bool Reviewed{ get; set; }

        [Display(Name = "File Size")]
        public long FileSize { get; set; }

        [Display(Name = "Notes")]
        [DataType(DataType.MultilineText)]
        public string? Notes { get; set; }
    }
}
