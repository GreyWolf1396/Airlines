using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using BLL;
using BLL.Services;
using Contracts.DomainEntities.Airlines;
using Contracts.DomainEntities.Cargo_flights;
using Contracts.DomainEntities.Crews;
using Contracts.Enums;
using Grey_Airlines.Models;

namespace Grey_Airlines.Controllers
{
    [AllowAnonymous]
    public class CargoFlightController : Controller
    {
        private readonly BllUnit _bllUnit;
        private readonly CargoFlightService _service;

        public CargoFlightController()
        {
            _bllUnit = new BllUnit();
            _service = _bllUnit.CargoFlightService;
        }
        // GET: CargoFlight
    
        public ActionResult IndexFlights()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<CargoFlight, CargoFlightModel>()
               .ForMember(m => m.Airline, opt => opt.MapFrom(o => o.Airline.Title))
               .ForMember(m => m.Crew, opt => opt.MapFrom(o => o.Crew.Title))
               .ForMember(m=>m.Plane, opt => opt.MapFrom(o => o.Plane.Type.Title+"/"+o.Plane.Id)));
            var model = new List<CargoFlightModel>();
            foreach (var flight in _service.Flights.GetAll())
            {
                if(flight.Status!=FlightStatus.Finished)
                    model.Add(Mapper.Map<CargoFlight, CargoFlightModel>(flight));
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
            return View(new CargoFlightModel());
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult CreateFlight(CargoFlightModel cargoFlight)
        {
            var airline = _bllUnit.AirlineService.Airlines.GetAll().FirstOrDefault(a => a.Title.Equals(cargoFlight.Airline));
            var crew = _bllUnit.CrewService.Crews.GetAll().FirstOrDefault(c => c.Title.Equals(cargoFlight.Crew));
            var plane = _service.Planes.GetAll().FirstOrDefault(p => p.Id == int.Parse(cargoFlight.Plane.Split('/')[1]));
            var flightEntity = new CargoFlight()
            {
                Airline = airline,
                Crew = crew,
                Plane = plane,
                WeightLeft = plane.Type.CarryingCapacity,
                StartDate = cargoFlight.StartDate,
                EndDate = cargoFlight.EndDate,
                Status = FlightStatus.Created
            };
            _service.Flights.Insert(flightEntity);
            return RedirectToAction("IndexFlights");
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult DetailsFlight(int id)
        {
            try
            {
                var entity = _service.Flights.GetById(id);
                var model = new CargoFlightViewModel();
                Mapper.Initialize(cfg => cfg.CreateMap<CargoFlight, CargoFlightModel>()
                    .ForMember(m => m.Airline, opt => opt.Ignore())
                    .ForMember(m => m.Crew, opt => opt.Ignore())
                    .ForMember(m => m.Plane, opt => opt.Ignore()));
                model.Flight = Mapper.Map<CargoFlight, CargoFlightModel>(entity);
                Mapper.Initialize(cfg => cfg.CreateMap<CargoPlane, CargoPlaneModel>()
                    .ForMember(m => m.Type, opt => opt.Ignore())
                    .ForMember(m => m.HomeAirport, opt => opt.MapFrom(o => o.HomeAirport.Name)));
                model.Plane = Mapper.Map<CargoPlane, CargoPlaneModel>(entity.Plane);
                Mapper.Initialize(cfg => cfg.CreateMap<CargoPlaneType, CargoPlaneTypeModel>());
                model.Plane.Type = Mapper.Map<CargoPlaneType, CargoPlaneTypeModel>(entity.Plane.Type);
                Mapper.Initialize(
                    cfg =>
                        cfg.CreateMap<Crew, CrewModel>()
                            .ForMember(t => t.Pilots, opt => opt.Ignore())
                            .ForMember(t => t.Employees, opt => opt.Ignore()));
                model.Crew = Mapper.Map<Crew, CrewModel>(entity.Crew);
                model.Crew.Pilots = new List<PilotModel>();
                Mapper.Initialize(cfg => cfg.CreateMap<Pilot, PilotModel>());
                foreach (var pilot in entity.Crew.Pilots)
                {
                    model.Crew.Pilots.Add(Mapper.Map<Pilot, PilotModel>(pilot.Pilot));
                }
                model.Crew.Employees = new List<EmployeeModel>();
                Mapper.Initialize(cfg => cfg.CreateMap<Employee, EmployeeModel>());
                foreach (var employee in entity.Crew.Employees)
                {
                    model.Crew.Employees.Add(Mapper.Map<Employee, EmployeeModel>(employee.Employee));
                }
                Mapper.Initialize(cfg => cfg.CreateMap<Airline, AirlineModel>()
                    .ForMember("RouteNodes",
                        opt => opt.MapFrom(a => a.Nodes.Select(n => new RouteNodeModel()
                        {
                            Airline = n.Airline.Title,
                            Airport = n.Airport.Name,
                            NumberInRoute = n.NumberInRoute,
                            Departure = n.Departure,
                            Arriving = n.Arriving
                        }).ToList())));
                model.Airline = Mapper.Map<Airline, AirlineModel>(entity.Airline);
                Mapper.Initialize(cfg => cfg.CreateMap<CargoTicket, CargoTicketModel>()
                    .ForMember(m => m.FlightId, o => o.MapFrom(t => t.Flight.Id)));
                model.Tickets = new List<CargoTicketModel>();
                foreach (var ticket in entity.Tickets)
                {
                    model.Tickets.Add(Mapper.Map<CargoTicket, CargoTicketModel>(ticket));
                }
                model.IsPlaneError = model.Plane.Status == PlaneStatus.Repair;
                model.IsCrewError = !_service.CheckCrewCount(entity, entity.Crew);
                if (model.IsPlaneError)
                {
                    ModelState.AddModelError("",
                        "Plane, setted on this flight on repair.\n Set another plane, or check status of current plane");
                }
                if (model.IsCrewError)
                {
                    ModelState.AddModelError("",
                        "Crew is too small for plane on this flight.\n Set another plane or another team.\n Also you can add members to a crew.");

                }
                ViewBag.Crews = new SelectList(_bllUnit.CrewService.Crews.GetAll().Select(c => new CrewModel()
                {
                    Id = c.Id,
                    Title = c.Title
                }).ToList(), "Id", "Title");
                ViewBag.Planes = new SelectList(
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

        /******Planes*****/
        public ActionResult IndexPlanes()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<CargoPlane, CargoPlaneModel>()
                .ForMember(m => m.Type, opt => opt.Ignore())
                .ForMember(m => m.HomeAirport, opt => opt.MapFrom(o => o.HomeAirport.Name))
                .ForMember(m => m.TypeId, opt => opt.MapFrom(s => s.Type.Id)));
            var model = new List<CargoPlaneModel>();
            foreach (var plane in _service.Planes.GetAll())
            {
                var planeModel = Mapper.Map<CargoPlane, CargoPlaneModel>(plane);
                model.Add(planeModel);
            }
            Mapper.Initialize(cfg => cfg.CreateMap<CargoPlaneType, CargoPlaneTypeModel>());
            foreach (var planeModel in model)
            {
                planeModel.Type =
                    Mapper.Map<CargoPlaneType, CargoPlaneTypeModel>
                        (_service.PlaneTypes.GetById(planeModel.TypeId));
            }
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult CreatePlane()
        {
            var types = _service.PlaneTypes.GetAll().Select(a => new CargoPlaneTypeModel()
            {
                Id = a.Id,
                Title = a.Title
            }).ToList();
            ViewBag.Types = new SelectList(types, "Id", "Title");
            ViewBag.Airports = new SelectList(_bllUnit.AirlineService.Airports.GetAll().Select(a=>a.Name).ToList());
            return View(new CargoPlaneModel());
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult CreatePlane(CargoPlaneModel cargoPlane)
        {
            var type = _service.PlaneTypes.GetById(cargoPlane.TypeId);
            var airport = _bllUnit.AirlineService.Airports.GetAll().First(a => a.Name.Equals(cargoPlane.HomeAirport));
            var plane = new CargoPlane()
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

        /******Plane types*****/
        public ActionResult IndexPlaneTypes()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<CargoPlaneType, CargoPlaneTypeModel>());
            var model = new List<CargoPlaneTypeModel>();
            foreach (var planeType in _service.PlaneTypes.GetAll())
            {
                model.Add(Mapper.Map<CargoPlaneType, CargoPlaneTypeModel>(planeType));
            }
            return View(model);
        }

        public ActionResult DetailsPlaneType(int id)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<CargoPlaneType, CargoPlaneTypeModel>());
            var model = Mapper.Map<CargoPlaneType, CargoPlaneTypeModel>(_service.PlaneTypes.GetById(id));
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult CreatePlaneType()
        {
            return View(new CargoPlaneTypeModel());
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult CreatePlaneType(CargoPlaneTypeModel planeType)
        {
            if (!ModelState.IsValid)
            {
                return View(planeType);
            }
            Mapper.Initialize(
                cfg => cfg.CreateMap<CargoPlaneTypeModel, CargoPlaneType>().ForMember(m => m.Id, opt => opt.Ignore()));
            var entity = Mapper.Map<CargoPlaneTypeModel, CargoPlaneType>(planeType);
            _service.PlaneTypes.Insert(entity);
            return RedirectToAction("IndexPlaneTypes");
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult EditPlaneType(int id)
        {
            try
            {
                Mapper.Initialize(cfg => cfg.CreateMap<CargoPlaneType, CargoPlaneTypeModel>());
                var model = Mapper.Map<CargoPlaneType, CargoPlaneTypeModel>(_service.PlaneTypes.GetById(id));
                return View(model);
            }
            catch (Exception)
            {
                return View("_Error");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult EditPlaneType(CargoPlaneTypeModel planeType)
        {
            Mapper.Initialize(
               cfg => cfg.CreateMap<CargoPlaneTypeModel, CargoPlaneType>().ForMember(m => m.Id, opt => opt.Ignore()));
            var entity = Mapper.Map<CargoPlaneTypeModel, CargoPlaneType>(planeType);
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
            }),"NumberInRoute","Airport");
            return View(new CargoTicketModel()
            {
                FlightId = flight.Id,
                StartPoint = 1,
                EndPoint = flight.Airline.Nodes.Count
            });
        }

        [HttpPost]
        [Authorize]
        public ActionResult CreateTicket(CargoTicketModel ticket)
        {
            var flight = _service.Flights.GetById(ticket.FlightId);
            var entity = new CargoTicket()
            {
                Title = ticket.Title,
                Flight = _service.Flights.GetById(ticket.FlightId),
                StartPoint = ticket.StartPoint,
                EndPoint = ticket.EndPoint,
                Weight = ticket.Weight
            };
            var checkTicket = _service.CheckTicket(entity);
            if (!checkTicket.Item1)
            {
                ViewBag.RouteNodes = new SelectList(flight.Airline.Nodes.Select(r => new RouteNodeModel()
                {
                    Airport = r.Airport.Name,
                    NumberInRoute = r.NumberInRoute
                }), "NumberInRoute", "Airport");
                ModelState.AddModelError("",checkTicket.Item2);
                return View(ticket);
            }
            _service.Tickets.Insert(entity);
            if (ticket.StartPoint == 1)
            {
                flight.WeightLeft -= entity.Weight;
                _service.Flights.Update(flight);
                _bllUnit.Save();
            }
            return RedirectToAction("DetailsFlight", new {id = flight.Id});
        }

        [Authorize]
        public ActionResult DeleteTicket(int id)
        {
            var flightId = _service.Tickets.GetById(id).Flight.Id;
            _service.Tickets.Delete(id);
            return RedirectToAction("DetailsFlight", new {id = flightId});
        }

        [Authorize]
        public ActionResult PushFlightStatus(int id)
        {
            _service.PushFlightStatus(_service.Flights.GetById(id));
            return RedirectToAction("DetailsFlight", new {id});
        }

        [Authorize]
        public ActionResult SetCrew(int flightId, int crewId)
        {
            var flight = _service.Flights.GetById(flightId);
            var crew = _bllUnit.CrewService.Crews.GetById(crewId);
            _service.SetCrew(flight, crew);
            return RedirectToAction("DetailsFlight", new {id = flight.Id});
        }

        [Authorize]
        public ActionResult SetPlane(int flightId, string newPlane)
        {
            var flight = _service.Flights.GetById(flightId);
            var plane = _service.Planes.GetById(int.Parse(newPlane.Split('/')[1]));
            _service.SetPlane(flight, plane);
            return RedirectToAction("DetailsFlight", new { id = flight.Id });
        }

        public ActionResult SearchFlightById(int flightId)
        {
            if (_service.Flights.GetById(flightId) != null)
                return RedirectToAction("DetailsFlight", new {id = flightId});
            return RedirectToAction("ErrorSearch", "Search", new {message = $"Flight with id {flightId} not found"});
        }

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
                        $"{start.Name} ({start.City})  -> {end.Name} ({end.City}) on {date.ToShortDateString()}"
                    });
            }
            Mapper.Initialize(cfg => cfg.CreateMap<CargoFlight, CargoFlightModel>()
               .ForMember(m => m.Airline, opt => opt.MapFrom(o => o.Airline.Title))
               .ForMember(m => m.Crew, opt => opt.MapFrom(o => o.Crew.Title))
               .ForMember(m => m.Plane, opt => opt.MapFrom(o => o.Plane.Type.Title + "/" + o.Plane.Id)));
            var model = new List<CargoFlightModel>();
            foreach (var flight in flights)
            {
                model.Add(Mapper.Map<CargoFlight, CargoFlightModel>(flight));
            }
            return View("IndexFlights", model);
        }
    }
}