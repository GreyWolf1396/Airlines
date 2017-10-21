using Contracts.DomainEntities.Airlines;
using Contracts.Enums;

namespace Contracts.DomainEntities.Cargo_flights
{
    public class CargoPlane
    {
        public int Id { get; set; }

        public virtual CargoPlaneType Type { get; set; }

        public virtual Airport HomeAirport { get; set; }

        public PlaneStatus Status { get; set; }
    }
}
