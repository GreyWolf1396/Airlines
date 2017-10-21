using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;
using Contracts.DomainEntities.Crews;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Grey_Airlines.Tests
{
    [TestClass]
    public class CrewTests
    {
        BllUnit _bllUnit = new BllUnit();

        [TestMethod]
        public void CheckCrewForAllRequiredRolesFilled_Incorrect()
        {
            var actual = _bllUnit.CrewService.CheckCrewMembers(new Crew()
            {
                Pilots = new List<PilotInCrew>(),
                Employees = new List<EmployeeInCrew>()
            });

            Assert.AreEqual(false,actual);
        }

        [TestMethod]
        public void CheckCrewForAllRequiredRolesFilled_Correct()
        {

            var crew = new Crew()
            {
                Pilots = new List<PilotInCrew>()
            };
            foreach (var role in _bllUnit.CrewService.CrewRoles.GetAll())
            {
                crew.Pilots.Add(new PilotInCrew() {Role = role});
            }

            var actual = _bllUnit.CrewService.CheckCrewMembers(crew);

            Assert.AreEqual(true, actual);
        }
    }
}
