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
    public class TestTeams
    {
        public UnitOfWork unit;
        static IRepository<Team> teams;

        [OneTimeSetUp]
        public void SetUp()
        {
            string conStr = "User ID=postgres; Password=admin; Server=localhost; Port=5432; Database=testera; Integrated Security=true; Pooling=true;";
            FileInfo fileLocation = new FileInfo(@"C:\TimeKeeper_DATA\TimeKeeperTest.xlsx");
            unit = new UnitOfWork(new TimeKeeperContext(conStr));
            unit.Seed(fileLocation);
        }

        [Test]
        public void GetAll()
        {
            var collection = unit.Teams.Get();
            Assert.AreEqual(collection.Count(), 3);
        }
        [Test]
        public void GetById(int id)
        {
            Team team = unit.Teams.Get(id);
            Assert.False(team == null);
        }
        [Test]
        public void WrongId()
        {
            Team team = teams.Get(11);
            Assert.True(team == null);
        }

        [Test]
        public void InsertTeam()
        {
            Team t = new Team
            {
                Name = "New team"
            };
            teams.Insert(t);
            int N = unit.Save();
            Assert.AreEqual(1, N);
            Assert.AreEqual(4, t.Id);
        }

        [Test]
        public void UpdateTeam()
        {
            int id = 2;
            Team t = new Team
            {
                Id = id,
                Name = "Updated!"
            };
            teams.Update(t, id);
            int N = unit.Save();
            Assert.AreEqual(1, N);
            Assert.AreEqual("Updated!", t.Name);
        }

        [Test]
        public void UpdateTeamWithWrongId()
        {
            int id = 2;
            Team t = new Team
            {
                Id = 2,
                Name = "Updated!"
            };
            teams.Update(t, id + 1);
            int N = unit.Save();
            Assert.AreEqual(0, N);
        }

        [Test]
        public void DeleteTeam()
        {
            teams.Delete(2);
            int N = unit.Save();
            Assert.AreEqual(1, N);
        }

        [Test]
        public void WrongDelete()
        {
            teams.Delete(22);
            int N = unit.Save();
            Assert.AreEqual(0, N);
        }
    }
}
