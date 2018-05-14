using BLL.Services.ServiceBase;
using Contracts.DomainEntities.Airlines;
using Contracts.DomainEntities.Cargo_flights;
using DAL;

namespace BLL.Services.Airlines
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
