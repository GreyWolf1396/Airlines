using System.Collections.Generic;
using Grey_Airlines.Models.AirlineModels;
using Grey_Airlines.Models.CargoFlightModels;

namespace Grey_Airlines.Models.ViewModels
{
    public class AirlineViewModel
    {
        public AirlineModel Airline { get; set; }

        public ICollection<RouteNodeModel> RouteNodes { get; set; }

        public ICollection<PassengerFlightModel> PassengerFlihts { get; set; }

        public ICollection<CargoFlightModel> CargoFlights { get; set; }
    }
}