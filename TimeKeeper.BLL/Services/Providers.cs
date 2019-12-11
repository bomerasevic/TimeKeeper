using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeper.DAL;
using TimeKeeper.Domain;
using TimeKeeper.DTO.Models;
using TimeKeeper.DTO.Models.DomainModels;

namespace TimeKeeper.BLL.Services
{
    public static class Providers
    {
        public static decimal GetOvertimeHours(List<DayModel> workDays)
        {
            decimal overtime = 0;
            foreach (DayModel day in workDays)
            {
                if (day.DayType.Name == "weekend" && day.TotalHours <= 5)
                {
                    overtime += day.TotalHours;
                }

                if (day.DayType.Name == "workday" && day.TotalHours > 8 && day.TotalHours <= 12)
                {
                    overtime += day.TotalHours - 8;
                }
            }

            return overtime;
        }
        public static int GetMonthWeekDays(int month, int year)
        {
            DateTime day = new DateTime(year, month, 1);
            int monthWeekDays = 0;
            while (day.Month == month)
            {
                if (Validators.IsWeekend(day)) { day = day.AddDays(1); continue; }
                monthWeekDays++;
                day = day.AddDays(1);
            }
            return monthWeekDays;
        }
        public static Dictionary<int, decimal> SetMonths()
        {
            Dictionary<int, decimal> HoursPerMonth = new Dictionary<int, decimal>();

            for (int i = 1; i <= 12; i++)
            {
                HoursPerMonth.Add(i, 0);
            }
            return HoursPerMonth;
        }
        public static int GetMonthlyWorkingDays(int year, int month)
        {
            int daysInMonth = DateTime.DaysInMonth(year, month);
            int workingDays = 0;

            for (int i = 1; i <= daysInMonth; i++)
            {
                DateTime thisDay = new DateTime(year, month, i);
                if (thisDay.DayOfWeek != DayOfWeek.Saturday && thisDay.DayOfWeek != DayOfWeek.Sunday)
                {
                    workingDays += 1;
                }
            }
            return workingDays;
        }
        public static Dictionary<string, decimal> SetMonthlyOverviewColumns(List<Project> projects)
        {
            Dictionary<string, decimal> projectColumns = new Dictionary<string, decimal>();
            foreach (Project project in projects)
            {
                projectColumns.Add(project.Name, 0);
            }
            return projectColumns;
        }
        public static Dictionary<int, decimal> SetYearsColumns(List<Assignment> tasks)
        {
            Dictionary<int, decimal> yearColumns = new Dictionary<int, decimal>();

            foreach (Assignment a in tasks)
            {
                if (!yearColumns.ContainsKey(a.Day.Date.Year))
                    yearColumns.Add(a.Day.Date.Year, 0);
            }
            return yearColumns;
        }
    }
}
