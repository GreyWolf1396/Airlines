using System.ComponentModel.DataAnnotations;

namespace Grey_Airlines.Models.UserModels
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