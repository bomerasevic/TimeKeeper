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
    public class TestMembers
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
            var collection = unit.Members.Get();
            Assert.AreEqual(collection.Count(), 6);
        }
        [TestCase(1), Order(2)]
        public void GetById(int id)
        {
            Member member = unit.Members.Get(id);
            Assert.False(member == null);
        }
        [TestCase(11), Order(3)]
        public void GetByWrongId(int id)
        {
            Member member = unit.Members.Get(id);
            Assert.True(member == null);
        }

        [Test, Order(4)]
        public void InsertMember()
        {
            Member m = new Member
            {
                HoursWeekly = 20
            };
            unit.Members.Insert(m);
            int N = unit.Save();
            Assert.AreEqual(1, N);
            Assert.AreEqual(7, m.Id);
        }

        [TestCase(1), Order(5)]
        public void UpdateMember(int id)
        {
            Member m = new Member
            {
                Id = id,
                HoursWeekly = 10,
                Role = unit.Roles.Get(2),
                Status = unit.MemberStatuses.Get(2)
            };
            unit.Members.Update(m, id);
            int N = unit.Save();
            Assert.AreEqual(1, N);
            Assert.AreEqual(10, m.HoursWeekly);
        }

        [TestCase(11), Order(6)]
        public void UpdateMemberWithWrongId(int id)
        {
            try
            {
                Member m = new Member
                {
                    Id = id,
                    HoursWeekly = 10
                };
                unit.Members.Update(m, id);
                int N = unit.Save();
                Assert.AreEqual(0, N);
            }
            catch (ArgumentNullException ae)
            {
                return;
            }
        }

        [TestCase(1), Order(7)]
        public void DeleteMember(int id)
        {
            unit.Members.Delete(id);
            int N = unit.Save();
            Assert.AreEqual(1, N);
        }

        [TestCase(11), Order(8)]
        public void WrongDelete(int id)
        {
            try
            {
                unit.Members.Delete(id);
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
