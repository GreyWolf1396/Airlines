using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Contracts.Enums;

namespace Grey_Airlines.Models
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