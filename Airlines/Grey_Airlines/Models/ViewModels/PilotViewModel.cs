using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Grey_Airlines.Models.ViewModels
{
    public class PilotViewModel
    {
        public PilotModel Pilot { get; set; }

        public Dictionary<string,int> CargoExperience { get; set; }

        public Dictionary<string,int> PassengerExperience { get; set; }
    }
}