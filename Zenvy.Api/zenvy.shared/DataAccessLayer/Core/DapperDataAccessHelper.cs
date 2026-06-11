using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using DataAccessLayer.Interfaces;
namespace DataAccessLayer.Core
{
    public class DapperDataAccessHelper :  IDapperDataAccessHelper
    {
        private readonly string _connectionString;

        public DapperDataAccessHelper(string connectionString)
        {
            _connectionString = connectionString;
        }

        private async Task<SqlConnection> GetOpenConnectionAsync()
        {
            var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            return connection;
        }

        // Basic Query
        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, CommandType commandType = CommandType.Text)
        {
            using var connection = await GetOpenConnectionAsync();
            return await connection.QueryAsync<T>(sql, param, commandType: commandType);
        }

        // Single Record or Null
        public async Task<T> QuerySingleAsync<T>(string sql, object? param = null, CommandType commandType = CommandType.Text)
        {
            using var connection = await GetOpenConnectionAsync();
            return await connection.QuerySingleOrDefaultAsync<T>(sql, param, commandType: commandType);
        }

        // First Record or Null
        public async Task<T?> QueryFirstAsync<T>(string sql, object? param = null, CommandType commandType = CommandType.Text)
        {
            using var connection = await GetOpenConnectionAsync();
            return await connection.QueryFirstOrDefaultAsync<T>(sql, param, commandType: commandType);
        }

        // Scalar (e.g. count, sum, etc.)
        public async Task<T?> ExecuteScalarAsync<T>(string sql, object? param = null, CommandType commandType = CommandType.Text)
        {
            using var connection = await GetOpenConnectionAsync();
            return await connection.ExecuteScalarAsync<T>(sql, param, commandType: commandType);
        }

        // Execute (non-query)
        public async Task<int> ExecuteAsync(string sql, object param = null, CommandType commandType = CommandType.Text)
        {
            using var connection = await GetOpenConnectionAsync();
            return await connection.ExecuteAsync(sql, param, commandType: commandType);
        }

        // Stored Procedure Wrappers
        public async Task<IEnumerable<T>> QueryStoredProcAsync<T>(string procName, object param = null)
        {
            return await QueryAsync<T>(procName, param, CommandType.StoredProcedure);
        }

        public async Task<T> QueryStoredProcSingleAsync<T>(string procName, object param = null)
        {
            return await QuerySingleAsync<T>(procName, param, CommandType.StoredProcedure);
        }

        public async Task<T> QueryStoredProcFirstAsync<T>(string procName, object param = null)
        {
            return await QueryFirstAsync<T>(procName, param, CommandType.StoredProcedure);
        }

        public async Task<int> ExecuteStoredProcAsync(string procName, object param = null)
        {
            return await ExecuteAsync(procName, param, CommandType.StoredProcedure);
        }

        public async Task<T> ExecuteStoredProcScalarAsync<T>(string procName, object param = null)
        {
            return await ExecuteScalarAsync<T>(procName, param, CommandType.StoredProcedure);
        }

        // QueryMultiple (e.g. return multiple result sets)
        public async Task QueryMultipleAsync(string sql, object param, Func<SqlMapper.GridReader, Task> handleResults, CommandType commandType = CommandType.StoredProcedure)
        {
            using var connection = await GetOpenConnectionAsync();
            using var multi = await connection.QueryMultipleAsync(sql, param, commandType: commandType);
            await handleResults(multi);
        }

        // Transactional Execution
        public async Task ExecuteInTransactionAsync(Func<IDbConnection, IDbTransaction, Task> action)
        {
            using var connection = await GetOpenConnectionAsync();
            using var transaction = connection.BeginTransaction();
            try
            {
                await action(connection, transaction);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public async Task<int> ExecuteStoredProcNonQueryAsync(string procName, object? param = null)
        {
            using var connection = await GetOpenConnectionAsync();
            return await connection.ExecuteAsync(procName, param, commandType: CommandType.StoredProcedure);
        }
    }
}
