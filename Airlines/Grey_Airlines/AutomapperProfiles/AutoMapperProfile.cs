using System.Linq;
using AutoMapper;
using Contracts.DomainEntities.Airlines;
using Contracts.DomainEntities.Cargo_flights;
using Contracts.DomainEntities.Crews;
using Contracts.DomainEntities.Passenger_flights;
using Contracts.DomainEntities.Users;
using Grey_Airlines.Models;
using Grey_Airlines.Models.AirlineModels;
using Grey_Airlines.Models.CargoFlightModels;
using Grey_Airlines.Models.CrewModels;
using Grey_Airlines.Models.UserModels;

namespace Grey_Airlines.AutomapperProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Airline, AirlineModel>()
                .ForMember("RouteNodes",
                    opt => opt.MapFrom(a => a.Nodes.Select(n => new RouteNodeModel()
                    {
                        Id = n.Id,
                        Airline = n.Airline.Title,
                        Airport = n.Airport.Name,
                        NumberInRoute = n.NumberInRoute,
                        Departure = n.Departure,
                        Arriving = n.Arriving
                    }).OrderBy(r => r.NumberInRoute).ToList()))
                .ReverseMap()
                .ForMember(a => a.Nodes, opt => opt.Ignore());
            CreateMap<Airport, AirportModel>()
                .ReverseMap()
                .ForMember(a => a.CargoPlanes, opt => opt.Ignore())
                .ForMember(a => a.PassengerPlanes, opt => opt.Ignore())
                .ForMember(a => a.Crews, opt => opt.Ignore())
                .ForMember(a => a.Routes, opt => opt.Ignore());

            CreateMap<CargoFlight, CargoFlightModel>()
                .ForMember(m => m.Airline, opt => opt.MapFrom(o => o.Airline.Title))
                .ForMember(m => m.Crew, opt => opt.MapFrom(o => o.Crew.Title))
                .ForMember(m => m.Plane, opt => opt.MapFrom(o => o.Plane.Type.Title + "/" + o.Plane.Id));
            CreateMap<CargoPlane, CargoPlaneModel>()
                .ForMember(m => m.Type, opt => opt.Ignore())
                .ForMember(m => m.HomeAirport, opt => opt.MapFrom(o => o.HomeAirport.Name))
                .ForMember(m => m.TypeId, opt => opt.MapFrom(s => s.Type.Id))
                .ReverseMap()
                .ForMember(s => s.Id, o => o.Ignore());
            CreateMap<CargoPlaneType, CargoPlaneTypeModel>()
                .ReverseMap()
                .ForMember(m => m.Id, opt => opt.Ignore());
            CreateMap<CargoTicket, CargoTicketModel>()
                .ForMember(m => m.FlightId, o => o.MapFrom(t => t.Flight.Id));

            CreateMap<Crew, CrewModel>()
                .ForMember("Home", opt => opt.MapFrom(c => c.HomeAirport.Name))
                .ForMember("HomeId", opt => opt.MapFrom(c => c.HomeAirport.Id))
                .ForMember("Pilots", opt => opt.Ignore())
                .ForMember("Employees", opt => opt.Ignore())
                .ReverseMap()
                .ForMember(c => c.HomeAirport, opt => opt.Ignore())
                .ForMember(c => c.Pilots, opt => opt.Ignore())
                .ForMember(c => c.Employees, opt => opt.Ignore());
            CreateMap<Pilot, PilotModel>()
                .ForMember(p => p.CrewRole, o => o.Ignore());
            CreateMap<Employee, EmployeeModel>()
                .ForMember(p => p.CrewRole, o => o.Ignore());

            CreateMap<PassengerFlight, PassengerFlightModel>()
                .ForMember(m => m.Airline, opt => opt.MapFrom(o => o.Airline.Title))
                .ForMember(m => m.Crew, opt => opt.MapFrom(o => o.Crew.Title))
                .ForMember(m => m.Plane, opt => opt.MapFrom(o => o.Plane.Type.Title + "/" + o.Plane.Id));
            CreateMap<PassengerPlane, PassengerPlaneModel>()
                .ForMember(m => m.Type, opt => opt.Ignore())
                .ForMember(m => m.HomeAirport, opt => opt.MapFrom(o => o.HomeAirport.Name))
                .ForMember(m => m.AirportId, opt => opt.MapFrom(o => o.HomeAirport.Id))
                .ForMember(m => m.TypeId, opt => opt.MapFrom(o => o.Type.Id));
            CreateMap<PassengerPlaneType, PassengerPlaneTypeModel>().ReverseMap().ForMember(m => m.Id, opt => opt.Ignore());
            CreateMap<PassengerTicket, PassengerTicketModel>()
                .ForMember(m => m.FlightId, o => o.MapFrom(t => t.Flight.Id));

            CreateMap<User, UserModel>()
                .ForMember("Role", opt => opt.MapFrom(u => u.Role.Title))
                .ForMember("ChiefAdmin", opt => opt.MapFrom(u => u.ChiefAdmin.Name))
                .ForMember("RoleId", opt => opt.MapFrom(u => u.Role.Id))
                .ForMember("ChiefAdminId", opt => opt.Ignore());

            CreateMap<UserRequest, UserRequestModel>()
                .ForMember(r => r.Creator, opt => opt.MapFrom(r => r.Creator.Name))
                .ForMember(r => r.AssignedTo, opt => opt.MapFrom(r => r.AssignedTo.Name));
            CreateMap<Role, RoleModel>();



        }
    }
}