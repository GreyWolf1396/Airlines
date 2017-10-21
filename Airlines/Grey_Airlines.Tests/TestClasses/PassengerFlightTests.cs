using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using BLL;
using Contracts.DomainEntities.Cargo_flights;
using Contracts.DomainEntities.Crews;
using Contracts.DomainEntities.Passenger_flights;
using Contracts.Enums;
using Grey_Airlines.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Grey_Airlines.Tests.TestClasses
{
    [TestClass]
    public class PassengerFlightTests
    {
        readonly BllUnit _bllUnit = new BllUnit();

        [TestMethod]
        public void CheckInvalidPassTicket_InvalidStartPoint()
        {
            var passengerTicket = new PassengerTicket()
            {
                StartPoint = 10,
                EndPoint = 10
            };
            var actual = _bllUnit.PassengerFlightService.CheckTicket(passengerTicket);

            Assert.AreEqual(false, actual.Item1, actual.Item2);
        }

        [TestMethod]
        public void CheckInvalidPassTicket_ZeroPlaces()
        {
            var passengerTicket = new PassengerTicket()
            {
                StartPoint = 1,
                EndPoint = 2,
                Flight = new PassengerFlight()
                {
                    Plane = new PassengerPlane()
                    {
                        Type = new PassengerPlaneType()
                        {
                            EconomyClassPlaces = 0,
                            BusinesClassPlaces = 0,
                            FirstClassPlaces = 0
                        }
                    },
                    EconomyClassPlacesLeft = 0,
                    BusinessClassPlacesLeft = 0,
                    FirstClassPlacesLeft = 0,
                    Tickets = new List<PassengerTicket>()
                }
            };
            var actual = _bllUnit.PassengerFlightService.CheckTicket(passengerTicket);

            Assert.AreEqual(false, actual.Item1, actual.Item2);
        }

        [TestMethod]
        public void CheckPassCrewCount_Invalid5_0()
        {
            var crew = new Crew()
            {
                CrewCount = 0
            };
            var flight = new PassengerFlight()
            {
                Plane = new PassengerPlane()
                {
                    Type = new PassengerPlaneType()
                    {
                        MinimalCrew = 5
                    }
                }
            };

            var actual = _bllUnit.PassengerFlightService.CheckCrewCount(flight, crew);

            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        public void CheckPassCrewCount_Correct5_5()
        {
            var crew = new Crew()
            {
                CrewCount = 5
            };
            var flight = new PassengerFlight()
            {
                Plane = new PassengerPlane()
                {
                    Type = new PassengerPlaneType()
                    {
                        MinimalCrew = 5
                    }
                }
            };

            var actual = _bllUnit.PassengerFlightService.CheckCrewCount(flight, crew);

            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        public void CheckPassCrewCount_Correct5_6()
        {
            var crew = new Crew()
            {
                CrewCount = 6
            };
            var flight = new PassengerFlight()
            {
                Plane = new PassengerPlane()
                {
                    Type = new PassengerPlaneType()
                    {
                        MinimalCrew = 5
                    }
                }
            };

            var actual = _bllUnit.PassengerFlightService.CheckCrewCount(flight, crew);

            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        public void CheckPassPlaneAvailable_RepairPlane()
        {
            var plane = new PassengerPlane()
            {
                Status = PlaneStatus.Repair
            };
            var actual = _bllUnit.PassengerFlightService.CheckPlaneAvailable(plane);
            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        public void CheckPassPlaneAvailable_WorkingPlane()
        {
            var plane = new PassengerPlane()
            {
                Status = PlaneStatus.Working
            };
            var actual = _bllUnit.PassengerFlightService.CheckPlaneAvailable(plane);
            Assert.AreEqual(true, actual);
        }


        [TestMethod]
        public void TryDetailsFlightInvalidId()
        {
            var controller = new PassengerFlightController();
            var actualView = controller.DetailsFlight(-1).;
                Assert.AreSame(0,0);
        }
    }
}
