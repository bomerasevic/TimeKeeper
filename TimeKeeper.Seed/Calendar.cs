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
            for (int row = 2; row <= rawData.Dimension.Rows; row++)
            {
                int oldId = rawData.ReadInteger(row, 1);
                Day d = new Day
                {
                    Date = rawData.ReadDate(row, 2),
                    TotalHours = rawData.ReadDecimal(row, 3)
                };
                unit.Calendar.Insert(d);
                unit.Save();
                Utility.dayDictionary.Add(oldId, d.Id);
            }
        }
    }
}
