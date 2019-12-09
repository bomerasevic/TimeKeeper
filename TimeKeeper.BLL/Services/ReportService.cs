//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using TimeKeeper.DAL;
//using TimeKeeper.Domain;
//using TimeKeeper.DTO.Factory;
//using TimeKeeper.DTO.Models;

//namespace TimeKeeper.BLL.Services
//{
//    public class ReportService
//    {
//        protected UnitOfWork Unit;
//        public ReportService(UnitOfWork unit)
//        {
//            Unit = unit;
//        }
//        public EmployeeMonthlyProjectModel GetEmployeeMonthlyOverview(List<Project> projects, EmployeeModel employee, List<Assignment> tasks)
//        {
//            Dictionary<string, decimal> projectColumns = Providers.SetMonthlyOverviewColumns(projects);
//            EmployeeMonthlyProjectModel employeeProject = new EmployeeMonthlyProjectModel();
//            employeeProject.Employee = Unit.Employees.Get(employee.Id).Master();
//            employeeProject.HoursByProject = projectColumns;
//            employeeProject.TotalHours = 0;
//            employeeProject.PaidTimeOff = 0;

//            foreach (Assignment task in tasks)
//            {
//                employeeProject.HoursByProject[task.Project.Name] += task.Hours;
//                employeeProject.TotalHours += task.Hours;
//                if (task.Day.IsAbsence()) employeeProject.PaidTimeOff += 8;
//            }
//            return employeeProject;
//        }
//        public MonthlyOverviewModel GetMonthlyOverview(int year, int month)
//        {
//            MonthlyOverviewModel monthlyOverview = new MonthlyOverviewModel();
//            List<EmployeeModel> employees = new List<EmployeeModel>();
//            List<Assignment> tasks = Unit.Assignments.Get().Where(x => x.Day.Date.Year == year && x.Day.Date.Month == month).ToList();
//            List<Project> projects = new List<Project>();

//            foreach (Assignment task in tasks)
//            {
//                if (!Validators.IsDuplicateProject(projects, task.Project))
//                {
//                    projects.Add(task.Project);
//                }
//            }
//            Dictionary<string, decimal> projectColumns = Providers.SetMonthlyOverviewColumns(projects);
//            monthlyOverview.HoursByProject = projectColumns;
//            monthlyOverview.TotalHours = 0;
//            monthlyOverview.EmployeeProjectHours = new List<EmployeeMonthlyProjectModel>();

//            foreach (Assignment task in tasks)
//            {
//                if (!Validators.IsDuplicateEmployeeInMonthlyOverview(employees, task.Day.Employee))
//                {
//                    employees.Add(task.Day.Employee.Create());
//                }
//            }

//            foreach (EmployeeModel emp in employees)
//            {
//                List<Assignment> employeeTasks = tasks.Where(x => x.Day.Employee.Id == emp.Id).ToList();
//                EmployeeMonthlyProjectModel employeeProjectModel = GetEmployeeMonthlyOverview(projects, emp, employeeTasks);
//                foreach (KeyValuePair<string, decimal> keyValuePair in employeeProjectModel.HoursByProject)
//                {
//                    monthlyOverview.HoursByProject[keyValuePair.Key] += keyValuePair.Value;
//                }

//                monthlyOverview.EmployeeProjectHours.Add(employeeProjectModel);
//                monthlyOverview.TotalPossibleWorkingHours = monthlyOverview.TotalWorkingDays * 8;
//            }

//            monthlyOverview.TotalWorkingDays = Providers.GetMonthlyWorkingDays(year, month);
//            monthlyOverview.TotalPossibleWorkingHours = monthlyOverview.TotalWorkingDays * 8;
//            return monthlyOverview;
//        }
//        public ProjectHistoryModel GetProjectHistoryModel(int projectId)
//        {
//            ProjectHistoryModel projectHistory = new ProjectHistoryModel();

