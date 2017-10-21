using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Services.ServiceBase;
using Contracts.DomainEntities.Airlines;
using DAL;

namespace BLL.Services
{
    /// <summary>
    /// Service for work with entitites: Airline, Airport, RouteNodes.
    /// </summary>
    public partial class AirlineService
    {
        private BllUnit _bllUnit;
        private UnitOfWork _unitOfWork;
        public AirlineService(UnitOfWork unitOfWork, BllUnit bllUnit)
        {
            _unitOfWork = unitOfWork;
            _bllUnit = bllUnit;
        }

        private AirlineMicroMicroService _airlineMicroMicroService;
        private AirportMicroMicroService _airportMicroMicroService;
        private RouteMicroMicroService _routeMicroMicroService;

        public AirlineMicroMicroService Airlines
            => _airlineMicroMicroService ?? (_airlineMicroMicroService = new AirlineMicroMicroService(_unitOfWork));
        public AirportMicroMicroService Airports
            => _airportMicroMicroService ?? (_airportMicroMicroService = new AirportMicroMicroService(_unitOfWork));
        public RouteMicroMicroService Routes
            => _routeMicroMicroService ?? (_routeMicroMicroService = new RouteMicroMicroService(_unitOfWork));


    }
}
