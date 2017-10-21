
using System.ComponentModel.DataAnnotations;

namespace Contracts.DomainEntities.Crews
{
    public class PilotInCrew
    {
        public int Id { get; set; }
        public virtual Crew Crew { get; set; }
        
        public virtual Pilot Pilot { get; set; }
        
        public virtual CrewRole Role { get; set; }
    }
}
