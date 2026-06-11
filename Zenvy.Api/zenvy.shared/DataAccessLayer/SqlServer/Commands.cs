using System;
using System.Data;
using System.Data.Common;
//using System.Data.SqlClient;
using System.Xml;
using DataAccessLayer.Interfaces;
using Microsoft.Data.SqlClient;

namespace DataAccessLayer.SqlServer
{
    public class Commands : ICommands
    {
        private readonly int _commandTimeOut;
        private readonly SqlCommandType _commandTypeToUse;
        private readonly IConnection _currentConnection;
        private readonly SqlTransaction _currentTransaction;

        internal Commands(IConnection currentConnection, IDbTransaction currentTransaction, int commandTimeOut)
        {
            if (currentConnection == null) throw new ArgumentNullException("currentConnection");

            _currentTransaction = currentTransaction as SqlTransaction;
            _currentConnection = currentConnection;
            _commandTimeOut = commandTimeOut;
            _commandTypeToUse = new SqlCommandType(_currentConnection.ConnectionString);
        }

        /// <summary>
        ///     Executes a command that does not return a query
        /// </summary>
        /// <param name="commandText">Name of stored procedure to execute</param>
        /// <param name="parameters">DbParameter collection to use in executing</param>
        public int ExecuteNonQuery(string commandText, params DbParameter[] parameters)
        {
            try
            {
                return Execute(x => x.ExecuteNonQuery(), commandText, parameters);
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error: {ex.Number} - {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                
                return 0;// throw; // Rethrow the exception after logging
            }
            catch (Exception ex)
            {
                // Log other exceptions
                Console.WriteLine($"General Error: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return 0;
            }

          
        }

        public async Task<int> ExecuteNonQueryAsync(string commandText, params DbParameter[] parameters)
        {
            try
            {
                return await ExecuteAsync(async x => await x.ExecuteNonQueryAsync(), commandText, parameters);
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error: {ex.Number} - {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return 0;// throw; // Rethrow the exception after logging
            }
            catch (Exception ex)
            {
                // Log other exceptions
                Console.WriteLine($"General Error: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return 0;
            }


        }

        /// <summary>
        ///     Executes a command that does not return a query
        /// </summary>
        /// <param name="cmd">Output parameter that holds reference to the command object just executed</param>
        /// ///
        /// <param name="commandText">Name of stored procedure to execute</param>
        /// <param name="parameters">DbParameter collection to use in executing</param>
        /// <returns>DbCommand containing the command executed</returns>
        public int ExecuteNonQuery(out DbCommand cmd, string commandText, params DbParameter[] parameters)
        {
            return Execute(x => x.ExecuteNonQuery(), out cmd, commandText, parameters);
        }

        /// <summary>
        ///     Executes a command that returns a single value
        /// </summary>
        /// <param name="commandText">Name of stored procedure to execute</param>
        /// <param name="parameters">DbParameter collection to use in executing</param>
        /// <returns>Object holding result of execution of database</returns>
        public object ExecuteScalar(string commandText, params DbParameter[] parameters)
        {
            return Execute(x => x.ExecuteScalar(), commandText, parameters);
        }

        /// <summary>
        ///     Executes a command that returns a single value
        /// </summary>
        /// <param name="cmd">Output parameter that holds reference to the command object just executed</param>
        /// <param name="commandText">Name of stored procedure to execute</param>
        /// <param name="parameters">DbParameter collection to use in executing</param>
        /// <returns>Object holding result of execution of database</returns>
        public object ExecuteScalar(out DbCommand cmd, string commandText, params DbParameter[] parameters)
        {
            return Execute(x => x.ExecuteScalar(), out cmd, commandText, parameters);
        }
        public async Task<object> ExecuteScalarAsync(string commandText, params DbParameter[] parameters)
        {
            return await ExecuteAsync(async x => await x.ExecuteScalarAsync(), commandText, parameters);
        }

       

        /// <summary>
        ///     Executes a command and returns a data reader
        /// </summary>
        /// <param name="commandText">Name of stored procedure to execute</param>
        /// <param name="parameters">DbParameter collection to use in executing</param>
        /// <returns>SqlDataReader allowing access to results from command</returns>
        public DbDataReader ExecuteReader(string commandText, params DbParameter[] parameters)
        {
            SqlDataReader reader;

            _currentConnection.Open();
            try
            {
                using (
                    var readerCommand = new SqlCommand(commandText,
                        (SqlConnection) _currentConnection.DatabaseConnection))
                {
                    readerCommand.CommandType = _commandTypeToUse.Get(commandText);
                    readerCommand.Transaction = _currentTransaction;

                    if (parameters != null && parameters.Length > 0)
                        readerCommand.Parameters.AddRange(parameters);

                    reader = readerCommand.ExecuteReader(CommandBehavior.CloseConnection);
                }
            }
            catch (Exception ex)
            {
                _currentConnection.Close();
                throw new Exception(ex.Message);
            }
            return reader;
        }

        public async Task<DbDataReader> ExecuteReaderAsync(string commandText, params DbParameter[] parameters)
        {
            SqlDataReader reader;

           await _currentConnection.OpenAsync();
            try
            {
                using var readerCommand = new SqlCommand(commandText,
                        (SqlConnection)_currentConnection.DatabaseConnection);
                readerCommand.CommandType = _commandTypeToUse.Get(commandText);
                readerCommand.Transaction = _currentTransaction;

                if (parameters != null && parameters.Length > 0)
                    readerCommand.Parameters.AddRange(parameters);

                reader = await readerCommand.ExecuteReaderAsync(CommandBehavior.CloseConnection);

            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);

            }
            catch (Exception ex)
            {
                _currentConnection.Close();
                throw new Exception(ex.Message);

            }
            return reader;
        }

        public async Task<DbDataReader> ExecuteReaderAsync(string commandText, DbParameter[] parameters, CancellationToken cancellationToken)
        {
            await _currentConnection.OpenAsync(cancellationToken);
            try
            {
                using var readerCommand = new SqlCommand(commandText, (SqlConnection)_currentConnection.DatabaseConnection)
                {
                    CommandType = _commandTypeToUse.Get(commandText),
                    Transaction = _currentTransaction
                };

                if (parameters != null && parameters.Length > 0)
                    readerCommand.Parameters.AddRange(parameters);

                return await readerCommand.ExecuteReaderAsync(CommandBehavior.CloseConnection, cancellationToken);
            }
            catch (Exception ex)
            {
                _currentConnection.Close();
                throw new Exception(ex.Message, ex);
            }
        }


        /// <summary>
        ///     Executes a command and returns a DataTable
        /// </summary>
        /// <param name="commandText">Name of stored procedure to execute</param>
        /// <param name="parameters">DbParameter collection to use in executing</param>
        /// <returns>DataTable populated with data from executing stored procedure</returns>
        public DataTable ExecuteDataTable(string commandText, params DbParameter[] parameters)
        {
            DbCommand cmd = null;
            DataTable results;
            try
            {
                results = ExecuteDataTable(out cmd, commandText, parameters);
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Parameters.Clear();
                    cmd.Dispose();
                }
            }

            return results;
        }

        /// <summary>
        ///     Executes a command and returns a DataTable
        /// </summary>
        /// <param name="cmd">Output parameter that holds reference to the command object just executed</param>
        /// <param name="commandText">Name of stored procedure to execute</param>
        /// <param name="parameters">SqlParameter collection to use in executing</param>
        /// <returns>DataTable populated with data from executing stored procedure</returns>
        public DataTable ExecuteDataTable(out DbCommand cmd, string commandText, params DbParameter[] parameters)
        {
            var result = new DataTable();
            SqlCommand cmdDataTable;

            try
            {
                _currentConnection.Open();
                cmdDataTable = BuildCommand(commandText, parameters);

                using (var da = new SqlDataAdapter(cmdDataTable))
                {
                    da.Fill(result);
                }
            }
            finally
            {
                _currentConnection.Close();
            }

            cmd = cmdDataTable;
            return result;
        }

        /// <summary>
        ///     Executes a command and returns a DataTable
        /// </summary>
        /// <param name="commandText">Name of stored procedure to execute</param>
        /// <param name="parameters">SqlParameter collection to use in executing</param>
        /// <returns>DataTable populated with data from executing stored procedure</returns>
        public DataSet ExecuteDataSet(string commandText, params DbParameter[] parameters)
        {
            DbCommand cmd; 
            var results = ExecuteDataSet(out cmd, commandText, parameters);
            cmd.Parameters.Clear();
            cmd.Dispose();

            return results;
        }

        /// <summary>
        ///     Executes a command and returns a DataTable
        /// </summary>
        /// <param name="cmd">Output parameter that holds reference to the command object just executed</param>
        /// <param name="commandText">Name of stored procedure to execute</param>
        /// <param name="parameters">SqlParameter collection to use in executing</param>
        /// <returns>DataTable populated with data from executing stored procedure</returns>
        public DataSet ExecuteDataSet(out DbCommand cmd, string commandText, params DbParameter[] parameters)
        {
            SqlCommand cmdDataSet;

            var result = new DataSet();

            try
            {
                _currentConnection.Open();
                cmdDataSet = BuildCommand(commandText, parameters);

                using (var adapter = new SqlDataAdapter(cmdDataSet))
                {
                    adapter.Fill(result);
                }
            }
            finally
            {
                _currentConnection.Close();
            }

            cmd = cmdDataSet;
            return result;
        }

        /// <summary>
        ///     Executes a command and returns an XML reader.
        /// </summary>
        /// <param name="commandText">Name of stored procedure to execute</param>
        /// <param name="parameters">SqlParameter collection to use in executing</param>
        /// <returns>An instance of XmlReader pointing to the stream of xml returned</returns>
        public XmlReader ExecuteXmlReader(string commandText, params DbParameter[] parameters)
        {
            DbCommand cmd;
            var result = ExecuteXmlReader(out cmd, commandText, parameters);
            cmd.Parameters.Clear();
            cmd.Dispose();

            return result;
        }

        /// <summary>
        ///     Executes a command and returns an XML reader.
        /// </summary>
        /// <param name="cmd">Output parameter that holds reference to the command object just executed</param>
        /// <param name="commandText">Name of stored procedure to execute</param>
        /// <param name="parameters">DbParameter collection to use in executing</param>
        /// <returns>An instance of XmlReader pointing to the stream of xml returned</returns>
        public XmlReader ExecuteXmlReader(out DbCommand cmd, string commandText, params DbParameter[] parameters)
        {
            _currentConnection.Open();
            var cmdXmlReader = BuildCommand(commandText, parameters);

            var outputReader = cmdXmlReader.ExecuteSafeXmlReader();
            cmd = cmdXmlReader;
            return outputReader;
        }



        /// <summary>
        ///     Builds a SqlCommand to execute
        /// </summary>
        /// <param name="commandText">Name of stored procedure to execute</param>
        /// <param name="parameters">Param array of DbParameter objects to use with command</param>
        /// <returns>SqlCommand object ready for use</returns>
        private SqlCommand BuildCommand(string commandText, params DbParameter[] parameters)
        {
            var newCommand = new SqlCommand(commandText, (SqlConnection) _currentConnection.DatabaseConnection)
            {
                Transaction = _currentTransaction,
                CommandType = _commandTypeToUse.Get(commandText)
            };

            if (_commandTimeOut > 0)
            {
                newCommand.CommandTimeout = _commandTimeOut;
            }

            if (parameters != null)
                newCommand.Parameters.AddRange(parameters);

            return newCommand;
        }

        private T Execute<T>(Func<SqlCommand, T> commandToExecute, string commandText, params DbParameter[] parameters)
        {
            DbCommand cmd = null;
            T result;
            try
            {
                result = Execute(commandToExecute, out cmd, commandText, parameters);
            }
            //catch (SqlException ex)
            //{
            //    Console.WriteLine($"SQL Error: {ex.Number} - {ex.Message}");
            //    Console.WriteLine(ex.StackTrace);
            //    //throw; // Rethrow the exception after logging
            //   // return null;
            //}
            finally
            {
                if (cmd != null)
                {
                    cmd.Parameters.Clear();
                    cmd.Dispose();
                }
            }

            return result;
        }

        private T Execute<T>(Func<SqlCommand, T> commandToExecute, out DbCommand cmd, string commandText,
            params DbParameter[] parameters)
        {
            object result;

            try
            {
                _currentConnection.Open();
                var toExecute = BuildCommand(commandText, parameters);
                result = commandToExecute(toExecute);

                cmd = toExecute;
            }
            //catch (SqlException ex)
            //{
            //    Console.WriteLine($"SQL Error: {ex.Number} - {ex.Message}");
            //    Console.WriteLine(ex.StackTrace);
            //    throw; // Rethrow the exception after logging
            //}
            finally
            {
                _currentConnection.Close();
            }

            return (T) result;
        }
        //public async Task<int> ExecuteNonQueryAsync(string commandText, params DbParameter[] parameters)
        //{
        //    return await ExecuteAsync(async x => await x.ExecuteNonQueryAsync(), commandText, parameters);
        //}

   

        private async Task<T> ExecuteAsync<T>(Func<SqlCommand, Task<T>> commandToExecute, string commandText, params DbParameter[] parameters)
        {
            await _currentConnection.OpenAsync();
            try
            {
                using var cmd = BuildCommand(commandText, parameters);
                return await commandToExecute(cmd);
            }
            finally
            {
                _currentConnection.Close();
            }
        }

      
    }
}