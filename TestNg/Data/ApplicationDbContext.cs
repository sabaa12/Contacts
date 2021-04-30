using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TestNg.Data.Models;
namespace TestNg.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public DbSet<contact> contacts { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<contact>()
                .HasOne(x => x.user)
                .WithMany(c => c.contacts)
                .HasForeignKey(v => v.userID)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
