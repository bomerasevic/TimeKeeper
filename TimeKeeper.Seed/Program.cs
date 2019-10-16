using OfficeOpenXml;
using System;
using System.IO;
using TimeKeeper.DAL;

namespace TimeKeeper.Seed
{
    class Program
    {
        static readonly string fileLocation = @"C:\TimeKeeper_DATA\TimeKeeper.xlsx";

        static void Main()
        {
            FileInfo file = new FileInfo(fileLocation);
            using(ExcelPackage package = new ExcelPackage(file))
            {
                using(UnitOfWork unit = new UnitOfWork(new TimeKeeperContext()))
                {
                    unit.Context.Database.EnsureDeleted();
                    unit.Context.Database.EnsureCreated();
                    //unit.Context.ChangeTracker.AutoDetectChangesEnabled = false;

                    var sheets = package.Workbook.Worksheets;
                    Teams.Collect(sheets["Teams"], unit);
                    Roles.Collect(sheets["Roles"], unit);
                    Customers.Collect(sheets["Customers"], unit);
                    Projects.Collect(sheets["Projects"], unit);
                    Employees.Collect(sheets["Employees"], unit);
                    Members.Collect(sheets["Members"], unit);
                    Calendar.Collect(sheets["Calendar"], unit);
                    Assignments.Collect(sheets["Assignments"], unit);
                }
            }
            Console.WriteLine("All tasks done!");
            Console.ReadKey();
        }
    }
}
