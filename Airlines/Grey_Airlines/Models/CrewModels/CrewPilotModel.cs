using System.ComponentModel.DataAnnotations;

namespace Grey_Airlines.Models.CrewModels
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