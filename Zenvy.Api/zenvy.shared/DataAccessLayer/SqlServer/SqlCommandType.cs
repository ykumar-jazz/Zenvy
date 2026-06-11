using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;

namespace DataAccessLayer.SqlServer
{
    internal class SqlCommandType
    {
        private SortedList<string, CommandType> _dbObjects;
        private static ConcurrentDictionary<string, SortedList<string, CommandType>> _cacheData
            = new ConcurrentDictionary<string, SortedList<string, CommandType>>();

        public SqlCommandType(string connectionString)
        {
            _dbObjects = GetCachedObjectForDb(connectionString);

            if (_dbObjects == null)
                PopulateCacheData(connectionString);
        }

        public CommandType Get(string commandText)
        {
            if (commandText.Contains("[") && commandText.LastIndexOf(']') == commandText.Length - 1)
            {
                return TryGetStoredProcedure(commandText.Substring(commandText.LastIndexOf('[')));
            }

            if (commandText.Split(' ', '\t').Length == 1)
            {
                return TryGetStoredProcedure(commandText);
            }

            return CommandType.Text;
        }

        private CommandType TryGetStoredProcedure(string commandText)
        {
            commandText = commandText.Replace("[", "").Replace("]", "").ToLowerInvariant();

            if (_dbObjects.ContainsKey(commandText))
            {
                _dbObjects.TryGetValue(commandText, out var toReturn);
                return toReturn;
            }

            return CommandType.Text;
        }

        private void PopulateCacheData(string connectionString)
        {
            using var connection = new SqlConnection(connectionString);
            connection.Open();

            // Ensure only one thread populates per connection string
            _cacheData.GetOrAdd(connectionString, key =>
            {
                var dbObjects = new SortedList<string, CommandType>();
                RetrieveStoredProcedures(connection, dbObjects);
                return dbObjects;
            });

            _dbObjects = _cacheData[connectionString];
        }

        private void RetrieveStoredProcedures(SqlConnection connection, SortedList<string, CommandType> dbObjects)
        {
            ExtractSchemaDetail(connection, "Procedures", "specific_name", CommandType.StoredProcedure, dbObjects);
        }

        private void ExtractSchemaDetail(SqlConnection connection, string collectionName, string columnName, CommandType type, SortedList<string, CommandType> dbObjects)
        {
            var dt = connection.GetSchema(collectionName);

            foreach (var row in dt.Rows.Cast<DataRow>())
            {
                string key = row[columnName].ToString().ToLowerInvariant();
                if (!dbObjects.ContainsKey(key))
                    dbObjects.Add(key, type);
            }
        }

        private static SortedList<string, CommandType> GetCachedObjectForDb(string connectionString)
        {
            _cacheData.TryGetValue(connectionString, out var currentDbObjects);
            return currentDbObjects;
        }
    }
}


//using System.Collections.Generic;
//using System.Data;
//using Microsoft.Data.SqlClient;
//using System.Linq;

//namespace DataAccessLayer.SqlServer
//{
//    internal class SqlCommandType
//    {
//        private SortedList<string, CommandType> _dbObjects;
//        private static Dictionary<string, SortedList<string, CommandType>> _cacheData;


//        public SqlCommandType(string connectionString)
//        {
//            _dbObjects = GetCachedObjectForDb(connectionString);

//            if (_dbObjects == null)
//                PopulateCacheData(connectionString);
//        }

//        public CommandType Get(string commandText)
//        {
//            if (commandText.Contains("["))
//            {
//                if (commandText.LastIndexOf(']') == commandText.Length - 1)
//                {
//                    return TryGetStoredProcedure(commandText.Substring(commandText.LastIndexOf('[')));
//                }
//            }

//            if (commandText.Split(' ', '\t').Length == 1)
//            {
//                return TryGetStoredProcedure(commandText);
//            }

//            return CommandType.Text;
//        }

//        private CommandType TryGetStoredProcedure(string commandText)
//        {
//            commandText = commandText.Replace("[", "");
//            commandText = commandText.Replace("]", "");
//            commandText = commandText.ToLowerInvariant();

//            if (_dbObjects.ContainsKey(commandText))
//            {
//                CommandType toReturn;

//                _dbObjects.TryGetValue(commandText.ToLowerInvariant(), out toReturn);

//                return toReturn;
//            }

//            return CommandType.Text;

//        }

//        private void PopulateCacheData(string connectionString)
//        {
//            if (_cacheData == null)
//                _cacheData = new Dictionary<string, SortedList<string, CommandType>>();

//            using (var connection = new SqlConnection(connectionString))
//            {
//                connection.Open();
//                var lockObject = new object();
//                lock (lockObject)
//                {
//                    RetrieveStoredProcedures(connectionString, connection);
//                }
//                connection.Close();
//            }
//        }

//        private void RetrieveStoredProcedures(string connectionString, SqlConnection connection)
//        {
//            ExtractSchemaDetail(connectionString, connection, "Procedures", "specific_name", CommandType.StoredProcedure);
//        }

//        private void ExtractSchemaDetail(string connectionString, SqlConnection connection, string collectionName, string columnName, CommandType type)
//        {
//            var dt = connection.GetSchema(collectionName);

//            _dbObjects = new SortedList<string, CommandType>();
//            if (_cacheData != null && !_cacheData.ContainsKey(connectionString))
//                _cacheData.Add(connectionString, _dbObjects);

//            foreach (var row in dt.Rows.Cast<DataRow>().Where(row => !_dbObjects.ContainsKey(row[columnName].ToString().ToLowerInvariant())))
//            {
//                _dbObjects.Add(row[columnName].ToString().ToLowerInvariant(), type);
//            }
//        }

//        private static SortedList<string, CommandType> GetCachedObjectForDb(string connectionString)
//        {
//            SortedList<string, CommandType> currentDbObjects = null;

//            if (_cacheData != null && _cacheData.ContainsKey(connectionString))
//            {
//                _cacheData.TryGetValue(connectionString, out currentDbObjects);
//            }

//            return currentDbObjects;
//        }
//    }
//}