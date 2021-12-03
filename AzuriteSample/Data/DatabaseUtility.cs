#nullable disable
using Microsoft.EntityFrameworkCore;

namespace AzuriteSample.Data
{
    public static class DatabaseUtility
    {
        /// Please don't use this for production databases. This is only intended for development to create and pre-seed the database.
        public static async Task EnsureDbCreatedAndSeedAsync(this DbContextOptions<AzuriteSampleContext> options)
        {
            var builder = new DbContextOptionsBuilder<AzuriteSampleContext>(options);
            builder.UseLoggerFactory(new LoggerFactory());
            using var context = new AzuriteSampleContext(builder.Options);
            if (await context.Database.EnsureCreatedAsync())
            {
                // Add the code to seed the database
            }
        }
    }
}
