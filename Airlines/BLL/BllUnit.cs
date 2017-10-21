using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Services;
using DAL;

namespace BLL
{
    /// <summary>
    /// Main class of BLL project. Represents an object that available on Website layer
    /// </summary>
    public class BllUnit
    {
        private readonly UnitOfWork _unitOfWork;
        public BllUnit()
        {
            _unitOfWork = new UnitOfWork();
        }

        private AirlineService _airlineService;
        public AirlineService AirlineService
            => _airlineService ?? (_airlineService = new AirlineService(_unitOfWork, this));

        private CrewService _crewService;
        public CrewService CrewService
            => _crewService ?? (_crewService = new CrewService(_unitOfWork, this));

        private PassengerFlightService _passengerFlightService;
        public PassengerFlightService PassengerFlightService
            => _passengerFlightService ?? (_passengerFlightService = new PassengerFlightService(_unitOfWork, this));

        private CargoFlightService _cargoFlightService;
        public CargoFlightService CargoFlightService
            => _cargoFlightService ?? (_cargoFlightService = new CargoFlightService(_unitOfWork, this));

        private UserService _userService;
        public UserService UserService
            => _userService ?? (_userService = new UserService(_unitOfWork, this));


        /// <summary>
        /// Save the changes in database
        /// </summary>
        public void Save()
        {
            _unitOfWork.Save();
        }
    }
}
