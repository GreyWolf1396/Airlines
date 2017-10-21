using System;
using System.ComponentModel.DataAnnotations;
using Contracts.Enums;

namespace Grey_Airlines.Models
{
    public class PassengerFlightModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Select an airline")]
        public string Airline { get; set; }

        [Required(ErrorMessage = "Select a crew")]
        public string Crew { get; set; }

        [Required(ErrorMessage = "Select a plane")]
        public string Plane { get; set; }

        [Required(ErrorMessage = "Select a date of departure")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Select a date of arriving")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime EndDate { get; set; }

        public FlightStatus Status { get; set; }

        public int EconomyClassPlacesLeft { get; set; }

        public int BusinessClassPlacesLeft { get; set; }

        public int FirstClassPlacesLeft { get; set; }

        public bool CrewError { get; set; }
    }
}