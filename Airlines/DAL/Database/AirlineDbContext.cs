using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Contracts.DomainEntities.Airlines;
using Contracts.DomainEntities.Cargo_flights;
using Contracts.DomainEntities.Crews;
using Contracts.DomainEntities.Passenger_flights;
using Contracts.DomainEntities.Users;

namespace DAL.Database
{
    public class AirlineDbContext:DbContext
    {
        public AirlineDbContext() : base("AirlineDb")
        {
            System.Data.Entity.Database.SetInitializer( new AirlineDbInitializer());
            Database.Initialize(true);
        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRequest> UserRequests { get; set; }

        public DbSet<Airport> Airports { get; set; }
        public DbSet<Airline> Airlines { get; set; }
        public DbSet<RouteNode> RouteNodes { get; set; }

        public DbSet<CargoPlaneType> CargoPlaneModels { get; set; }
        public DbSet<CargoPlane> CargoPlanes { get; set; }
        public DbSet<CargoFlight> CargoFlights { get; set; }
        public DbSet<CargoTicket> CargoTickets { get; set; }

        public DbSet<PassengerPlaneType> PassengerPlaneModels { get; set; }
        public DbSet<PassengerPlane> PassengerPlanes { get; set; }
        public DbSet<PassengerFlight> PassengerFlights { get; set; }
        public DbSet<PassengerTicket> PassengerTickets { get; set; }

        public DbSet<Pilot> Pilots { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<CrewRole> CrewRoles { get; set; }
        public DbSet<Crew> Crews { get; set; }
        public DbSet<PilotInCrew> PilotsInCrews { get; set; }
        public DbSet<EmployeeInCrew> EmployeesInCrews { get; set; }
        public DbSet<PilotsPassengerExperience> PilotsPassengerExperiences { get; set; }
        public DbSet<PilotsCargoExperience> PilotsCargoExperiences { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        }
    }
}
