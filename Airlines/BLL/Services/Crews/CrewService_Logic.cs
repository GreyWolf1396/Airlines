using System.Linq;
using Contracts.DomainEntities.Cargo_flights;
using Contracts.DomainEntities.Crews;
using Contracts.DomainEntities.Passenger_flights;

namespace BLL.Services.Crews
{
    public partial class CrewService
    {
        /// <summary>
        /// Check that crew is contains all required roles
        /// </summary>
        /// <param name="crew"></param>
        /// <returns></returns>
        public bool CheckCrewMembers(Crew crew)
        {
            foreach (var role in CrewRoles.GetAll())
            {
                if (role.IsRequired)
                {
                    bool roleUsed = crew.Pilots.FirstOrDefault(p => p.Role == role) != null 
                                    || crew.Employees.FirstOrDefault(e => e.Role == role) !=null;
                    if (!roleUsed) return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Calculating the count of crew members after add/remove member
        /// </summary>
        /// <param name="crew"></param>
        public void UpdateCrewCount(Crew crew)
        {
            crew.CrewCount = crew.Pilots.Count + crew.Employees.Count;
            Crews.Update(crew);
            _bllUnit.Save();
        }

        /// <summary>
        /// Updating pilot experience of flight on cargo planes
        /// </summary>
        /// <param name="crew"></param>
        /// <param name="plane"></param>
        /// <param name="hours"></param>
        public void UpdatePilotCargoExperience(Crew crew, CargoPlane plane, int hours)
        {
            foreach (var crewPilot in crew.Pilots)
            {
                var entry = PilotCargoExp.GetAll().FirstOrDefault(p => p.Pilot == crewPilot.Pilot && p.Plane == plane.Type);
                if (entry == null)
                    PilotCargoExp.Insert(new PilotsCargoExperience()
                    {
                        Pilot = crewPilot.Pilot,
                        Plane = plane.Type,
                        HoursOfFlights = hours
                    });
                else
                    entry.HoursOfFlights += hours;
                crewPilot.Pilot.HoursOfExperience += hours;
            }
        }

        /// <summary>
        /// Updating pilot experience of flight on passenger planes
        /// </summary>
        /// <param name="crew"></param>
        /// <param name="plane"></param>
        /// <param name="hours"></param>
        public void UpdatePilotPassengerExperience(Crew crew, PassengerPlane plane, int hours)
        {
            foreach (var crewPilot in crew.Pilots)
            {
                var entry = PilotPassengerExp.GetAll().FirstOrDefault(p => p.Pilot == crewPilot.Pilot && p.Plane == plane.Type);
                if (entry == null)
                    PilotPassengerExp.Insert(new PilotsPassengerExperience()
                    {
                        Pilot = crewPilot.Pilot,
                        Plane = plane.Type,
                        HoursOfFlights = hours
                    });
                else
                    entry.HoursOfFlights += hours;
                crewPilot.Pilot.HoursOfExperience += hours;
            }
        }

        /// <summary>
        /// Removing Pilot from Crew
        /// </summary>
        /// <param name="pilotId"></param>
        /// <param name="crewId"></param>
        public void RemovePilotFromCrew(int pilotId, int crewId)
        {
            var pilotInCrew = CrewPilots.GetAll().FirstOrDefault(p => p.Crew.Id == crewId && p.Pilot.Id == pilotId);
            if (pilotInCrew != null)
                _unitOfWork.PilotInCrewRepository.Delete(pilotInCrew);
            UpdateCrewCount(Crews.GetById(crewId));
        }

        /// <summary>
        /// Removing Employee from crew
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="crewId"></param>
        public void RemoveEmployeeFromCrew(int employeeId, int crewId)
        {
            var employeeInCrew = CrewEmployees.GetAll().FirstOrDefault(p => p.Crew.Id == crewId && p.Employee.Id == employeeId);
            if (employeeInCrew != null)
                _unitOfWork.EmployeeInCrewRepository.Delete(employeeInCrew);
            UpdateCrewCount(Crews.GetById(crewId));
        }
    }
}

