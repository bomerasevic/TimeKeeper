using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using TimeKeeper.DAL;
using TimeKeeper.DTO.Models;
using TimeKeeper.DTO.Models.DashboardModels;

namespace TimeKeeper.BLL.Services
{
    public class StoredProcedureService
    {
        protected UnitOfWork Unit;
        public StoredProcedureService(UnitOfWork unit)
        {
            Unit = unit;
        }
        public List<Entity> GetStoredProcedure<Entity>(string procedureName, int[] args)
        {
            var arguments = string.Join(", ", args);
            var cmd = Unit.Context.Database.GetDbConnection().CreateCommand();  // pokrecemo query definisan u bazi
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = $"select * from {procedureName}({arguments})";
            if (cmd.Connection.State == ConnectionState.Closed) cmd.Connection.Open();
            DbDataReader sql = cmd.ExecuteReader();
            if (sql.HasRows)
            {
                if (typeof(Entity) == typeof(AdminRawCountModel))  // arminov adminrawmodel
                {
                    List<AdminRawCountModel> rawData = new List<AdminRawCountModel>();
                    while(sql.Read()) CreateAdminRawCountModel(sql);
                    cmd.Connection.Close();
                    return rawData as List<Entity>;
                }
                if (typeof(Entity) == typeof(AdminRawPTOModel))
                {
                    List<AdminRawPTOModel> rawData = new List<AdminRawPTOModel>();
                    while (sql.Read()) rawData.Add(CreateAdminRawPTOModel(sql));
                    cmd.Connection.Close();
                    return rawData as List<Entity>;
                }
                if (typeof(Entity) == typeof(RawMasterModel))
                {
                    List<RawMasterModel> rawData = new List<RawMasterModel>();
                    while (sql.Read()) rawData.Add(CreateRawMasterModel(sql));
                    cmd.Connection.Close();
                    return rawData as List<Entity>;
                }
                if(typeof(Entity) == typeof(TeamRawNonWorkingHoursModel))
                {
                    List<TeamRawNonWorkingHoursModel> rawData = new List<TeamRawNonWorkingHoursModel>();
                    while (sql.Read()) rawData.Add(CreateTeamRawNonWorkingHoursModel(sql));
                    cmd.Connection.Close();
                    return rawData as List<Entity>;
                }
                if (typeof(Entity) == typeof(TeamRawModel))
                {
                    List<TeamRawModel> rawData = new List<TeamRawModel>();
                    while (sql.Read()) rawData.Add(CreateTeamRawModel(sql));
                    cmd.Connection.Close();
                    return rawData as List<Entity>;
                }
                if (typeof(Entity) == typeof(TeamRawCountModel))
                {
                    List<TeamRawCountModel> rawData = new List<TeamRawCountModel>();
                    while (sql.Read()) rawData.Add(CreateTeamRawCountModel(sql));
                    cmd.Connection.Close();
                    return rawData as List<Entity>;
                }
            }
            return null;
        }
        public AdminRawCountModel CreateAdminRawCountModel(DbDataReader sql)  // koristit ce se isti za admin/team
        {
            return new AdminRawCountModel
            {
                EmployeeId = sql.GetInt32(0),
                ProjectId = sql.GetInt32(2),
                WorkingHours = sql.GetDecimal(3)
            };
        }
        public AdminRawPTOModel CreateAdminRawPTOModel(DbDataReader sql)
        {
            return new AdminRawPTOModel
            {
                TeamId = sql.GetInt32(0),
                TeamName = sql.GetString(1),
                PaidTimeOff = sql.GetDecimal(2)
            };
        }
        public RawMasterModel CreateRawMasterModel(DbDataReader sql)
        {
            return new RawMasterModel
            {
                Id = sql.GetInt32(0),
                Name = sql.GetString(1),
                Value = sql.GetDecimal(2)
            };
        }
        public TeamRawNonWorkingHoursModel CreateTeamRawNonWorkingHoursModel(DbDataReader sql)
        {
            return new TeamRawNonWorkingHoursModel
            {
                MemberId = sql.GetInt32(0),
                Value = sql.GetDecimal(1)
            };
        }
        public TeamRawModel CreateTeamRawModel(DbDataReader sql)
        {
            return new TeamRawModel
            {
                EmployeeId = sql.GetInt32(0),
                EmployeeName = sql.GetString(1),
                //ProjectId = sql.GetInt32(2),
                WorkingHours = sql.GetDecimal(2)
            };
        }
        public TeamRawCountModel CreateTeamRawCountModel(DbDataReader sql)
        {
            return new TeamRawCountModel
            {
                ProjectId = sql.GetInt32(0)
            };
        }
    }
}
