using System.Collections.Generic;
using Contracts.DomainEntities.Airlines;
using Contracts.Enums;

namespace Contracts.DomainEntities.Crews
{
    public class Crew
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public virtual Airport HomeAirport { get; set; }

        public CrewSpecialization Specialization { get; set; }

        public int CrewCount { get; set; }

        public virtual ICollection<PilotInCrew> Pilots { get; set; }

        public virtual ICollection<EmployeeInCrew> Employees { get; set; }
    }
}
