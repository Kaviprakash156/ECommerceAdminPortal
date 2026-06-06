using ECommerceAdminPortal.Data;
using ECommerceAdminPortal.Filters;
using ECommerceAdminPortal.Middleware;
using ECommerceAdminPortal.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;


Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.File(
        "Logs/log-.txt",
        rollingInterval: RollingInterval.Day)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddRazorPages(options =>
{
})
.AddMvcOptions(options =>
{
    options.Filters.Add<
        ActivityLoggingPageFilter>();
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<ProductService>();

builder.Services.AddScoped<VendorService>();

builder.Services.AddScoped<InventoryService>();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout =
        TimeSpan.FromMinutes(30);
});

builder.Services.AddScoped<OrderService>();

builder.Services.AddScoped<
    ActivityLoggingPageFilter>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseSession();

app.UseRouting();

app.UseGlobalException();

app.UseRequestLogging();

app.UseAuthorization();

app.MapRazorPages();

using (var scope = app.Services.CreateScope())
{
    var context =
        scope.ServiceProvider
        .GetRequiredService<ApplicationDbContext>();

    DbInitializer.Seed(context);
}

app.Run();
