using System.Collections.Generic;

namespace Grey_Airlines.Models
{
    public class AirlineViewModel
    {
        public AirlineModel Airline { get; set; }

        public ICollection<RouteNodeModel> RouteNodes { get; set; }

        public ICollection<PassengerFlightModel> PassengerFlihts { get; set; }

        public ICollection<CargoFlightModel> CargoFlights { get; set; }
    }
}