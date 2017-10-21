
using System.ComponentModel.DataAnnotations;

namespace Contracts.DomainEntities.Crews
{
    public class EmployeeInCrew
    {
        public int Id { get; set; }
        
        public virtual Crew Crew { get; set; }
        
        public virtual Employee Employee { get; set; }
        
        public virtual CrewRole Role { get; set; }
    }
}
