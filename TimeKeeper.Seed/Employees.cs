using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeper.DAL;
using TimeKeeper.Domain;
using TimeKeeper.Utility;

namespace TimeKeeper.Seed
{
    public static class Employees
    {

        public static void Collect(ExcelWorksheet rawData, UnitOfWork unit)
        {
            Console.Write("Employee status");
            EmployeeStatus active = new EmployeeStatus { Name = "active" };
            EmployeeStatus trial = new EmployeeStatus { Name = "trial" };
            EmployeeStatus leaver = new EmployeeStatus { Name = "leaver" };
            unit.EmployeeStatuses.Insert(active);
            unit.EmployeeStatuses.Insert(trial);
            unit.EmployeeStatuses.Insert(leaver);
            unit.Save();
            Console.Write("Employee position");
            EmployeePosition dev = new EmployeePosition { Name = "DEV" };
            EmployeePosition uix = new EmployeePosition { Name = "UIX" };
            EmployeePosition qae = new EmployeePosition { Name = "QAE" };
            EmployeePosition mgr = new EmployeePosition { Name = "MGR" };
            EmployeePosition hrm = new EmployeePosition { Name = "HRM" };
            EmployeePosition ceo = new EmployeePosition { Name = "CEO" };
            EmployeePosition cto = new EmployeePosition { Name = "CTO" };
            EmployeePosition coo = new EmployeePosition { Name = "COO" };
            unit.EmployeePositions.Insert(dev);
            Utility.dicEmpPosition.Add("DEV", 1);
            unit.EmployeePositions.Insert(uix);
            Utility.dicEmpPosition.Add("UIX", 2);
            unit.EmployeePositions.Insert(qae);
            Utility.dicEmpPosition.Add("QAE", 3);
            unit.EmployeePositions.Insert(mgr);
            Utility.dicEmpPosition.Add("MGR", 4);
            unit.EmployeePositions.Insert(hrm);
            Utility.dicEmpPosition.Add("HRM", 5);
            unit.EmployeePositions.Insert(ceo);
            Utility.dicEmpPosition.Add("CEO", 6);
            unit.EmployeePositions.Insert(cto);
            Utility.dicEmpPosition.Add("CTO", 7);
            unit.EmployeePositions.Insert(coo);
            Utility.dicEmpPosition.Add("COO", 8);
            unit.Save();
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
                    Status = unit.EmployeeStatuses.Get(rawData.ReadInteger(row, 11) + 1),
                    Position = unit.EmployeePositions.Get(Utility.dicEmpPosition[rawData.ReadString(row, 12)])
                };
                User u = UsersUtility.CreateUser(e);
                unit.Users.Insert(u);
                unit.Employees.Insert(e);
                unit.Save();
                Utility.dicEmpl.Add(oldId, e.Id);
                N++;
            }
            Console.WriteLine(N);
        }
    }
}
