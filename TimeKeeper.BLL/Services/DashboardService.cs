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
//    public class DashboardService
//    {
//        protected UnitOfWork Unit;
//        public DashboardService(UnitOfWork unit)
//        {
//            Unit = unit;
//        }
//        public int GetNumberOfEmployeesForTimePeriod(int month, int year)
//        {
//            return Unit.Employees.Get(x => x.BeginDate < new DateTime(year, month, DateTime.DaysInMonth(year, month)) //if employees begin date is in required month
//                            && (x.EndDate == null || x.EndDate == new DateTime(1, 1, 1) || x.EndDate > new DateTime(year, month, DateTime.DaysInMonth(year, month)))).Count(); // still works in company, or left company after required month          
//        }
//        public int GetNumberOfProjectsForTimePeriod(int month, int year)
//        {
//            return Unit.Projects.Get(x => x.StartDate < new DateTime(year, month, DateTime.DaysInMonth(year, month)) //if project began is in required month
//                            && (x.EndDate == null || x.EndDate == new DateTime(1, 1, 1) || x.EndDate > new DateTime(year, month, DateTime.DaysInMonth(year, month)))).Count(); // project still in progress, or ended after the required month          
//        }
//        public AdminDashboardModel GetAdminDashboardInfo(int year, int month)
//        {
//            //Is the missing entries chart in Admin dashboard referring to missing entries per team or?

//            AdminDashboardModel adminDashboard = new AdminDashboardModel();

//            //no of employees at current month/year
//            adminDashboard.NumberOfEmployees = GetNumberOfEmployeesForTimePeriod(month, year);

//            //projects in a current month/year            
//            //List<ProjectModel> projects = GetNumberOfProjectsForTimePeriod(month, year);
//            //Adds all ProjectDashboardModels to adminDashboard
//            //adminDashboard.Projects.AddRange(projects.Select(x => GetAdminProjectDashboard(x)).ToList());
//            adminDashboard.NumberOfProjects = GetNumberOfProjectsForTimePeriod(month, year);

//            //total hours; what is total hours?
//            //adminDashboardModel.BaseTotalHours = GetMonthlyWorkingDays(year, month) * 8;
//            decimal monthlyBaseHours = Providers.GetMonthlyWorkingDays(year, month) * 8;
//            adminDashboard.BaseTotalHours = monthlyBaseHours * adminDashboard.NumberOfEmployees;
//            adminDashboard.TotalHours = 0;

//            List<int> teamIds = Unit.Teams.Get().Select(x => x.Id).ToList();

//            foreach (int teamId in teamIds)
//            {
//                MasterModel team = Unit.Teams.Get(teamId).Master();
//                AdminTeamDashboardModel teamDashboardModel = GetAdminTeamDashboard(team, year, month);
//                adminDashboard.Teams.Add(teamDashboardModel);
//                adminDashboard.TotalHours += teamDashboardModel.WorkingHours;
//            }
//            //what is considered by utilization? :thinkig-face:
//            return adminDashboard;
//        }

//    public AdminProjectDashboardModel GetAdminProjectDashboard(ProjectModel project)
//    {
//        return new AdminProjectDashboardModel
//        {
//            Project = new MasterModel { Id = project.Id, Name = project.Name },
//            Revenue = project.Amount
//        };
//    }

//    public AdminTeamDashboardModel GetAdminTeamDashboard(MasterModel team, int year, int month)
//    {
//        TeamDashboardModel teamDashboard = GetTeamDashboard(team.Id, year, month);
//        return new AdminTeamDashboardModel
//        {
//            Team = team,
//            TotalHours = teamDashboard.TotalHours,
//            WorkingHours = teamDashboard.TotalWorkingHours,
//            PaidTimeOff = teamDashboard.EmployeeTimes.Sum(x => x.PaidTimeOff),
//            MissingEntries = teamDashboard.TotalMissingEntries,
//            Overtime = teamDashboard.EmployeeTimes.Sum(x => x.Overtime)
//        };
//    }

