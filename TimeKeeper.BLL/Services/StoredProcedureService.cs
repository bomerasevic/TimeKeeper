using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using TimeKeeper.DAL;
using TimeKeeper.DTO.Models;

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
                if (typeof(Entity) == typeof(RawCountModel))  // arminov adminrawmodel
                {
                    List<RawCountModel> rawData = new List<RawCountModel>();
                    while(sql.Read())CreateRawCountModel(sql);
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
                //if (typeof(Entity) == typeof(AnnualRawModel))
                //{
                //    List<AnnualRawModel> rawData = new List<AnnualRawModel>();
                //    while (sql.Read()) rawData.Add(CreateAnnualRawModel(sql));
                //    cmd.Connection.Close();
                //    return rawData as List<Entity>;
                //}
                if (typeof(Entity) == typeof(RawMasterModel))
                {
                    List<RawMasterModel> rawData = new List<RawMasterModel>();
                    while (sql.Read()) rawData.Add(CreateRawMasterModel(sql));
                    cmd.Connection.Close();
                    return rawData as List<Entity>;
                }
            }
            return null;
        }
        public RawCountModel CreateRawCountModel(DbDataReader sql)  // koristit ce se isti za admin/team
        {
            return new RawCountModel
            {
                EmployeeId = sql.GetInt32(0),
                ProjectId = sql.GetInt32(1),
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
                Name = sql.GetInt32(1),
                Value = sql.GetDecimal(2)
            };
        }

    }
}
