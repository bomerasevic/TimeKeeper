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

namespace TimeKeeper.BLL.Services
{
    public class MonthlyReport
    {
        protected UnitOfWork _unit;
        public MonthlyReport(UnitOfWork unit)
        {
            _unit = unit;
        }
        public EmployeeTimeModel GetEmployeeReport(int empId, int year, int month)
        {
            Employee emp = _unit.Employees.Get(empId);
            EmployeeTimeModel result = new EmployeeTimeModel
            {
                Employee = emp.Master()
            };
            List<Day> list = emp.Days.Where(c => c.Date.Month == month && c.Date.Year == year).ToList();
            var query = list.GroupBy(c => c.DayType.ToString()).Select(d => new { type = d.Key, hours = d.Sum(h => h.TotalHours) });
            DayType future = new DayType { Id = 10, Name = "Future" }; // svaki dan naredni od danasnjeg
            DayType weekend = new DayType { Id = 11, Name = "Weekend" };
            DayType empty = new DayType { Id = 12, Name = "Empty" };
            DayType na = new DayType { Id = 13, Name = "N/A" };
            foreach (var d in query) result.HourTypes[d.type] = d.hours;
            result.TotalHours = list.Sum(h => h.TotalHours);
            result.PTO = list.Where(d => d.DayType.Name != "workday").Sum(h => h.TotalHours);
            result.Overtime = list.Where(d => d.DayType.Name == "weekend").Sum(h => h.TotalHours)
                            + list.Where(d => d.DayType.Name != "weekend" && d.TotalHours > 8).Sum(h => (h.TotalHours - 8));
            return result;
        }

        public TeamTimeTrackingModel GetTeamReport(int teamId, int year, int month)  // time tracking
        {
            Team team = _unit.Teams.Get(teamId);
            TeamTimeTrackingModel result = new TeamTimeTrackingModel
            {
                Team = team.Master()
            };
            List<int> members = team.TeamMembers.Select(m => m.Employee.Id).ToList();
            foreach (int empId in members)
            {
                EmployeeTimeModel e = GetEmployeeReport(empId, year, month);
                if (e.TotalHours != 0) result.Employees.Add(e);
            }
            return result;
        }

        public ProjectMonthlyModel GetMonthly(int year, int month)
        {
            ProjectMonthlyModel pmm = new ProjectMonthlyModel();

            var source = _unit.Assignments.Get(d => d.Day.Date.Year == year && d.Day.Date.Month == month).ToList();
            var tasks = source.OrderBy(x => x.Day.Employee.Id)
                              .ThenBy(x => x.Project.Id)
                              .ToList();

            pmm.Projects = tasks.GroupBy(p => new { p.Project.Id, p.Project.Name })
                                .Select(p => new MasterModel { Id = p.Key.Id, Name = p.Key.Name })
                                .ToList();
            List<int> pList = pmm.Projects.Select(p => p.Id).ToList();

            var details = tasks.GroupBy(x => new
            {
                empId = x.Day.Employee.Id,
                empName = x.Day.Employee.FullName,
                projId = x.Project.Id,
                projName = x.Project.Name
            })
                                            .Select(y => new
                                            {
                                                employee = new MasterModel { Id = y.Key.empId, Name = y.Key.empName },
                                                project = new MasterModel { Id = y.Key.projId, Name = y.Key.projName },
                                                hours = y.Sum(h => h.Hours)
                                            }).ToList();

            EmployeeProjectModel epm = new EmployeeProjectModel(pList) { Employee = new MasterModel { Id = 0 } };
            foreach (var item in details)
            {
                if (epm.Employee.Id != item.employee.Id)
                {
                    if (epm.Employee.Id != 0) pmm.Employees.Add(epm);
                    epm = new EmployeeProjectModel(pList) { Employee = item.employee };
                }
                epm.Hours[item.project.Id] += item.hours;
                epm.TotalHours += item.hours;
            }
            if (epm.Employee.Id != 0) pmm.Employees.Add(epm);

            return pmm;
        }
        public ProjectMonthlyModel GetStored(int year, int month)
        {
            ProjectMonthlyModel result = new ProjectMonthlyModel();
            var cmd = _unit.Context.Database.GetDbConnection().CreateCommand();  // pokrecemo query definisan u bazi
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = $"select * from MonthlyReport({year},{month})";
            if (cmd.Connection.State == ConnectionState.Closed) cmd.Connection.Open();
            DbDataReader sql = cmd.ExecuteReader();
            List<MonthlyRawData> rawData = new List<MonthlyRawData>();
            if(sql.HasRows)
            {
                while(sql.Read())
                {
                    rawData.Add(new MonthlyRawData
                    {
                        EmpId = sql.GetInt32(0),
                        EmpName = sql.GetString(1),
                        ProjId = sql.GetInt32(2),
                        ProjName = sql.GetString(3),
                        Hours = sql.GetDecimal(4)
                    });
                }
                result.Projects = rawData.GroupBy(x => new { x.ProjId, x.ProjName })
                                         .Select(x => new MasterModel { Id = x.Key.ProjId, Name = x.Key.ProjName}).ToList();
                List<int> projList = result.Projects.Select(x => x.Id).ToList();
                EmployeeProjectModel epm = new EmployeeProjectModel(projList) {Employee = new MasterModel { Id = 0 } };
                foreach (MonthlyRawData item in rawData)
                {
                    if(item.EmpId != epm.Employee.Id)
                    {
                        if (epm.Employee.Id != 0) result.Employees.Add(epm);
                        epm = new EmployeeProjectModel(projList)
                        {
                            Employee = new MasterModel { Id = item.EmpId, Name = item.EmpName }
                        };
                    }
                    epm.Hours[item.ProjId] = item.Hours;
                    epm.TotalHours += item.Hours;
                }
                if (epm.Employee.Id != 0) result.Employees.Add(epm);
            }
            return result;
        }
    }
}
