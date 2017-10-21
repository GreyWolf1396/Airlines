using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Grey_Airlines.Models
{
    public class CrewPilotModel
    {
        public int CrewId { get; set; }

        [Required(ErrorMessage = "Select a pilot")]
        [Display(Name = "Pilot")]
        public int PilotId { get; set; }

        [Required(ErrorMessage = "Select a role")]
        [Display(Name = "Crew role")]
        public int CrewRoleId { get; set; }
    }
}