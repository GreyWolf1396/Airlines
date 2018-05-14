using System;
using System.Collections.Generic;
using System.Linq;
using Contracts.DomainEntities.Cargo_flights;
using Contracts.DomainEntities.Crews;
using Contracts.Enums;

namespace BLL.Services.CargoFlights
{
    public partial class CargoFlightService
    {
        /// <summary>
        /// Check posibility of adding ticket to database.
        /// </summary>
        /// <param name="ticket">Ticket for check</param>
        /// <returns></returns>
        public (bool result,string message) CheckTicket(CargoTicket ticket)
        {
            if (ticket.StartPoint >= ticket.EndPoint)
                return (false,"Incorrect start and/or end points");
            var capacityOnStart = CapacityOnStart(ticket.StartPoint, ticket.Flight);
            if (ticket.Weight > capacityOnStart)
                return (false, $"Insufficient weight available. Available capacity: {capacityOnStart} kg");
            return (true,"");
        }

        /// <summary>
        /// Check, that crew is matching for flight
        /// </summary>
        /// <param name="flight"></param>
        /// <param name="crew"></param>
        /// <returns></returns>
        public bool CheckCrewCount(CargoFlight flight, Crew crew)
        {
            if (crew.CrewCount < flight.Plane.Type.MinimalCrew)
            {
                flight.CrewError = true;
                return false;
            }
            flight.CrewError = false;
            return true;
        }

        /// <summary>
        /// Calculating a free capacity of flight in airport on start node.
        /// </summary>
        /// <param name="startPoint">Number of start node</param>
        /// <param name="flight">Flight for calculate</param>
        /// <returns></returns>
        private double CapacityOnStart(int startPoint, CargoFlight flight)
        {
            var result = flight.Plane.Type.CarryingCapacity;
            for (int i = 1; i <= startPoint; i++)
            {
                result -= flight.Tickets
                    .Where(t => t.StartPoint == i)
                    .ToList().Sum(t => t.Weight);
                result += flight.Tickets
                    .Where(t => t.EndPoint == i)
                    .ToList().Sum(t => t.Weight);
            }
            return result;
        }

        /// <summary>
        /// Moving flight status to the next state.
        /// </summary>
        /// <param name="flight"></param>
        /// <returns></returns>
        public bool PushFlightStatus(CargoFlight flight)
        {
            flight.Status++;
            if (flight.Status==FlightStatus.Finished)
                _bllUnit.CrewService.UpdatePilotCargoExperience(flight.Crew,flight.Plane,flight.Airline.HoursTaken);
            Flights.Update(flight);
            _bllUnit.Save();
            return true;
        }

        /// <summary>
        /// Check, the plane is available
        /// </summary>
        /// <param name="plane"></param>
        /// <returns></returns>
        public bool CheckPlaneAvailable(CargoPlane plane)
        {
            return plane.Status != PlaneStatus.Repair;
        }

        /// <summary>
        /// Set crew to the flight
        /// </summary>
        /// <param name="flight"></param>
        /// <param name="crew"></param>
        public void SetCrew(CargoFlight flight, Crew crew)
        {
            CheckCrewCount(flight, crew);
            flight.Crew = crew;
        }

        /// <summary>
        /// Set plane to the flight
        /// </summary>
        /// <param name="flight"></param>
        /// <param name="plane"></param>
        public void SetPlane(CargoFlight flight, CargoPlane plane)
        {
            flight.Plane = plane;
            CheckCrewCount(flight, flight.Crew);
        }

        /// <summary>
        /// Search for flights by Id of departure airport, Id of destination airport, date od flight
        /// </summary>
        /// <param name="startPoint"></param>
        /// <param name="endPoint"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public IEnumerable<CargoFlight> SearchForFlights(int startPoint, int endPoint, DateTime date)
        {
            var airlines = _bllUnit.AirlineService.SearchAirlinesByAirports(startPoint, endPoint);
            var flights = Flights.GetAll().Where(f => airlines.Contains(f.Airline) && f.StartDate == date);
            return flights;
        }
    }
}
