using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Grey_Airlines.Models
{
    public class CreateUserModel
    {
        [Required(ErrorMessage = "Enter a user's name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter a login")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Select a role")]
        [Display(Name = "Role")]
        public int RoleId { get; set; }

        [Display(Name = "Chief administrator")]
        public int? ChiefId { get; set; }

        [Required(ErrorMessage = "Enter a password")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [Required(ErrorMessage = "Enter a password confirmation")]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}