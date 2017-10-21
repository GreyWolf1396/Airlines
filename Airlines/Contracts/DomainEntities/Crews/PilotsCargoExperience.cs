using System.ComponentModel.DataAnnotations;
using Contracts.DomainEntities.Cargo_flights;

namespace Contracts.DomainEntities.Crews
{
    public class PilotsCargoExperience
    {
        public int Id { get; set; }
        
        public virtual Pilot Pilot { get; set; }
        
        public virtual CargoPlaneType Plane { get; set; }

        public int HoursOfFlights { get; set; }
    }
}
