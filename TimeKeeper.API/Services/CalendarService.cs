using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeKeeper.API.Factory;
using TimeKeeper.API.Models;
using TimeKeeper.DAL;
using TimeKeeper.Domain;

namespace TimeKeeper.API.Services
{
    public class CalendarService
    {
        protected UnitOfWork Unit;
        public CalendarService(UnitOfWork unit)
        {
            Unit = unit;
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
                    overtime = GetOvertimeHours(calendar.FindAll(x => x.DayType.Name == "workday").ToList());
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

        private decimal GetOvertimeHours(List<DayModel> workDays)
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

        public List<DayModel> GetEmployeeMonth(int empId, int year, int month)
        {
            List<DayModel> calendar = new List<DayModel>();
            if (!ValidateGetEmployeeMonth(year, month)) throw new Exception("Invalid data! Check year and month again.");
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

        public bool ValidateGetEmployeeMonth(int year, int month)
        {
            if (month > 12 || month < 1)
                return false;

            if (year < 2010)//Founding year
                return false;

            DateTime date = new DateTime(year, month, 1);

            //User can not see more than 6 months in advance
            if (date > DateTime.Today.AddMonths(6))
                return false;

            return true;
        }
        public bool IsWeekend(DateTime day)
        {
            if (day.DayOfWeek == DayOfWeek.Sunday || day.DayOfWeek == DayOfWeek.Saturday) return true;
            return false;
        }
        public int GetMonthWeekDays(int month, int year)
        {
            DateTime day = new DateTime(year, month, 1);
            int monthWeekDays = 0;
            while (day.Month == month)
            {
                if (IsWeekend(day)) { day = day.AddDays(1); continue; }
                monthWeekDays++;
                day = day.AddDays(1);
            }
            return monthWeekDays;
        }
        public TeamDashboardModel GetTeamDashboardInfo(int teamId, int year, int month)
        {            
            TeamDashboardModel teamDashboardModel = new TeamDashboardModel();

            List<EmployeeTimeModel> teamMonthReport = GetTeamMonthReport(teamId, year, month);

            teamDashboardModel.EmployeesCount = teamMonthReport.Count(); //get this from team
            teamDashboardModel.ProjectsCount = Unit.Teams.Get(teamId).Projects.Count();
            teamDashboardModel.BaseTotalHours = GetMonthWeekDays(month, year) * 8; //base Total Hours
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

            adminDashboardModel.BaseTotalHours = GetMonthWeekDays(month, year) * 8;
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

        public MonthlyOverviewModel GetMonthlyOverview(int year, int month)
        {
            MonthlyOverviewModel monthlyOverview = new MonthlyOverviewModel();
            List<EmployeeModel> employees = new List<EmployeeModel>();
            List<Assignment> tasks = Unit.Assignments.Get().Where(x => x.Day.Date.Year == year && x.Day.Date.Month == month).ToList();
            List<Project> projects = new List<Project>();

            foreach (Assignment task in tasks)
            {
                if (!IsDuplicateProject(projects, task.Project))
                {
                    projects.Add(task.Project);
                }
            }
            Dictionary<string, decimal> projectColumns = SetMonthlyOverviewColumns(projects);
            monthlyOverview.HoursByProject = projectColumns;
            monthlyOverview.TotalHours = 0;
            monthlyOverview.EmployeeProjectHours = new List<EmployeeMonthlyProjectModel>();

            foreach (Assignment task in tasks)
            {
                if (!IsDuplicateEmployeeInMonthlyOverview(employees, task.Day.Employee))
                {
                    employees.Add(task.Day.Employee.Create());
                }
            }

            foreach (EmployeeModel emp in employees)
            {
                List<Assignment> employeeTasks = tasks.Where(x => x.Day.Employee.Id == emp.Id).ToList();
                EmployeeMonthlyProjectModel employeeProjectModel = GetEmployeeMonthlyOverview(projects, emp, employeeTasks);
                foreach (KeyValuePair<string, decimal> keyValuePair in employeeProjectModel.HoursByProject)
                {
                    monthlyOverview.HoursByProject[keyValuePair.Key] += keyValuePair.Value;
                }

                monthlyOverview.EmployeeProjectHours.Add(employeeProjectModel);
                monthlyOverview.TotalPossibleWorkingHours = monthlyOverview.TotalWorkingDays * 8;
            }

            monthlyOverview.TotalWorkingDays = GetMonthlyWorkingDays(year, month);
            monthlyOverview.TotalPossibleWorkingHours = monthlyOverview.TotalWorkingDays * 8;
            return monthlyOverview;
        }

        public EmployeeMonthlyProjectModel GetEmployeeMonthlyOverview(List<Project> projects, EmployeeModel employee, List<Assignment> tasks)
        {
            Dictionary<string, decimal> projectColumns = SetMonthlyOverviewColumns(projects);
            EmployeeMonthlyProjectModel employeeProject = new EmployeeMonthlyProjectModel();
            employeeProject.Employee = Unit.Employees.Get(employee.Id).Master();
            employeeProject.HoursByProject = projectColumns;
            employeeProject.TotalHours = 0;
            employeeProject.PaidTimeOff = 0;

            foreach (Assignment task in tasks)
            {
                employeeProject.HoursByProject[task.Project.Name] += task.Hours;
                employeeProject.TotalHours += task.Hours;
                if (task.Day.IsAbsence()) employeeProject.PaidTimeOff += 8;
            }
            return employeeProject;
        }

        public ProjectAnnualOverviewModel GetProjectAnnualOverview(int projectId, int year)
        {
            var project = Unit.Projects.Get(projectId);
            List<Assignment> tasksOnProjects = Unit.Assignments.Get().Where(x => x.Project.Id == projectId && x.Day.Date.Year == year).ToList();
            ProjectAnnualOverviewModel projectAnnualOverview = new ProjectAnnualOverviewModel { Project = project.Master() };

            projectAnnualOverview.Months = SetMonths();

            foreach (Assignment task in tasksOnProjects)
            {
                projectAnnualOverview.Months[task.Day.Date.Month] += task.Hours;
                projectAnnualOverview.Total += task.Hours;
            }

            return projectAnnualOverview;
        }

        public TotalAnnualOverviewModel GetTotalAnnualOverview(int year)
        {
            TotalAnnualOverviewModel totalAnnualOverview = new TotalAnnualOverviewModel();
            List<ProjectAnnualOverviewModel> annualList = new List<ProjectAnnualOverviewModel>();

            List<Project> projectsInYear = Unit.Projects.Get().ToList();
            totalAnnualOverview.Months = SetMonths();

            foreach (Project projectInYear in projectsInYear)
            {
                List<Assignment> tasksInYear = Unit.Assignments.Get().Where(x => x.Project.Id == projectInYear.Id && x.Day.Date.Year == year).ToList();
                ProjectAnnualOverviewModel projectOverview = GetProjectAnnualOverview(projectInYear.Id, year);
                annualList.Add(projectOverview);
                totalAnnualOverview.Projects = annualList.ToList();

                foreach (Assignment taskInYear in tasksInYear)
                {
                    totalAnnualOverview.Months[taskInYear.Day.Date.Month] += taskInYear.Hours;
                    totalAnnualOverview.SumTotal += taskInYear.Hours;
                }
            }

            return totalAnnualOverview;
        }

        public Dictionary<string, decimal> SetMonthlyOverviewColumns(List<Project> projects)
        {
            Dictionary<string, decimal> projectColumns = new Dictionary<string, decimal>();
            foreach (Project project in projects)
            {
                projectColumns.Add(project.Name, 0);
            }
            return projectColumns;
        }

        public Dictionary<int, decimal> SetMonths()
        {
            Dictionary<int, decimal> HoursPerMonth = new Dictionary<int, decimal>();

            for (int i = 1; i <= 12; i++)
            {
                HoursPerMonth.Add(i, 0);
            }
            return HoursPerMonth;
        }

        public int GetMonthlyWorkingDays(int year, int month)
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
        public Dictionary<int, decimal> SetYearsColumns(int projectId)
        {
            Dictionary<int, decimal> yearColumns = new Dictionary<int, decimal>();
            List<Assignment> tasks = Unit.Projects.Get().ToList().FirstOrDefault(x => x.Id == projectId).Tasks.ToList();

            foreach (Assignment a in tasks)
            {
                if (!IsDuplicateYear(yearColumns, a.Day.Date.Year))
                    yearColumns.Add(a.Day.Date.Year, 0);
            }
            return yearColumns;
        }
        public bool IsDuplicateEmployee(List<Employee> employees, Employee employee)
        {
            if (employees.Count == 0) return false;
            foreach (Employee emp in employees)
            {
                if (emp.Id == employee.Id)
                {
                    return true;
                }
            }
            return false;
        }
        public bool IsDuplicateYear(Dictionary<int, decimal> yearColumns, int year)
        {
            if (yearColumns.ContainsKey(year))
                return true;

            return false;
        }
        public ProjectHistoryModel GetProjectHistoryModel(int projectId)
        {
            ProjectHistoryModel projectHistory = new ProjectHistoryModel();

            List<Assignment> tasks = Unit.Projects.Get(projectId).Tasks.ToList();
            List<Employee> employees = new List<Employee>();

            foreach (Assignment a in tasks)
            {
                if (Unit.Employees.Get().ToList().FirstOrDefault(x => x.Id == a.Day.Employee.Id) != null && !IsDuplicateEmployee(employees, a.Day.Employee))
                {
                    employees.Add(Unit.Employees.Get(a.Day.Employee.Id));
                }
            }

            foreach (Employee emp in employees)
            {
                EmployeeProjectModel e = new EmployeeProjectModel
                {
                    EmployeeName = emp.FullName,
                    HoursPerYears = SetYearsColumns(projectId),
                    TotalHoursPerProjectPerEmployee = 0
                };
                foreach (Assignment a in tasks)
                {
                    if (a.Day.Employee.Id == emp.Id && e.HoursPerYears.ContainsKey(a.Day.Date.Year))
                    {
                        e.HoursPerYears[a.Day.Date.Year] += a.Hours;
                    }
                }
                foreach (KeyValuePair<int, decimal> keyValuePair in e.HoursPerYears)
                {
                    e.TotalHoursPerProjectPerEmployee += keyValuePair.Value;
                }
                projectHistory.Employees.Add(e);
            }
            projectHistory.TotalYearlyProjectHours = SetYearsColumns(projectId);

            foreach (EmployeeProjectModel empProjectModel in projectHistory.Employees)
            {
                foreach (KeyValuePair<int, decimal> keyValuePair in empProjectModel.HoursPerYears)
                {
                    projectHistory.TotalYearlyProjectHours[keyValuePair.Key] += keyValuePair.Value;
                }
                projectHistory.TotalHoursPerProject += empProjectModel.TotalHoursPerProjectPerEmployee;
            }
            return projectHistory;
        }

        public bool IsDuplicateProject(List<Project> projects, Project project)
        {
            foreach (Project p in projects)
            {
                if (p.Id == project.Id)
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsDuplicateEmployeeInMonthlyOverview(List<EmployeeModel> employees, Employee employee)
        {
            foreach (EmployeeModel emp in employees)
            {
                if (emp.Id == employee.Id)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
