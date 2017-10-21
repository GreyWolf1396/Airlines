using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Grey_Airlines.Models
{
    public class PilotModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter a pilot's name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter a license number")]
        [Display(Name = "License number")]
        public string LicenseNumber { get; set; }

        [Required(ErrorMessage = "Enter an experience of pilot")]
        [Display(Name = "Experience (hours)")]
        public int HoursOfExperience { get; set; }

        [Required(ErrorMessage = "Enter an educational level of pilot")]
        [Display(Name = "Educational level")]
        public string Education { get; set; }

        [Display(Name = "Role in crew")]
        public string CrewRole { get; set; }
    }
}