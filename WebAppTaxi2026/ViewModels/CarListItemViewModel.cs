namespace WebAppTaxi2026.ViewModels
{
    public class CarListItemViewModel
    {
        public int Id { get; set; }
        public string Brand { get; set; } = null!;
        public string RegNumber { get; set; } = null!;
        public int Year { get; set; }
        public int Places { get; set; }
    }
}
