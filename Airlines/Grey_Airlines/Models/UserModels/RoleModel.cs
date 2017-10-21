using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Grey_Airlines.Models
{
    public class RoleModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int AccessLevel { get; set; }
    }
}