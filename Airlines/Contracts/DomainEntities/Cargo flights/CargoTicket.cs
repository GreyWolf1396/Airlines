using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contracts.DomainEntities.Cargo_flights
{
    public class CargoTicket
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public double Weight { get; set; }

        public virtual CargoFlight Flight { get; set; }

        public int StartPoint { get; set; }

        public int EndPoint { get; set; }
    }
}
