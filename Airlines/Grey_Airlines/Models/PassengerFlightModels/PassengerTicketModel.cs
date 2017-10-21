using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Contracts.Enums;

namespace Grey_Airlines.Models
{
    public class PassengerTicketModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Flight")]
        public int FlightId { get; set; }

        [Required(ErrorMessage = "Enter a passenger name")]
        [Display(Name = "Passenger")]
        public string PassengerName { get; set; }

        [Required(ErrorMessage = "Select a ticket class")]
        [Display(Name = "Ticket class")]
        public PassengerTicketClass Class { get; set; }

        [Required(ErrorMessage = "Select an airport of departure")]
        [Display(Name = "Airport of departure")]
        public int StartPoint { get; set; }

        [Required(ErrorMessage = "Select an airport of destination")]
        [Display(Name = "Airport of destination")]
        public int EndPoint { get; set; }
    }
}