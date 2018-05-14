using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Grey_Airlines.Models.CrewModels
{
    public class CrewModel
    {
        public int Id { get; set; }

        [Display(Name = "Crew codename")]
        [Required(ErrorMessage = "Enter a codename")]
        public string Title { get; set; }

        [Display(Name = "Airport home")]
        [Required(ErrorMessage = "Select a home airport")]
        public int HomeId { get; set; }

        [Display(Name = "Airport home")]
        public string Home { get; set; }

        public ICollection<PilotModel> Pilots { get; set; }

        public ICollection<EmployeeModel> Employees { get; set; }

        [Display(Name = "Count of members")]
        public int CrewCount { get; set; }
    }
}