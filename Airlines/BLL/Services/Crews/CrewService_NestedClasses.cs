using BLL.Services.ServiceBase;
using Contracts.DomainEntities.Crews;
using DAL;

namespace BLL.Services.Crews
{
    public partial class CrewService
    {
        public class CrewMicroMicroService : MicroServiceBase<Crew>
        {
            public CrewMicroMicroService(UnitOfWork unitOfWork) : base(unitOfWork.CrewsRepository)
            {
            }
        }
        public class CrewPilotMicroMicroService : MicroServiceBase<PilotInCrew>
        {
            public CrewPilotMicroMicroService(UnitOfWork unitOfWork) : base(unitOfWork.PilotInCrewRepository)
            {
                
            }
        }
        public class CrewEmployeeMicroMicroService : MicroServiceBase<EmployeeInCrew>
        {
            public CrewEmployeeMicroMicroService(UnitOfWork unitOfWork) : base(unitOfWork.EmployeeInCrewRepository)
            {
            }
        }
        public class CrewRoleMicroMicroService : MicroServiceBase<CrewRole>
        {
            public CrewRoleMicroMicroService(UnitOfWork unitOfWork) : base(unitOfWork.CrewRolesRepository)
            {
            }
        }
        public class EmployeesMicroMicroService : MicroServiceBase<Employee>
        {
            public EmployeesMicroMicroService(UnitOfWork unitOfWork) : base(unitOfWork.EmployeesRepository)
            {
            }
        }
        public class PilotsMicroMicroService : MicroServiceBase<Pilot>
        {
            public PilotsMicroMicroService(UnitOfWork unitOfWork) : base(unitOfWork.PilotsRepository)
            {
            }
        }
        public class PilotPassengerExpMicroMicroService : MicroServiceBase<PilotsPassengerExperience>
        {
            public PilotPassengerExpMicroMicroService(UnitOfWork unitOfWork) : base(unitOfWork.PilotPassengerRepository)
            {
            }
        }
        public class PilotCargoExpMicroMicroService:MicroServiceBase<PilotsCargoExperience>
        {
            public PilotCargoExpMicroMicroService(UnitOfWork unitOfWork) : base(unitOfWork.PilotCargoRepository)
            {
            }
        }
    }
}

