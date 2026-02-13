using System.ComponentModel.DataAnnotations;

namespace WebAppTaxi2026.ViewModels
{
    public class TaxServiceDetailsViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Дата и час")]
        public DateTime HireDateTime { get; set; }

        [Display(Name = "Престой (минути)")]
        public int DownTime { get; set; }

        [Display(Name = "Изминати километри")]
        public double TraveledKm { get; set; }

        [Display(Name = "Автомобил")]
        public string CarInfo { get; set; } = null!;
    }
}
