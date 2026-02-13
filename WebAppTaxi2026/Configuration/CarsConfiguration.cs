using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAppTaxi2026.Models;

namespace WebAppTaxi2026.Configuration
{
    public class CarsConfiguration : IEntityTypeConfiguration<Car>
    {
        private readonly ICollection<Car>Cars= new List<Car>() 
        
        {
             new Car
    {
        Id = 1,
        Brand = "Toyota Corolla",
        RegNumber = "CB1234AB",
        Year = 2018,
        Places = 4,
        InitialFee = 2.50,
        PricePerKm = 1.20,
        PricePerMinute = 0.30,
        DriverId = 1
    },
    new Car
    {
        Id = 2,
        Brand = "Skoda Octavia",
        RegNumber = "CB5678CD",
        Year = 2020,
        Places = 4,
        InitialFee = 3.00,
        PricePerKm = 1.40,
        PricePerMinute = 0.35,
        DriverId = 1
    },

    // Cars for Driver 2
    new Car
    {
        Id = 3,
        Brand = "Volkswagen Passat",
        RegNumber = "PB1111EF",
        Year = 2017,
        Places = 4,
        InitialFee = 2.80,
        PricePerKm = 1.25,
        PricePerMinute = 0.32,
        DriverId = 2
    },
    new Car
    {
        Id = 4,
        Brand = "Dacia Logan",
        RegNumber = "PB2222GH",
        Year = 2019,
        Places = 4,
        InitialFee = 2.00,
        PricePerKm = 1.10,
        PricePerMinute = 0.28,
        DriverId = 2
    }
        };
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.HasData(Cars);
        }
    }
}
