using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Grey_Airlines.Models
{
    public class CargoTicketModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Flight")]
        public int FlightId { get; set; }

        [Required(ErrorMessage = "Enter a title for cargo item")]
        [Display(Name = "Cargo title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Enter a weight for cargo item")]
        [Display(Name = "Weight")]
        public double Weight { get; set; }

        [Required(ErrorMessage = "Select an airport of departure")]
        [Display(Name = "Airport of departure")]
        public int StartPoint { get; set; }

        [Required(ErrorMessage = "Select an airport of destination")]
        [Display(Name = "Airport of destination")]
        public int EndPoint { get; set; }
    }
}