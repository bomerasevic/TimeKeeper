using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using TimeKeeper.DAL;
using TimeKeeper.DTO.Factory;
using TimeKeeper.DTO.Models;
using TimeKeeper.DTO.Models.DashboardModels;

namespace TimeKeeper.BLL.Services
{
    public class StoredProcedureService
    {
        protected UnitOfWork _unit;
        protected SQLFactory _sqlFactory;
        public StoredProcedureService(UnitOfWork unit)
        {
            _unit = unit;
            _sqlFactory = new SQLFactory();
        }

        private DbCommand CreateSelectProcedure(string procedureName, int[] args)
        {
            var arguments = string.Join(", ", args);
            var cmd = _unit.Context.Database.GetDbConnection().CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = $"select * from {procedureName}({arguments})";
            if (cmd.Connection.State == ConnectionState.Open) cmd.Connection.Close();
            cmd.Connection.Open();
            return cmd;
        }

        public List<Entity> GetStoredProcedure<Entity>(string procedureName, int[] args)
        {
            var cmd = CreateSelectProcedure(procedureName, args);
            DbDataReader sql = cmd.ExecuteReader();
            //if(!sql.HasRows) cmd.Connection.Close();
            if (sql.HasRows) return _sqlFactory.BuildSQL<Entity>(sql);

            return new List<Entity>();
        }
    }    
}

