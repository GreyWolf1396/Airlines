using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Grey_Airlines.Tests
{
    [TestClass]
    public class UserTests
    {
        BllUnit _bllUnit = new BllUnit();

        [TestMethod]
        public void GetUserByLogin_Null()
        {
            var actual = _bllUnit.UserService.GetUserByLogin(String.Empty);
            Assert.IsNull(actual);
        }

        [TestMethod]
        public void GetUserByLogin_Correct()
        {
            var expected = _bllUnit.UserService.Users.GetAll().First();

            var actual = _bllUnit.UserService.GetUserByLogin(expected.Login);

            Assert.AreSame(expected,actual);
        }
    }
}
