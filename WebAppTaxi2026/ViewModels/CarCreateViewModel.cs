using System.ComponentModel.DataAnnotations;
using static WebAppTaxi2026.Comman.ValidationProperties;
namespace WebAppTaxi2026.ViewModels
{
    public class CarCreateViewModel
    {
        //Add id for model
        public int Id { get; set; }
        [Required]
        [Display(Name ="Марка")]
        [MinLength(CarBrandMinLength)]
        public string Brand { get; set; } = null!;
        [Required]
        [MinLength(CarRegNumberMinLength)]
        [Display(Name ="Регистрационен номер")]
        public string RegNumber { get; set; } = null!;
        [Required]
        [Range(CarYearMinValue,CarYearMaxValue)]
        [Display(Name ="Година на производство")]
        public int Year { get; set; }
        [Display(Name ="Брой места")]
        [Required]
        [Range(CarPlacesMinValue, CarPlacesMaxValue)]
        public int Places { get; set; }
        [Required]
        [Range(CarInitialFeeMinValue, CarInitialFeeMaxValue)]
        [Display(Name ="Начална такса")]
        public double InitialFee { get; set; }
        [Required]
        [Display(Name ="Цена за километър")]
        [Range(CarPricePerKmMinValue, CarPricePerKmMaxValue)]
        public double PricePerKm { get; set; }
        [Required]
        [Display(Name = "Цена за престой(минута)")]
        [Range(CarPricePerMinuteMinValue, CarPricePerMinuteMaxValue)]
        public double PricePerMinute { get; set; }
        
    }
}
