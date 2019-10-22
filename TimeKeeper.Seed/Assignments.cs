using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeper.DAL;
using TimeKeeper.Domain;

namespace TimeKeeper.Seed
{
    public static class Assignments
    {
        public static void Collect(ExcelWorksheet rawData, UnitOfWork unit)
        {
            Console.Write("Assignments: ");
            int N = 0;
            for (int row = 2; row <= rawData.Dimension.Rows; row++)
            {
                Assignment a = new Assignment
                {
                    Day = unit.Calendar.Get(Utility.dicDays[rawData.ReadInteger(row, 4)]),
                    Project = unit.Projects.Get(Utility.dicProj[rawData.ReadInteger(row, 3)]),
                    Hours = rawData.ReadDecimal(row, 2),
                    Description = rawData.ReadString(row, 1)
                };

                unit.Assignments.Insert(a);
                N++;
                if (N % 100 == 0)
                {
                    Console.Write($"{N} ");
                    unit.Save();
                }
            }
            unit.Save();
            Console.WriteLine(N);
        }
    }
}
