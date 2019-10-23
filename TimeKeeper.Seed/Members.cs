using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeper.DAL;
using TimeKeeper.Domain;

namespace TimeKeeper.Seed
{
    public static class Members
    {
        public static void Collect(ExcelWorksheet rawData, UnitOfWork unit)
        {
            Console.Write("Members: ");

            MemberStatus active = new MemberStatus { Name = "active" };
            MemberStatus waitingForTheTask = new MemberStatus { Name = "waiting for the task" };
            MemberStatus onHold = new MemberStatus { Name = "on hold" };
            MemberStatus leaver = new MemberStatus { Name = "leaver" };
            unit.MemberStatuses.Insert(active);
            unit.MemberStatuses.Insert(waitingForTheTask);            
            unit.MemberStatuses.Insert(onHold);
            unit.MemberStatuses.Insert(leaver);
            unit.Save();

            int N = 0;
            for (int row = 2; row <= rawData.Dimension.Rows; row++)
            {
                Member m = new Member
                {
                    Employee = unit.Employees.Get(Utility.dicEmpl[rawData.ReadInteger(row, 1)]),
                    Team = unit.Teams.Get(Utility.dicTeam[rawData.ReadString(row, 2)]),
                    Role = unit.Roles.Get(Utility.dicRole[rawData.ReadString(row, 3)]),
                    Status = unit.MemberStatuses.Get(rawData.ReadInteger(row, 5)),
                    HoursWeekly = rawData.ReadInteger(row, 4)
                };
                unit.Members.Insert(m);
                N++;
            }
            unit.Save();
            Console.WriteLine(N);
        }
    }
}
