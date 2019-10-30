using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TimeKeeper.API.Controllers;
using TimeKeeper.API.Models;
using TimeKeeper.DAL;
using TimeKeeper.Domain;

namespace TimeKeeper.Test.TestControllers
{
    [TestFixture]
    public class TestRoles
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
            var controller = new RolesController(context);
            var response = controller.Get() as ObjectResult;
            var value = response.Value as List<RoleModel>;
            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual(5, value.Count);
        }
        [TestCase(1), Order(2)]
        public void GetById(int id)
        {
            var controller = new RolesController(context);
            var response = controller.Get(id) as ObjectResult;
            var value = response.Value as RoleModel;
            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual(1, value.Id);
        }
        [TestCase(11), Order(3)]
        public void GetByWrongId(int id)
        {
            var controller = new RolesController(context);
            var response = controller.Get(id) as ObjectResult;

            Assert.Null(response);
        }
        [Test, Order(4)]
        public void InsertRole()
        {
            var controller = new RolesController(context);
            Role role = new Role
            {
                Name = "New role"
            };
            var response = controller.Post(role) as ObjectResult;
            var value = response.Value as RoleModel;

            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual(6, value.Id);
        }
        [TestCase(1), Order(5)]
        public void UpdateRole(int id)
        {
            var controller = new RolesController(context);

            Role role = new Role
            {
                Id = id,
                Name = "Updated!"
            };

            var response = controller.Put(id, role) as ObjectResult;
            var value = response.Value as RoleModel;

            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual("Updated!", value.Name);
        }
        [TestCase(11), Order(6)]
        public void UpdateRoleWithWrongId(int id)
        {
            var controller = new RolesController(context);

            Role role = new Role
            {
                Id = id,
                Name = "Updated!"
            };
            var response = controller.Put(id, role) as ObjectResult;
            Assert.Null(response);
        }
        [TestCase(2), Order(7)]
        public void DeleteRole(int id)
        {
            var controller = new RolesController(context);

            var response = controller.Delete(id) as StatusCodeResult;
            Assert.AreEqual(204, response.StatusCode);
        }
        [TestCase(11), Order(8)]
        public void WrongDelete(int id)
        {
            var controller = new RolesController(context);

            var response = controller.Delete(id) as StatusCodeResult;
            Assert.AreEqual(404, response.StatusCode);
        }
    }
}
