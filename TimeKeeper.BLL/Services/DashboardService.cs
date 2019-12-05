using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimeKeeper.DAL;
using TimeKeeper.Domain;
using TimeKeeper.DTO.Factory;
using TimeKeeper.DTO.Models;

namespace TimeKeeper.BLL.Services
{
    public class DashboardService
    {
        protected UnitOfWork Unit;
        public DashboardService(UnitOfWork unit)
        {
            Unit = unit;
        }
        public int GetNumberOfEmployeesForTimePeriod(int month, int year)
        {
            return Unit.Employees.Get(x => x.BeginDate < new DateTime(year, month, DateTime.DaysInMonth(year, month)) //if employees begin date is in required month
                            && (x.EndDate == null || x.EndDate == new DateTime(1, 1, 1) || x.EndDate > new DateTime(year, month, DateTime.DaysInMonth(year, month)))).Count(); // still works in company, or left company after required month          
        }
        public int GetNumberOfProjectsForTimePeriod(int month, int year)
        {
            return Unit.Projects.Get(x => x.StartDate < new DateTime(year, month, DateTime.DaysInMonth(year, month)) //if project began is in required month
                            && (x.EndDate == null || x.EndDate == new DateTime(1, 1, 1) || x.EndDate > new DateTime(year, month, DateTime.DaysInMonth(year, month)))).Count(); // project still in progress, or ended after the required month          
        }
        public AdminDashboardModel GetAdminDashboardModel(int year, int month)
        {
            AdminDashboardModel adminDashboardModel = new AdminDashboardModel();

            adminDashboardModel.NumberOfEmployees = GetNumberOfEmployeesForTimePeriod(month, year);
            adminDashboardModel.NumberOfProjects = GetNumberOfProjectsForTimePeriod(month, year);

            adminDashboardModel.BaseTotalHours = Providers.GetMonthWeekDays(month, year) * 8;
            adminDashboardModel.TotalHours = adminDashboardModel.BaseTotalHours * adminDashboardModel.NumberOfEmployees;

            List<int> teamIds = Unit.Teams.Get().Select(x => x.Id).ToList();

            foreach (int teamId in teamIds)
            {
                MasterModel team = Unit.Teams.Get(teamId).Master();
                TeamDashboardModel teamDashboardModel = GetTeamDashboardInfo(teamId, year, month);

                //missing entries by team
                adminDashboardModel.MissingEntries.Add(new TeamKeyDictionary(team, teamDashboardModel.EmployeeMissingEntries.Sum(x => x.Value)));
                //pto hours by team
                adminDashboardModel.PTOHours.Add(new TeamKeyDictionary(team, teamDashboardModel.EmployeePTO.Sum(x => x.Value)));
                //overtime horus by team
                adminDashboardModel.OvertimeHours.Add(new TeamKeyDictionary(team, teamDashboardModel.EmployeeOvertime.Sum(x => x.Value)));
            }

            return adminDashboardModel;
        }
        public List<DayModel> GetEmployeeMonth(int empId, int year, int month)
        {
            List<DayModel> calendar = new List<DayModel>();
            if (!Validators.ValidateGetEmployeeMonth(year, month)) throw new Exception("Invalid data! Check year and month again.");
            DayType future = new DayType { Id = 10, Name = "Future" }; // svaki dan naredni od danasnjeg
            DayType weekend = new DayType { Id = 11, Name = "Weekend" };
            DayType empty = new DayType { Id = 12, Name = "Empty" };
            //to rethink name
            DayType na = new DayType { Id = 13, Name = "N/A" };
            DateTime day = new DateTime(year, month, 1);
            Employee emp = Unit.Employees.Get(empId);
            while (day.Month == month)
            {
                DayModel newDay = new DayModel
                {
                    Employee = emp.Master(),
                    Date = day,
                    DayType = empty.Master()
                };
                if (day.DayOfWeek == DayOfWeek.Sunday || day.DayOfWeek == DayOfWeek.Saturday) newDay.DayType = weekend.Master();
                if (day > DateTime.Today) newDay.DayType = future.Master();
                if (day < emp.BeginDate || (emp.EndDate != null && emp.EndDate != new DateTime(1, 1, 1) && day > emp.EndDate)) newDay.DayType = na.Master();
                calendar.Add(newDay);
                day = day.AddDays(1);
            }
            List<DayModel> employeeDays = Unit.Calendar.Get(x => x.Employee.Id == empId && x.Date.Year == year && x.Date.Month == month).ToList().Select(x => x.Create()).ToList();
            foreach (var d in employeeDays)
            {
                calendar[d.Date.Day - 1] = d;
            }
            return calendar;
        }
        public EmployeeTimeModel CreateEmployeeReport(int employeeId, int year, int month)
        {
            Employee employee = Unit.Employees.Get(employeeId);
            EmployeeTimeModel employeePersonalReport = employee.CreateEmployeeTimeModel();
            List<DayModel> calendar = GetEmployeeMonth(employeeId, year, month);
            List<DayType> dayTypes = Unit.DayTypes.Get().ToList();

            int totalHours = 0;
            decimal overtime = 0;

            foreach (DayType day in dayTypes)
            {
                int sumHoursPerDay = (int)calendar.FindAll(x => x.DayType.Id == day.Id).Sum(x => x.TotalHours);

                if (day.Name == "workday" || day.Name == "weekend")
                {
                    overtime = Providers.GetOvertimeHours(calendar.FindAll(x => x.DayType.Name == "workday").ToList());
                }

                employeePersonalReport.HourTypes.Add(day.Name, sumHoursPerDay);
                totalHours += sumHoursPerDay;
            }
            int missingEntries = calendar.FindAll(x => x.DayType.Name == "Empty").Count() * 8;

            employeePersonalReport.HourTypes.Add("missingEntries", missingEntries);
            employeePersonalReport.OverTime = overtime;
            employeePersonalReport.HourTypes.Add("totalHours", totalHours + missingEntries);
            employeePersonalReport.PTO = employeePersonalReport.HourTypes["totalHours"] - employeePersonalReport.HourTypes["missingEntries"] - employeePersonalReport.HourTypes["workday"];

            return employeePersonalReport;
        }
        public List<EmployeeTimeModel> GetTeamMonthReport(int teamId, int year, int month)
        {
            Team team = Unit.Teams.Get(teamId);
            List<EmployeeTimeModel> employeeTimeModels = new List<EmployeeTimeModel>();

            foreach (Member member in team.TeamMembers)
            {
                employeeTimeModels.Add(CreateEmployeeReport(member.Employee.Id, year, month));
            }

            return employeeTimeModels;
        }
        public TeamDashboardModel GetTeamDashboardInfo(int teamId, int year, int month)
        {
            TeamDashboardModel teamDashboardModel = new TeamDashboardModel();

            List<EmployeeTimeModel> teamMonthReport = GetTeamMonthReport(teamId, year, month);

            teamDashboardModel.EmployeesCount = teamMonthReport.Count(); //get this from team
            teamDashboardModel.ProjectsCount = Unit.Teams.Get(teamId).Projects.Count();
            teamDashboardModel.BaseTotalHours = Providers.GetMonthWeekDays(month, year) * 8; //base Total Hours
            teamDashboardModel.TotalHours = teamDashboardModel.BaseTotalHours * teamDashboardModel.EmployeesCount; // base total hours multiplied by number of employees


            teamDashboardModel.WorkingHours = teamMonthReport.Sum(x => x.HourTypes["workday"]);

            foreach (EmployeeTimeModel report in teamMonthReport)
            {
                //PTO
                teamDashboardModel.EmployeePTO.Add(new EmployeeKeyDictionary(report.Employee, report.PTO));

                //Overtime
                teamDashboardModel.EmployeeOvertime.Add(new EmployeeKeyDictionary(report.Employee, report.OverTime));

                //MissingEntries
                teamDashboardModel.EmployeeMissingEntries.Add(new EmployeeKeyDictionary(report.Employee, report.HourTypes["missingEntries"]));
            }

            return teamDashboardModel;
        }
    }
}
