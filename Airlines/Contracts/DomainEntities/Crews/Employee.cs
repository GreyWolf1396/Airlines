using Contracts.Enums;

namespace Contracts.DomainEntities.Crews
{
    public class Employee
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Education { get; set; }

        public EmployeeCategory EducationCategory { get; set; }
    }
}
