using System;
using System.ComponentModel.DataAnnotations;
using Contracts.Enums;

namespace Grey_Airlines.Models.CargoFlightModels
{
    public class CargoFlightModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Select an airline")]
        [Display(Name = "Airline")]
        public string Airline { get; set; }

        [Required(ErrorMessage = "Select a plane")]
        [Display(Name = "Plane")]
        public string Plane { get; set; }

        [Required(ErrorMessage = "Select a crew")]
        [Display(Name = "Crew codename")]
        public string Crew { get; set; }

        [Required(ErrorMessage = "Enter a date of departure")]
        [Display(Name = "Date of departure")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Enter a date of arriving")]
        [Display(Name = "Date of arriving")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Status")]
        public FlightStatus Status { get; set; }

        [Display(Name = "Free weight left")]
        public int WeightLeft { get; set; }

        public bool CrewError { get; set; }
    }
}