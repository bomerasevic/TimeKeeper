using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimeKeeper.DAL;
using TimeKeeper.Domain;

namespace TimeKeeper.Test
{
    [TestFixture]
    public class TestTeams
    {
        public TimeKeeperContext context;

        [OneTimeSetUp]
        public void SetUp()
        {
            string conStr = "User ID=postgres; Password=admin; Server=localhost; Port=5432; Database=testera; Integrated Security=true; Pooling=true;";
            context = new TimeKeeperContext(conStr);
            context.Seed();
        }

        [Test]
        public void FirstTest()
        {
            // Act
            int x = context.Teams.Count();

            // Assert
            Assert.AreEqual(x, 3);
        }

        [TestCase(1, "Red")]
        [TestCase(2, "Blue")]
        [TestCase(3, "Green")]
        public void TestTeamById(int id, string name)
        {
            IRepository<Team> teams = new Repository<Team>(context);
            var result = teams.Get(id);
            Assert.AreEqual(result.Name, name);
        }
    }
}
