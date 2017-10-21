using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Grey_Airlines.Models
{
    public class CrewViewModel
    {
        public CrewModel Crew { get; set; }

        public ICollection<PilotModel> PilotsInCrew { get; set; }

        public ICollection<EmployeeModel> EmployeesInCrew { get; set; }

        public bool IsCrewFull { get; set; }
    }
}