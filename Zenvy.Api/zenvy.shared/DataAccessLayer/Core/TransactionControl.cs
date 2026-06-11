using System.Data;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer.Core
{
    public class TransactionControl : ITransactionControl
    {
        private readonly IConnection _connection;

        public IDbTransaction CurrentTransaction { get; private set; }

        internal TransactionControl(IConnection connection)
        {
            _connection = connection;
        }

        /// <summary>
        /// Starts a transaction on the current connection
        /// </summary>
        public void BeginTransaction()
        {
            _connection.Open();
            CurrentTransaction = _connection.DatabaseConnection.BeginTransaction();
            _connection.InTransaction = true;
        }

        /// <summary>
        /// Commits the current transaction
        /// </summary>
        public void CommitTransaction()
        {
            if (CurrentTransaction != null)
            {
                CurrentTransaction.Commit();
                CurrentTransaction = null;
                _connection.InTransaction = false;
            }
        }

        /// <summary>
        /// Rolls back the current transaction
        /// </summary>
        public void RollbackTransaction()
        {
            if (CurrentTransaction != null)
            {
                CurrentTransaction.Rollback();
                CurrentTransaction = null;
                _connection.InTransaction = false;
            }
        }
    }
}