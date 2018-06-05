namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Airlines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        HoursTaken = c.Int(nullable: false),
                        Periodicity = c.Int(nullable: false),
                        BaseTicketValue = c.Double(nullable: false),
                        Departure = c.String(),
                        Arriving = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RouteNodes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NumberInRoute = c.Int(nullable: false),
                        Arriving = c.String(),
                        Departure = c.String(),
                        Airline_Id = c.Int(),
                        Airport_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Airlines", t => t.Airline_Id)
                .ForeignKey("dbo.Airports", t => t.Airport_Id)
                .Index(t => t.Airline_Id)
                .Index(t => t.Airport_Id);
            
            CreateTable(
                "dbo.Airports",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        City = c.String(),
                        Class = c.Int(nullable: false),
                        Codename = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CargoPlanes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Status = c.Int(nullable: false),
                        HomeAirport_Id = c.Int(),
                        Type_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Airports", t => t.HomeAirport_Id)
                .ForeignKey("dbo.CargoPlaneTypes", t => t.Type_Id)
                .Index(t => t.HomeAirport_Id)
                .Index(t => t.Type_Id);
            
            CreateTable(
                "dbo.CargoPlaneTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Vendor = c.String(),
                        CarryingCapacity = c.Double(nullable: false),
                        EnginesCount = c.Int(nullable: false),
                        MinimalCrew = c.Int(nullable: false),
                        RequiredAirportClass = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Crews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Specialization = c.Int(nullable: false),
                        CrewCount = c.Int(nullable: false),
                        HomeAirport_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Airports", t => t.HomeAirport_Id)
                .Index(t => t.HomeAirport_Id);
            
            CreateTable(
                "dbo.EmployeeInCrews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Crew_Id = c.Int(),
                        Employee_Id = c.Int(),
                        Role_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Crews", t => t.Crew_Id)
                .ForeignKey("dbo.Employees", t => t.Employee_Id)
                .ForeignKey("dbo.CrewRoles", t => t.Role_Id)
                .Index(t => t.Crew_Id)
                .Index(t => t.Employee_Id)
                .Index(t => t.Role_Id);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Education = c.String(),
                        EducationCategory = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CrewRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        IsRequired = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PilotInCrews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Crew_Id = c.Int(),
                        Pilot_Id = c.Int(),
                        Role_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Crews", t => t.Crew_Id)
                .ForeignKey("dbo.Pilots", t => t.Pilot_Id)
                .ForeignKey("dbo.CrewRoles", t => t.Role_Id)
                .Index(t => t.Crew_Id)
                .Index(t => t.Pilot_Id)
                .Index(t => t.Role_Id);
            
            CreateTable(
                "dbo.Pilots",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        HoursOfExperience = c.Int(nullable: false),
                        LicenseNumber = c.String(),
                        Education = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PassengerPlanes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Status = c.Int(nullable: false),
                        HomeAirport_Id = c.Int(),
                        Type_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Airports", t => t.HomeAirport_Id)
                .ForeignKey("dbo.PassengerPlaneTypes", t => t.Type_Id)
                .Index(t => t.HomeAirport_Id)
                .Index(t => t.Type_Id);
            
            CreateTable(
                "dbo.PassengerPlaneTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Vendor = c.String(),
                        EnginesCount = c.Int(nullable: false),
                        EconomyClassPlaces = c.Int(nullable: false),
                        BusinesClassPlaces = c.Int(nullable: false),
                        FirstClassPlaces = c.Int(nullable: false),
                        MinimalCrew = c.Int(nullable: false),
                        RequiredAirportClass = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CargoFlights",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        WeightLeft = c.Double(nullable: false),
                        CrewError = c.Boolean(nullable: false),
                        Airline_Id = c.Int(),
                        Crew_Id = c.Int(),
                        Plane_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Airlines", t => t.Airline_Id)
                .ForeignKey("dbo.Crews", t => t.Crew_Id)
                .ForeignKey("dbo.CargoPlanes", t => t.Plane_Id)
                .Index(t => t.Airline_Id)
                .Index(t => t.Crew_Id)
                .Index(t => t.Plane_Id);
            
            CreateTable(
                "dbo.CargoTickets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Weight = c.Double(nullable: false),
                        StartPoint = c.Int(nullable: false),
                        EndPoint = c.Int(nullable: false),
                        Flight_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CargoFlights", t => t.Flight_Id)
                .Index(t => t.Flight_Id);
            
            CreateTable(
                "dbo.PassengerFlights",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        EconomyClassPlacesLeft = c.Int(nullable: false),
                        BusinessClassPlacesLeft = c.Int(nullable: false),
                        FirstClassPlacesLeft = c.Int(nullable: false),
                        CrewError = c.Boolean(nullable: false),
                        Airline_Id = c.Int(),
                        Crew_Id = c.Int(),
                        Plane_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Airlines", t => t.Airline_Id)
                .ForeignKey("dbo.Crews", t => t.Crew_Id)
                .ForeignKey("dbo.PassengerPlanes", t => t.Plane_Id)
                .Index(t => t.Airline_Id)
                .Index(t => t.Crew_Id)
                .Index(t => t.Plane_Id);
            
            CreateTable(
                "dbo.PassengerTickets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PassengerName = c.String(),
                        Class = c.Int(nullable: false),
                        StartPoint = c.Int(nullable: false),
                        EndPoint = c.Int(nullable: false),
                        Flight_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PassengerFlights", t => t.Flight_Id)
                .Index(t => t.Flight_Id);
            
            CreateTable(
                "dbo.PilotsCargoExperiences",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HoursOfFlights = c.Int(nullable: false),
                        Pilot_Id = c.Int(),
                        Plane_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pilots", t => t.Pilot_Id)
                .ForeignKey("dbo.CargoPlaneTypes", t => t.Plane_Id)
                .Index(t => t.Pilot_Id)
                .Index(t => t.Plane_Id);
            
            CreateTable(
                "dbo.PilotsPassengerExperiences",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HoursOfFlights = c.Int(nullable: false),
                        Pilot_Id = c.Int(),
                        Plane_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pilots", t => t.Pilot_Id)
                .ForeignKey("dbo.PassengerPlaneTypes", t => t.Plane_Id)
                .Index(t => t.Pilot_Id)
                .Index(t => t.Plane_Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        AccessLevel = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserRequests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Text = c.String(),
                        Status = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        LastModified = c.DateTime(nullable: false),
                        AssignedTo_Id = c.Int(),
                        Creator_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.AssignedTo_Id)
                .ForeignKey("dbo.Users", t => t.Creator_Id)
                .Index(t => t.AssignedTo_Id)
                .Index(t => t.Creator_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Login = c.String(),
                        Password = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ChiefAdmin_Id = c.Int(),
                        Role_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.ChiefAdmin_Id)
                .ForeignKey("dbo.Roles", t => t.Role_Id)
                .Index(t => t.ChiefAdmin_Id)
                .Index(t => t.Role_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRequests", "Creator_Id", "dbo.Users");
            DropForeignKey("dbo.UserRequests", "AssignedTo_Id", "dbo.Users");
            DropForeignKey("dbo.Users", "Role_Id", "dbo.Roles");
            DropForeignKey("dbo.Users", "ChiefAdmin_Id", "dbo.Users");
            DropForeignKey("dbo.PilotsPassengerExperiences", "Plane_Id", "dbo.PassengerPlaneTypes");
            DropForeignKey("dbo.PilotsPassengerExperiences", "Pilot_Id", "dbo.Pilots");
            DropForeignKey("dbo.PilotsCargoExperiences", "Plane_Id", "dbo.CargoPlaneTypes");
            DropForeignKey("dbo.PilotsCargoExperiences", "Pilot_Id", "dbo.Pilots");
            DropForeignKey("dbo.PassengerTickets", "Flight_Id", "dbo.PassengerFlights");
            DropForeignKey("dbo.PassengerFlights", "Plane_Id", "dbo.PassengerPlanes");
            DropForeignKey("dbo.PassengerFlights", "Crew_Id", "dbo.Crews");
            DropForeignKey("dbo.PassengerFlights", "Airline_Id", "dbo.Airlines");
            DropForeignKey("dbo.CargoTickets", "Flight_Id", "dbo.CargoFlights");
            DropForeignKey("dbo.CargoFlights", "Plane_Id", "dbo.CargoPlanes");
            DropForeignKey("dbo.CargoFlights", "Crew_Id", "dbo.Crews");
            DropForeignKey("dbo.CargoFlights", "Airline_Id", "dbo.Airlines");
            DropForeignKey("dbo.RouteNodes", "Airport_Id", "dbo.Airports");
            DropForeignKey("dbo.PassengerPlanes", "Type_Id", "dbo.PassengerPlaneTypes");
            DropForeignKey("dbo.PassengerPlanes", "HomeAirport_Id", "dbo.Airports");
            DropForeignKey("dbo.PilotInCrews", "Role_Id", "dbo.CrewRoles");
            DropForeignKey("dbo.PilotInCrews", "Pilot_Id", "dbo.Pilots");
            DropForeignKey("dbo.PilotInCrews", "Crew_Id", "dbo.Crews");
            DropForeignKey("dbo.Crews", "HomeAirport_Id", "dbo.Airports");
            DropForeignKey("dbo.EmployeeInCrews", "Role_Id", "dbo.CrewRoles");
            DropForeignKey("dbo.EmployeeInCrews", "Employee_Id", "dbo.Employees");
            DropForeignKey("dbo.EmployeeInCrews", "Crew_Id", "dbo.Crews");
            DropForeignKey("dbo.CargoPlanes", "Type_Id", "dbo.CargoPlaneTypes");
            DropForeignKey("dbo.CargoPlanes", "HomeAirport_Id", "dbo.Airports");
            DropForeignKey("dbo.RouteNodes", "Airline_Id", "dbo.Airlines");
            DropIndex("dbo.Users", new[] { "Role_Id" });
            DropIndex("dbo.Users", new[] { "ChiefAdmin_Id" });
            DropIndex("dbo.UserRequests", new[] { "Creator_Id" });
            DropIndex("dbo.UserRequests", new[] { "AssignedTo_Id" });
            DropIndex("dbo.PilotsPassengerExperiences", new[] { "Plane_Id" });
            DropIndex("dbo.PilotsPassengerExperiences", new[] { "Pilot_Id" });
            DropIndex("dbo.PilotsCargoExperiences", new[] { "Plane_Id" });
            DropIndex("dbo.PilotsCargoExperiences", new[] { "Pilot_Id" });
            DropIndex("dbo.PassengerTickets", new[] { "Flight_Id" });
            DropIndex("dbo.PassengerFlights", new[] { "Plane_Id" });
            DropIndex("dbo.PassengerFlights", new[] { "Crew_Id" });
            DropIndex("dbo.PassengerFlights", new[] { "Airline_Id" });
            DropIndex("dbo.CargoTickets", new[] { "Flight_Id" });
            DropIndex("dbo.CargoFlights", new[] { "Plane_Id" });
            DropIndex("dbo.CargoFlights", new[] { "Crew_Id" });
            DropIndex("dbo.CargoFlights", new[] { "Airline_Id" });
            DropIndex("dbo.PassengerPlanes", new[] { "Type_Id" });
            DropIndex("dbo.PassengerPlanes", new[] { "HomeAirport_Id" });
            DropIndex("dbo.PilotInCrews", new[] { "Role_Id" });
            DropIndex("dbo.PilotInCrews", new[] { "Pilot_Id" });
            DropIndex("dbo.PilotInCrews", new[] { "Crew_Id" });
            DropIndex("dbo.EmployeeInCrews", new[] { "Role_Id" });
            DropIndex("dbo.EmployeeInCrews", new[] { "Employee_Id" });
            DropIndex("dbo.EmployeeInCrews", new[] { "Crew_Id" });
            DropIndex("dbo.Crews", new[] { "HomeAirport_Id" });
            DropIndex("dbo.CargoPlanes", new[] { "Type_Id" });
            DropIndex("dbo.CargoPlanes", new[] { "HomeAirport_Id" });
            DropIndex("dbo.RouteNodes", new[] { "Airport_Id" });
            DropIndex("dbo.RouteNodes", new[] { "Airline_Id" });
            DropTable("dbo.Users");
            DropTable("dbo.UserRequests");
            DropTable("dbo.Roles");
            DropTable("dbo.PilotsPassengerExperiences");
            DropTable("dbo.PilotsCargoExperiences");
            DropTable("dbo.PassengerTickets");
            DropTable("dbo.PassengerFlights");
            DropTable("dbo.CargoTickets");
            DropTable("dbo.CargoFlights");
            DropTable("dbo.PassengerPlaneTypes");
            DropTable("dbo.PassengerPlanes");
            DropTable("dbo.Pilots");
            DropTable("dbo.PilotInCrews");
            DropTable("dbo.CrewRoles");
            DropTable("dbo.Employees");
            DropTable("dbo.EmployeeInCrews");
            DropTable("dbo.Crews");
            DropTable("dbo.CargoPlaneTypes");
            DropTable("dbo.CargoPlanes");
            DropTable("dbo.Airports");
            DropTable("dbo.RouteNodes");
            DropTable("dbo.Airlines");
        }
    }
}
