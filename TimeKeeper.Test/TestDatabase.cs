using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TimeKeeper.DAL;
using TimeKeeper.Domain;
using TimeKeeper.Seed;

namespace TimeKeeper.Test
{
    public static class TestDatabase
    {
        public static void Seed(this UnitOfWork unit, FileInfo file)
        {
            using(ExcelPackage package = new ExcelPackage(file))
            {
                unit.Context.Database.EnsureDeleted();
                unit.Context.Database.EnsureCreated();

                var sheets = package.Workbook.Worksheets;
                Teams.Collect(sheets["Teams"], unit);
                Roles.Collect(sheets["Roles"], unit);
                Customers.Collect(sheets["Customers"], unit);
                Projects.Collect(sheets["Projects"], unit);
                Employees.Collect(sheets["Employees"], unit);
                Members.Collect(sheets["Engagement"], unit);
                Calendar.Collect(sheets["Calendar"], unit);
                Assignments.Collect(sheets["Details"], unit);
            }
        }
    }
}

