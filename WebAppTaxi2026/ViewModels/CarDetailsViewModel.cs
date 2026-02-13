namespace WebAppTaxi2026.ViewModels
{
    public class CarDetailsViewModel
    {
        public int Id { get; set; }

        public string Brand { get; set; } = null!;
        public string RegNumber { get; set; } = null!;
        public int Year { get; set; }
        public int Places { get; set; }

        public double InitialFee { get; set; }
        public double PricePerKm { get; set; }
        public double PricePerMinute { get; set; }
    }
}
