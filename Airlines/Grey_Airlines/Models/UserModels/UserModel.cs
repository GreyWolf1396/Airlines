using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Grey_Airlines.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Login")]
        public string Login { get; set; }

        [Display(Name = "Chief administrator")]
        public int? ChiefAdminId { get; set; }
        [Display(Name = "Chief administrator")]
        public string ChiefAdmin { get; set; }

        [Display(Name = "Role")]
        public int? RoleId { get; set; }
        [Display(Name = "Role")]
        public string Role { get; set; }
    }
}