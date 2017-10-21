using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace Contracts.DomainEntities.Airlines
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
