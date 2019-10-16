using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeper.DAL;
using TimeKeeper.Domain;

namespace TimeKeeper.Seed
{
    public static class Teams
    {
        public static void Collect(ExcelWorksheet rawData, UnitOfWork unit)
        {
            Console.Write("Teams: ");
            int N = 0;
            for (int row = 2; row <= rawData.Dimension.Rows; row++)
            {
                string oldId = rawData.ReadString(row, 1);
                Team t = new Team
                {
                    Name = rawData.ReadString(row, 2),
                    Description = rawData.ReadString(row, 3)
                };
                unit.Teams.Insert(t);
                unit.Save();
                Utility.dicTeam.Add(oldId, t.Id);
                N++;
            }
            Console.WriteLine(N);
        }
    }
}
