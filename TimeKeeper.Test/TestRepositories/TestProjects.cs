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
    public class TestProjects
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
            var collection = unit.Projects.Get();
            Assert.AreEqual(collection.Count(), 3);
        }
        [TestCase(1), Order(2)]
        public void GetById(int id)
        {
            Project project = unit.Projects.Get(id);
            Assert.False(project == null);
        }
        [TestCase(11), Order(3)]
        public void GetByWrongId(int id)
        {
            Project project = unit.Projects.Get(id);
            Assert.True(project == null);
        }

        [Test, Order(4)]
        public void InsertProject()
        {
            Project p = new Project
            {
                Name = "New role"
            };
            unit.Projects.Insert(p);
            int N = unit.Save();
            Assert.AreEqual(1, N);
            Assert.AreEqual(4, p.Id);
        }

        [TestCase(1), Order(5)]
        public void UpdateProject(int id)
        {
            Project p = new Project
            {
                Id = id,
                Name = "Updated!",
                Team = unit.Teams.Get(2),
                EndDate = DateTime.Now,
                Status = unit.ProjectStatuses.Get(4)
            };
            unit.Projects.Update(p, id);
            int N = unit.Save();
            Assert.AreEqual(1, N);
            Assert.AreEqual("Updated!", p.Name);
        }

        [TestCase(11), Order(6)]
        public void UpdateProjectWithWrongId(int id)
        {
            try
            {
                Project p = new Project
                {
                    Id = id,
                    Name = "Updated!"
                };
                unit.Projects.Update(p, id);
                int N = unit.Save();
                Assert.AreEqual(0, N);
            }
            catch (ArgumentNullException ae)
            {
                return;
            }
        }

        [TestCase(1), Order(7)]
        public void DeleteProject(int id)
        {
            unit.Projects.Delete(id);
            int N = unit.Save();
            Assert.AreEqual(1, N);
        }

        [TestCase(11), Order(8)]
        public void WrongDelete(int id)
        {
            try
            {
                unit.Projects.Delete(id);
                int N = unit.Save();
                Assert.AreEqual(0, N);
            }            
            catch (ArgumentNullException ae)
            {
                return;
            }
        }
    }
}
