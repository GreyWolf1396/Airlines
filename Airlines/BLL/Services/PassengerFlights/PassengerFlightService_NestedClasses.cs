using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Services.ServiceBase;
using Contracts.DomainEntities.Passenger_flights;
using DAL;

namespace BLL.Services
{
    public partial class PassengerFlightService
    {
        public class PassengerPlaneModelsMicroMicroService : MicroServiceBase<PassengerPlaneType>
        {
            public PassengerPlaneModelsMicroMicroService(UnitOfWork unitOfWork)
                : base(unitOfWork.PassengerPlaneModelsRepository)
            { }
        }
        public class PassengerPlanesMicroMicroService : MicroServiceBase<PassengerPlane>
        {
            public PassengerPlanesMicroMicroService(UnitOfWork unitOfWork) : base(unitOfWork.PassengerPlanes)
            { }
        }
        public class PassengerFligtsMicroMicroService : MicroServiceBase<PassengerFlight>
        {
            public PassengerFligtsMicroMicroService(UnitOfWork unitOfWork) : base(unitOfWork.PassengerFlightsRepository)
            { }
        }
        public class PassengerTicketsMicroMicroService : MicroServiceBase<PassengerTicket>
        {
            public PassengerTicketsMicroMicroService(UnitOfWork unitOfWork) : base(unitOfWork.PassengerTicketsRepository)
            { }
        }
    }
}
