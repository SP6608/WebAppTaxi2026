using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using WebAppTaxi2026.Models;

namespace WebAppTaxi2026.ViewModels
{
    public class TaxServiceCreateViewModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Дата и час")]
        public DateTime HireDateTime { get; set; }

        [Required]
        [Display(Name = "Престой в минути")]
        [Range(0, 10000)]
        public int DownTime { get; set; }   

        [Required]
        [Display(Name = "Изминати километри")]
        [Range(0, 10000)]
        public double TraveledKm { get; set; }

        [Required]
        [Display(Name = "Автомобил")]
        public int CarId { get; set; }
        public string CarBrand { get; set; } = null!;
        [Required]
        public string CarRegNumber { get; set; } = null!;

        public ICollection<SelectListItem> Cars { get; set; }
            = new List<SelectListItem>();
    }
}
