using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WebAppTaxi2026.Models;

namespace WebAppTaxi2026.ViewModels
{
    public class TaxServiceCreateViewModel
    {
        [Required]
        [Display(Name ="Дата и час")]
        public DateTime HireDateTime { get; set; }
        [Required]
        [Display(Name ="Престой в минути")]
        public DateTime DownTime { get; set; }
        [Required]
        [Display(Name ="Изминати километри")]
        public double TraveledKm { get; set; } = 0;
        [Required]
        public int CarId {  get; set; }
        //Empty Collection for DropDown menu
        public ICollection<SelectListItem> Cars { get; set; } =new List<SelectListItem>();

    }
}
