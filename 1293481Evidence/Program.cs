using _1293481Evidence.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<StoredProcedureDbpharmaContext>(op => op.UseSqlServer(builder.Configuration.GetConnectionString("con")));

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "InvoiceList",
    pattern: "InvoiceList",
    defaults: new { controller = "Invoices", action = "Index" });

app.MapControllerRoute(
    name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
