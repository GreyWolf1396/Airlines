using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using BLL;
using BLL.Services.Crews;
using Contracts.DomainEntities.Crews;
using Grey_Airlines.Models.AirlineModels;
using Grey_Airlines.Models.CrewModels;
using Grey_Airlines.Models.ViewModels;

namespace Grey_Airlines.Controllers
{
    [Authorize]
    public class CrewController : Controller
    {
        private readonly BllUnit _bllUnit;
        private readonly CrewService _service;

        public CrewController()
        {
            _bllUnit = new BllUnit();
            _service = _bllUnit.CrewService;
        }
        
        // Crew actions
        public ActionResult IndexCrew()
        {
            var model = new List<CrewModel>();
            foreach (var crew in _service.Crews.GetAll())
            {
                model.Add(Mapper.Map<Crew,CrewModel>(crew));
            }
            return View(model);
        }

        public ActionResult DetailsCrew(int id)
        {
            var entity = _service.Crews.GetById(id);
            var model = new CrewViewModel
            {
                Crew = Mapper.Map<Crew, CrewModel>(entity),
                PilotsInCrew = new List<PilotModel>(),
                EmployeesInCrew = new List<EmployeeModel>()
            };
            var crewPilots = _service.CrewPilots.GetAll().Where(p => p.Crew == entity);
            var crewEmployees = _service.CrewEmployees.GetAll().Where(p => p.Crew == entity).ToList();
            foreach (var crewPilot in crewPilots)
            {
                var pilotModel = Mapper.Map<Pilot, PilotModel>(crewPilot.Pilot);
                pilotModel.CrewRole = crewPilot.Role.Title;
                model.PilotsInCrew.Add(pilotModel);
            }
            foreach (var crewEmployee in crewEmployees)
            {
                var employeeModel = Mapper.Map<Employee, EmployeeModel>(crewEmployee.Employee);
                employeeModel.CrewRole = crewEmployee.Role.Title;
                model.EmployeesInCrew.Add(employeeModel);
            }
            model.IsCrewFull = _service.CheckCrewMembers(entity);
            if (!model.IsCrewFull)
            {
                ModelState.AddModelError("","Crew is not full! Crew must contain: captain, second pilot, air mechanic, navigator, main steward.");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult CreateCrew()
        {
            var airports = _bllUnit.AirlineService.Airports.GetAll().Select(a => new AirportModel()
            {
                Id = a.Id,
                Name = a.Name
            });
            ViewBag.Airports = new SelectList(airports, "Id", "Name");
            return View(new CrewModel());
        }

        [HttpPost]
        public ActionResult CreateCrew(CrewModel crew)
        {
            if (!ModelState.IsValid)
            {
                var airports = _bllUnit.AirlineService.Airports.GetAll().Select(a => new AirportModel()
                {
                    Id = a.Id,
                    Name = a.Name
                });
                ViewBag.Airports = new SelectList(airports, "Id", "Name");
                return View(crew);
            }
            var entity = Mapper.Map<CrewModel, Crew>(crew);
            entity.HomeAirport = _bllUnit.AirlineService.Airports.GetById(crew.HomeId);
            entity.CrewCount = 0;
            _service.Crews.Insert(entity);
            return RedirectToAction("IndexCrew");
        }

        public ActionResult DeleteCrew(int id)
        {
            _service.Crews.Delete(id);
            return RedirectToAction("IndexCrew");
        }



        /*******Pilots actions*****/
        public ActionResult IndexPilots()
        {
            var model = new List<PilotModel>();
            foreach (var pilot in _service.Pilots.GetAll())
            {
                model.Add(Mapper.Map<Pilot,PilotModel>(pilot));
            }
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult CreatePilot()
        {
            return View(new PilotModel());
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult CreatePilot(PilotModel pilot)
        {
            if (!ModelState.IsValid)
            {
                return View(pilot);
            }
            var entity = new Pilot()
            {
                Name = pilot.Name,
                Education = pilot.Education,
                HoursOfExperience = 0,
                LicenseNumber = pilot.LicenseNumber
            };
            _service.Pilots.Insert(entity);
            return RedirectToAction("IndexPilots");
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult EditPilot(int id)
        {
            var model = Mapper.Map<Pilot, PilotModel>(_service.Pilots.GetById(id));
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult EditPilot(PilotModel pilot)
        {
            var entity = _service.Pilots.GetAll().First(e => e.Id == pilot.Id);
            entity.Name = pilot.Name;
            entity.Education = pilot.Education;
            entity.LicenseNumber = pilot.LicenseNumber;
            entity.HoursOfExperience = pilot.HoursOfExperience;
            _service.Pilots.Update(entity);
            _bllUnit.Save();
            return RedirectToAction("DetailsPilot",new {id = pilot.Id});
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult DeletePilot(int id)
        {
            _service.Pilots.Delete(id);
            return RedirectToAction("IndexPilots");
        }

        [Authorize]
        public ActionResult DetailsPilot(int id)
        {
            var entity = _service.Pilots.GetById(id);
            var model = new PilotViewModel()
            {
                Pilot = Mapper.Map<Pilot, PilotModel>(entity),
                PassengerExperience =
                    _service.PilotPassengerExp.GetAll()
                        .Where(p => p.Pilot == entity)
                        .ToDictionary(k => k.Plane.Title, k => k.HoursOfFlights),
                CargoExperience =
                    _service.PilotCargoExp.GetAll()
                        .Where(p => p.Pilot == entity)
                        .ToDictionary(k => k.Plane.Title, k => k.HoursOfFlights)
            };
            return View(model);
        }


        /*******Employees actions*******/
        public ActionResult IndexEmployees()
        {
            var model = new List<EmployeeModel>();
            foreach (var employee in _service.Employees.GetAll())
            {
                model.Add(Mapper.Map<Employee, EmployeeModel>(employee));
            }
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult CreateEmployee()
        {
            return View(new EmployeeModel());
        }
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult CreateEmployee(EmployeeModel employee)
        {
            if (!ModelState.IsValid)
            {
                return View(employee);
            }
            var entity = new Employee()
            {
                Name = employee.Name,
                Education = employee.Education,
                EducationCategory = employee.EducationCategory
            };
            _service.Employees.Insert(entity);
            return RedirectToAction("IndexEmployees");
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult EditEmployee(int id)
        {
            var model = Mapper.Map<Employee, EmployeeModel>(_service.Employees.GetById(id));
            return View(model);
        }
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult EditEmployee(EmployeeModel employee)
        {
            var entity = _service.Employees.GetAll().First(e => e.Id == employee.Id);
            entity.Name = employee.Name;
            entity.Education = employee.Education;
            entity.EducationCategory = employee.EducationCategory;
            _service.Employees.Update(entity);
            _bllUnit.Save();
            return RedirectToAction("IndexEmployees");
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteEmployee(int id)
        {
            _service.Employees.Delete(id);
            return RedirectToAction("IndexEmployees");
        }


        /*****Add/delete pilots and employees from crews*****/
        [HttpGet]
        public ActionResult AddPilotToCrew(int id)
        {
            var existingPilots = _service.CrewPilots.GetAll().Where(p => p.Crew.Id == id).Select(p => p.Pilot).ToList();
            var pilots = new List<PilotModel>();
            foreach (var pilot in _service.Pilots.GetAll())
            {
                if (!existingPilots.Contains(pilot))
                    pilots.Add(new PilotModel()
                    {
                        Id = pilot.Id,
                        Name = pilot.Name
                    });
            }
            ViewBag.Pilots = new SelectList(pilots, "Id", "Name");
            ViewBag.Roles = new SelectList(_service.CrewRoles.GetAll()
                .Select(r => new CrewRoleModel()
                {
                    Id = r.Id,
                    Title = r.Title
                }).ToList(), "Id", "Title");
            return View(new CrewPilotModel() {CrewId = id});
        }
        [HttpPost]
        public ActionResult AddPilotToCrew(CrewPilotModel crewPilot)
        {
            var crew = _service.Crews.GetById(crewPilot.CrewId);
            var entity = new PilotInCrew()
            {
                Crew = crew,
                Pilot = _service.Pilots.GetById(crewPilot.PilotId),
                Role = _service.CrewRoles.GetById(crewPilot.CrewRoleId)
            };
            _service.CrewPilots.Insert(entity);
            _service.UpdateCrewCount(crew);
            return RedirectToAction("DetailsCrew", new {id = crewPilot.CrewId});
        }

        public ActionResult DeletePilotFromCrew(int pilotId, int crewId)
        {
            _service.RemovePilotFromCrew(pilotId,crewId);
            return RedirectToAction("DetailsCrew", new {id = crewId});
        }

        [HttpGet]
        public ActionResult AddEmployeeToCrew(int id)
        {
            var existingEmployees = _service.CrewEmployees.GetAll().Where(p => p.Crew.Id == id).Select(p => p.Employee).ToList();
            var employees = new List<EmployeeModel>();
            foreach (var employee in _service.Employees.GetAll())
            {
                if (!existingEmployees.Contains(employee))
                    employees.Add(new EmployeeModel()
                    {
                        Id = employee.Id,
                        Name = employee.Name
                    });
            }
            ViewBag.Employees = new SelectList(employees, "Id", "Name");
            ViewBag.Roles = new SelectList(_service.CrewRoles.GetAll()
                .Select(r => new CrewRoleModel()
                {
                    Id = r.Id,
                    Title = r.Title
                }).ToList(), "Id", "Title");
            return View(new CrewEmployeeModel() { CrewId = id });
        }
        [HttpPost]
        public ActionResult AddEmployeeToCrew(CrewEmployeeModel crewEmployee)
        {
            var crew = _service.Crews.GetById(crewEmployee.CrewId);
            var entity = new EmployeeInCrew()
            {
                Crew = crew,
                Employee = _service.Employees.GetById(crewEmployee.EmployeeId),
                Role = _service.CrewRoles.GetById(crewEmployee.CrewRoleId)
            };
            _service.CrewEmployees.Insert(entity);
            _service.UpdateCrewCount(crew);
            return RedirectToAction("DetailsCrew", new { id = crewEmployee.CrewId });
        }

        public ActionResult DeleteEmployeeFromCrew(int employeeId, int crewId)
        {
            _service.RemoveEmployeeFromCrew(employeeId, crewId);
            _service.UpdateCrewCount(_service.Crews.GetById(crewId));
            return RedirectToAction("DetailsCrew", new { id = crewId });
        }
    }
}