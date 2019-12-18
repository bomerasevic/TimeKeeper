using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using TimeKeeper.DAL;
using TimeKeeper.Domain;
using TimeKeeper.DTO.Factory;
using TimeKeeper.DTO.Models;
using TimeKeeper.DTO.Models.DashboardModels;
using TimeKeeper.DTO.Models.DomainModels;
using TimeKeeper.DTO.Models.ReportModels;

namespace TimeKeeper.BLL.Services
{
    public class DashboardService
    {
        protected UnitOfWork Unit;
        protected Providers Providers;
        protected StoredProcedureService StoredProcedureService;
        protected TimeTracking TimeTracking;
        public DashboardService(UnitOfWork unit)
        {
            Unit = unit;
            Providers = new Providers(Unit);
            StoredProcedureService = new StoredProcedureService(Unit);
        }
        public AdminDashboardModel GetAdminDashboardStored(int year, int month)
        {
            List<string> roles = Unit.Roles.Get().Select(x => x.Name).ToList();
            AdminDashboardModel adminDashboard = new AdminDashboardModel(roles);

            List<RawCountModel> rawData = StoredProcedureService.GetStoredProcedure<RawCountModel>("AdminDashboard", new int[] { year, month });

            adminDashboard.NumberOfEmployees = rawData.GroupBy(x => x.EmployeeId).Count();
            adminDashboard.NumberOfProjects = rawData.GroupBy(x => x.ProjectId).Count();
            adminDashboard.TotalWorkingHours = rawData.Sum(x => x.WorkingHours);

            List<Team> teams = Unit.Teams.Get().ToList();
            List<Project> projects = teams.SelectMany(x => x.Projects).ToList();
            adminDashboard.Projects.AddRange(projects.Select(x => GetAdminProjectDashboard(x, year, month)).ToList());
            adminDashboard.NumberOfProjects = adminDashboard.Projects.Count();

            foreach (Team team in teams)
            {
                MasterModel teamModel = team.Master();

                //This method also calculates the role utilization
                TeamDashboardModel teamDashboard = GetTeamDashboardForAdmin(team, year, month, adminDashboard.Roles);

                AdminTeamDashboardModel teamDashboardModel = GetAdminTeamDashboard(teamDashboard, teamModel);

                adminDashboard.Teams.Add(teamDashboardModel);
                //adminDashboard.TotalWorkingHours += teamDashboardModel.WorkingHours;
                adminDashboard.TotalHours += teamDashboardModel.TotalHours;
            }

            return adminDashboard;
        }

        public TeamDashboardModel GetTeamDashboardStored(int teamId, int year, int month)
        {
            TeamDashboardModel teamDashboard = new TeamDashboardModel();
            List<RawCountModel> rawData = StoredProcedureService.GetStoredProcedure<RawCountModel>("TeamDashboard", new int[] { teamId, year, month });

            teamDashboard.NumberOfEmployees = rawData.GroupBy(x => x.EmployeeId).Count();
            teamDashboard.NumberOfProjects = rawData.GroupBy(x => x.ProjectId).Count();
            teamDashboard.TotalWorkingHours = rawData.Sum(x => x.WorkingHours);

            return teamDashboard;
        }

