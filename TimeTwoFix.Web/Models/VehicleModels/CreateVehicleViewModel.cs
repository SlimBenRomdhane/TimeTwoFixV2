using System.ComponentModel.DataAnnotations;

namespace TimeTwoFix.Web.Models.VehicleModels
{
    public class CreateVehicleViewModel
    {
        public int ClientId { get; set; }

        [Required]
        [StringLength(17, MinimumLength = 17)]
        [RegularExpression(@"^[A-HJ-NPR-Z0-9]{17}$", ErrorMessage = "VIN format is not valid.")]
        public string Vin { get; set; }

        public string Brand { get; set; }
        public string Model { get; set; }

        [RegularExpression(
    @"^(\d{4}\sTunis\s\d{3})$|^(\d{2}\s-\s\d{6})$|^([A-Z]{2,3}\s\d{4,6})$|^(NT\s\d{4,6})$",
            ErrorMessage = "Accepted formats: 123 Tunis 4567, 03 - 123456, CD 12345, NT 123456")]
        public string LicensePlate { get; set; }

        public string FuelType { get; set; }
        public string TransmissionType { get; set; }

        [Range(1950, int.MaxValue)]
        public int Year { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Only positive numbers allowed")]
        public int Mileage { get; set; }

        public string? Notes { get; set; }
    }
}