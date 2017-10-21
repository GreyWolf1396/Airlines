
using Contracts.Enums;

namespace Contracts.DomainEntities.Passenger_flights
{
    public class PassengerPlaneType
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Vendor { get; set; }

        public int EnginesCount { get; set; }

        public int EconomyClassPlaces { get; set; }

        public int BusinesClassPlaces { get; set; }

        public int FirstClassPlaces { get; set; }

        public int MinimalCrew { get; set; }

        public AirportClass RequiredAirportClass { get; set; }
    }
}
