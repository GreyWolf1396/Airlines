using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using BLL;
using BLL.Services.Airlines;
using Contracts.DomainEntities.Airlines;
using Contracts.DomainEntities.Cargo_flights;
using Contracts.Enums;
using Grey_Airlines.Models;
using Grey_Airlines.Models.AirlineModels;
using Grey_Airlines.Models.CargoFlightModels;
using Grey_Airlines.Models.ViewModels;

namespace Grey_Airlines.Controllers
{
    public class AirlineController : Controller
    {
        private readonly BllUnit _bllUnit;
        private readonly AirlineService _service;

        public AirlineController()
        {
            _bllUnit = new BllUnit();
            _service = _bllUnit.AirlineService;
        }

        
        public ActionResult IndexAirlines()
        {
            var model = new List<AirlineModel>();
            foreach (var airline in _service.Airlines.GetAll())
            {
                model.Add(Mapper.Map<Airline,AirlineModel>(airline));
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult DetailsAirline(int id)
        {
            try
            {
                var model = new AirlineViewModel()
                {
                    Airline = Mapper.Map<Airline, AirlineModel>(_service.Airlines.GetById(id))
                };
                model.RouteNodes = model.Airline.RouteNodes;
                model.CargoFlights =
                    _bllUnit.CargoFlightService.Flights.GetAll()
                        .Where(p => p.Airline.Id == model.Airline.Id && p.Status < FlightStatus.Finished)
                        .Select(f => new CargoFlightModel()
                        {
                            Id = f.Id,
                            StartDate = f.StartDate,
                            Status = f.Status
                        }).ToList();
                model.PassengerFlihts =
                    _bllUnit.PassengerFlightService.Flights.GetAll()
                        .Where(p => p.Airline.Id == model.Airline.Id && p.Status < FlightStatus.Finished)
                        .Select(f => new PassengerFlightModel()
                        {
                            Id = f.Id,
                            StartDate = f.StartDate,
                            Status = f.Status
                        }).ToList();
                return View(model);
            }
            catch (Exception)
            {
                return View("_Error");
            }
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult EditAirline(int id)
        {
            try
            {
                var model = new AirlineViewModel()
                {
                    Airline = Mapper.Map<Airline, AirlineModel>(_service.Airlines.GetById(id))
                };
                model.RouteNodes = model.Airline.RouteNodes;
                return View(model);
            }
            catch (Exception)
            {
                return View("_Error");
            }
        }
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult EditAirline(AirlineViewModel airlineView)
        {
            if (!ModelState.IsValid)
            {
                return View(airlineView);
            }

            var updatedAirline = _service.Airlines.GetById(airlineView.Airline.Id);

            updatedAirline.Title = airlineView.Airline.Title;
            updatedAirline.HoursTaken = airlineView.Airline.HoursTaken;
            updatedAirline.Periodicity = airlineView.Airline.Periodicity;
            updatedAirline.BaseTicketValue = airlineView.Airline.BaseTicketValue;

            _service.Airlines.Update(updatedAirline);
            _bllUnit.Save();
            return RedirectToAction("DetailsAirline",new {id=airlineView.Airline.Id});
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteAirline(int id)
        {
            _service.Airlines.Delete(id);
            return RedirectToAction("IndexAirlines");
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult CreateAirline()
        {
            return View(new AirlineModel());
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult CreateAirline(AirlineModel airline)
        {
            var entity = Mapper.Map<AirlineModel, Airline>(airline);
            _service.Airlines.Insert(entity);
            return RedirectToAction("IndexAirlines");
        }


        /*Airports*/
        public ActionResult IndexAirports()
        {
            var model = new List<AirportModel>();
            foreach (var airport in _service.Airports.GetAll())
            {
                model.Add(Mapper.Map<Airport, AirportModel>(airport));
            }
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult EditAirport(int id)
        {
            var model = Mapper.Map<Airport, AirportModel>(_service.Airports.GetById(id));
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult EditAirport(AirportModel airport)
        {
            var entity = _service.Airports.GetById(airport.Id);

            entity.Name = airport.Name;
            entity.City = airport.City;
            entity.Codename = airport.CodeName;
            entity.Class = airport.Class;

            _service.Airports.Update(entity);
            _bllUnit.Save();
            return RedirectToAction("IndexAirports");
        }

        [HttpGet]
        [Authorize(Roles="Administrator")]
        public ActionResult CreateAirport()
        {
            return View(new AirportModel());
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult CreateAirport( AirportModel airport)
        {
            var entity = Mapper.Map<AirportModel, Airport>(airport);
            _service.Airports.Insert(entity);
            return RedirectToAction("IndexAirports");
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteAirport(int id)
        {
            _service.Airports.Delete(id);
            return RedirectToAction("IndexAirports");
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult AddRouteNode(int id)
        {
            ViewBag.Airports = new SelectList(_service.Airports.GetAll().Select(a => new AirportModel()
            {
                Id = a.Id,
                Name = a.Name
            }).ToList(), "Id", "Name");
            return View(new RouteNodeModel()
            {
                AirlineId = id
            });
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult AddRouteNode(RouteNodeModel routeNode)
        {
            if (routeNode.Arriving == null && routeNode.Departure == null)
            {
                ModelState.AddModelError("","Node must have time of arriving or departure. Both of them can't be empty");
            }
            if (routeNode.NumberInRoute <= 0)
            {
                ModelState.AddModelError("","Node can't have number below 1");
            }
            var airline = _service.Airlines.GetById(routeNode.AirlineId);
            if (airline.Nodes.FirstOrDefault(r => r.NumberInRoute == routeNode.NumberInRoute)!=null)
            {
                ModelState.AddModelError("","Node with this number in route is already exists");
            }
            if (!ModelState.IsValid)
            {
                ViewBag.Airports = new SelectList(_service.Airports.GetAll().Select(a => new AirportModel()
                {
                    Id = a.Id,
                    Name = a.Name
                }).ToList(), "Id", "Name");
                return View(routeNode);
            }
            var entity = new RouteNode()
            {
                Airline = _service.Airlines.GetById(routeNode.AirlineId),
                Airport = _service.Airports.GetById(routeNode.AirportId),
                NumberInRoute = routeNode.NumberInRoute,
                Arriving = routeNode.Arriving,
                Departure = routeNode.Departure
            };
            _service.Routes.Insert(entity);
            return RedirectToAction("DetailsAirline", new {id = routeNode.AirlineId});
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteRouteNode(int id)
        {
            int airlineId = _service.Routes.GetById(id).Airline.Id;
            _service.Routes.Delete(id);
            return RedirectToAction("DetailsAirline", new {id = airlineId});
        }
    }
}