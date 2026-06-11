using System;
using System.Data;
using System.Data.Common;
using System.Xml;
using DataAccessLayer.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;

namespace DataAccessLayer.Core
{
    public class DapperDataAccess : IDapperDataAccess
    {
        private readonly string _connectionString;

        public DapperDataAccess(string connectionString)
        {
            _connectionString = connectionString;
        }

        private IDbConnection GetConnection() => new SqlConnection(_connectionString);

        public int ExecuteNonQuery(string commandText, params DbParameter[] parameters)
        {
            using var connection = GetConnection();
            return connection.Execute(commandText, ToDynamicParameters(parameters), commandType: CommandType.StoredProcedure);
        }

        public int ExecuteNonQuery(out DbCommand cmd, string commandText, params DbParameter[] parameters)
        {
            cmd = null;
            return ExecuteNonQuery(commandText, parameters);
        }

        public object ExecuteScalar(string commandText, params DbParameter[] parameters)
        {
            using var connection = GetConnection();
            return connection.ExecuteScalar(commandText, ToDynamicParameters(parameters), commandType: CommandType.StoredProcedure);
        }

        public object ExecuteScalar(out DbCommand cmd, string commandText, params DbParameter[] parameters)
        {
            cmd = null;
            return ExecuteScalar(commandText, parameters);
        }

        public DbDataReader ExecuteReader(string commandText, params DbParameter[] parameters)
        {
            var connection = (SqlConnection)GetConnection();
            var command = new SqlCommand(commandText, connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddRange(parameters);
            connection.Open();
            return command.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public async Task<DbDataReader> ExecuteReaderAsync(string commandText, params DbParameter[] parameters)
        {
            var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand(commandText, connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddRange(parameters);
            await connection.OpenAsync();
            return await command.ExecuteReaderAsync(CommandBehavior.CloseConnection);
        }

        public DataTable ExecuteDataTable(string commandText, params DbParameter[] parameters)
        {
            using var connection = GetConnection();
            using var command = new SqlCommand(commandText, (SqlConnection)connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddRange(parameters);
            using var adapter = new SqlDataAdapter(command);
            var table = new DataTable();
            adapter.Fill(table);
            return table;
        }

        public DataTable ExecuteDataTable(out DbCommand cmd, string commandText, params DbParameter[] parameters)
        {
            cmd = null;
            return ExecuteDataTable(commandText, parameters);
        }

        public DataSet ExecuteDataSet(string commandText, params DbParameter[] parameters)
        {
            using var connection = GetConnection();
            using var command = new SqlCommand(commandText, (SqlConnection)connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddRange(parameters);
            using var adapter = new SqlDataAdapter(command);
            var dataSet = new DataSet();
            adapter.Fill(dataSet);
            return dataSet;
        }

        public DataSet ExecuteDataSet(out DbCommand cmd, string commandText, params DbParameter[] parameters)
        {
            cmd = null;
            return ExecuteDataSet(commandText, parameters);
        }

        public XmlReader ExecuteXmlReader(string commandText, params DbParameter[] parameters)
        {
            var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand(commandText, connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddRange(parameters);
            connection.Open();
            return command.ExecuteXmlReader();
        }

        public XmlReader ExecuteXmlReader(out DbCommand cmd, string commandText, params DbParameter[] parameters)
        {
            cmd = null;
            return ExecuteXmlReader(commandText, parameters);
        }

        private DynamicParameters ToDynamicParameters(DbParameter[] parameters)
        {
            var dynamicParams = new DynamicParameters();
            foreach (SqlParameter param in parameters)
            {
                dynamicParams.Add(param.ParameterName, param.Value, param.DbType, param.Direction);
            }
            return dynamicParams;
        }
    }
}
