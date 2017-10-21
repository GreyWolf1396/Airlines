using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Contracts.Enums;

namespace Grey_Airlines.Models
{
    public class EmployeeModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter a name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Select an education category")]
        [Display(Name = "Education category")]
        public EmployeeCategory EducationCategory { get; set; }

        [Display(Name = "Role in crew")]
        public string CrewRole { get; set; }

        [Required(ErrorMessage = "Enter an education level")]
        public string Education { get; set; }
    }
}