using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeper.DAL;
using TimeKeeper.Domain;

namespace TimeKeeper.Seed
{
    public static class Calendar
    {
        public static void Collect(ExcelWorksheet rawData, UnitOfWork unit)
        {
            Console.Write("Calendar: ");
            int N = 0;

            for (int row = 2; row <= rawData.Dimension.Rows; row++)
            {
                int oldId = rawData.ReadInteger(row, 1);
                Day d = new Day
                {
                    Employee = unit.Employees.Get(Utility.dicEmpl[rawData.ReadInteger(row, 2)]),
                    //DayType = (DayType)rawData.ReadInteger(row, 3), ERROR OVDJE
                    Date = rawData.ReadDate(row, 4)
                };

                unit.Calendar.Insert(d);
                unit.Save();
                Utility.dicDays.Add(oldId, d.Id);
                N++;
                if (N % 100 == 0)
                {
                    Console.Write($"{N}");
                }
            }
            Console.WriteLine(N);
        }
    }
}
