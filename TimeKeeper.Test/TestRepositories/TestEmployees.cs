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
    public class TestEmployees
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
            var collection = unit.Employees.Get();
            Assert.AreEqual(collection.Count(), 6);
        }
        [TestCase(1), Order(2)]
        public void GetById(int id)
        {
            Employee employee = unit.Employees.Get(id);
            Assert.False(employee == null);
        }
        [TestCase(11), Order(3)]
        public void GetByWrongId(int id)
        {
            Employee employee = unit.Employees.Get(id);
            Assert.True(employee == null);
        }

        [Test, Order(4)]
        public void InsertEmployee()
        {
            Employee e = new Employee
            {
                FirstName = "New employee"
            };
            unit.Employees.Insert(e);
            int N = unit.Save();
            Assert.AreEqual(1, N);
            Assert.AreEqual(7, e.Id);
        }

        [TestCase(1), Order(5)]
        public void UpdateEmployee(int id)
        {
            Employee e = new Employee
            {
                Id = id,
                FirstName = "Updated!",
                EndDate = DateTime.Now,
                Status = unit.EmployeeStatuses.Get(2)
            };
            unit.Employees.Update(e, id);
            int N = unit.Save();
            Assert.AreEqual(1, N);
            Assert.AreEqual("Updated!", e.FirstName);
        }

        [TestCase(11), Order(6)]
        public void UpdateEmployeeWithWrongId(int id)
        {
            Employee e = new Employee
            {
                Id = id,
                FirstName = "Updated!"
            };
            unit.Employees.Update(e, id);
            int N = unit.Save();
            Assert.AreEqual(0, N);
        }

        [TestCase(1), Order(7)]
        public void DeleteEmployee(int id)
        {
            unit.Employees.Delete(id);
            int N = unit.Save();
            Assert.AreEqual(1, N);
        }

        [TestCase(11), Order(8)]
        public void WrongDelete(int id)
        {
            unit.Employees.Delete(id);
            int N = unit.Save();
            Assert.AreEqual(0, N);
        }
    }
}
