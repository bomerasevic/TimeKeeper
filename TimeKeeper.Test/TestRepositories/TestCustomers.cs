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
    public class TestCustomers
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
            var collection = unit.Customers.Get();
            Assert.AreEqual(collection.Count(), 2);
        }
        [TestCase(1), Order(2)]
        public void GetById(int id)
        {
            Customer customer = unit.Customers.Get(id);
            Assert.False(customer == null);
        }
        [TestCase(11), Order(3)]
        public void GetByWrongId(int id)
        {
            Customer customer = unit.Customers.Get(id);
            Assert.True(customer == null);
        }

        [Test, Order(4)]
        public void InsertCustomer()
        {
            Customer c = new Customer
            {
                Name = "New customer"
            };
            unit.Customers.Insert(c);
            int N = unit.Save();
            Assert.AreEqual(1, N);
            Assert.AreEqual(3, c.Id);
        }

        [TestCase(1), Order(5)]
        public void UpdateCustomer(int id)
        {
            Customer c = new Customer
            {
                Id = id,
                Name = "Updated!"
            };
            unit.Customers.Update(c, id);
            int N = unit.Save();
            Assert.AreEqual(1, N);
            Assert.AreEqual("Updated!", c.Name);
        }

        [TestCase(11), Order(6)]
        public void UpdateCustomerWithWrongId(int id)
        {
            Customer c = new Customer
            {
                Id = id,
                Name = "Updated!"
            };
            unit.Customers.Update(c, id);
            int N = unit.Save();
            Assert.AreEqual(0, N);
        }

        [TestCase(1), Order(7)]
        public void DeleteCustomer(int id)
        {
            unit.Customers.Delete(id);
            int N = unit.Save();
            Assert.AreEqual(1, N);
        }

        [TestCase(11), Order(8)]
        public void WrongDelete(int id)
        {
            unit.Customers.Delete(id);
            int N = unit.Save();
            Assert.AreEqual(0, N);
        }
    }
}
