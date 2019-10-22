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

            DayType workDay = new DayType { Name = "workday" };
            DayType holiday = new DayType { Name = "holiday" };
            DayType business = new DayType { Name = "business" };
            DayType religious = new DayType { Name = "religious" };
            DayType sick = new DayType { Name = "sick" };
            DayType vacation = new DayType { Name = "vacation" };
            DayType other = new DayType { Name = "other" };
            unit.DayTypes.Insert(workDay);
            unit.DayTypes.Insert(holiday);
            unit.DayTypes.Insert(business);
            unit.DayTypes.Insert(religious);
            unit.DayTypes.Insert(sick);
            unit.DayTypes.Insert(vacation);
            unit.DayTypes.Insert(other);
            unit.Save();

            int N = 0;

            for (int row = 2; row <= rawData.Dimension.Rows; row++)
            {
                int oldId = rawData.ReadInteger(row, 1);
                Day d = new Day
                {
                    Employee = unit.Employees.Get(Utility.dicEmpl[rawData.ReadInteger(row, 2)]),
                    DayType = unit.DayTypes.Get(rawData.ReadInteger(row, 3)), 
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
