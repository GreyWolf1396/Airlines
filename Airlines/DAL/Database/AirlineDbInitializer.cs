using System;
using System.Data.Entity;
using System.Linq;
using Contracts.DomainEntities.Airlines;
using Contracts.DomainEntities.Cargo_flights;
using Contracts.DomainEntities.Crews;
using Contracts.DomainEntities.Passenger_flights;
using Contracts.DomainEntities.Users;
using Contracts.Enums;

namespace DAL.Database
{
    class AirlineDbInitializer:DropCreateDatabaseIfModelChanges<AirlineDbContext>
    {
        protected override void Seed(AirlineDbContext db)
        {
            db.Roles.Add(new Role()
            {
                Id = 1,
                Title = "Administrator",
                AccessLevel = 2
            });
            db.Roles.Add(new Role()
            {
                Id = 2,
                Title = "Manager",
                AccessLevel = 1
            });
            db.Roles.Add(new Role()
            {
                Id = 3,
                Title = "Guest",
                AccessLevel = 0
            });
            db.SaveChanges();

            db.Users.Add(new User()
            {
                Id = 1,
                Name = "Carl Johnson",
                Login = "CJ01",
                Password = -645393744,
                ChiefAdmin = null,
                Role = db.Roles.Find(1)
            });
            db.Users.Add(new User()
            {
                Id = 2,
                Name = "Michael Corwin",
                Login = "MC02",
                Password = 1311380130,
                ChiefAdmin = null,
                Role = db.Roles.Find(1)
            });

            db.Users.Add(new User()
            {
                Id = 3,
                Name = "Luke Cage",
                Login = "LC03",
                Password = -1027272029,
                ChiefAdmin = db.Users.Find(1),
                Role = db.Roles.Find(2)
            });
            db.Users.Add(new User()
            {
                Id = 4,
                Name = "Matt Murdock",
                Login = "MM04",
                Password = -1837493598,
                ChiefAdmin = db.Users.Find(1),
                Role = db.Roles.Find(2)
            });
            db.Users.Add(new User()
            {
                Id = 5,
                Name = "Roberta Mitches",
                Login = "RM05",
                Password = 118821569,
                ChiefAdmin = db.Users.Find(2),
                Role = db.Roles.Find(2)
            });
            db.Users.Add(new User()
            {
                Id = 6,
                Name = "Sarah Conor",
                Login = "SC01",
                Password = -644934976,
                ChiefAdmin = db.Users.Find(2),
                Role = db.Roles.Find(2)
            });
            db.SaveChanges();

            db.UserRequests.Add(new UserRequest()
            {
                Id = 1,
                Title = "Test request",
                Text = "That's test message created with database",
                CreateTime = DateTime.UtcNow,
                Status = RequestStatus.Created,
                Creator = db.Users.First(u => u.ChiefAdmin != null),
                AssignedTo = db.Users.First(u => u.ChiefAdmin != null).ChiefAdmin,
                LastModified = DateTime.UtcNow,
            });

            db.Pilots.Add(new Pilot()
            {
                Id = 1,
                Name = "Tony Stark",
                LicenseNumber = "IM-Mk-42",
                Education = "High",
                HoursOfExperience = 0
            });
            db.Pilots.Add(new Pilot()
            {
                Id = 2,
                Name = "Thor Odinson",
                LicenseNumber = "As-Ga-Rd",
                Education = "Medium",
                HoursOfExperience = 0
            });

            db.Employees.Add(new Employee()
            {
                Id = 1,
                Name = "Mia Johnson",
                Education = "High",
                EducationCategory = EmployeeCategory.Steward
            });
            db.Employees.Add(new Employee()
            {
                Id = 2,
                Name = "Mark Town",
                Education = "High",
                EducationCategory = EmployeeCategory.AirMechanic
            });
            db.Employees.Add(new Employee()
            {
                Id = 3,
                Name = "Perry White",
                Education = "High",
                EducationCategory = EmployeeCategory.Navigator
            });

            db.CrewRoles.Add(new CrewRole()
            {
                Id = 1,
                Title = "Captain",
                IsRequired = true
            });
            db.CrewRoles.Add(new CrewRole()
            {
                Id = 2,
                Title = "Second Pilot",
                IsRequired = true
            });
            db.CrewRoles.Add(new CrewRole()
            {
                Id = 3,
                Title = "Navigator",
                IsRequired = true
            });
            db.CrewRoles.Add(new CrewRole()
            {
                Id = 4,
                Title = "Air Mechanic",
                IsRequired = true
            });
            db.CrewRoles.Add(new CrewRole()
            {
                Id = 5,
                Title = "Main steward",
                IsRequired = true
            });
            db.CrewRoles.Add(new CrewRole()
            {
                Id = 6,
                Title = "Steward",
                IsRequired = false
            });

            db.Airports.Add(new Airport()
            {
                Id = 1,
                Name = "Kharkiv airport",
                Codename = "UKHD",
                Class = AirportClass.IV,
                City = "Kharkiv"
            });
            db.Airports.Add(new Airport()
            {
                Id = 2,
                Name = "Boryspil",
                Codename = "UKBB",
                Class = AirportClass.I,
                City = "Kiyv"
            });
            db.Airports.Add(new Airport()
            {
                Id = 3,
                Name = "Odessa airport",
                Codename = "UKOO",
                Class = AirportClass.IV,
                City = "Odessa"
            });
            db.Airports.Add(new Airport()
            {
                Id = 4,
                Name = "Lviv airport",
                Codename = "UKLL",
                Class = AirportClass.IV,
                City = "Lviv"
            });
            db.SaveChanges();
            db.Crews.Add(new Crew()
            { 
                Id = 1,
                HomeAirport = db.Airports.Find(1),
                Title = "PsCr_01"
            });

            db.PilotsInCrews.Add(new PilotInCrew()
            {
                Crew = db.Crews.Find(1),
                Pilot = db.Pilots.Find(1),
                Role = db.CrewRoles.Find(1)
            });
            db.PilotsInCrews.Add(new PilotInCrew()
            {
                Crew = db.Crews.Find(1),
                Pilot = db.Pilots.Find(2),
                Role = db.CrewRoles.Find(2)
            });

            db.EmployeesInCrews.Add(new EmployeeInCrew()
            {
                Crew = db.Crews.Find(1),
                Employee = db.Employees.Find(3),
                Role = db.CrewRoles.Find(3)
            });
            db.EmployeesInCrews.Add(new EmployeeInCrew()
            {
                Crew = db.Crews.Find(1),
                Employee = db.Employees.Find(2),
                Role = db.CrewRoles.Find(4)
            });
            db.EmployeesInCrews.Add(new EmployeeInCrew()
            {
                Crew = db.Crews.Find(1),
                Employee = db.Employees.Find(1),
                Role = db.CrewRoles.Find(5)
            });

            db.CargoPlaneModels.Add(new CargoPlaneType()
            {
                Id = 1,
                Title = "AN-22",
                Vendor = "Antonov",
                CarryingCapacity = 60_000,
                EnginesCount = 4,
                MinimalCrew = 5,
                RequiredAirportClass = AirportClass.V
            });
            db.CargoPlaneModels.Add(new CargoPlaneType()
            {
                Id = 2,
                Title = "AN-124 Ruslan",
                Vendor = "Antonov",
                CarryingCapacity = 120_000,
                EnginesCount = 4,
                MinimalCrew = 7,
                RequiredAirportClass = AirportClass.IV
            });

            db.PassengerPlaneModels.Add(new PassengerPlaneType()
            {
                Id = 1,
                Title = "Boeing 737",
                Vendor = "Boeing",
                EnginesCount = 4,
                EconomyClassPlaces = 114,
                BusinesClassPlaces = 0,
                FirstClassPlaces = 0,
                MinimalCrew = 7,
                RequiredAirportClass = AirportClass.IV
            });
            db.PassengerPlaneModels.Add(new PassengerPlaneType()
            {
                Id = 2,
                Title = "Boeing-747",
                Vendor = "Boeing",
                EnginesCount = 4,
                EconomyClassPlaces = 298,
                BusinesClassPlaces = 66,
                FirstClassPlaces = 14,
                MinimalCrew = 10,
                RequiredAirportClass = AirportClass.IV
            });

            db.Airlines.Add(new Airline()
            {
                Title = "Kharkiv-Odessa",
                HoursTaken = 6,
                BaseTicketValue = 1000,
                Periodicity = AirlinePeriodicity.Everyday,
                Departure = "18:00",
                Arriving = "23:50"
            });
            db.Airlines.Add(new Airline()
            {
                Title = "Kiev-Lviv",
                HoursTaken = 3,
                BaseTicketValue = 700,
                Periodicity = AirlinePeriodicity.OnceInTwoDays,
                Departure = "12:40",
                Arriving = "15:50"
            });
            db.SaveChanges();
            db.RouteNodes.Add(new RouteNode()
            {
                Airline = db.Airlines.Find(1),
                Airport = db.Airports.Find(1),
                NumberInRoute = 1,
                Departure = "18:00",
            });
            db.RouteNodes.Add(new RouteNode()
            {
                Airline = db.Airlines.Find(1),
                Airport = db.Airports.Find(2),
                NumberInRoute = 2,
                Departure = "19:30",
                Arriving = "19:10"
            });
            db.RouteNodes.Add(new RouteNode()
            {
                Airline = db.Airlines.Find(1),
                Airport = db.Airports.Find(3),
                NumberInRoute = 3,
                Arriving = "23:50"
            });

            db.RouteNodes.Add(new RouteNode()
            {
                Airline = db.Airlines.Find(2),
                Airport = db.Airports.Find(2),
                NumberInRoute = 1,
                Departure = "12:40",
            });
            db.RouteNodes.Add(new RouteNode()
            {
                Airline = db.Airlines.Find(2),
                Airport = db.Airports.Find(4),
                NumberInRoute = 2,
                Arriving = "15:50"
            });

            db.SaveChanges();

            base.Seed(db);
        }
    }
}
