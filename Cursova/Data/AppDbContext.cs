using Cursova.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Cursova.Data
{

    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Statements> Statements { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<AppUser>().Ignore(c => c.NormalizedUserName)
            //                              .Ignore(c => c.EmailConfirmed)
            //                              .Ignore(c => c.PhoneNumberConfirmed)
            //                              .Ignore(c => c.TwoFactorEnabled)
            //                              .Ignore(c => c.LockoutEnd)
            //                              .Ignore(c => c.LockoutEnabled)
            //                              .Ignore(c => c.AccessFailedCount);
        }
    }
}