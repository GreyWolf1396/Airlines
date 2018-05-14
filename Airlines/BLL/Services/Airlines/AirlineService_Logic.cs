using System.Collections.Generic;
using System.Linq;
using Contracts.DomainEntities.Airlines;

namespace BLL.Services.Airlines
{
    public partial class AirlineService
    {
        /// <summary>
        /// Search for airlines by Id's of 2 airports
        /// </summary>
        /// <param name="startPoint">Id of departure airport</param>
        /// <param name="endPoint">Id of destination airport</param>
        /// <returns></returns>
        public IEnumerable<Airline> SearchAirlinesByAirports(int startPoint, int endPoint)
        {
            var startAirport = Airports.GetById(startPoint);
            var endAirport = Airports.GetById(endPoint);
            var result = new List<Airline>();
            foreach (var airline in Airlines.GetAll())
            {
                var nodeStart = airline.Nodes.FirstOrDefault(a => a.Airport == startAirport);
                var nodeEnd = airline.Nodes.FirstOrDefault(a => a.Airport == endAirport);
                if((nodeEnd==null)||(nodeStart==null))
                    continue;
                if (nodeEnd.NumberInRoute>nodeStart.NumberInRoute)
                    result.Add(airline);
            }
            return result;
        }
    }
}
