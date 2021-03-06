﻿using System.ComponentModel.DataAnnotations;

namespace Grey_Airlines.Models.CrewModels
{
    public class CrewEmployeeModel
    {
        public int CrewId { get; set; }

        [Required(ErrorMessage = "Select an employee")]
        [Display(Name="Employee")]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Select a role")]
        [Display(Name = "Crew role")]
        public int CrewRoleId { get; set; }
    }
}