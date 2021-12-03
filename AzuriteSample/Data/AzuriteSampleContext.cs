#nullable disable
using Microsoft.EntityFrameworkCore;

namespace AzuriteSample.Data
{
    public class AzuriteSampleContext : DbContext
    {
        public AzuriteSampleContext (DbContextOptions<AzuriteSampleContext> options)
            : base(options)
        {
        }

        public DbSet<Models.Workflow> Workflow { get; set; }
    }
}
