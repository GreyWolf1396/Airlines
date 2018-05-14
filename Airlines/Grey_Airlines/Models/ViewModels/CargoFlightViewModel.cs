using System.Collections.Generic;
using Grey_Airlines.Models.AirlineModels;
using Grey_Airlines.Models.CargoFlightModels;
using Grey_Airlines.Models.CrewModels;

namespace Grey_Airlines.Models.ViewModels
{
    public class CargoFlightViewModel
    {
        public CargoFlightModel Flight { get; set; }

        public AirlineModel Airline { get; set; }

        public CrewModel Crew { get; set; }

        public CargoPlaneModel Plane { get; set; }

        public ICollection<CargoTicketModel> Tickets { get; set; }

        public bool IsPlaneError { get; set; }

        public bool IsCrewError { get; set; }
    }
}