        public decimal GetProjectRevenue(Project project, int year, int month)
        {
            switch (project.Pricing.Name)
            {
                case "Hourly":
                    //DATABASE DOESN'T HAVE COHERENT DATA, THIS IS FOR SHOWCASE ONLY - FURTHER IMPLEMENTATION IS NEEDED!!!
                    //return project.Tasks.Where(x => x.Day.IsDateInPeriod(year, month)).Sum(x => x.Hours * 15);
                    return project.Team.TeamMembers.Sum(x => x.Role.MonthlyPrice);
                case "PerCapita":
                    //DATABASE DOESN'T HAVE COHERENT DATA, THIS IS FOR SHOWCASE ONLY - FURTHER IMPLEMENTATION IS NEEDED!!!
                    //Only members who have tasks in this month need to be calculated
                    return project.Team.TeamMembers.Sum(x => x.Role.MonthlyPrice);
                case "Fixed bid":
                    return project.Amount;
                default:
                    return 0;
            }
        }
        private AdminProjectDashboardModel GetAdminProjectDashboard(Project project, int year, int month)
        {
            return new AdminProjectDashboardModel
            {
                Project = new MasterModel { Id = project.Id, Name = project.Name },
                Revenue = GetProjectRevenue(project, year, month)
            };
        }
        private AdminTeamDashboardModel GetAdminTeamDashboard(TeamDashboardModel teamDashboard, MasterModel team)
        {
            return new AdminTeamDashboardModel
            {
                Team = team,
                TotalHours = teamDashboard.TotalHours,
                WorkingHours = teamDashboard.TotalWorkingHours,
                PaidTimeOff = teamDashboard.EmployeeTimes.Sum(x => x.PaidTimeOff),
                MissingEntries = teamDashboard.TotalMissingEntries,
                Overtime = teamDashboard.EmployeeTimes.Sum(x => x.Overtime)
            };
        }
        public TeamDashboardModel GetTeamDashboardForAdmin(Team team, int year, int month, List<AdminRolesDashboardModel> roles)
        {
            //The DashboardService shouldn't really depend on the report service, this should be handled in another way
            TeamDashboardModel teamDashboard = new TeamDashboardModel
            {
                EmployeeTimes = GetTeamMembersDashboard(team, year, month)
            };
            //projects for this month!!!
            teamDashboard.NumberOfEmployees = teamDashboard.EmployeeTimes.Count();
            teamDashboard.NumberOfProjects = team.Projects.Count();//_unit.Teams.Get(teamId).Projects.Count();
            foreach (TeamMemberDashboardModel employeeTime in teamDashboard.EmployeeTimes)
            {
                teamDashboard.TotalHours += employeeTime.TotalHours;
                teamDashboard.TotalWorkingHours += employeeTime.WorkingHours;
                teamDashboard.TotalMissingEntries += employeeTime.MissingEntries;
                //Role utilization is also calculated here
                roles.FirstOrDefault(x => x.RoleName == employeeTime.MemberRole).TotalHours += employeeTime.TotalHours;
                roles.FirstOrDefault(x => x.RoleName == employeeTime.MemberRole).WorkingHours += employeeTime.WorkingHours;
            }
            return teamDashboard;
        }
        //public AdminDashboardModel GetAdminDashboardInfo(int year, int month)
        //{
        //    AdminDashboardModel adminDashboard = new AdminDashboardModel();

        //    adminDashboard.NumberOfEmployees = Providers.GetNumberOfEmployeesForTimePeriod(month, year);

        //    adminDashboard.NumberOfProjects = Providers.GetNumberOfProjectsForTimePeriod(month, year);

        //    decimal monthlyBaseHours = Providers.GetMonthlyWorkingDays(year, month) * 8;
        //    //adminDashboard.BaseTotalHours = monthlyBaseHours * adminDashboard.NumberOfEmployees;
        //    adminDashboard.TotalHours = 0;

        //    List<int> teamIds = Unit.Teams.Get().Select(x => x.Id).ToList();

        //    foreach (int teamId in teamIds)
        //    {
        //        MasterModel team = Unit.Teams.Get(teamId).Master();
        //        AdminTeamDashboardModel teamDashboardModel = GetAdminTeamDashboard(team, year, month);
        //        adminDashboard.Teams.Add(teamDashboardModel);
        //        adminDashboard.TotalHours += teamDashboardModel.WorkingHours;
        //    }

        //    return adminDashboard;
        //}

        public AdminProjectDashboardModel GetAdminProjectDashboard(ProjectModel project)  // ok
        {
            return new AdminProjectDashboardModel
            {
                Project = new MasterModel { Id = project.Id, Name = project.Name },
                Revenue = project.Amount
            };
        }

        public AdminTeamDashboardModel GetAdminTeamDashboard(MasterModel team, int year, int month)
        {
            TeamDashboardModel teamDashboard = GetTeamDashboard(team, year, month);
            return new AdminTeamDashboardModel
            {
                Team = team,
                TotalHours = teamDashboard.TotalHours,
                WorkingHours = teamDashboard.TotalWorkingHours,
                PaidTimeOff = teamDashboard.EmployeeTimes.Sum(x => x.PaidTimeOff),
                Overtime = teamDashboard.EmployeeTimes.Sum(x => x.Overtime)
            };
        }

