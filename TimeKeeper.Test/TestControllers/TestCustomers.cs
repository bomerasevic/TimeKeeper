﻿using Microsoft.AspNetCore.Mvc;
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
    public class TestCustomers
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
        public void GetAllCustomers()
        {
            var controller = new CustomersController(context);
            var response = controller.Get() as ObjectResult;
            var value = response.Value as List<CustomerModel>;
            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual(2, value.Count);
        }

        [TestCase(1), Order(2)]
        public void GetCustomerById(int id)
        {
            var controller = new CustomersController(context);
            var response = controller.Get(id) as ObjectResult;
            var value = response.Value as CustomerModel;
            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual(1, value.Id);
        }

        [TestCase(11), Order(3)]
        public void GetCustomerByWrongId(int id)
        {
            var controller = new CustomersController(context);
            var response = controller.Get(id) as ObjectResult;

            Assert.Null(response);
        }

        [Test, Order(4)]
        public void InsertCustomer()
        {
            var controller = new CustomersController(context);
            Customer c = new Customer
            {
                Name = "New Customer!",
                Status = unit.CustomerStatuses.Get(1),
                Address = new CustomerAddress { City = "City" }
            };
            var response = controller.Post(c) as ObjectResult;
            var value = response.Value as CustomerModel;

            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual(3, value.Id);
        }

        [TestCase(1), Order(5)]
        public void UpdateCustomer(int id)
        {
            var controller = new CustomersController(context);
            Customer c = new Customer
            {
                Id = id,
                Name = "Updated!",
                Status = unit.CustomerStatuses.Get(1),
                Address = new CustomerAddress { City = "City" }
            };
            var response = controller.Put(id, c) as ObjectResult;
            var value = response.Value as CustomerModel;

            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual("Updated!", value.Name);
        }

        [TestCase(11), Order(6)]
        public void UpdateCustomerWithWrongId(int id)
        {
            var controller = new CustomersController(context);
            var response = controller.Get(id) as ObjectResult;
            if (response == null)
            {
                Assert.Null(response);
            }
            else
            {
                Customer c = new Customer
                {
                    Id = id,
                    Name = "Updated!",
                    Status = unit.CustomerStatuses.Get(1),
                    Address = new CustomerAddress { City = "City"}
                };
                response = controller.Put(id, c) as ObjectResult;
                var value = response.Value as EmployeeModel;
                Assert.AreEqual(200, response.StatusCode);
                Assert.AreEqual("Updated!", value.FirstName);
            }
        }

        [TestCase(1), Order(7)]
        public void DeleteCustomer(int id)
        {
            var controller = new CustomersController(context);
            var response = controller.Delete(id) as StatusCodeResult;
            Assert.AreEqual(204, response.StatusCode);
        }

        [TestCase(11), Order(8)]
        public void DeleteCustomerByWrongId(int id)
        {
            var controller = new CustomersController(context);
            var response = controller.Delete(id) as StatusCodeResult;
            Assert.AreEqual(404, response.StatusCode);
        }
    }
}
