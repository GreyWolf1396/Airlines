using System.Collections.Generic;
using Contracts.DomainEntities.Cargo_flights;
using Contracts.DomainEntities.Crews;
using Contracts.DomainEntities.Passenger_flights;
using Contracts.Enums;

namespace Contracts.DomainEntities.Airlines
{
    public class Airport
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string City { get; set; }

        public AirportClass Class { get; set; }

        public string Codename { get; set; }

        public virtual ICollection<CargoPlane> CargoPlanes { get; set; }

        public virtual ICollection<PassengerPlane> PassengerPlanes { get; set; }

        public virtual ICollection<Crew> Crews { get; set; }

        public virtual ICollection<RouteNode> Routes { get; set; }
    }
}
