using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;
using BLL.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Grey_Airlines.Tests
{
    [TestClass]
    public class AirlinesTests
    {
        [TestMethod]
        public void TrySearchAirlinesByParameters()
        {
            AirlineService service = new BllUnit().AirlineService;
            var expected = service.Airlines.GetAll().ToList()[0];
            var actual =
                service.SearchAirlinesByAirports(
                    expected.Nodes.ToList()[0].Airport.Id,
                    expected.Nodes.ToList()[1].Airport.Id)
                    .ToList()[0];
            Assert.AreSame(expected,actual);
        }
    }
}
