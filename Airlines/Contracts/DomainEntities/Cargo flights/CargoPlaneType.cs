
using Contracts.Enums;

namespace Contracts.DomainEntities.Cargo_flights
{
    public class CargoPlaneType
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Vendor { get; set; }

        public double CarryingCapacity { get; set; }

        public int EnginesCount { get; set; }

        public int MinimalCrew { get; set; }

        public AirportClass RequiredAirportClass { get; set; }
    }
}
