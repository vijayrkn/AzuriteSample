using AzuriteSample.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<AzuriteSampleContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AzuriteSampleContext")));

builder.Services.AddAzureClients(clientBuilder =>
{
    clientBuilder.AddBlobServiceClient(builder.Configuration["StorageConnectionString:blob"], preferMsi: true);
    clientBuilder.AddQueueServiceClient(builder.Configuration["StorageConnectionString:queue"], preferMsi: true);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    await using var scope = app.Services?.GetService<IServiceScopeFactory>()?.CreateAsyncScope();
    var options = scope?.ServiceProvider.GetRequiredService<DbContextOptions<AzuriteSampleContext>>();
    await DatabaseUtility.EnsureDbCreatedAndSeedAsync(options);
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
