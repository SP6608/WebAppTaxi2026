using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebAppTaxi2026.Models;

namespace WebAppTaxi2026.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Driver>()
                .HasIndex(d => d.UserId)
                .IsUnique();

            builder.Entity<Driver>()
                .HasOne(d => d.User)
                .WithOne()
                .HasForeignKey<Driver>(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            //var hasher = new PasswordHasher<IdentityUser>();

            var user1 = new IdentityUser
            {
                Id = "driver-user-1",
                UserName = "driver1@taxi.com",
                NormalizedUserName = "DRIVER1@TAXI.COM",
                Email = "driver1@taxi.com",
                NormalizedEmail = "DRIVER1@TAXI.COM",
                EmailConfirmed = true,
                SecurityStamp = "seed-driver-1"
            };
            //user1.PasswordHash = hasher.HashPassword(user1, "Driver123!");

            var user2 = new IdentityUser
            {
                Id = "driver-user-2",
                UserName = "driver2@taxi.com",
                NormalizedUserName = "DRIVER2@TAXI.COM",
                Email = "driver2@taxi.com",
                NormalizedEmail = "DRIVER2@TAXI.COM",
                EmailConfirmed = true,
                SecurityStamp = "seed-driver-2"
            };
           // user2.PasswordHash = hasher.HashPassword(user2, "Driver123!");

            //builder.Entity<IdentityUser>().HasData(user1, user2);

            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }

        public virtual DbSet<Driver> Drivers { get; set; } = null!;
        public virtual DbSet<Car> Cars { get; set; } = null!;
        public virtual DbSet<TaxService> TaxServices { get; set; } = null!;
    }
}
