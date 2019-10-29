using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TimeKeeper.API.Controllers;
using TimeKeeper.API.Models;
using TimeKeeper.DAL;

namespace TimeKeeper.Test.TestControllers
{
    [TestFixture]
    public class TestTeams
    {
        public UnitOfWork unit;
        public TimeKeeperContext context;

        [OneTimeSetUp]
        public void SetUp()
        {
            string conStr = "User ID=postgres; Password=admin; Server=localhost; Port=5432; Database=testera; Integrated Security=true; Pooling=true;";
            FileInfo fileLocation = new FileInfo(@"C:\TimeKeeper_DATA\TimeKeeperTest.xlsx");
            context = new TimeKeeperContext(conStr);
            unit = new UnitOfWork(context);
            unit.Seed(fileLocation);
        }

        [Test, Order(1)]
        public void GetAll()
        {
            var controller = new TeamsController(context);
            var response = controller.Get() as ObjectResult;
            var value = response.Value as List<TeamModel>;
            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual(3, value.Count);
        }
        [TestCase(1), Order(2)]
        public void GetById(int id)
        {
            var controller = new TeamsController(context);
            var response = controller.Get(id) as ObjectResult;
            var value = response.Value as TeamModel;
            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual(1, value.Id);
        }

    }
}
