using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using LoginMVC_Database.Data;
using LoginMVC_Database.Areas.Identity.Data;
using LoginMVC_Database.Models;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("LoginMVC_DbContextConnection") ?? throw new InvalidOperationException("Connection string 'LoginMVC_DbContextConnection' not found.");

builder.Services.AddDbContext<LoginMVC_DbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<LoginMVC_DatabaseUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<LoginMVC_DbContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

using (var scope = app.Services.CreateScope())
{
    var roleMnager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>(); //get required service

    var roles = new[] { "Admin", "User" };

    foreach(var role in roles)
    {
        if (!await roleMnager.RoleExistsAsync(role))//Check if role exists in DB
            await roleMnager.CreateAsync(new IdentityRole(role));
    }

}

using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<LoginMVC_DatabaseUser>>(); //get required service

    string email = "admin@admin.com";
    string password = "Admin123!";

    if (await userManager.FindByNameAsync(email) == null)
    {
        var user = new LoginMVC_DatabaseUser();

        user.UserName = email;
        user.Email = email;
        user.FirstName = "Admin";
        user.LastName = "Admin";


        await userManager.CreateAsync(user,password);

        await userManager.AddToRoleAsync(user, "Admin");
    }
}

app.Run();
