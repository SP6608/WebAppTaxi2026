using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAppTaxi2026.Models;

namespace WebAppTaxi2026.Configuration
{
    public class TaxsConfiguration : IEntityTypeConfiguration<TaxService>
    {
        private readonly ICollection<TaxService> TaxServices=new List<TaxService>() 
        {

        new TaxService { Id = 1,  CarId = 1, HireDateTime = new DateTime(2023, 5, 1,  8, 15, 0), TraveledKm = 6.4,  DownTime = 5  },
        new TaxService { Id = 2,  CarId = 2, HireDateTime = new DateTime(2023, 5, 1,  9, 05, 0), TraveledKm = 12.2, DownTime = 12 },
        new TaxService { Id = 3,  CarId = 3, HireDateTime = new DateTime(2023, 5, 1, 10, 40, 0), TraveledKm = 4.8,  DownTime = 3  },
        new TaxService { Id = 4,  CarId = 4, HireDateTime = new DateTime(2023, 5, 1, 12, 10, 0), TraveledKm = 18.6, DownTime = 15 },
        };
        public void Configure(EntityTypeBuilder<TaxService> builder)
        {
            builder.HasData(TaxServices);
        }
    }
}
