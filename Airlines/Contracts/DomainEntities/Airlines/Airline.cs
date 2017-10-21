using System;
using System.Collections.Generic;
using Contracts.Enums;

namespace Contracts.DomainEntities.Airlines
{
    public class Airline
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int HoursTaken { get; set; }

        public AirlinePeriodicity Periodicity { get; set; }

        public double BaseTicketValue { get; set; }

        public virtual ICollection<RouteNode> Nodes { get; set; }

        public string Departure { get; set; }

        public string Arriving { get; set; }
    }
}
