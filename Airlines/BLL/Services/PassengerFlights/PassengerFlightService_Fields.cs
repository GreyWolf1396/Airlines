using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.DomainEntities.Passenger_flights;
using DAL;
using BLL.Services.ServiceBase;

namespace BLL.Services
{
    /// <summary>
    /// Service for work with entities: PassengerPlaneModel, PassengerPlane, PassengerFlight, PassengerTicket
    /// </summary>
    public partial class PassengerFlightService 
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly BllUnit _bllUnit;

        public PassengerFlightService(UnitOfWork unitOfWork, BllUnit bllUnit)
        {
            _unitOfWork = unitOfWork;
            _bllUnit = bllUnit;
        }

        private PassengerPlaneModelsMicroMicroService _planeModels;
        private PassengerPlanesMicroMicroService _planes;
        private PassengerFligtsMicroMicroService _fligts;
        private PassengerTicketsMicroMicroService _tickets;

        public PassengerPlaneModelsMicroMicroService PlaneTypes
            => _planeModels ?? (_planeModels = new PassengerPlaneModelsMicroMicroService(_unitOfWork));
        public PassengerPlanesMicroMicroService Planes 
            => _planes ?? (_planes = new PassengerPlanesMicroMicroService(_unitOfWork));
        public PassengerFligtsMicroMicroService Flights 
            => _fligts ?? (_fligts = new PassengerFligtsMicroMicroService(_unitOfWork));
        public PassengerTicketsMicroMicroService Tickets 
            => _tickets ?? (_tickets = new PassengerTicketsMicroMicroService(_unitOfWork));
    }
}
