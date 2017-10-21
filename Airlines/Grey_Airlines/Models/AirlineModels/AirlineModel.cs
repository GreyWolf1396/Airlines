using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Contracts.Enums;

namespace Grey_Airlines.Models
{
    public class AirlineModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter a title for airline")]
        [Display(Name = "Airline Title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Enter a duration of flight")]
        [Display(Name = "Duration(hours)")]
        public int HoursTaken { get; set; }

        
        [Display(Name = "Periodicity")]
        public AirlinePeriodicity Periodicity { get; set; }


        [Display(Name = "Cost of a ticket")]
        public double BaseTicketValue { get; set; }

        public ICollection<RouteNodeModel> RouteNodes { get; set; }

        [Required(ErrorMessage = "Enter a departure time")]
        [Display(Name = "Time of departure")]
        public string Departure { get; set; }

        [Required(ErrorMessage = "Enter an arriving time")]
        [Display(Name = "Time of arriving")]
        public string Arriving { get; set; }
    }
}