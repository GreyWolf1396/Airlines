using System.ComponentModel.DataAnnotations;
using Contracts.DomainEntities.Passenger_flights;

namespace Contracts.DomainEntities.Crews
{
    public class PilotsPassengerExperience
    {
        public int Id { get; set; }
        
        public virtual Pilot Pilot { get; set; }
        
        public virtual PassengerPlaneType Plane { get; set; }

        public int HoursOfFlights { get; set; }
    }
}