        public TeamDashboardModel GetTeamDashboard(Team team, int year, int month)
        {
            //The DashboardService shouldn't really depend on the report service, this should be handled in another way
            TeamDashboardModel teamDashboard = new TeamDashboardModel
            {
                EmployeeTimes = GetTeamMembersDashboard(team, year, month)
            };

            //projects for this month!!!
            teamDashboard.NumberOfEmployees = teamDashboard.EmployeeTimes.Count();
            teamDashboard.NumberOfProjects = Unit.Teams.Get(team.Id).Projects.Count();

            foreach (TeamMemberDashboardModel employeeTime in teamDashboard.EmployeeTimes)
            {
                teamDashboard.TotalHours += employeeTime.TotalHours;
                teamDashboard.TotalWorkingHours += employeeTime.WorkingHours;
                //teamDashboard.TotalMissingEntries += employeeTime.MissingEntries;
            }

            return teamDashboard;
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
            employeePersonalReport.Overtime = overtime;
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
        //public List<TeamMemberDashboardModel> GetTeamMembersDashboard(int teamId, int year, int month)
        //{
        //    List<EmployeeTimeModel> employeeTimes = GetTeamMonthReport(teamId, year, month);
        //    List<TeamMemberDashboardModel> teamMembers = new List<TeamMemberDashboardModel>();
        //    foreach (EmployeeTimeModel employeeTime in employeeTimes)
        //    {
        //        teamMembers.Add(new TeamMemberDashboardModel
        //        {
        //            Employee = employeeTime.Employee,
        //            TotalHours = employeeTime.TotalHours,
        //            Overtime = employeeTime.Overtime,
        //            PaidTimeOff = employeeTime.PaidTimeOff,
        //            WorkingHours = employeeTime.HourTypes["workday"],
        //            MissingEntries = employeeTime.HourTypes["missingEntries"]
        //        });
        //    }

        //    return teamMembers;
        //}
        private List<TeamMemberDashboardModel> GetTeamMembersDashboard(Team team, int year, int month)
        {
            List<EmployeeTimeModel> employeeTimes = _timeTracking.GetTeamMonthReport(team, year, month);
            List<TeamMemberDashboardModel> teamMembers = new List<TeamMemberDashboardModel>();
            foreach (EmployeeTimeModel employeeTime in employeeTimes)
            {
                teamMembers.Add(new TeamMemberDashboardModel
                {
                    Employee = employeeTime.Employee,
                    TotalHours = employeeTime.TotalHours,
                    Overtime = employeeTime.Overtime,
                    PaidTimeOff = employeeTime.PaidTimeOff,
                    WorkingHours = employeeTime.HourTypes["Workday"],
                    MissingEntries = employeeTime.HourTypes["Missing entries"],
                    MemberRole = employeeTime.Role
                });
            }
            return teamMembers;
        }

        public PersonalDashboardModel GetEmployeeDashboard(int employeeId, int year)
        {
            List<DayModel> calendar = Providers.GetEmployeeCalendar(employeeId, year);
            decimal totalHours = Providers.GetYearlyWorkingDays(year) * 8;

            return CreatePersonalDashboard(employeeId, year, totalHours, calendar);
        }
        public PersonalDashboardModel GetEmployeeDashboard(int employeeId, int year, int month)
        {
            List<DayModel> calendar = Providers.GetEmployeeCalendar(employeeId, year, month);
            decimal totalHours = Providers.GetMonthlyWorkingDays(year, month) * 8;

            return CreatePersonalDashboard(employeeId, year, totalHours, calendar);
        }

        private PersonalDashboardModel CreatePersonalDashboard(int employeeId, int year, decimal totalHours, List<DayModel> calendar)
        {
            decimal workingHours = calendar.Where(x => x.DayType.Name == "Workday").Sum(x => x.TotalHours);

            return new PersonalDashboardModel
            {
                Employee = Unit.Employees.Get(employeeId).Master(),
                TotalHours = totalHours,
                WorkingHours = workingHours,
                BradfordFactor = GetBradfordFactor(employeeId, year)
            };
        }

        public decimal GetBradfordFactor(int employeeId, int year)
        {
            List<DayModel> calendar = Providers.GetEmployeeCalendar(employeeId, year);
            //an absence instance are any number of consecutive absence days. 3 consecutive absence days make an instance.
            int absenceInstances = 0;
            int absenceDays = 0;
            calendar = calendar.OrderBy(x => x.Date).ToList();

            //Bradford factor calculates only dates until the present day, because the calendar in argument returns the whole period
            absenceDays = calendar.Where(x => x.DayType.Name == "sick" && x.Date < DateTime.Now).Count();

            for (int i = 0; i < calendar.Count; i++)
            {
                if (calendar[i].DayType.Name == "sick" && calendar[i].Date < DateTime.Now)
                {
                    if (i == 0) absenceInstances++;

                    else if (calendar[i - 1].DayType.Name != "sick")
                    {
                        absenceInstances++;
                    }
                }
            }
            return (decimal)Math.Pow(absenceInstances, 2) * absenceDays;
        }

    }
}
