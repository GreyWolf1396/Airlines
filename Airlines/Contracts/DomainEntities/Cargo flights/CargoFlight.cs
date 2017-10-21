using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Contracts.DomainEntities.Airlines;
using Contracts.DomainEntities.Crews;
using Contracts.Enums;

namespace Contracts.DomainEntities.Cargo_flights
{
    public class CargoFlight
    {
        public int Id { get; set; }
        
        public virtual Airline Airline { get; set; }

        public virtual CargoPlane Plane { get; set; }

        public virtual Crew Crew { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public FlightStatus Status { get; set; }

        public double WeightLeft { get; set; }

        public virtual ICollection<CargoTicket> Tickets { get; set; }

        public bool CrewError { get; set; }
    }
}
