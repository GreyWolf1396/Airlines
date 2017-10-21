using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Grey_Airlines.Models
{
    public class PassengerFlightViewModel
    {
        public PassengerFlightModel Flight { get; set; }

        public AirlineModel Airline { get; set; }

        public CrewModel Crew { get; set; }

        public PassengerPlaneModel Plane { get; set; }

        public ICollection<PassengerTicketModel> Tickets { get; set; }

        public bool IsPlaneError { get; set; }

        public bool IsCrewError { get; set; }
    }
}