//            List<Assignment> tasks = Unit.Projects.Get(projectId).Tasks.ToList();
//            List<Employee> employees = new List<Employee>();

//            foreach (Assignment a in tasks)
//            {
//                if (Unit.Employees.Get().ToList().FirstOrDefault(x => x.Id == a.Day.Employee.Id) != null && !Validators.IsDuplicateEmployee(employees, a.Day.Employee))
//                {
//                    employees.Add(Unit.Employees.Get(a.Day.Employee.Id));
//                }
//            }

//            foreach (Employee emp in employees)
//            {
//                EmployeeProjectModel e = new EmployeeProjectModel
//                {
//                    EmployeeName = emp.FullName,
//                    HoursPerYears = Providers.SetYearsColumns(tasks),
//                    TotalHoursPerProjectPerEmployee = 0
//                };
//                foreach (Assignment a in tasks)
//                {
//                    if (a.Day.Employee.Id == emp.Id && e.HoursPerYears.ContainsKey(a.Day.Date.Year))
//                    {
//                        e.HoursPerYears[a.Day.Date.Year] += a.Hours;
//                    }
//                }
//                foreach (KeyValuePair<int, decimal> keyValuePair in e.HoursPerYears)
//                {
//                    e.TotalHoursPerProjectPerEmployee += keyValuePair.Value;
//                }
//                projectHistory.Employees.Add(e);
//            }
//            projectHistory.TotalYearlyProjectHours = Providers.SetYearsColumns(tasks);

//            foreach (EmployeeProjectModel empProjectModel in projectHistory.Employees)
//            {
//                foreach (KeyValuePair<int, decimal> keyValuePair in empProjectModel.HoursPerYears)
//                {
//                    projectHistory.TotalYearlyProjectHours[keyValuePair.Key] += keyValuePair.Value;
//                }
//                projectHistory.TotalHoursPerProject += empProjectModel.TotalHoursPerProjectPerEmployee;
//            }
//            return projectHistory;
//        }
//        public ProjectAnnualOverviewModel GetProjectAnnualOverview(int projectId, int year)
//        {
//            var project = Unit.Projects.Get(projectId);
//            List<Assignment> tasksOnProjects = Unit.Assignments.Get().Where(x => x.Project.Id == projectId && x.Day.Date.Year == year).ToList();
//            ProjectAnnualOverviewModel projectAnnualOverview = new ProjectAnnualOverviewModel { Project = project.Master() };

//            projectAnnualOverview.Months = Providers.SetMonths();

//            foreach (Assignment task in tasksOnProjects)
//            {
//                projectAnnualOverview.Months[task.Day.Date.Month] += task.Hours;
//                projectAnnualOverview.Total += task.Hours;
//            }

//            return projectAnnualOverview;
//        }
//        public TotalAnnualOverviewModel GetTotalAnnualOverview(int year)
//        {
//            TotalAnnualOverviewModel totalAnnualOverview = new TotalAnnualOverviewModel();
//            List<ProjectAnnualOverviewModel> annualList = new List<ProjectAnnualOverviewModel>();

//            List<Project> projectsInYear = Unit.Projects.Get().ToList();
//            totalAnnualOverview.Months = Providers.SetMonths();

//            foreach (Project projectInYear in projectsInYear)
//            {
//                List<Assignment> tasksInYear = Unit.Assignments.Get().Where(x => x.Project.Id == projectInYear.Id && x.Day.Date.Year == year).ToList();
//                ProjectAnnualOverviewModel projectOverview = GetProjectAnnualOverview(projectInYear.Id, year);
//                annualList.Add(projectOverview);
//                totalAnnualOverview.Projects = annualList.ToList();

//                foreach (Assignment taskInYear in tasksInYear)
//                {
//                    totalAnnualOverview.Months[taskInYear.Day.Date.Month] += taskInYear.Hours;
//                    totalAnnualOverview.SumTotal += taskInYear.Hours;
//                }
//            }

//            return totalAnnualOverview;
//        }
//    }
//}
