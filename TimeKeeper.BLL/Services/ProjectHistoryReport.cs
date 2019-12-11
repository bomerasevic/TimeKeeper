using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using TimeKeeper.DAL;
using TimeKeeper.DTO.Models;

namespace TimeKeeper.BLL.Services
{
    public class ProjectHistoryReport
    {
        protected UnitOfWork _unit;

        public ProjectHistoryReport(UnitOfWork unit)
        {
            _unit = unit;
        }


        public ProjectHistoryModel GetStoredProjectHistory(int projectId)
        {
            ProjectHistoryModel result = new ProjectHistoryModel();
            var cmd = _unit.Context.Database.GetDbConnection().CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = $"select * from ProjectHistory({projectId})";
            if (cmd.Connection.State == ConnectionState.Closed) cmd.Connection.Open();
            DbDataReader sql = cmd.ExecuteReader();
            List<ProjectHistoryRawData> rawData = new List<ProjectHistoryRawData>();
            if (sql.HasRows)
            {
                while (sql.Read())
                {
                    rawData.Add(new ProjectHistoryRawData
                    {
                        EmployeeId = sql.GetInt32(0),
                        EmployeeName = sql.GetString(1),
                        Hours = sql.GetDecimal(2),
                        Year = sql.GetInt32(3)
                    });
                }
                HashSet<int> set = new HashSet<int>();

                result.Years = rawData.Select(x => x.Year).Distinct().ToList();

                EmployeeProjectHistoryModel projHistory = new EmployeeProjectHistoryModel(result.Years)
                { Employee = new MasterModel { Id = 0 } };

                foreach (ProjectHistoryRawData item in rawData)
                {
                    if (item.EmployeeId != projHistory.Employee.Id)
                    {
                        if (projHistory.Employee.Id != 0) result.Employees.Add(projHistory);
                        projHistory = new EmployeeProjectHistoryModel(result.Years)
                        {
                            Employee = new MasterModel { Id = item.EmployeeId, Name = item.EmployeeName }
                        };
                    }
                    projHistory.TotalYearlyProjectHours[item.Year] = item.Hours;
                    projHistory.TotalHoursPerProject += item.Hours;
                }
                if (projHistory.Employee.Id != 0) result.Employees.Add(projHistory);
            }
            return result;
        }
    }
}
