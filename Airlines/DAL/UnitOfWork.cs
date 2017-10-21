using Contracts.DomainEntities.Airlines;
using Contracts.DomainEntities.Cargo_flights;
using Contracts.DomainEntities.Crews;
using Contracts.DomainEntities.Passenger_flights;
using Contracts.DomainEntities.Users;
using DAL.Database;
using DAL.Repository;

namespace DAL
{
    public class UnitOfWork
    {
        private readonly AirlineDbContext _dbContext = new AirlineDbContext();

        private GenericRepository<Role> _rolesRepository;
        private GenericRepository<User> _usersRepository;
        private GenericRepository<UserRequest> _requestRepository;

        private GenericRepository<Airport> _airportRepository;
        private GenericRepository<RouteNode> _routeNodesRepository;
        private GenericRepository<Airline> _airlinesRepository;

        private GenericRepository<Crew> _crewsRepository;
        private GenericRepository<PilotInCrew> _pilotInCrewRepository;
        private GenericRepository<EmployeeInCrew> _employeeInCrewRepository;
        private GenericRepository<Pilot> _pilotsRepository;
        private GenericRepository<Employee> _employeesRepository;
        private GenericRepository<CrewRole> _crewRolesRepository;
        private GenericRepository<PilotsCargoExperience> _pilotCargoRepository;
        private GenericRepository<PilotsPassengerExperience> _pilotPassengerRepository;

        private GenericRepository<CargoPlaneType> _cargoPlaneModelsRepository;
        private GenericRepository<CargoPlane> _cargoPlanesRepository;
        private GenericRepository<CargoFlight> _cargoFlightsRepository;
        private GenericRepository<CargoTicket> _cargoTicketsRepository;

        private GenericRepository<PassengerPlaneType> _passengerPlaneModelsRepository;
        private GenericRepository<PassengerPlane> _passengerPlanesRepository;
        private GenericRepository<PassengerFlight> _passengerFlightsRepository;
        private GenericRepository<PassengerTicket> _passengerTicketsRepository;

        /***********************Properties********************/
        public GenericRepository<Role> RolesRepository
            => _rolesRepository ?? (_rolesRepository = new GenericRepository<Role>(_dbContext));
        public GenericRepository<User> UsersRepository
            => _usersRepository ?? (_usersRepository = new GenericRepository<User>(_dbContext));
        public GenericRepository<UserRequest> RequestsRepository
            => _requestRepository ?? (_requestRepository = new GenericRepository<UserRequest>(_dbContext));

        public GenericRepository<Airport> AirportRepository
            => _airportRepository ?? (_airportRepository = new GenericRepository<Airport>(_dbContext));
        public GenericRepository<Airline> AirlinesRepository
            => _airlinesRepository ?? (_airlinesRepository = new GenericRepository<Airline>(_dbContext));
        public GenericRepository<RouteNode> RouteNodesRepository
            => _routeNodesRepository ?? (_routeNodesRepository = new GenericRepository<RouteNode>(_dbContext));

        public GenericRepository<Crew> CrewsRepository
            => _crewsRepository ?? (_crewsRepository = new GenericRepository<Crew>(_dbContext));
        public GenericRepository<PilotInCrew> PilotInCrewRepository
            => _pilotInCrewRepository ?? (_pilotInCrewRepository = new GenericRepository<PilotInCrew>(_dbContext));
        public GenericRepository<EmployeeInCrew> EmployeeInCrewRepository
            => _employeeInCrewRepository ?? (_employeeInCrewRepository = new GenericRepository<EmployeeInCrew>(_dbContext));
        public GenericRepository<Pilot> PilotsRepository
            => _pilotsRepository ?? (_pilotsRepository = new GenericRepository<Pilot>(_dbContext));
        public GenericRepository<CrewRole> CrewRolesRepository
            => _crewRolesRepository ?? (_crewRolesRepository = new GenericRepository<CrewRole>(_dbContext));
        public GenericRepository<Employee> EmployeesRepository
            => _employeesRepository ?? (_employeesRepository = new GenericRepository<Employee>(_dbContext));
        public GenericRepository<PilotsCargoExperience> PilotCargoRepository
            =>
                _pilotCargoRepository ??
                (_pilotCargoRepository = new GenericRepository<PilotsCargoExperience>(_dbContext));
        public GenericRepository<PilotsPassengerExperience> PilotPassengerRepository
            =>
                _pilotPassengerRepository ??
                (_pilotPassengerRepository = new GenericRepository<PilotsPassengerExperience>(_dbContext));

        public GenericRepository<CargoPlaneType> CargoPlaneModelsRepository
            =>
                _cargoPlaneModelsRepository ??
                (_cargoPlaneModelsRepository = new GenericRepository<CargoPlaneType>(_dbContext));
        public GenericRepository<CargoPlane> CargoPlanesRepository
            => _cargoPlanesRepository ?? (_cargoPlanesRepository = new GenericRepository<CargoPlane>(_dbContext));
        public GenericRepository<CargoFlight> CargoFlightsRepository
            => _cargoFlightsRepository ?? (_cargoFlightsRepository = new GenericRepository<CargoFlight>(_dbContext));
        public GenericRepository<CargoTicket> CargoTicketsRepository
            => _cargoTicketsRepository ?? (_cargoTicketsRepository = new GenericRepository<CargoTicket>(_dbContext));

        public GenericRepository<PassengerPlaneType> PassengerPlaneModelsRepository
            =>
                _passengerPlaneModelsRepository ??
                (_passengerPlaneModelsRepository = new GenericRepository<PassengerPlaneType>(_dbContext));
        public GenericRepository<PassengerPlane> PassengerPlanes
            =>
                _passengerPlanesRepository ??
                (_passengerPlanesRepository = new GenericRepository<PassengerPlane>(_dbContext));
        public GenericRepository<PassengerFlight> PassengerFlightsRepository
            =>
                _passengerFlightsRepository ??
                (_passengerFlightsRepository = new GenericRepository<PassengerFlight>(_dbContext));
        public GenericRepository<PassengerTicket> PassengerTicketsRepository
            =>
                _passengerTicketsRepository ??
                (_passengerTicketsRepository = new GenericRepository<PassengerTicket>(_dbContext));

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
