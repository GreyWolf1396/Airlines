using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Grey_Airlines.Models
{
    public class AccountModel
    {
        [Required(ErrorMessage = "Enter a login")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Enter a password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}