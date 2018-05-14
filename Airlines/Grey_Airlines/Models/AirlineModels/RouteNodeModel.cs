using System.ComponentModel.DataAnnotations;

namespace Grey_Airlines.Models.AirlineModels
{
    public class RouteNodeModel
    {
        public int Id { get; set; }

        [Display(Name = "Airline")]
        public int AirlineId { get; set; }
        [Display(Name = "Airline")]
        public string Airline { get; set; }

        [Required(ErrorMessage = "Select an airport")]
        [Display(Name = "Airport")]
        public int AirportId { get; set; }
        [Display(Name = "Airport")]
        public string Airport { get; set; }

        [Required(ErrorMessage = "Enter a number of airport in the route")]
        [Display(Name = "Number in route")]
        public int NumberInRoute { get; set; }

        [Display(Name = "Arriving")]
        public string Arriving { get; set; }

        [Display(Name = "Departure")]
        public string Departure { get; set; }
    }
}