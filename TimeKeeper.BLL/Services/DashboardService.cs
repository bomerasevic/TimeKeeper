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
        public DashboardService(UnitOfWork unit)
        {
            Unit = unit;
            Providers = new Providers(Unit);
            StoredProcedureService = new StoredProcedureService(Unit);
        }
        
        public CompanyDashboardModel GetCompanyDashboard(int year, int month)
        {
            CompanyDashboardModel companyDashboard = new CompanyDashboardModel();
            decimal baseHours = Providers.GetMonthlyWorkingDays(year, month) * 8;
            List<CompanyDashboardRawModel> rawData = StoredProcedureService.GetStoredProcedure<CompanyDashboardRawModel>("CompanyWorkingHoursData", new int[] { year, month });
            List<CompanyEmployeeHoursModel> employeeHours = StoredProcedureService.GetStoredProcedure<CompanyEmployeeHoursModel>("EmployeeHoursByDayType", new int[] { year, month });
            List<CompanyOvertimeModel> overtime = StoredProcedureService.GetStoredProcedure<CompanyOvertimeModel>("CompanyOvertimeHours", new int[] { year, month });

            List<MasterModel> activeTeams = new List<MasterModel>();
            activeTeams.AddRange(rawData.GroupBy(x => new
            {
                Id = x.TeamId,
                Name = x.TeamName
            }).ToList().Select(x => new MasterModel
            {
                Id = x.Key.Id,
                Name = x.Key.Name
            }).ToList());

            companyDashboard.EmployeesCount = rawData.GroupBy(x => x.EmployeeId).Count();
            companyDashboard.ProjectsCount = rawData.GroupBy(x => x.ProjectId).Count();
            companyDashboard.TotalHours = companyDashboard.EmployeesCount * baseHours;
            companyDashboard.TotalWorkingHours = rawData.Sum(x => x.WorkingHours);
            companyDashboard.Projects = GetCompanyProjectModels(rawData);
            companyDashboard.Roles = GetRoleUtilization(rawData, baseHours);
            companyDashboard.Teams = GetCompanyTeamModels(rawData, employeeHours, activeTeams, overtime);
            GetCompanyMissingEntries(employeeHours, companyDashboard.Teams, baseHours, overtime);

            return companyDashboard;
        }
        private decimal EmployeeRatioInTeam(List<CompanyEmployeeHoursModel> workDays, int teamId, int employeeId)
        {
            decimal empTeamWorkingHours = workDays.Where(x => x.EmployeeId == employeeId && x.TeamId == teamId).Sum(x => x.DayTypeHours);
            decimal empWorkingHours = workDays.Where(x => x.EmployeeId == employeeId).Sum(x => x.DayTypeHours);
            return empTeamWorkingHours / empWorkingHours;
        }
        private void GetCompanyMissingEntries(List<CompanyEmployeeHoursModel> employeeHours, List<CompanyTeamModel> teams, decimal baseHours, List<CompanyOvertimeModel> overtime)
        {
            List<EmployeeMissingEntries> missingEntriesEmployee = employeeHours.GroupBy(x => new { x.EmployeeId, x.EmployeeName })
                .Select(x => new EmployeeMissingEntries
                {
                    Employee = new MasterModel { Id = x.Key.EmployeeId, Name = x.Key.EmployeeName },
                    MissingEntries = baseHours - x.Sum(y => y.DayTypeHours) + overtime.Where(y => y.EmployeeId == x.Key.EmployeeId).Sum(y => y.OvertimeHours)
                }).Where(x => x.MissingEntries > 0).ToList();

            foreach (CompanyTeamModel team in teams)
            {
                foreach (EmployeeMissingEntries employee in missingEntriesEmployee)
                {
                    if (employeeHours.Any(x => x.EmployeeId == employee.Employee.Id && x.TeamId == team.Team.Id))
                    {
                        team.MissingEntries += employee.MissingEntries;
                    }
                }
            }
        }
        private List<CompanyTeamModel> GetCompanyTeamModels(List<CompanyDashboardRawModel> rawData, List<CompanyEmployeeHoursModel> employeeHours, List<MasterModel> activeTeams, List<CompanyOvertimeModel> overtime)
        {
            List<CompanyTeamModel> teams = new List<CompanyTeamModel>();
            teams.AddRange(activeTeams.Select(x => new CompanyTeamModel
            {
                Team = x,
                MissingEntries = 0,                
                PaidTimeOff = 0,
                Overtime = 0
            }).OrderBy(x => x.Team.Name).ToList());

            List<CompanyOvertimeModel> overtimeNotNull = overtime.Where(x => x.OvertimeHours > 0).ToList();
            List<CompanyEmployeeHoursModel> paidTimeOff = employeeHours.Where(x => x.DayTypeName != "Workday").ToList();
            List<CompanyEmployeeHoursModel> workDays = employeeHours.Where(x => x.DayTypeName == "Workday").ToList();

            foreach (CompanyEmployeeHoursModel row in workDays)
            {
                if (row.TeamId != 0)
                {
                    if (overtimeNotNull.FirstOrDefault(x => x.EmployeeId == row.EmployeeId) != null)
                    {
                        GetCompanyOvertime(teams, workDays, overtimeNotNull, row.TeamId, row.EmployeeId);
                    }

                    if (paidTimeOff.FirstOrDefault(x => x.EmployeeId == row.EmployeeId) != null)
                    {
                        GetCompanyPaidTimeOff(teams, workDays, paidTimeOff, row.TeamId, row.EmployeeId);
                    }
                }
            }

            return teams;
        }
        private void GetCompanyOvertime(List<CompanyTeamModel> teams, List<CompanyEmployeeHoursModel> workDays, List<CompanyOvertimeModel> overtime, int teamId, int employeeId)
        {
            decimal empOvertime = overtime.Where(x => x.EmployeeId == employeeId).Sum(x => x.OvertimeHours);
            teams.FirstOrDefault(x => x.Team.Id == teamId).Overtime += empOvertime * EmployeeRatioInTeam(workDays, teamId, employeeId);
        }
        private void GetCompanyPaidTimeOff(List<CompanyTeamModel> teams, List<CompanyEmployeeHoursModel> workDays, List<CompanyEmployeeHoursModel> paidTimeOff, int teamId, int employeeId)
        {
            decimal empPaidTimeOff = paidTimeOff.Where(x => x.EmployeeId == employeeId).Sum(x => x.DayTypeHours);
            decimal empRatio = EmployeeRatioInTeam(workDays, teamId, employeeId);
            teams.FirstOrDefault(x => x.Team.Id == teamId).PaidTimeOff += empPaidTimeOff * empRatio;
        }
        private List<CompanyRolesDashboardModel> GetRoleUtilization(List<CompanyDashboardRawModel> rawData, decimal baseHours)
        {
            List<CompanyRolesDashboardModel> roles = new List<CompanyRolesDashboardModel>();
            //Employee and role are grouped, and the roles utilization model is created
            List<CompanyRolesRawModel> rolesRaw = CreateRolesRaw(rawData);
            CompanyRolesDashboardModel role = new CompanyRolesDashboardModel { Role = new MasterModel { Id = 0, Name = "" } };
            foreach (CompanyRolesRawModel row in rolesRaw)
            {
                if (row.RoleName != role.Role.Name)
                {
                    if (role.Role.Name != "") roles.Add(role);
                    role = new CompanyRolesDashboardModel { Role = new MasterModel { Id = row.RoleId, Name = row.RoleName } };
                    role.WorkingHours = rolesRaw.Where(x => x.RoleName == role.Role.Name).Sum(x => x.WorkingHours);
                }
                /*Calculates the ratio of this employees total working hours 
                 * as this role in employees overall total working hours, 
                 * and uses the ratio to extract a number from the monthly base hours*/
                decimal hoursEmployeeRole = rolesRaw.Where(x => x.EmployeeId == row.EmployeeId && x.RoleName == role.Role.Name).Sum(x => x.WorkingHours);
                decimal hoursEmployee = rolesRaw.Where(x => x.EmployeeId == row.EmployeeId).Sum(x => x.WorkingHours);
                role.TotalHours += (hoursEmployeeRole / hoursEmployee) * baseHours;
            }
            if (role.Role.Name != "") roles.Add(role);
            return roles;
        }

        private List<CompanyRolesRawModel> CreateRolesRaw(List<CompanyDashboardRawModel> rawData)
        {
            List<CompanyRolesRawModel> rolesRaw = rawData.GroupBy(x => new { x.EmployeeId, x.RoleId, x.RoleName }).Select(
                x => new CompanyRolesRawModel
                {
                    EmployeeId = x.Key.EmployeeId,
                    RoleId = x.Key.RoleId,
                    RoleName = x.Key.RoleName,
                    WorkingHours = x.Sum(y => y.WorkingHours)
                }).ToList().OrderBy(x => x.RoleName).ToList();

            return rolesRaw;
        }

        private List<CompanyProjectsDashboardModel> GetCompanyProjectModels(List<CompanyDashboardRawModel> rawData)
        {
            List<CompanyProjectsDashboardModel> projects = new List<CompanyProjectsDashboardModel>();
            //Data isn't sorted by projects unless a new List is created
            List<CompanyDashboardRawModel> rawProjects = rawData.OrderBy(x => x.ProjectId).ToList();

            CompanyProjectsDashboardModel project = new CompanyProjectsDashboardModel { Project = new MasterModel { Id = 0 } };
            foreach (CompanyDashboardRawModel row in rawProjects)
            {
                if (row.ProjectId != project.Project.Id)
                {
                    if (project.Project.Id != 0) projects.Add(project);
                    project = new CompanyProjectsDashboardModel
                    {
                        Project = new MasterModel { Id = row.ProjectId, Name = row.ProjectName },
                        Revenue = GetProjectRevenue(rawProjects, row.ProjectId, row.ProjectPricingName)
                    };
                }
            }
            if (project.Project.Id != 0) projects.Add(project);

            return projects;
        }

        private decimal GetProjectRevenue(List<CompanyDashboardRawModel> rawData, int projectId, string pricingType)
        {
            switch (pricingType)
            {
                case "Fixed bid":
                    return rawData.FirstOrDefault(x => x.ProjectId == projectId).ProjectAmount;
                case "Hourly":
                    return rawData.Where(x => x.ProjectId == projectId).Sum(x => x.WorkingHours * x.RoleHourlyPrice);
                case "PerCapita":
                    return rawData.Where(x => x.ProjectId == projectId)
                                  .GroupBy(x => new { x.EmployeeId, x.ProjectId, x.RoleMonthlyPrice })
                                  .ToList().Sum(x => x.Key.RoleMonthlyPrice);
                default:
                    return 0;
            }
        }

        public TeamDashboardModel GetTeamDashboardStored(Team team, int year, int month)
        {
            TeamDashboardModel teamDashboard = new TeamDashboardModel();
            List<TeamRawModel> rawData = StoredProcedureService.GetStoredProcedure<TeamRawModel>("TeamDashboard", new int[] { team.Id, year, month });
            List<TeamRawNonWorkingHoursModel> rawDataPTO = StoredProcedureService.GetStoredProcedure<TeamRawNonWorkingHoursModel>("GetMemberPTOHours", new int[] { team.Id, year, month });
            List<TeamRawNonWorkingHoursModel> rawDataOvertime = StoredProcedureService.GetStoredProcedure<TeamRawNonWorkingHoursModel>("GetMemberOvertimeHours", new int[] { team.Id, year, month });

            teamDashboard.Year = year;
            teamDashboard.Month = month;
            teamDashboard.Team = new MasterModel { Id = team.Id, Name = team.Name };
            teamDashboard.NumberOfEmployees = rawData.GroupBy(x => x.EmployeeId).Count();            
            teamDashboard.TotalWorkingHours = rawData.Sum(x => x.Value);            

            List<TeamRawCountModel> rawDataProjectsCount = StoredProcedureService.GetStoredProcedure<TeamRawCountModel>("CountProjects", new int[] { team.Id, year, month });

            teamDashboard.NumberOfProjects = rawDataProjectsCount.Count;

            decimal baseTotalHours = Providers.GetMonthlyWorkingDays(year, month) * 8;
            teamDashboard.TotalHours = baseTotalHours * teamDashboard.NumberOfEmployees;

            List<TeamRawModel> rawDataMissingEntries = GetMembersMissingEntries(team.Id, year, month, baseTotalHours);

            foreach (TeamRawModel r in rawData)
            {
                teamDashboard.EmployeeTimes.Add(new TeamMemberDashboardModel
                {
                    Employee = new MasterModel { Id = r.EmployeeId, Name = r.EmployeeName},
                    TotalHours = baseTotalHours,
                    Overtime = (rawDataOvertime == null || rawDataOvertime.FirstOrDefault(x => x.MemberId == r.EmployeeId) == null) ? 0 : rawDataOvertime.FirstOrDefault(x => x.MemberId == r.EmployeeId).Value,
                    PaidTimeOff = (rawDataPTO == null || rawDataPTO.FirstOrDefault(x => x.MemberId == r.EmployeeId) == null) ? 0 : rawDataPTO.FirstOrDefault(x => x.MemberId == r.EmployeeId).Value,
                    WorkingHours = r.Value,
                    MissingEntries = (rawDataMissingEntries == null || rawDataMissingEntries.FirstOrDefault(x => x.EmployeeId == r.EmployeeId) == null) ? 0 : rawDataMissingEntries.FirstOrDefault(x => x.EmployeeId == r.EmployeeId).Value,
                });
            }
            return teamDashboard;
        }
        public List<TeamRawModel> GetMembersMissingEntries(int teamId, int year, int month, decimal baseTotalHours)
        {
            List<TeamRawModel> rawData = StoredProcedureService.GetStoredProcedure<TeamRawModel>("DateMonth", new int[] { teamId, year, month });

            foreach (TeamRawModel trm in rawData)
            {
                trm.Value = baseTotalHours - trm.Value * 8;   // trm su odradjeni dani; 
            }
            return rawData;
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
            //employeePersonalReport.HourTypes.Add("totalHours", totalHours + missingEntries);
            employeePersonalReport.TotalHours = Providers.GetMonthlyWorkingDays(year, month) * 8;
            employeePersonalReport.PaidTimeOff = employeePersonalReport.TotalHours - employeePersonalReport.HourTypes["missingEntries"] - employeePersonalReport.HourTypes["workday"];

            return employeePersonalReport;
        }
        public List<EmployeeTimeModel> GetTeamMonthReport(Team team, int year, int month)
        {
            //Team team = Unit.Teams.Get(teamId);
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
            List<EmployeeTimeModel> employeeTimes = GetTeamMonthReport(team, year, month);
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
                    MissingEntries = employeeTime.HourTypes["Missing entries"]
                });
            }
            return teamMembers;
        }



        //public PersonalDashboardModel GetEmployeeDashboard(int employeeId, int year)
        //{
        //    List<DayModel> calendar = Providers.GetEmployeeCalendar(employeeId, year);
        //    decimal totalHours = Providers.GetYearlyWorkingDays(year) * 8;

        //    return CreatePersonalDashboard(employeeId, year, totalHours, calendar);
        //}
        //public PersonalDashboardModel GetEmployeeDashboard(int employeeId, int year, int month)
        //{
        //    List<DayModel> calendar = Providers.GetEmployeeCalendar(employeeId, year, month);
        //    decimal totalHours = Providers.GetMonthlyWorkingDays(year, month) * 8;

        //    return CreatePersonalDashboard(employeeId, year, totalHours, calendar);
        //}

        //private PersonalDashboardModel CreatePersonalDashboard(int employeeId, int year, decimal totalHours, List<DayModel> calendar)
        //{
        //    decimal workingHours = calendar.Where(x => x.DayType.Name == "Workday").Sum(x => x.TotalHours);

        //    return new PersonalDashboardModel
        //    {
        //        Employee = Unit.Employees.Get(employeeId).Master(),
        //        TotalHours = totalHours,
        //        WorkingHours = workingHours,
        //        BradfordFactor = GetBradfordFactor(employeeId, year)
        //    };
        //}

        //public decimal GetBradfordFactor(int employeeId, int year)
        //{
        //    List<DayModel> calendar = Providers.GetEmployeeCalendar(employeeId, year);
        //    //an absence instance are any number of consecutive absence days. 3 consecutive absence days make an instance.
        //    int absenceInstances = 0;
        //    int absenceDays = 0;
        //    calendar = calendar.OrderBy(x => x.Date).ToList();

        //    //Bradford factor calculates only dates until the present day, because the calendar in argument returns the whole period
        //    absenceDays = calendar.Where(x => x.DayType.Name == "sick" && x.Date < DateTime.Now).Count();

        //    for (int i = 0; i < calendar.Count; i++)
        //    {
        //        if (calendar[i].DayType.Name == "sick" && calendar[i].Date < DateTime.Now)
        //        {
        //            if (i == 0) absenceInstances++;

        //            else if (calendar[i - 1].DayType.Name != "sick")
        //            {
        //                absenceInstances++;
        //            }
        //        }
        //    }
        //    return (decimal)Math.Pow(absenceInstances, 2) * absenceDays;
        //}
        public PersonalDashboardModel GetPersonalDashboardStored(int empId, int year, int month)
        {
            PersonalDashboardModel personalDashboard = new PersonalDashboardModel();
            List<PersonalDashboardRawModel> rawData = StoredProcedureService.GetStoredProcedure<PersonalDashboardRawModel>("personalDashboard", new int[] { empId, year, month });
            decimal workingDaysInMonth = Providers.GetMonthlyWorkingDays(year, month) * 8;
            decimal workingDaysInYear = Providers.GetYearlyWorkingDays(year) * 8;

            personalDashboard.PersonalDashboardHours = rawData[0];
            // What if there's overtime?
            personalDashboard.UtilizationMonthly = decimal.Round(((rawData[0].WorkingMonthly / workingDaysInMonth) * 100), 2, MidpointRounding.AwayFromZero);
            personalDashboard.UtilizationYearly = decimal.Round(((rawData[0].WorkingYearly / workingDaysInYear) * 100), 2, MidpointRounding.AwayFromZero);
            personalDashboard.BradfordFactor = GetBradfordFactor(rawData[0], year);

            return personalDashboard;
        }
        public decimal GetBradfordFactor(PersonalDashboardRawModel personalDashboardHours, int year)
        {
            int absenceDays = personalDashboardHours.SickYearly;
            List<RawAbsenceModel> rawAbsenceData = StoredProcedureService.GetStoredProcedure<RawAbsenceModel>("sickByMonths", new int[] { personalDashboardHours.EmployeeId, year });
            if (rawAbsenceData == null)
            {
                rawAbsenceData = new List<RawAbsenceModel>();
                rawAbsenceData.Add(new RawAbsenceModel { AbsenceInstances = 0});
            }
                //throw new ArgumentException("There is an error in database. Please check again your data.");
            return (decimal)Math.Pow((int)rawAbsenceData[0].AbsenceInstances, 2) * absenceDays;
        }
    }
}
