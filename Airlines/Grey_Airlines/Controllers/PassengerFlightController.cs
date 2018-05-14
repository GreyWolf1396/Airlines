using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using BLL;
using BLL.Services.PassengerFlights;
using Contracts.DomainEntities.Airlines;
using Contracts.DomainEntities.Crews;
using Contracts.DomainEntities.Passenger_flights;
using Contracts.Enums;
using Grey_Airlines.Models;
using Grey_Airlines.Models.AirlineModels;
using Grey_Airlines.Models.CrewModels;
using Grey_Airlines.Models.ViewModels;

namespace Grey_Airlines.Controllers
{
    public class PassengerFlightController : Controller
    {
        private readonly BllUnit _bllUnit;
        private readonly PassengerFlightService _service;

        public PassengerFlightController()
        {
            _bllUnit = new BllUnit();
            _service = _bllUnit.PassengerFlightService;
        }


        [AllowAnonymous]
        public ActionResult IndexFlights()
        {
            var model = new List<PassengerFlightModel>();
            foreach (var flight in _service.Flights.GetAll().ToList())
            {
                if (flight.Status != FlightStatus.Finished)
                    model.Add(Mapper.Map<PassengerFlight, PassengerFlightModel>(flight));
            }
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult CreateFlight()
        {
            ViewBag.Crews = new SelectList(_bllUnit.CrewService.Crews.GetAll().Select(c => c.Title).ToList());
            ViewBag.Planes =
                new SelectList(
                    _service.Planes.GetAll()
                        .Where(s => s.Status != PlaneStatus.Repair)
                        .Select(s => s.Type.Title + "/" + s.Id)
                        .ToList());
            ViewBag.Airlines = new SelectList(_bllUnit.AirlineService.Airlines.GetAll().Select(a => a.Title).ToList());
            return View(new PassengerFlightModel());
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult CreateFlight(PassengerFlightModel passengerFlight)
        {
            var airline = _bllUnit.AirlineService.Airlines.GetAll().FirstOrDefault(a => a.Title.Equals(passengerFlight.Airline));
            var crew = _bllUnit.CrewService.Crews.GetAll().FirstOrDefault(c => c.Title.Equals(passengerFlight.Crew));
            var plane = _service.Planes.GetAll().FirstOrDefault(p => p.Id == int.Parse(passengerFlight.Plane.Split('/')[1]));
            var flightEntity = new PassengerFlight()
            {
                Airline = airline,
                Crew = crew,
                Plane = plane,
                EconomyClassPlacesLeft = plane.Type.EconomyClassPlaces,
                BusinessClassPlacesLeft = plane.Type.BusinesClassPlaces,
                FirstClassPlacesLeft = plane.Type.FirstClassPlaces,
                StartDate = passengerFlight.StartDate,
                EndDate = passengerFlight.EndDate,
                Status = FlightStatus.Created
            };
            _service.Flights.Insert(flightEntity);
            return RedirectToAction("IndexFlights");
        }

        [AllowAnonymous]
        public ActionResult DetailsFlight(int id)
        {
            try
            {
                var entity = _service.Flights.GetById(id);
                var model = new PassengerFlightViewModel();
                model.Flight = Mapper.Map<PassengerFlight, PassengerFlightModel>(entity);
                model.Plane = Mapper.Map<PassengerPlane, PassengerPlaneModel>(entity.Plane);
                model.Plane.Type = Mapper.Map<PassengerPlaneType, PassengerPlaneTypeModel>(entity.Plane.Type);
                model.Crew = Mapper.Map<Crew, CrewModel>(entity.Crew);
                model.Crew.Pilots = new List<PilotModel>();
                foreach (var pilot in entity.Crew.Pilots)
                {
                    model.Crew.Pilots.Add(Mapper.Map<Pilot, PilotModel>(pilot.Pilot));
                }
                model.Crew.Employees = new List<EmployeeModel>();
                foreach (var employee in entity.Crew.Employees)
                {
                    model.Crew.Employees.Add(Mapper.Map<Employee, EmployeeModel>(employee.Employee));
                }
                model.Airline = Mapper.Map<Airline, AirlineModel>(entity.Airline);


                model.Tickets = new List<PassengerTicketModel>();
                foreach (var ticket in entity.Tickets)
                {
                    model.Tickets.Add(Mapper.Map<PassengerTicket, PassengerTicketModel>(ticket));
                }
                model.IsPlaneError = model.Plane.Status == PlaneStatus.Repair;
                model.IsCrewError = !_service.CheckCrewCount(entity, entity.Crew);
                if (model.IsPlaneError)
                {

                    ModelState.AddModelError("Plane",
                        "Plane, setted on this flight on repair.\n Set another plane, or check status of current plane");
                }
                if (model.IsCrewError)
                {
                    ModelState.AddModelError("Crew",
                        "Crew is too small for plane on this flight.\n Set another plane or another team.\n Also you can add members to a crew.");

                }
                ViewBag.Crews = new SelectList(_bllUnit.CrewService.Crews.GetAll().Select(c => new CrewModel()
                {
                    Id = c.Id,
                    Title = c.Title
                }).ToList(), "Id", "Title");
                ViewBag.Planes =
                    new SelectList(
                        _service.Planes.GetAll()
                            .Where(s => s.Status != PlaneStatus.Repair)
                            .Select(s => s.Type.Title + "/" + s.Id)
                            .ToList());
                return View(model);
            }
            catch (Exception)
            {
                return View("_Error");
            }
        }


        /*********Planes*********/
        public ActionResult IndexPlanes()
        {
            var model = new List<PassengerPlaneModel>();
            foreach (var plane in _service.Planes.GetAll())
            {
                model.Add(Mapper.Map<PassengerPlane, PassengerPlaneModel>(plane));
            }
            foreach (var planeModel in model)
            {
                planeModel.Type =
                    Mapper.Map<PassengerPlaneType, PassengerPlaneTypeModel>
                        (_service.PlaneTypes.GetById(planeModel.TypeId));
            }
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult CreatePlane()
        {
            ViewBag.Types = new SelectList(_service.PlaneTypes.GetAll().Select(a => new PassengerPlaneTypeModel()
            {
                Id = a.Id,
                Title = a.Title
            }).ToList(), "Id", "Title");
            ViewBag.Airports = new SelectList(_bllUnit.AirlineService.Airports.GetAll().Select(a => new AirportModel()
            {
                Id = a.Id,
                Name = a.Name
            }).ToList(), "Id", "Name");
            return View(new PassengerPlaneModel());
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult CreatePlane(PassengerPlaneModel passengerPlane)
        {
            var type = _service.PlaneTypes.GetById(passengerPlane.TypeId);
            var airport = _bllUnit.AirlineService.Airports.GetById(passengerPlane.AirportId);
            var plane = new PassengerPlane()
            {
                Type = type,
                HomeAirport = airport,
                Status = PlaneStatus.Reserve
            };
            _service.Planes.Insert(plane);
            return RedirectToAction("IndexPlanes");
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult DeletePlane(int id)
        {
            _service.Planes.Delete(id);
            return RedirectToAction("IndexPlanes");
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult EditPlane(int id, PlaneStatus status)
        {
            try
            {
                var plane = _service.Planes.GetById(id);
                plane.Status = status;
                _service.Planes.Update(plane);
                _bllUnit.Save();
                return RedirectToAction("IndexPlanes");
            }
            catch (Exception)
            {
                return View("_Error");
            }
        }

        /*************Plane Types*/
        [AllowAnonymous]
        public ActionResult IndexPlaneTypes()
        {
            var model = new List<PassengerPlaneTypeModel>();
            foreach (var planeType in _service.PlaneTypes.GetAll())
            {
                model.Add(Mapper.Map<PassengerPlaneType, PassengerPlaneTypeModel>(planeType));
            }
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult DetailsPlaneType(int id)
        {
            try
            {
                var model = Mapper.Map<PassengerPlaneType, PassengerPlaneTypeModel>(_service.PlaneTypes.GetById(id));
                return View(model);
            }
            catch (Exception)
            {
                return View("_Error");
            }
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult CreatePlaneType()
        {
            return View(new PassengerPlaneTypeModel());
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult CreatePlaneType(PassengerPlaneTypeModel planeType)
        {
            if (!ModelState.IsValid)
            {
                return View(planeType);
            }
            var entity = Mapper.Map<PassengerPlaneTypeModel, PassengerPlaneType>(planeType);
            _service.PlaneTypes.Insert(entity);
            return RedirectToAction("IndexPlaneTypes");
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult EditPlaneType(int id)
        {
            try
            {
                var model = Mapper.Map<PassengerPlaneType, PassengerPlaneTypeModel>(_service.PlaneTypes.GetById(id));
                return View(model);
            }
            catch (Exception)
            {
                return View("_Error");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult EditPlaneType(PassengerPlaneTypeModel planeType)
        {
            var entity = Mapper.Map<PassengerPlaneTypeModel, PassengerPlaneType>(planeType);
            entity.Id = planeType.Id;
            _service.PlaneTypes.Update(entity);
            return RedirectToAction("IndexPlaneTypes");
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult DeletePlaneType(int id)
        {
            _service.PlaneTypes.Delete(id);
            return RedirectToAction("IndexPlaneTypes");
        }

        public ActionResult PartialCrew(int flightId, int crewId)
        {
            var flight = _service.Flights.GetById(flightId);
            var entity = _bllUnit.CrewService.Crews.GetById(crewId);
            _service.SetCrew(flight, entity);
            _service.Flights.Update(flight);
            _bllUnit.Save();
            var model = Mapper.Map<Crew, CrewModel>(entity);
            model.Pilots = new List<PilotModel>();
            foreach (var pilot in entity.Pilots)
            {
                model.Pilots.Add(Mapper.Map<Pilot, PilotModel>(pilot.Pilot));
            }
            model.Employees = new List<EmployeeModel>();
            foreach (var employee in entity.Employees)
            {
                model.Employees.Add(Mapper.Map<Employee, EmployeeModel>(employee.Employee));
            }
            return PartialView("PartialViews/CrewDetails", model);
        }


        /****Tickets*/

        [HttpGet]
        [Authorize]
        public ActionResult CreateTicket(int id)
        {
            var flight = _service.Flights.GetById(id);
            ViewBag.RouteNodes = new SelectList(flight.Airline.Nodes.Select(r => new RouteNodeModel()
            {
                Airport = r.Airport.Name,
                NumberInRoute = r.NumberInRoute
            }), "NumberInRoute", "Airport");
            return View(new PassengerTicketModel()
            {
                FlightId = flight.Id,
                StartPoint = 1,
                EndPoint = flight.Airline.Nodes.Count
            });
        }

        [HttpPost]
        [Authorize]
        public ActionResult CreateTicket(PassengerTicketModel ticket)
        {
            var flight = _service.Flights.GetById(ticket.FlightId);
            var entity = new PassengerTicket()
            {
                PassengerName = ticket.PassengerName,
                Flight = _service.Flights.GetById(ticket.FlightId),
                StartPoint = ticket.StartPoint,
                EndPoint = ticket.EndPoint,
                Class = ticket.Class
            };
            var checkTicket = _service.CheckTicket(entity);
            if (!checkTicket.Item1)
            {
                ViewBag.RouteNodes = new SelectList(flight.Airline.Nodes.Select(r => new RouteNodeModel()
                {
                    Airport = r.Airport.Name,
                    NumberInRoute = r.NumberInRoute
                }), "NumberInRoute", "Airport");
                ModelState.AddModelError("", checkTicket.Item2);
                return View(ticket);
            }
            _service.Tickets.Insert(entity);
            if (ticket.StartPoint == 1)
            {
                switch (ticket.Class)
                {
                    case PassengerTicketClass.Econom:
                        flight.EconomyClassPlacesLeft--;
                        break;
                    case PassengerTicketClass.Business:
                        flight.BusinessClassPlacesLeft--;
                        break;
                    case PassengerTicketClass.FirstClass:
                        flight.FirstClassPlacesLeft--;
                        break;
                }
                _service.Flights.Update(flight);
                _bllUnit.Save();
            }
            return RedirectToAction("DetailsFlight", new { id = flight.Id });
        }

        [Authorize]
        public ActionResult DeleteTicket(int id)
        {
            var flightId = _service.Tickets.GetById(id).Flight.Id;
            _service.Tickets.Delete(id);
            return RedirectToAction("DetailsFlight", new { id = flightId });
        }

        [Authorize]
        public ActionResult PushFlightStatus(int id)
        {
            _service.PushFlightStatus(_service.Flights.GetById(id));
            return RedirectToAction("DetailsFlight", new { id });
        }

        [HttpPost]
        [Authorize]
        public ActionResult SetCrew(int flightId, int crewId)
        {
            var flight = _service.Flights.GetById(flightId);
            var crew = _bllUnit.CrewService.Crews.GetById(crewId);
            _service.SetCrew(flight, crew);
            _service.Flights.Update(flight);
            _bllUnit.Save();
            return RedirectToAction("DetailsFlight", new { id = flight.Id });
        }

        [HttpPost]
        [Authorize]
        public ActionResult SetPlane(int flightId, string newPlane)
        {
            var flight = _service.Flights.GetById(flightId);
            var plane = _service.Planes.GetById(int.Parse(newPlane.Split('/')[1]));
            _service.SetPlane(flight, plane);
            _service.Flights.Update(flight);
            _bllUnit.Save();
            return RedirectToAction("DetailsFlight", new { id = flight.Id });
        }

        [AllowAnonymous]
        public ActionResult SearchFlightById(int flightId)
        {
            if (_service.Flights.GetById(flightId) != null)
                return RedirectToAction("DetailsFlight", new { id = flightId });
            return RedirectToAction("ErrorSearch", "Search", new { message = $"Flight with id {flightId} not found" });
        }

        [AllowAnonymous]
        public ActionResult SearchFlightByParams(int startPoint, int endPoint, DateTime date)
        {
            if (startPoint == endPoint)
            {
                return RedirectToAction("ErrorSearch", "Search",
                   new
                   {
                       message = "Error in input parameters. Departure airport the same with destination. Fix it and try again"
                   });
            }
            var flights = _service.SearchForFlights(startPoint, endPoint, date).ToList();
            if (!flights.Any())
            {
                var start = _bllUnit.AirlineService.Airports.GetById(startPoint);
                var end = _bllUnit.AirlineService.Airports.GetById(endPoint);
                return RedirectToAction("ErrorSearch", "Search",
                    new
                    {
                        message =
                        "No flights found by your request:\n" +
                        $"{start.Name} ({start.City}) -> {end.Name} ({end.City}) on {date.ToShortDateString()}"
                    });
            }
            var model = new List<PassengerFlightModel>();
            foreach (var flight in flights)
            {
                model.Add(Mapper.Map<PassengerFlight, PassengerFlightModel>(flight));
            }
            return View("IndexFlights", model);
        }
    }
}