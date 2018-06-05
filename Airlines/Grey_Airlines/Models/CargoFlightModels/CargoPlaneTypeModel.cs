using System.ComponentModel.DataAnnotations;
using Contracts.Enums;

namespace Grey_Airlines.Models.CargoFlightModels
{
    public class CargoPlaneTypeModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter a title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Enter a vendor")]
        public string Vendor { get; set; }

        [Required(ErrorMessage = "Enter a carrying capacity of the plane")]
        [Display(Name = "Maximum weight of cargo")]
        public int CarryingCapacity { get; set; }

        
        [Display(Name = "Count of engines")]
        public int EnginesCount { get; set; }

        [Required(ErrorMessage = "Enter a minimal crew count")]
        [Display(Name = "Minimal count of crew")]
        public int MinimalCrew { get; set; }

        [Display(Name = "Required airport class ")]
        public AirportClass RequiredAirportClass { get; set; }
    }
}