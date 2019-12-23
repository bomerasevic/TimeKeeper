using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TimeKeeper.API.Controllers;
using TimeKeeper.DAL;
using TimeKeeper.Domain;
using TimeKeeper.DTO.Models.DomainModels;

namespace TimeKeeper.Test.TestControllers
{
    [TestFixture]
    public class TestMembers
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
        public void GetAllMembers()
        {
            var controller = new MembersController(context);
            var response = controller.Get() as ObjectResult;
            var value = response.Value as List<MemberModel>;
            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual(6, value.Count);
        }

        [TestCase(1), Order(2)]
        public void GetMemberById(int id)
        {
            var controller = new MembersController(context);
            var response = controller.Get(id) as ObjectResult;
            var value = response.Value as MemberModel;
            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual(1, value.Id);
        }

        [TestCase(11), Order(3)]
        public void GetMemberByWrongId(int id)
        {
            var controller = new MembersController(context);
            var response = controller.Get(id) as ObjectResult;

            Assert.Null(response);
        }

        [Test, Order(4)]
        public void InsertMember()
        {
            var controller = new MembersController(context);
            Member m = new Member
            {
                Team = unit.Teams.Get(1),
                Role = unit.Roles.Get(1),
                Employee = unit.Employees.Get(1),
                Status = unit.MemberStatuses.Get(1),
                HoursWeekly = 20
            };
            var response = controller.Post(m) as ObjectResult;
            var value = response.Value as MemberModel;

            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual(7, value.Id);
        }

        [TestCase(1), Order(5)]
        public void UpdateMember(int id)
        {
            var controller = new MembersController(context);
            Member m = new Member
            {
                Id = id,
                HoursWeekly = 10,
                Role = unit.Roles.Get(1),
                Employee = unit.Employees.Get(1),
                Team = unit.Teams.Get(1),
                Status = unit.MemberStatuses.Get(1)
            };
            var response = controller.Put(id, m) as ObjectResult;
            var value = response.Value as MemberModel;

            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual(10, value.HoursWeekly);
        }

        [TestCase(11), Order(6)]
        public void UpdateMemberWithWrongId(int id)
        {
            var controller = new MembersController(context);
            var response = controller.Get(id) as ObjectResult;
            if (response == null)
            {
                Assert.Null(response);
            }
            else
            {
                Member m = new Member
                {
                    Id = id,
                    HoursWeekly = 10,
                    Role = unit.Roles.Get(1),
                    Employee = unit.Employees.Get(1),
                    Team = unit.Teams.Get(1)
                };
                response = controller.Put(id, m) as ObjectResult;
                var value = response.Value as MemberModel;
                Assert.AreEqual(200, response.StatusCode);
                Assert.AreEqual(10, value.HoursWeekly);
            }
        }

        [TestCase(1), Order(7)]
        public void DeleteMember(int id)
        {
            var controller = new MembersController(context);
            var response = controller.Delete(id) as StatusCodeResult;
            Assert.AreEqual(204, response.StatusCode);
        }

        [TestCase(11), Order(8)]
        public void DeleteMemberByWrongId(int id)
        {
            var controller = new MembersController(context);
            var response = controller.Delete(id) as StatusCodeResult;
            Assert.AreEqual(404, response.StatusCode);
        }
    }
}
