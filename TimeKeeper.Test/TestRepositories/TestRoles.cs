using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TimeKeeper.DAL;
using TimeKeeper.Domain;

namespace TimeKeeper.Test.TestRepositories
{
    [TestFixture]
    public class TestRoles
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

        [Test, Order(1)]
        public void GetAll()
        {
            var collection = unit.Roles.Get();
            Assert.AreEqual(collection.Count(), 5);
        }
        [TestCase(1), Order(2)]
        public void GetById(int id)
        {
            Role role = unit.Roles.Get(id);
            Assert.False(role == null);
        }
        [TestCase(11), Order(3)]
        public void GetByWrongId(int id)
        {
            Role role = unit.Roles.Get(id);
            Assert.True(role == null);
        }

        [Test, Order(4)]
        public void InsertRole()
        {
            Role r = new Role
            {
                Name = "New role"
            };
            unit.Roles.Insert(r);
            int N = unit.Save();
            Assert.AreEqual(1, N);
            Assert.AreEqual(6, r.Id);
        }

        [TestCase(1), Order(5)]
        public void UpdateRole(int id)
        {
            Role r = new Role
            {
                Id = id,
                Name = "Updated!"
            };
            unit.Roles.Update(r, id);
            int N = unit.Save();
            Assert.AreEqual(1, N);
            Assert.AreEqual("Updated!", r.Name);
        }

        [TestCase(11), Order(6)]
        public void UpdateRoleWithWrongId(int id)
        {
            Role r = new Role
            {
                Id = id,
                Name = "Updated!"
            };
            unit.Roles.Update(r, id);
            int N = unit.Save();
            Assert.AreEqual(0, N);
        }

        [TestCase(1), Order(7)]
        public void DeleteRole(int id)
        {
            unit.Roles.Delete(id);
            int N = unit.Save();
            Assert.AreEqual(1, N);
        }

        [TestCase(11), Order(8)]
        public void WrongDelete(int id)
        {
            unit.Roles.Delete(id);
            int N = unit.Save();
            Assert.AreEqual(0, N);
        }
    }
}
