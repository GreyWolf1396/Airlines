using System;
using System.Collections.Generic;
using BLL;
using Contracts.DomainEntities.Airlines;
using Contracts.DomainEntities.Cargo_flights;
using Contracts.DomainEntities.Crews;
using Contracts.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Grey_Airlines.Tests
{
    [TestClass]
    public class CargoFlightTests
    {
        readonly BllUnit _bllUnit = new BllUnit();

        [TestMethod]
        public void CheckInvalidCargoTicket_InvalidStartPoint()
        {
            var cargoTicket = new CargoTicket()
            {
                StartPoint = 10,
                EndPoint = 10
            };
            var actual = _bllUnit.CargoFlightService.CheckTicket(cargoTicket);

            Assert.AreEqual(false,actual.Item1,actual.Item2);
        }

        [TestMethod]
        public void CheckInvalidCargoTicket_TooMuchWeigth()
        {
            var cargoTicket = new CargoTicket()
            {
                StartPoint = 1,
                EndPoint = 2,
                Weight = 5000000,
                Flight = new CargoFlight()
                {
                    Plane = new CargoPlane()
                    {
                        Type = new CargoPlaneType()
                        {
                            CarryingCapacity = 100000
                        }
                    },
                    Tickets = new List<CargoTicket>()
                }
            };
            var actual = _bllUnit.CargoFlightService.CheckTicket(cargoTicket);

            Assert.AreEqual(false,actual.Item1,actual.Item2);
        }

        [TestMethod]
        public void CheckCargoCrewCount_Invalid5_0()
        {
            var crew = new Crew()
            {
                CrewCount = 0
            };
            var flight = new CargoFlight()
            {
                Plane = new CargoPlane()
                {
                    Type = new CargoPlaneType()
                    {
                        MinimalCrew = 5
                    }
                }
            };

            var actual = _bllUnit.CargoFlightService.CheckCrewCount(flight, crew);

            Assert.AreEqual(false,actual);
        }

        [TestMethod]
        public void CheckCargoCrewCount_Correct5_5()
        {
            var crew = new Crew()
            {
                CrewCount = 5
            };
            var flight = new CargoFlight()
            {
                Plane = new CargoPlane()
                {
                    Type = new CargoPlaneType()
                    {
                        MinimalCrew = 5
                    }
                }
            };

            var actual = _bllUnit.CargoFlightService.CheckCrewCount(flight, crew);

            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        public void CheckCargoCrewCount_Correct5_6()
        {
            var crew = new Crew()
            {
                CrewCount = 6
            };
            var flight = new CargoFlight()
            {
                Plane = new CargoPlane()
                {
                    Type = new CargoPlaneType()
                    {
                        MinimalCrew = 5
                    }
                }
            };

            var actual = _bllUnit.CargoFlightService.CheckCrewCount(flight, crew);

            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        public void CheckCargoPlaneAvailable_RepairPlane()
        {
            var plane = new CargoPlane()
            {
                Status = PlaneStatus.Repair
            };
            var actual = _bllUnit.CargoFlightService.CheckPlaneAvailable(plane);
            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        public void CheckCargoPlaneAvailable_WorkingPlane()
        {
            var plane = new CargoPlane()
            {
                Status = PlaneStatus.Working
            };
            var actual = _bllUnit.CargoFlightService.CheckPlaneAvailable(plane);
            Assert.AreEqual(true, actual);
        }

    }
}
