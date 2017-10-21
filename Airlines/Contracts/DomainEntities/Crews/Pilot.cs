namespace Contracts.DomainEntities.Crews
{
    public class Pilot
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int HoursOfExperience { get; set; }

        public string LicenseNumber { get; set; }

        public string Education { get; set; }
    }
}
