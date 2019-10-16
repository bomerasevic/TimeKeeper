using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeper.DAL;
using TimeKeeper.Domain;

namespace TimeKeeper.Seed
{
    public static class Projects
    {
        public static void Collect(ExcelWorksheet rawData, UnitOfWork unit)
        {
            Console.Write("Projects: ");
            int N = 0;
            for (int row = 2; row <= rawData.Dimension.Rows; row++)
            {
                int oldId = rawData.ReadInteger(row, 1);
                Project p = new Project
                {
                    Name = rawData.ReadString(row, 2),
                    Description = rawData.ReadString(row, 4),
                    StartDate = rawData.ReadDate(row, 5),
                    EndDate = rawData.ReadDate(row, 6),
                    //Status = (ProjectStatus)rawData.ReadInteger(row, 7), ERROR OVDJE
                    Customer = unit.Customers.Get(Utility.dicCust[rawData.ReadInteger(row, 8)]),
                    Team = unit.Teams.Get(Utility.dicTeam[rawData.ReadString(row, 9)]),
                    //Pricing = (ProjectPricing)rawData.ReadInteger(row, 10), ERROR OVDJE
                    Amount = rawData.ReadDecimal(row, 11)
                };
                unit.Projects.Insert(p);
                unit.Save();
                Utility.dicProj.Add(oldId, p.Id);
                N++;
            }
            Console.WriteLine(N);
        }
    }
}
