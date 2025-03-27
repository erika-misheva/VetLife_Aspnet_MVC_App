using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VetLife.Data;
using VetLife.Data.Services;
using VetLife.Models;
using Microsoft.Extensions.Caching.Memory;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IOwnersService, OwnersService>();
builder.Services.AddScoped<IPetsService, PetsService>();
builder.Services.AddScoped<IVaccinesService, VaccinesService>();
builder.Services.AddScoped<IDrugsService, DrugsService>();
builder.Services.AddScoped<IOperationsService, OperationsService>();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login"; 
        options.AccessDeniedPath = "/Account/AccessDenied"; 
    });

builder.Services.AddAuthorization();

builder.Services.AddMemoryCache();

var app = builder.Build();

AppDbInitializer.Seed(app);


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Pets}/{action=Index}/{id?}");

AppDbInitializer.SeedUsersAndRolesAsync(app).Wait();
app.Run();
