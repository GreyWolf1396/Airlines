using System.ComponentModel.DataAnnotations;
using Contracts.Enums;

namespace Grey_Airlines.Models
{
    public class PassengerPlaneTypeModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter a title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Enter a vendor")]
        public string Vendor { get; set; }

        public int EnginesCount { get; set; }

        [Required(ErrorMessage = "Enter a count of places in economy class")]
        [Display(Name = "Economy class places")]
        public int EconomyClassPlaces { get; set; }

        [Required(ErrorMessage = "Enter a count of places in business class")]
        [Display(Name = "Business class places")]
        public int BusinesClassPlaces { get; set; }

        [Required(ErrorMessage = "Enter a count of places in first class")]
        [Display(Name = "First class places")]
        public int FirstClassPlaces { get; set; }

        [Required(ErrorMessage = "Enter a minimal count of crew members")]
        [Display(Name = "Minimal count of crew")]
        public int MinimalCrew { get; set; }

        [Display(Name = "Required airport class")]
        public AirportClass RequiredAirportClass { get; set; }
    }
}