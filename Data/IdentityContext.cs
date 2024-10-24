using ExamForms.Models.Accounts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace ExamForms.Data;

public class IdentityContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
{
    public IdentityContext(DbContextOptions<IdentityContext> options)
        : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ApplicationRole>().HasData(
            new ApplicationRole { Id = "90df4184-8e50-41c6-a720-283922d6cdae", Name = "Admin", NormalizedName = "ADMIN" },
            new ApplicationRole { Id = "fd9b69ed-8f72-48c2-808a-07fb26619962", Name = "User", NormalizedName = "USER" },
            new ApplicationRole { Id = "6a7babe2-e03f-45da-8eb9-96a9f6d82f2d", Name = "Anynomous", NormalizedName = "ANYNOMOUS" }
        );
    }
}
