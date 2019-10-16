using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeper.DAL;
using TimeKeeper.Domain;

namespace TimeKeeper.Seed
{
    public static class Employees
    {
        public static void Collect(ExcelWorksheet rawData, UnitOfWork unit)
        {
            Console.Write("Employees: ");
            int N = 0;
            for (int row = 2; row <= rawData.Dimension.Rows; row++)
            {
                int oldId = rawData.ReadInteger(row, 1);
                Employee e = new Employee
                {
                    FirstName = rawData.ReadString(row, 2),
                    LastName = rawData.ReadString(row, 3),
                    Image = rawData.ReadString(row, 4),
                    Email = rawData.ReadString(row, 6),
                    Phone = rawData.ReadString(row, 7),
                    Birthday = rawData.ReadDate(row, 8),
                    BeginDate = rawData.ReadDate(row, 9),
                    EndDate = rawData.ReadDate(row, 10),
                    //Status = (EmployeeStatus)rawData.ReadInteger(row, 11), ERROR OVDJE
                    //Position = rawData.ReadString(row, 12) ERROR OVDJE
                };
                unit.Employees.Insert(e);
                unit.Save();
                Utility.dicEmpl.Add(oldId, e.Id);
                N++;
            }
            Console.WriteLine(N);
        }
    }
}
