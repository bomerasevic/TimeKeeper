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

            ProjectStatus inProgress = new ProjectStatus { Name = "in progress" };
            ProjectStatus onHold = new ProjectStatus { Name = "on hold" };
            ProjectStatus finished = new ProjectStatus { Name = "finished" };
            ProjectStatus cancelled = new ProjectStatus { Name = "cancelled" };
            unit.ProjectStatuses.Insert(inProgress);
            unit.ProjectStatuses.Insert(onHold);
            unit.ProjectStatuses.Insert(finished);
            unit.ProjectStatuses.Insert(cancelled);
            unit.Save();

            ProjectPricing fixedBid = new ProjectPricing { Name = "fixed bid" };
            ProjectPricing hourly = new ProjectPricing { Name = "hourly" };
            ProjectPricing perCapita = new ProjectPricing { Name = "per capita" };
            ProjectPricing proBono = new ProjectPricing { Name = "pro bono" };
            unit.ProjectPrices.Insert(fixedBid);
            unit.ProjectPrices.Insert(hourly);
            unit.ProjectPrices.Insert(perCapita);
            unit.ProjectPrices.Insert(proBono);
            unit.Save();

            int N = 0;
            for (int row = 2; row <= rawData.Dimension.Rows; row++)
            {
                int oldId = rawData.ReadInteger(row, 1);
                Project p = new Project
                {
                    Name = rawData.ReadString(row, 2),
                    Description = rawData.ReadString(row, 4),
                    StartDate = rawData.ReadDateProjects(row, 5),
                    EndDate = rawData.ReadDateProjects(row, 6),
                    Status = unit.ProjectStatuses.Get(rawData.ReadInteger(row, 7)), 
                    Customer = unit.Customers.Get(Utility.dicCust[rawData.ReadInteger(row, 8)]),
                    Team = unit.Teams.Get(Utility.dicTeam[rawData.ReadString(row, 9)]),
                    Pricing = unit.ProjectPrices.Get(rawData.ReadInteger(row, 10) + 1),
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