//    public TeamDashboardModel GetTeamDashboard(int teamId, int year, int month)
//    {
//        //The DashboardService shouldn't really depend on the report service, this should be handled in another way
//        TeamDashboardModel teamDashboard = new TeamDashboardModel
//        {
//            EmployeeTimes = GetTeamMembersDashboard(teamId, year, month)
//        };

//        //projects for this month!!!
//        teamDashboard.EmployeesCount = teamDashboard.EmployeeTimes.Count();
//        teamDashboard.ProjectsCount = _unit.Teams.Get(teamId).Projects.Count();

//        foreach (TeamMemberDashboardModel employeeTime in teamDashboard.EmployeeTimes)
//        {
//            teamDashboard.TotalHours += employeeTime.TotalHours;
//            teamDashboard.TotalWorkingHours += employeeTime.WorkingHours;
//            teamDashboard.TotalMissingEntries += employeeTime.MissingEntries;
//        }

//        return teamDashboard;
//    }

//    public List<TeamMemberDashboardModel> GetTeamMembersDashboard(int teamId, int year, int month)
//    {
//        List<EmployeeTimeModel> employeeTimes = _reportService.GetTeamMonthReport(teamId, year, month);
//        List<TeamMemberDashboardModel> teamMembers = new List<TeamMemberDashboardModel>();
//        foreach (EmployeeTimeModel employeeTime in employeeTimes)
//        {
//            teamMembers.Add(new TeamMemberDashboardModel
//            {
//                Employee = employeeTime.Employee,
//                TotalHours = employeeTime.TotalHours,
//                Overtime = employeeTime.Overtime,
//                PaidTimeOff = employeeTime.PaidTimeOff,
//                WorkingHours = employeeTime.HourTypes["Workday"],
//                MissingEntries = employeeTime.HourTypes["Missing entries"]
//            });
//        }

//        return teamMembers;
//    }


//    public PersonalDashboardModel GetEmployeeDashboard(int employeeId, int year)
//    {
//        List<DayModel> calendar = GetEmployeeCalendar(employeeId, year);
//        decimal totalHours = GetYearlyWorkingDays(year) * 8;

//        return CreatePersonalDashboard(employeeId, year, totalHours, calendar);
//    }
//    public PersonalDashboardModel GetEmployeeDashboard(int employeeId, int year, int month)
//    {
//        List<DayModel> calendar = GetEmployeeCalendar(employeeId, year, month);
//        decimal totalHours = Providers.GetMonthlyWorkingDays(year, month) * 8;

//        return CreatePersonalDashboard(employeeId, year, totalHours, calendar);
//    }

//    private PersonalDashboardModel CreatePersonalDashboard(int employeeId, int year, decimal totalHours, List<DayModel> calendar)
//    {
//        decimal workingHours = calendar.Where(x => x.DayType.Name == "Workday").Sum(x => x.TotalHours);

//        return new PersonalDashboardModel
//        {
//            Employee = Unit.Employees.Get(employeeId).Master(),
//            TotalHours = totalHours,
//            WorkingHours = workingHours,
//            BradfordFactor = GetBradfordFactor(employeeId, year)


//        };
//    }

//    public decimal GetBradfordFactor(int employeeId, int year)
//    {
//        List<DayModel> calendar = GetEmployeeCalendar(employeeId, year);
//        //an absence instance are any number of consecutive absence days. 3 consecutive absence days make an instance.
//        int absenceInstances = 0;
//        int absenceDays = 0;
//        calendar = calendar.OrderBy(x => x.Date).ToList();

//        //Bradford factor calculates only dates until the present day, because the calendar in argument returns the whole period
//        absenceDays = calendar.Where(x => x.DayType.Name == "Sick" && x.Date < DateTime.Now).Count();

//        for (int i = 0; i < calendar.Count; i++)
//        {
//            if (calendar[i].DayType.Name == "Sick" && calendar[i].Date < DateTime.Now)
//            {
//                if (i == 0) absenceInstances++;

//                else if (calendar[i - 1].DayType.Name != "Sick")
//                {
//                    absenceInstances++;
//                }
//            }
//        }
//        return (decimal)Math.Pow(absenceInstances, 2) * absenceDays;
//    }

//}
//}
