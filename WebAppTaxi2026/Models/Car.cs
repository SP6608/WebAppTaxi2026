using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static WebAppTaxi2026.Comman.ValidationProperties;
namespace WebAppTaxi2026.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(CarBrandMaxLength)]
        public string Brand { get; set; } = null!;
        [Required]
        [MaxLength(CarRegNumberMaxLength)]
        public string RegNumber { get; set; } = null!;
        [Required]
        public int Year { get; set; }
        [Required]
        public int Places { get; set; }
        [Required]
        public double InitialFee { get; set; }
        [Required]
        public double PricePerKm { get; set; }
        [Required]
        public double PricePerMinute { get; set; }
        //Navigation property
        [Required]
        public int DriverId { get; set; }
        [ForeignKey(nameof(DriverId))]
        public virtual Driver Driver { get; set; } = null!;
        public virtual ICollection<TaxService> TaxiServices { get; set; } = new List<TaxService>();

    }
}
