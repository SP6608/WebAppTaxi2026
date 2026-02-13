using System.ComponentModel.DataAnnotations;

namespace WebAppTaxi2026.ViewModels
{
    public class CarDeleteViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Марка")]
        public string Brand { get; set; } = null!;

        [Display(Name = "Рег. No")]
        public string RegNumber { get; set; } = null!;

        [Display(Name = "Година")]
        public int Year { get; set; }
    }
}
