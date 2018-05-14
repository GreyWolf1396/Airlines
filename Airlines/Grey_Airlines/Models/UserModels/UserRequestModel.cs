using System;
using System.ComponentModel.DataAnnotations;
using Contracts.Enums;

namespace Grey_Airlines.Models.UserModels
{
    public class UserRequestModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter a title of the request")]
        public string Title { get; set; }

        [Display(Name = "Created")]
        public DateTime CreateTime { get; set; }

        [Required(ErrorMessage = "Enter a message for administrator")]
        public string Text { get; set; }

        public RequestStatus Status { get; set; }

        public string Creator { get; set; }

        public string AssignedTo { get; set; }

        [Display(Name = "Last modified")]
        public DateTime LastModified { get; set; }
    }
}