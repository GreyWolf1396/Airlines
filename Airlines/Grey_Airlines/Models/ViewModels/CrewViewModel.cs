using System.Collections.Generic;
using Grey_Airlines.Models.CrewModels;

namespace Grey_Airlines.Models.ViewModels
{
    public class CrewViewModel
    {
        public CrewModel Crew { get; set; }

        public ICollection<PilotModel> PilotsInCrew { get; set; }

        public ICollection<EmployeeModel> EmployeesInCrew { get; set; }

        public bool IsCrewFull { get; set; }
    }
}