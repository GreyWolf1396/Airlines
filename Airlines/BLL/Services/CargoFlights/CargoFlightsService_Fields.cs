using DAL;

namespace BLL.Services.CargoFlights
{
    /// <summary>
    /// Service for work with entities: CargoPlaneModel, CargoPlane, CargoFlight, CargoTicket
    /// </summary>
    public partial class CargoFlightService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly BllUnit _bllUnit;

        public CargoFlightService(UnitOfWork unitOfWork, BllUnit bllUnit)
        {
            _unitOfWork = unitOfWork;
            _bllUnit = bllUnit;
        }

        private CargoPlaneModelsMicroMicroService _planeModels;
        private CargoPlanesMicroMicroService _planes;
        private CargoFlightsMicroMicroService _flights;
        private CargoTicketsMicroMicroService _tickets;

        public CargoPlaneModelsMicroMicroService PlaneTypes
            => _planeModels ?? (_planeModels = new CargoPlaneModelsMicroMicroService(_unitOfWork));
        public CargoFlightsMicroMicroService Flights 
            => _flights ?? (_flights = new CargoFlightsMicroMicroService(_unitOfWork));
        public CargoPlanesMicroMicroService Planes
            => _planes ?? (_planes = new CargoPlanesMicroMicroService(_unitOfWork));
        public CargoTicketsMicroMicroService Tickets
            => _tickets ?? (_tickets = new CargoTicketsMicroMicroService(_unitOfWork));


    }
}
