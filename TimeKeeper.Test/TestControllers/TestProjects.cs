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
    public class TestProjects
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
            var controller = new ProjectsController(context);
            var response = controller.Get() as ObjectResult;
            var value = response.Value as List<ProjectModel>;
            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual(3, value.Count);
        }
        [TestCase(1), Order(2)]
        public void GetById(int id)
        {
            var controller = new ProjectsController(context);
            var response = controller.Get(id) as ObjectResult;
            var value = response.Value as ProjectModel;
            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual(1, value.Id);
        }
        [TestCase(11), Order(3)]
        public void GetByWrongId(int id)
        {
            var controller = new ProjectsController(context);
            var response = controller.Get(id) as ObjectResult;

            Assert.Null(response);
        }
        [Test, Order(4)]
        public void InsertProject()
        {
            var controller = new ProjectsController(context);
            Project project = new Project
            {
                Name = "New role",
                Team = unit.Teams.Get(1),
                Customer = unit.Customers.Get(1),
                Status = unit.ProjectStatuses.Get(1),
                Pricing = unit.ProjectPrices.Get(1)
            };
            var response = controller.Post(project) as ObjectResult;
            var value = response.Value as ProjectModel;

            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual(4, value.Id);
        }
        [TestCase(1), Order(5)]
        public void UpdateProject(int id)
        {
            var controller = new ProjectsController(context);

            Project project = new Project
            {
                Id = id,
                Name = "Updated!",
                Team = unit.Teams.Get(1),
                Customer = unit.Customers.Get(1),
                Status = unit.ProjectStatuses.Get(1),
                Pricing = unit.ProjectPrices.Get(1)
            };

            var response = controller.Put(id, project) as ObjectResult;
            var value = response.Value as ProjectModel;

            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual("Updated!", value.Name);
        }
        [TestCase(21), Order(6)]
        public void UpdateProjectWithWrongId(int id)
        {
            var controller = new ProjectsController(context);
            var response = controller.Get(id) as ObjectResult;
            if (response == null)
            {
                Assert.Null(response);
            }
            else
            {
                Project project = new Project
                {
                    Id = id,
                    Name = "Updated!"
                };
                response = controller.Put(id, project) as ObjectResult;
                var value = response.Value as ProjectModel;
                Assert.AreEqual(200, response.StatusCode);
                Assert.AreEqual("Updated!", value.Name);
            }
            //var controller = new ProjectsController(context);
            //Project project = new Project
            //{
            //    Id = id,
            //    Name = "Updated!"
            //};
            //var response = controller.Put(id, project) as StatusCodeResult;
            //Assert.AreEqual(404, response.StatusCode);
        }
        [TestCase(2), Order(7)]
        public void DeleteProject(int id)
        {
            var controller = new ProjectsController(context);

            var response = controller.Delete(id) as StatusCodeResult;
            Assert.AreEqual(204, response.StatusCode);
        }
        [TestCase(11), Order(8)]
        public void WrongDelete(int id)
        {
            var controller = new ProjectsController(context);
            var response = controller.Delete(id) as StatusCodeResult;
            Assert.AreEqual(404, response.StatusCode);
        }
    }
}
