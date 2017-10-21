using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Contracts.Enums;

namespace Grey_Airlines.Models
{
    public class CargoPlaneModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Select a plne model")]
        [Display(Name = "Plane model")]
        public int TypeId { get; set; }

        [Display(Name = "Plane model")]
        public CargoPlaneTypeModel Type { get; set; }

        [Required(ErrorMessage = "Select a home airport for this plane")]
        [Display(Name = "Airport home")]
        public string HomeAirport { get; set; }

        [Display(Name = "Status of plane")]
        public PlaneStatus Status{ get; set; }
    }
}