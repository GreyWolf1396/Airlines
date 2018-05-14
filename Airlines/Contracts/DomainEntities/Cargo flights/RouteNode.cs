using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Contracts.DomainEntities.Airlines;

namespace Contracts.DomainEntities.Cargo_flights
{
    public class RouteNode
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public virtual Airline Airline { get; set; }

        public virtual Airport Airport { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NumberInRoute { get; set; }

        public string Arriving { get; set; }

        public string Departure { get; set; }
    }
}
