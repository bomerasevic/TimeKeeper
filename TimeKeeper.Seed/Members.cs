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
            int N = 0;
            for (int row = 2; row <= rawData.Dimension.Rows; row++)
            {
                Member m = new Member
                {
                    Employee = unit.Employees.Get(Utility.dicEmpl[rawData.ReadInteger(row, 1)]),
                    Team = unit.Teams.Get(Utility.dicTeam[rawData.ReadString(row, 2)]),
                    Role = unit.Roles.Get(Utility.dicRole[rawData.ReadString(row, 3)]),
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
