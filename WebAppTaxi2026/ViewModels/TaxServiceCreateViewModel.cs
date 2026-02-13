using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

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

    // Само за All() и Details()
    public string? CarBrand { get; set; }
    public string? CarRegNumber { get; set; }

    public ICollection<SelectListItem> Cars { get; set; }
        = new List<SelectListItem>();
}
