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
    public class TestEmployees
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
        public void GetAllEmployees()
        {
            var controller = new EmployeesController(context);
            var response = controller.Get() as ObjectResult;
            var value = response.Value as List<EmployeeModel>;
            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual(6, value.Count);
        }

        [TestCase(1), Order(2)]
        public void GetEmployeeById(int id)
        {
            var controller = new EmployeesController(context);
            var response = controller.Get(id) as ObjectResult;
            var value = response.Value as EmployeeModel;
            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual(1, value.Id);
        }

        [TestCase(11), Order(3)]
        public void GetEmployeeByWrongId(int id)
        {
            var controller = new EmployeesController(context);
            var response = controller.Get(id) as ObjectResult;

            Assert.Null(response);
        }

        [Test, Order(4)]
        public void InsertEmployee()
        {
            var controller = new EmployeesController(context);
            Employee e = new Employee
            {
                FirstName = "New Employee!",
                Status = unit.EmployeeStatuses.Get(1),
                Position = unit.EmployeePositions.Get(1)
            };
            var response = controller.Post(e) as ObjectResult;
            var value = response.Value as EmployeeModel;

            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual(7, value.Id);
        }

        [TestCase(1), Order(5)]
        public void UpdateEmployee(int id)
        {
            var controller = new EmployeesController(context);
            Employee e = new Employee
            {
                Id = id,
                FirstName = "Updated!",
                Status = unit.EmployeeStatuses.Get(1),
                Position = unit.EmployeePositions.Get(1)
            };
            var response = controller.Put(id, e) as ObjectResult;
            var value = response.Value as EmployeeModel;

            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual("Updated!", value.FirstName);
        }

        [TestCase(11), Order(6)]
        public void UpdateEmployeeWithWrongId(int id)
        {
            var controller = new EmployeesController(context);
            var response = controller.Get(id) as ObjectResult;
            if (response == null)
            {
                Assert.Null(response);

            }
            else
            {
                Employee e = new Employee
                {
                    Id = id,
                    FirstName = "Updated!",
                    Status = unit.EmployeeStatuses.Get(1),
                    Position = unit.EmployeePositions.Get(1)
                };
                response = controller.Put(id, e) as ObjectResult;
                var value = response.Value as EmployeeModel;
                Assert.AreEqual(200, response.StatusCode);
                Assert.AreEqual("Updated!", value.FirstName);
            }
        }

        [TestCase(1), Order(7)]
        public void DeleteEmployee(int id)
        {
            var controller = new EmployeesController(context);
            var response = controller.Delete(id) as StatusCodeResult;
            Assert.AreEqual(204, response.StatusCode);
        }

        [TestCase(11), Order(8)]
        public void DeleteEmployeeByWrongId(int id)
        {
            var controller = new EmployeesController(context);
            var response = controller.Delete(id) as StatusCodeResult;
            Assert.AreEqual(404, response.StatusCode);
        }
    }
}
