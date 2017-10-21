using Contracts.DomainEntities.Airlines;
using Contracts.Enums;

namespace Contracts.DomainEntities.Passenger_flights
{
    public class PassengerPlane
    {
        public int Id { get; set; }

        public virtual PassengerPlaneType Type { get; set; }

        public virtual Airport HomeAirport { get; set; }

        public PlaneStatus Status { get; set; }
    }
}