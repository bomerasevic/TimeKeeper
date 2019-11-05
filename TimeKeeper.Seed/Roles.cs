using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeper.DAL;
using TimeKeeper.Domain;

namespace TimeKeeper.Seed
{
    public static class Roles
    {
        public static void Collect(ExcelWorksheet rawData, UnitOfWork unit)
        {
            Console.Write("Roles: ");
            int N = 0;
            for (int row = 2; row <= rawData.Dimension.Rows; row++)
            {
                string oldId = rawData.ReadString(row, 1);
                Role r = new Role
                {
                    Name = rawData.ReadString(row, 2),
                    HourlyPrice = rawData.ReadDecimal(row, 3),
                    MonthlyPrice = rawData.ReadDecimal(row, 4)
                };
                unit.Roles.Insert(r);
                unit.Save();
                Utility.dicRole.Add(oldId, r.Id);
                N++;
            }
            Console.WriteLine(N);
        }
    }
}
