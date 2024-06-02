using LoginMVC_Database.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LoginMVC_Database.Data;

public class LoginMVC_DbContext : IdentityDbContext<LoginMVC_DatabaseUser>
{
    public LoginMVC_DbContext(DbContextOptions<LoginMVC_DbContext> options)
        : base(options)
    {
    }

    public DbSet<FakePersonData> FakePeople { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
