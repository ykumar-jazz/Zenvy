//using System.Data;
using DataAccessLayer.Interfaces;
//using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.Common;

namespace DataAccessLayer.SqlServer
{
    internal class Connection : IConnection
    {
        public string ConnectionString { get; private set; }
        public IDbConnection DatabaseConnection { get; private set; }
        public bool InTransaction { get; set; }

        internal Connection(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public void Close()
        {
            if (!InTransaction)
            {
                DatabaseConnection.Close();
                DatabaseConnection.Dispose();
            }
        }

        public void Open()
        {
            if (DatabaseConnection == null || DatabaseConnection.State != ConnectionState.Open)
            {
                var sqlConnection = new SqlConnection(ConnectionString)
                {
                    RetryLogicProvider = SqlConfigurableRetryFactory.CreateExponentialRetryProvider(
                        new SqlRetryLogicOption
                        {
                            NumberOfTries = 3,
                            DeltaTime = TimeSpan.FromSeconds(2),
                            MaxTimeInterval = TimeSpan.FromSeconds(10)
                        })
                };

                DatabaseConnection = sqlConnection;
                DatabaseConnection.Open();
            }
        }
        public async Task OpenAsync(CancellationToken cancellationToken = default)
        {
            if (DatabaseConnection == null || DatabaseConnection.State != ConnectionState.Open)
            {
                var sqlConnection = new SqlConnection(ConnectionString)
                {
                    RetryLogicProvider = SqlConfigurableRetryFactory.CreateExponentialRetryProvider(
                        new SqlRetryLogicOption
                        {
                            NumberOfTries = 3, // Retry 3 times
                            DeltaTime = TimeSpan.FromSeconds(2), // 2 seconds delay between retries
                            MaxTimeInterval = TimeSpan.FromSeconds(10) // Max retry duration
                        })
                };

                DatabaseConnection = sqlConnection;

                try
                {
                    await sqlConnection.OpenAsync(cancellationToken);
                }
                catch (SqlException ex)
                {
                    // Log and rethrow the exception
                    Console.WriteLine($"SQL Error: {ex.Number} - {ex.Message}");
                    throw;
                }
                catch (OperationCanceledException)
                {
                    Console.WriteLine("Connection opening was canceled.");
                    throw;
                }
            }
        }

    }
}
