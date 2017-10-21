using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.DomainEntities.Passenger_flights;
using DAL;
using BLL.Services.ServiceBase;
using Contracts.DomainEntities.Crews;
using Contracts.Enums;

namespace BLL.Services
{
    public partial class PassengerFlightService 
    {
        public (bool result, string message) CheckTicket(PassengerTicket ticket)
        {
            if (ticket.StartPoint >= ticket.EndPoint)
                return (false, "Incorrect start and/or end points");
            var placesOnStart = 0;
            var calculated = PlacesOnStart(ticket.StartPoint, ticket.Flight, ticket.Class);
            switch (ticket.Class)
            {
                case PassengerTicketClass.Econom:
                    placesOnStart = calculated.Item1;
                    break;
                case PassengerTicketClass.Business:
                    placesOnStart =calculated.Item2;
                    break;
                case PassengerTicketClass.FirstClass:
                    placesOnStart = calculated.Item3;
                    break;
            }
            if (placesOnStart == 0)
                return (false, $"No free places.Free: economy - {calculated.Item1}; business - {calculated.Item2}; first class - {calculated.Item3}");
            return (true, "");
        }

        private (int econ,int business,int frst) PlacesOnStart(int startPoint, PassengerFlight flight, PassengerTicketClass ticketClass)
        {
            var tClass = ticketClass == PassengerTicketClass.Econom
                ? flight.Plane.Type.EconomyClassPlaces
                : ticketClass == PassengerTicketClass.Business
                    ? flight.Plane.Type.BusinesClassPlaces
                    : flight.Plane.Type.FirstClassPlaces;
            var econ = flight.Plane.Type.EconomyClassPlaces;
            var business = flight.Plane.Type.BusinesClassPlaces;
            var frst = flight.Plane.Type.FirstClassPlaces;
            for (int currentPoint = 1; currentPoint <= startPoint; currentPoint++)
            {
                econ -= flight.Tickets
                    .Where(t => t.StartPoint == currentPoint && t.Class==PassengerTicketClass.Econom)
                    .ToList().Count;
                econ += flight.Tickets
                    .Where(t => t.EndPoint == currentPoint && t.Class==PassengerTicketClass.Econom)
                    .ToList().Count;
                business -= flight.Tickets
                    .Where(t => t.StartPoint == currentPoint && t.Class == PassengerTicketClass.Business)
                    .ToList().Count;
                business += flight.Tickets
                    .Where(t => t.EndPoint == currentPoint && t.Class == PassengerTicketClass.Business)
                    .ToList().Count;
                frst -= flight.Tickets
                    .Where(t => t.StartPoint == currentPoint && t.Class == PassengerTicketClass.FirstClass)
                    .ToList().Count;
                frst += flight.Tickets
                    .Where(t => t.EndPoint == currentPoint && t.Class == PassengerTicketClass.FirstClass)
                    .ToList().Count;
            }
            return (econ,business,frst);
        }

        public bool CheckCrewCount(PassengerFlight flight, Crew crew)
        {
            if (crew.CrewCount < flight.Plane.Type.MinimalCrew)
            {
                flight.CrewError = true;
                return false;
            }
            flight.CrewError = false;
            return true;
        }

        public bool PushFlightStatus(PassengerFlight flight)
        {
            flight.Status++;
            if (flight.Status == FlightStatus.Finished)
                _bllUnit.CrewService
                    .UpdatePilotPassengerExperience(flight.Crew, flight.Plane, flight.Airline.HoursTaken);
            Flights.Update(flight);
            _bllUnit.Save();
            return true;
        }

        public bool CheckPlaneAvailable(PassengerPlane plane)
        {
            return plane.Status != PlaneStatus.Repair;
        }

        public void SetCrew(PassengerFlight flight, Crew crew)
        {
            CheckCrewCount(flight, crew);
            flight.Crew = crew;
        }

        public void SetPlane(PassengerFlight flight, PassengerPlane plane)
        {
            flight.Plane = plane;
            CheckCrewCount(flight, flight.Crew);
        }

        public IEnumerable<PassengerFlight> SearchForFlights(int startPoint, int endPoint, DateTime date)
        {
            var airlines = _bllUnit.AirlineService.SearchAirlinesByAirports(startPoint, endPoint);
            var flights = Flights.GetAll().Where(f => airlines.Contains(f.Airline) && f.StartDate == date);
            return flights;
        }
    }
}
