using System.ComponentModel.DataAnnotations;
using static WebAppTaxi2026.Comman.ValidationProperties;
namespace WebAppTaxi2026.ViewModels
{
    public class DriverViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name ="Име")]
        [MinLength(DriverFirstNameMinLength)]
        public string FirstName { get; set; } = null!;
        [Required]
        [Display(Name = "Фамилия")]
        [MinLength(DriverLastNameMinLength)]
        public string LastName { get; set; } = null!;
        [Required]
        [Display(Name ="Лична карта")]
        [MinLength(DriverIdCardMinLength)]
        public string IDCard { get; set; } = null!;
        [Required]
        [Display(Name ="Адрес")]
        [MinLength(DriverAddressMinLength)]
        public string Address { get; set; } = null!;
        [Required]
        [Display(Name = "телефон")]
        [MinLength(DriverGsmMinLength)]
        public string GSM { get; set; }= null!;
        public string? UserName { get; set; }

        public ICollection<CarListItemViewModel> Cars { get; set; } = new List<CarListItemViewModel>();
    }
}
