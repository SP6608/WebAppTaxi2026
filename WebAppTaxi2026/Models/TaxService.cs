namespace WebAppTaxi2026.Models
{
    using Microsoft.AspNetCore.Authorization;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static WebAppTaxi2026.Comman.ValidationProperties;
    [Authorize]
    public class TaxService
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public DateTime HireDateTime { get; set; }
        [Required]
       
        public int DownTime { get; set; }
        [Required]
       
        public double TraveledKm { get; set; }
        //Navigation property
        [Required]
        public int CarId { get; set; }
        [ForeignKey(nameof(CarId))]
        public Car Car { get; set; } = null!;

    }
}
