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

        public List<EmployeeTimeModel> TeamMonthReport(int teamId, int year, int month)
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
            EmployeeTimeModel employeePersonalReport = employee.CreateTimeModel();
            List<DayModel> calendar = GetEmployeeMonth(employeeId, year, month);

            //SetDayTypes(employeePersonalReport.HourTypes);
            //Dictionary<string, decimal> hours = employeePersonalReport.HourTypes;

            //hours.Add("Total hours", 0);
            List<DayType> dayTypes = Unit.DayTypes.Get().ToList();
            int totalHours = 0;

            foreach (DayType day in dayTypes)
            {
                ////Adds the day's total hours to the appropriate place in the dictionary (to the appropriate day type)
                ////hours[day.DayType.Name] += day.JobDetails.Where(x => x.Project.Id == project.Id).Sum(jd => jd.Hours); 
                ////hours[day.DayType.Name] += day.TotalHours;//CalculateHoursOnProject(day, project);
                //hours["Total hours"] += day.TotalHours;
                //hours["Missing entries"] -= day.TotalHours;
                int sumHoursPerDay = (int)calendar.FindAll(x => x.DayType.Id == day.Id).Sum(x => x.TotalHours);
                employeePersonalReport.HourTypes.Add(day.Name, sumHoursPerDay);
                totalHours += sumHoursPerDay;
            }
            int MissingEntries = calendar.FindAll(x => x.DayType.Name == "Empty").Count() * 8;
            employeePersonalReport.HourTypes.Add("MissingEntries", MissingEntries);
            employeePersonalReport.HourTypes.Add("TotalHours", totalHours + MissingEntries);
            //hours.Add("Missing entries", calendar.FindAll(x => x.DayType.Name == "Empty").Sum(x => x.TotalHours));
            return employeePersonalReport;
        }

        private void SetDayTypes(Dictionary<string, decimal> hourTypes)
        {
            List<DayType> dayTypes = Unit.DayTypes.Get().ToList();
            DayType future = new DayType { Id = 10, Name = "Future" };
            DayType weekend = new DayType { Id = 11, Name = "Weekend" };
            DayType empty = new DayType { Id = 12, Name = "Empty" };
            //REFACTOR
            DayType na = new DayType { Id = 13, Name = "N/A" };
            foreach (DayType day in dayTypes)
            {
                hourTypes.Add(day.Name, 0);
            }
            hourTypes.Add(future.Name, 0);
            hourTypes.Add(empty.Name, 0);
            hourTypes.Add(weekend.Name, 0);
            hourTypes.Add(na.Name, 0);
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

        public ProjectHistoryModel GetProjectHistoryModel(int projectId)
        {
            ProjectHistoryModel projectHistory = new ProjectHistoryModel();
            projectHistory.Projects = Unit.Projects.Get().Select(x => x.Master()).ToList();

            foreach (MasterModel p in projectHistory.Projects)
            {
                if (p.Id == projectId)
                {

                }
            }
            return projectHistory;
        }

        //public List<DayModel> GetEmployeeMonth(int empId, int year, int month)
        //{
        //    List<DayModel> calendar = new List<DayModel>();

        //    if (!ValidateGetEmployeeMonth(year, month)) throw new Exception("Invalid data! Check year and month again!");


        //    DayType future = new DayType { Id = 10, Name = "Future" };
        //    DayType weekend = new DayType { Id = 11, Name = "Weekend" };
        //    DayType empty = new DayType { Id = 12, Name = "Empty" };
        //    //REFACTOR
        //    DayType na = new DayType { Id = 13, Name = "N/A" };

        //    DateTime day = new DateTime(year, month, 1);

        //    Employee emp = Unit.Employees.Get(empId);

        //    while (day.Month == month)
        //    {
        //        DayModel newDay = new DayModel
        //        {
        //            Employee = emp.Master(),
        //            Date = day,
        //            DayType = empty.Master()
        //        };

        //        if (day.DayOfWeek == DayOfWeek.Sunday || day.DayOfWeek == DayOfWeek.Saturday) newDay.DayType = weekend.Master();

        //        if (day > DateTime.Today) newDay.DayType = future.Master(); else newDay.DayType = empty.Master();

        //        if (day < emp.BeginDate || (emp.EndDate != null && emp.EndDate != new DateTime(1 / 1 / 1) && day > emp.EndDate)) newDay.DayType = na.Master();

        //        //TODO: Uslov kad je uposlenik poceo raditi
        //        calendar.Add(newDay);
        //        day = day.AddDays(1);
        //    }

        //    List<DayModel> employeeDays = Unit.Calendar.Get(x => x.Employee.Id == empId && x.Date.Year == year && x.Date.Month == month).ToList().Select(x => x.Create()).ToList();

        //    foreach (var d in employeeDays)
        //    {
        //        calendar[d.Date.Day - 1] = d;
        //    }
        //    return calendar;
        //}

        //private Dictionary<EmployeeTimeModel, decimal> GetPaidTimeOff(List<EmployeeTimeModel> employeeTimes)
        //{
        //    Dictionary<EmployeeModel, decimal> paidTimeOff = new Dictionary<EmployeeModel, decimal>();
        //    foreach (EmployeeTimeModel employeeTime in employeeTimes)
        //    {
        //        decimal PTO = employeeTime.HourTypes["Total Hours"]
        //                    - employeeTime.HourTypes["Missing entries"]
        //                    - employeeTime.HourTypes["Workday"];
        //        paidTimeOff.Add(employeeTime.Employee, PTO);
        //    }

        //    return paidTimeOff;
        //}

        //private Dictionary<EmployeeTimeModel, decimal> GetOvertime(List<EmployeeTimeModel> employeeTimes)
        //{
        //    Dictionary<EmployeeModel, decimal> overtime = new Dictionary<EmployeeModel, decimal>();
        //    foreach (EmployeeTimeModel employeeTime in employeeTimes)
        //    {
        //        decimal PTO = employeeTime.HourTypes["Total Hours"]
        //                    - employeeTime.HourTypes["Missing entries"]
        //                    - employeeTime.HourTypes["Workday"];
        //        overtime.Add(employeeTime.Employee, PTO);
        //    }

        //    return overtime;
        //}

        //private decimal GetMonthlyWorkingHours(int empId, int year, int month)
        //{
        //    //List<DayModel> calendar = new List<DayModel>();
        //    int daysInMonth = DateTime.DaysInMonth(year, month);

        //    int workingDays = 0;
        //    for (int i = 1; i < daysInMonth; i++)
        //    {
        //        DateTime thisDay = new DateTime(year, month, i);
        //        if (thisDay.DayOfWeek != DayOfWeek.Saturday && thisDay.DayOfWeek != DayOfWeek.Sunday)
        //        {
        //            workingDays += 1;
        //        }
        //    }

        //    return workingDays;
        //}

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
                if (!IsDuplicateEmployee(employees, task.Day.Employee))
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

            foreach(Project projectInYear in projectsInYear)
            {
                List<Assignment> tasksInYear = Unit.Assignments.Get().Where(x => x.Project.Id == projectInYear.Id && x.Day.Date.Year == year).ToList();
                ProjectAnnualOverviewModel projectOverview = GetProjectAnnualOverview(projectInYear.Id, year);
                annualList.Add(projectOverview);
                totalAnnualOverview.Projects = annualList.ToList();

                foreach(Assignment taskInYear in tasksInYear)
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

        public bool IsDuplicateEmployee(List<EmployeeModel> employees, Employee employee)
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
