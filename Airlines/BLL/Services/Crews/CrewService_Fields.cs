using BLL.Services.ServiceBase;
using DAL;

namespace BLL.Services
{
    /// <summary>
    /// Service for work with entities: Crew, Pilot, Employee and relations between them
    /// </summary>
    public partial class CrewService
    {
        private readonly BllUnit _bllUnit;
        private readonly UnitOfWork _unitOfWork;

        public CrewService(UnitOfWork unitOfWork, BllUnit bllUnit)
        {
            _unitOfWork = unitOfWork;
            _bllUnit = bllUnit;
        }

        private CrewMicroMicroService _crews;
        private CrewPilotMicroMicroService _crewPilots;
        private CrewEmployeeMicroMicroService _crewEmployees;
        private CrewRoleMicroMicroService _crewRoles;
        private EmployeesMicroMicroService _employees;
        private PilotsMicroMicroService _pilots;
        private PilotCargoExpMicroMicroService _pilotCargoExp;
        private PilotPassengerExpMicroMicroService _pilotPassengerExp;


        public CrewMicroMicroService Crews 
            => _crews ?? (_crews = new CrewMicroMicroService(_unitOfWork));
        public CrewPilotMicroMicroService CrewPilots 
            => _crewPilots ?? (_crewPilots = new CrewPilotMicroMicroService(_unitOfWork));
        public CrewEmployeeMicroMicroService CrewEmployees
            => _crewEmployees ?? (_crewEmployees = new CrewEmployeeMicroMicroService(_unitOfWork));
        public CrewRoleMicroMicroService CrewRoles
            => _crewRoles ?? (_crewRoles = new CrewRoleMicroMicroService(_unitOfWork));
        public EmployeesMicroMicroService Employees
            => _employees ?? (_employees = new EmployeesMicroMicroService(_unitOfWork));
        public PilotsMicroMicroService Pilots
            => _pilots ?? (_pilots = new PilotsMicroMicroService(_unitOfWork));
        public PilotCargoExpMicroMicroService PilotCargoExp
            => _pilotCargoExp ?? (_pilotCargoExp = new PilotCargoExpMicroMicroService(_unitOfWork));
        public PilotPassengerExpMicroMicroService PilotPassengerExp
            => _pilotPassengerExp ?? (_pilotPassengerExp = new PilotPassengerExpMicroMicroService(_unitOfWork));
    }
}

