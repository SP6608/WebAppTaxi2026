using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static WebAppTaxi2026.Comman.ValidationProperties;
namespace WebAppTaxi2026.Models
{
    public class Driver
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(DriverFirstNameMaxLength)]
        public string FirstName { get; set; } = null!;
        [Required]
        [MaxLength(DriverLastNameMaxLength)]
        public string LastName { get; set; } = null!;
        [Required]
        [MaxLength(DriverIdCardMaxLength)]
        public string IDCard { get; set; } = null!;
        [Required]
        [MaxLength(DriverAddressMaxLength)]
        public string Address { get; set; } = null!;
        [Required]
        [MaxLength(DriverGsmMaxLength)]
        [Phone]
        public string GSM { get; set; } = null!;
        [Required]
        [MaxLength(450)]
        public string UserId { get; set; } = null!;
        [ForeignKey(nameof(UserId))]
        public IdentityUser User { get; set; } = null!;
        //Navigation property
        public ICollection<Car> Cars { get; set; } = new List<Car>();

    }
}
