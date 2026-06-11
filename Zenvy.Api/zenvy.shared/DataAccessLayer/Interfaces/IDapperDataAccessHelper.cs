using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using System.Xml;

namespace DataAccessLayer.Interfaces
{
    public interface IDapperDataAccessHelper
    {
        Task<IEnumerable<T?>> QueryAsync<T>(string sql, object? param = null, CommandType commandType = CommandType.Text);
        Task<T> QuerySingleAsync<T>(string sql, object? param = null, CommandType commandType = CommandType.Text);
        Task<int> ExecuteAsync(string sql, object? param = null, CommandType commandType = CommandType.Text);
        Task<IEnumerable<T?>> QueryStoredProcAsync<T>(string procName, object? param = null);
        Task<T?> QueryStoredProcSingleAsync<T>(string procName, object? param = null);
        Task<int> ExecuteStoredProcAsync(string procName, object? param = null);
        Task ExecuteInTransactionAsync(Func<IDbConnection, IDbTransaction, Task> action);
        Task<int> ExecuteStoredProcNonQueryAsync(string procName, object? param = null);
    }

}