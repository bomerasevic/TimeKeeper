using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TimeKeeper.DAL;
using TimeKeeper.Domain;

namespace TimeKeeper.Test
{
    [TestFixture]
    public class TestTeams
    {
        public UnitOfWork unit;

        [OneTimeSetUp]
        public void SetUp()
        {
            string conStr = "User ID=postgres; Password=admin; Server=localhost; Port=5432; Database=testera; Integrated Security=true; Pooling=true;";
            FileInfo fileLocation = new FileInfo( @"C:\TimeKeeper_DATA\TimeKeeperTest.xlsx");
            unit = new UnitOfWork(new TimeKeeperContext (conStr));
            unit.Seed(fileLocation);
        }

        [Test]
        public void FirstTest()
        {
            // Act
            var collection = unit.Teams.Get();

            // Assert
            Assert.AreEqual(collection.Count(), 3);
        }

        //[TestCase(1, "Red")]
        //[TestCase(2, "Blue")]
        //[TestCase(3, "Green")]
        //public void TestTeamById(int id, string name)
        //{
        //    IRepository<Team> teams = new Repository<Team>(context);
        //    var result = teams.Get(id);
        //    Assert.AreEqual(result.Name, name);
        //}
    }
}
