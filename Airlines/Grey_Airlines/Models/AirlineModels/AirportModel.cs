using System.ComponentModel.DataAnnotations;
using Contracts.Enums;

namespace Grey_Airlines.Models.AirlineModels
{
    public class AirportModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter a title for airport")]
        [Display(Name = "Title")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter a city where airport is")]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required(ErrorMessage = "Enter an ICAO codename for airport")]
        [Display(Name = "ICAO Code")]
        public string CodeName { get; set; }

        [Required(ErrorMessage = "Enter an airport class for airport")]
        [Display(Name = "Airport Class")]
        public AirportClass Class { get; set; }
    }
}