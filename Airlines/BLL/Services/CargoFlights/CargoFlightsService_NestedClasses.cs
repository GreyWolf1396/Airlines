using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Services.ServiceBase;
using Contracts.DomainEntities.Cargo_flights;
using DAL;

namespace BLL.Services
{
    public partial class CargoFlightService
    {
        public class CargoPlaneModelsMicroMicroService : MicroServiceBase<CargoPlaneType>
        {
            public CargoPlaneModelsMicroMicroService(UnitOfWork unitOfWork) : base(unitOfWork.CargoPlaneModelsRepository)
            { }
        }
        public class CargoPlanesMicroMicroService : MicroServiceBase<CargoPlane>
        {
            public CargoPlanesMicroMicroService(UnitOfWork unitOfWork) : base(unitOfWork.CargoPlanesRepository)
            { }
        }
        public class CargoFlightsMicroMicroService : MicroServiceBase<CargoFlight>
        {
            public CargoFlightsMicroMicroService(UnitOfWork unitOfWork) : base(unitOfWork.CargoFlightsRepository)
            { }
        }
        public class CargoTicketsMicroMicroService : MicroServiceBase<CargoTicket>
        {
            public CargoTicketsMicroMicroService(UnitOfWork unitOfWork) : base(unitOfWork.CargoTicketsRepository)
            { }
        }
    }
}
