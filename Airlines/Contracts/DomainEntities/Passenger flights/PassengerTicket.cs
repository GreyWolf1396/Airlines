using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Contracts.Enums;

namespace Contracts.DomainEntities.Passenger_flights
{
    public class PassengerTicket
    {
        [Key]
        public int Id { get; set; }

        public string PassengerName { get; set; }

        public PassengerTicketClass Class { get; set; }

        public virtual PassengerFlight Flight { get; set; }

        public int StartPoint { get; set; }

        public int EndPoint { get; set; }
    }
}
