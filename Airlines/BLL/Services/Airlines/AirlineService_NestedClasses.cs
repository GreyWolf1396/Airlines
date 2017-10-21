using BLL.Services.ServiceBase;
using Contracts.DomainEntities.Airlines;
using DAL;

namespace BLL.Services
{
    partial class AirlineService
    {
        public class AirlineMicroMicroService : MicroServiceBase<Airline>
        {
            public AirlineMicroMicroService(UnitOfWork unitOfWork) : base(unitOfWork.AirlinesRepository)
            { }
        }
        public class AirportMicroMicroService : MicroServiceBase<Airport>
        {
            public AirportMicroMicroService(UnitOfWork unitOfWork) : base(unitOfWork.AirportRepository)
            { }
        }
        public class RouteMicroMicroService : MicroServiceBase<RouteNode>
        {
            public RouteMicroMicroService(UnitOfWork unitOfWork) : base(unitOfWork.RouteNodesRepository)
            { }
        }
    }
}
