namespace WebAppTaxi2026.Comman
{
    public static class ValidationProperties
    {
      //Driver

        public const int DriverFirstNameMinLength = 2;
        public const int DriverFirstNameMaxLength = 50;

        public const int DriverLastNameMinLength = 2;
        public const int DriverLastNameMaxLength = 50;

        public const int DriverIdCardMinLength = 6;
        public const int DriverIdCardMaxLength = 20;

        public const int DriverAddressMinLength = 5;
        public const int DriverAddressMaxLength = 200;

        public const int DriverGsmMinLength = 8;
        public const int DriverGsmMaxLength = 20;


      //Car

        public const int CarBrandMinLength = 2;
        public const int CarBrandMaxLength = 50;

        public const int CarRegNumberMinLength = 5;
        public const int CarRegNumberMaxLength = 8;

        public const int CarYearMinValue = 1980;
        public const int CarYearMaxValue = 2100;

        public const int CarPlacesMinValue = 1;
        public const int CarPlacesMaxValue = 8;

        public const double CarInitialFeeMinValue = 0.00;
        public const double CarInitialFeeMaxValue = 1000.00;

        public const double CarPricePerKmMinValue = 0.00;
        public const double CarPricePerKmMaxValue = 100.00;

        public const double CarPricePerMinuteMinValue = 0.00;
        public const double CarPricePerMinuteMaxValue = 50.00;


       
    }
}
