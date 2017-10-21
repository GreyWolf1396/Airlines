using System;
using System.Collections.Generic;
using Contracts.DomainEntities.Airlines;
using Contracts.DomainEntities.Crews;
using Contracts.Enums;

namespace Contracts.DomainEntities.Passenger_flights
{
    public class PassengerFlight
    {
        public int Id { get; set; }

        public virtual PassengerPlane Plane { get; set; }

        public virtual Airline Airline { get; set; }

        public virtual Crew Crew { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public FlightStatus Status { get; set; }

        public int EconomyClassPlacesLeft { get;set; }

        public int BusinessClassPlacesLeft { get; set; }

        public int FirstClassPlacesLeft { get; set; }

        public virtual ICollection<PassengerTicket> Tickets { get; set; }

        public bool CrewError { get; set; }
    }
}
