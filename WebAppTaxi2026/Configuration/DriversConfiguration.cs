using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAppTaxi2026.Models;

namespace WebAppTaxi2026.Configuration
{
    public class DriversConfiguration : IEntityTypeConfiguration<Driver>
    {
        private readonly ICollection<Driver> Drivers = new List<Driver>()
        {
             new Driver
        {
            Id = 1,
            FirstName = "Ivan",
            LastName = "Petrov",
            IDCard = "BG123456",
            Address = "Sofia, Bulgaria",
            GSM = "+359888123456",
            UserId = "driver-user-1"
        },
        new Driver
        {
            Id = 2,
            FirstName = "Georgi",
            LastName = "Ivanov",
            IDCard = "BG654321",
            Address = "Plovdiv, Bulgaria",
            GSM = "+359888654321",
            UserId = "driver-user-2"
        }
        };
        public void Configure(EntityTypeBuilder<Driver> builder)
        {
            builder.HasData(Drivers);
        }
    }
}
