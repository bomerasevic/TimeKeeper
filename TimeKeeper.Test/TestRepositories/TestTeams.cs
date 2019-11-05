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
            var collection = unit.Teams.Get();
            Assert.AreEqual(collection.Count(), 3);
        }
        [TestCase(1), Order(2)]
        public void GetById(int id)
        {
            Team team = unit.Teams.Get(id);
            Assert.False(team == null);
        }
        [TestCase(11), Order(3)]
        public void GetByWrongId(int id)
        {
            Team team = unit.Teams.Get(id);
            Assert.True(team == null);
        }

        [Test, Order(4)]
        public void InsertTeam()
        {
            Team t = new Team
            {
                Name = "New team"
            };
            unit.Teams.Insert(t);
            int N = unit.Save();
            Assert.AreEqual(1, N);
            Assert.AreEqual(4, t.Id);
        }

        [TestCase(1), Order(5)]
        public void UpdateTeam(int id)
        {
            Team t = new Team
            {
                Id = id,
                Name = "Updated!"
            };
            unit.Teams.Update(t, id);
            int N = unit.Save();
            Assert.AreEqual(1, N);
            Assert.AreEqual("Updated!", t.Name);
        }

        [TestCase(11), Order(6)]
        public void UpdateTeamWithWrongId(int id)
        {
            try
            {
                Team t = new Team
                {
                    Id = id,
                    Name = "Updated!"
                };
                unit.Teams.Update(t, id);
                int N = unit.Save();
                Assert.AreEqual(0, N);
            }
            catch (ArgumentNullException ae)
            {
                Team team = unit.Teams.Get(id);
                Assert.True(team == null);
                return;
            }
        }

        [TestCase(1), Order(7)]
        public void DeleteTeam(int id)
        {
            unit.Teams.Delete(id);
            int N = unit.Save();
            Assert.AreEqual(1, N);
        }

        [TestCase(11), Order(8)]
        public void WrongDelete(int id)
        {
            try
            {
                unit.Teams.Delete(id);
                int N = unit.Save();
                Assert.AreEqual(0, N);
            }            
            catch (ArgumentNullException ae)
            {
                Team team = unit.Teams.Get(id);
                Assert.True(team == null);
                return;
            }
        }
    }
}
