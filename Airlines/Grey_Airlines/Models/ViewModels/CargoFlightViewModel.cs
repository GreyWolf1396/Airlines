using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Grey_Airlines.Models
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