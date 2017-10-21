using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Contracts.Enums;

namespace Grey_Airlines.Models
{
    public class PassengerPlaneModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Select a plane model")]
        [Display(Name = "Plane model")]
        public int TypeId { get; set; }

        public PassengerPlaneTypeModel Type { get; set; }
        [Required(ErrorMessage = "Select a home airport")]
        [Display(Name = "Airport home")]
        public int AirportId { get; set; }

        public string HomeAirport { get; set; }

        public PlaneStatus Status { get; set; }
    }
}