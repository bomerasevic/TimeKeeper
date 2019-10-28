using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TimeKeeper.API.Controllers;
using TimeKeeper.DAL;

namespace TimeKeeper.Test.TestControllers
{
    [TestFixture]
    public class TestTeams
    {
        public UnitOfWork unit;

        [OneTimeSetUp]
        public void SetUp()
        {
            string conStr = "User ID=postgres; Password=admin; Server=localhost; Port=5432; Database=testera; Integrated Security=true; Pooling=true;";
            FileInfo fileLocation = new FileInfo(@"C:\TimeKeeper_DATA\TimeKeeperTest.xlsx");
            unit = new UnitOfWork(new TimeKeeperContext(conStr));
            unit.Seed(fileLocation);
        }

        [Test]
        public void GetAll()
        {
            //var controller = new TeamsController(unit);

            //Assert.AreEqual(200, response.StatusCode);
        }
    }
}
