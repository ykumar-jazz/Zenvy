using System;
using System.Data;
using System.Data.Common;
using System.Xml;
using DataAccessLayer.Interfaces;
using DataAccessLayer.SqlServer;

namespace DataAccessLayer.Core
{
    public class DataAccess : IDataAccess
    {
        private readonly IConnection _connection;
        private readonly ITransactionControl _transactionControl;
        private IParameterCreation _parameterFactory;

        public void SetCommandTimeOut(int commandTimeOut)
        {
            this.CommandTimeOut = commandTimeOut;
        }

        public DataAccess(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString)) throw new ArgumentNullException("connectionString");

            _connection = new Connection(connectionString);
            _transactionControl = new TransactionControl(_connection);
            CommandTimeOut = 120;
        }

        public int CommandTimeOut { get; set; }

        public IParameterCreation ParameterFactory
        {
            get { return _parameterFactory ?? (_parameterFactory = new SqlParameterFactory()); }
            set { _parameterFactory = value; }
        }

        public ITransactionControl Transactions
        {
            get { return _transactionControl; }
        }

        public int ExecuteNonQuery(out DbCommand cmd, string commandText, params DbParameter[] parameters)
        {
            return CreateCommand().ExecuteNonQuery(out cmd, commandText, parameters);
        }

        public int ExecuteNonQuery(string commandText, params DbParameter[] parameters)
        {
            return RunCommand(c => c.ExecuteNonQuery(commandText, parameters));
        }

        public async Task<int> ExecuteNonQueryAsync(string commandText, params DbParameter[] parameters)
        {
            return await RunCommand(c => c.ExecuteNonQueryAsync(commandText, parameters));
        }

        public object ExecuteScalar(string commandText, params DbParameter[] parameters)
        {
            return RunCommand(c => c.ExecuteScalar(commandText, parameters));
        }

        public object ExecuteScalar(out DbCommand cmd, string commandText, params DbParameter[] parameters)
        {
            return CreateCommand().ExecuteScalar(out cmd, commandText, parameters);
        }

        public DbDataReader ExecuteReader(string commandText, params DbParameter[] parameters)
        {
            return CreateCommand().ExecuteReader(commandText, parameters);
        }

        public DataTable ExecuteDataTable(string commandText, params DbParameter[] parameters)
        {
            return RunCommand(c => c.ExecuteDataTable(commandText, parameters));
        }

        public async Task<DbDataReader> ExecuteReaderAysnc(string commandText, params DbParameter[] parameters)
        {
            return await CreateCommand().ExecuteReaderAsync(commandText, parameters);
        }

        public DataTable ExecuteDataTable(out DbCommand cmd, string commandText, params DbParameter[] parameters)
        {
            return CreateCommand().ExecuteDataTable(out cmd, commandText, parameters);
        }

        public DataSet ExecuteDataSet(string commandText, params DbParameter[] parameters)
        {
            return RunCommand(c => c.ExecuteDataSet(commandText, parameters));
        }

        public DataSet ExecuteDataSet(out DbCommand cmd, string commandText, params DbParameter[] parameters)
        {
            return CreateCommand().ExecuteDataSet(out cmd, commandText, parameters);
        }

        public XmlReader ExecuteXmlReader(string commandText, params DbParameter[] parameters)
        {
            return RunCommand(c => c.ExecuteXmlReader(commandText, parameters));
        }

        public XmlReader ExecuteXmlReader(out DbCommand cmd, string commandText, params DbParameter[] parameters)
        {
            return CreateCommand().ExecuteXmlReader(out cmd, commandText, parameters);
        }

        private Commands CreateCommand()
        {
            return new Commands(_connection, _transactionControl.CurrentTransaction, CommandTimeOut);
        }

        private T RunCommand<T>(Func<Commands, T> toRun)
        {
            return toRun(CreateCommand());
        }
    }
}
