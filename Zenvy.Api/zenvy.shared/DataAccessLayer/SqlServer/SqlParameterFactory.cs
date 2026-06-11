using System;
using System.Data;
using System.Data.Common;
using Microsoft.Data.SqlClient;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer.SqlServer
{
    public class SqlParameterFactory : IParameterCreation
    {
        /// <summary>
        ///     Creates a SqlParameter with SqlDbType
        /// </summary>
        /// <param name="paramName">Name of the parameter</param>
        /// <param name="paramType">The SqlDBType for the parameter</param>
        /// <param name="value">The value for the parameter</param>
        /// <returns>a configured SqlParameter object</returns>
        public DbParameter Create(string paramName, SqlDbType paramType, object value)
        {
            var param = new SqlParameter(paramName, paramType) { Value = value ?? DBNull.Value };
            return param;
        }

        /// <summary>
        ///     Creates a SqlParameter
        /// </summary>
        /// <param name="paramName">Name of the parameter</param>
        /// <param name="paramType">The SqlDBType for the parameter</param>
        /// <param name="value">The value for the parameter</param>
        /// <returns>a configured SqlParameter object</returns>
        public DbParameter Create(string paramName, DbType paramType, object value)
        {
            var param = new SqlParameter(paramName, ConvertDbTypeToSqlDbType(paramType)) { Value = value ?? DBNull.Value };

            // If null passed for value then set value to DBNull

            return param;
        }

        /// <summary>
        ///     Creates a SqlParameter
        /// </summary>
        /// <param name="paramName">Name of the parameter</param>
        /// <param name="paramType">The DBType for the parameter</param>
        /// <param name="direction">
        ///     Indicates whether the parameter is input-only, output-only, bidirectional, or a stored
        ///     procedure return value parameter.
        /// </param>
        /// <returns>a configured SqlParameter object</returns>
        public DbParameter Create(string paramName, DbType paramType, ParameterDirection direction)
        {
            var param = Create(paramName, paramType, null);
            param.Direction = direction;

            return param;
        }

        /// <summary>
        ///     Creates a SqlParameter
        /// </summary>
        /// <param name="paramName">Name of the parameter</param>
        /// <param name="paramType">The DBType of the parameter</param>
        /// <param name="value">The value for the parameter</param>
        /// <param name="direction">
        ///     Indicates whether the parameter is input-only, output-only, bidirectional, or a stored
        ///     procedure return value parameter.
        /// </param>
        /// <returns>a configured SqlParameter object</returns>
        public DbParameter Create(string paramName, DbType paramType, object value, ParameterDirection direction)
        {
            var returnVal = Create(paramName, paramType, value);
            returnVal.Direction = direction;
            return returnVal;
        }

        /// <summary>
        ///     Creates a SqlParameter
        /// </summary>
        /// <param name="paramName">Name of parameter</param>
        /// <param name="paramType">The DBType for the parameter</param>
        /// <param name="value">The value of the parameter</param>
        /// <param name="size">The maximum size, in bytes, of the data within the column.</param>
        /// <returns>a configured SqlParameter object</returns>
        public DbParameter Create(string paramName, DbType paramType, object value, int size)
        {
            var returnVal = Create(paramName, paramType, value);
            returnVal.Size = size;
            return returnVal;
        }

        /// <summary>
        ///     Creates a SqlParameter
        /// </summary>
        /// <param name="paramName">Name of parameter</param>
        /// <param name="paramType">The DBType for the parameter</param>
        /// <param name="value">The value for the parameter</param>
        /// <param name="size">The maximum size, in bytes, of the data within the column.</param>
        /// <param name="direction">
        ///     Indicates whether the parameter is input-only, output-only, bidirectional, or a stored
        ///     procedure return value parameter.
        /// </param>
        /// >
        /// <returns>a configured SqlParameter object</returns>
        public DbParameter Create(string paramName, DbType paramType, object value, int size,
            ParameterDirection direction)
        {
            var returnVal = Create(paramName, paramType, value);
            returnVal.Direction = direction;
            returnVal.Size = size;
            return returnVal;
        }

        /// <summary>
        ///     Creates a SqlParameter
        /// </summary>
        /// <param name="paramName">Name of parameter</param>
        /// <param name="paramType">The DBType for the parameter</param>
        /// <param name="value">The value for the parameter</param>
        /// <param name="size">The maximum size, in bytes, of the data within the column.</param>
        /// <param name="precision">the maximum number of digits used to represent the Value property.</param>
        /// <returns>a configured SqlParameter object</returns>
        public DbParameter Create(string paramName, DbType paramType, object value, int size, byte precision)
        {
            var returnVal = Create(paramName, paramType, value);
            returnVal.Size = size;
            ((SqlParameter)returnVal).Precision = precision;
            return returnVal;
        }

        /// <summary>
        ///     Creates a SqlParameter
        /// </summary>
        /// <param name="paramName">Name of parameter</param>
        /// <param name="paramType">The DBType for the parameter</param>
        /// <param name="value">The value for the parameter</param>
        /// <param name="size">The maximum size, in bytes, of the data within the column.</param>
        /// <param name="precision">the maximum number of digits used to represent the Value property.</param>
        /// <param name="direction">
        ///     Indicates whether the parameter is input-only, output-only, bidirectional, or a stored
        ///     procedure return value parameter.
        /// </param>
        /// <returns>a configured SqlParameter object</returns>
        public DbParameter Create(string paramName, DbType paramType, object value, int size, byte precision,
            ParameterDirection direction)
        {
            var returnVal = Create(paramName, paramType, value);
            returnVal.Direction = direction;
            returnVal.Size = size;
            ((SqlParameter)returnVal).Precision = precision;
            return returnVal;
        }

        /// <summary>
        ///     Converts a generic DbType into an SqlDbType
        /// </summary>
        /// <param name="dbType">The DbType to convert</param>
        /// <returns>The corresponding SqlDbType</returns>
        private static SqlDbType ConvertDbTypeToSqlDbType(DbType dbType)
        {
            SqlDbType sqlType;

            switch (dbType)
            {
                case DbType.AnsiString:
                    sqlType = SqlDbType.VarChar;
                    break;
                case DbType.AnsiStringFixedLength:
                    sqlType = SqlDbType.Char;
                    break;
                case DbType.Binary:
                    sqlType = SqlDbType.Binary;
                    break;
                case DbType.Boolean:
                    sqlType = SqlDbType.Bit;
                    break;
                case DbType.Byte:
                    sqlType = SqlDbType.TinyInt;
                    break;
                case DbType.Currency:
                    sqlType = SqlDbType.Money;
                    break;
                case DbType.Date:
                    sqlType = SqlDbType.Date;
                    break;
                case DbType.DateTime:
                    sqlType = SqlDbType.DateTime;
                    break;
                case DbType.DateTime2:
                    sqlType = SqlDbType.DateTime2;
                    break;
                case DbType.DateTimeOffset:
                    sqlType = SqlDbType.DateTimeOffset;
                    break;
                case DbType.Decimal:
                    sqlType = SqlDbType.Decimal;
                    break;
                case DbType.Double:
                    sqlType = SqlDbType.Float;
                    break;
                case DbType.Guid:
                    sqlType = SqlDbType.UniqueIdentifier;
                    break;
                case DbType.Int16:
                    sqlType = SqlDbType.SmallInt;
                    break;
                case DbType.Int32:
                    sqlType = SqlDbType.Int;
                    break;
                case DbType.Int64:
                    sqlType = SqlDbType.BigInt;
                    break;
                case DbType.Object:
                    sqlType = SqlDbType.Variant;
                    break;
                case DbType.SByte:
                    throw new ArgumentException("DbType.SByte has no corresponding SQL Server datatype");
                case DbType.Single:
                    sqlType = SqlDbType.Real;
                    break;
                case DbType.String:
                    sqlType = SqlDbType.NVarChar;
                    break;
                case DbType.StringFixedLength:
                    sqlType = SqlDbType.NChar;
                    break;
                case DbType.Time:
                    sqlType = SqlDbType.Time;
                    break;
                case DbType.UInt16:
                    throw new ArgumentException("DbType.UInt16 has no corresponding SQL Server datatype");
                case DbType.UInt32:
                    throw new ArgumentException("DbType.UInt32 has no corresponding SQL Server datatype");
                case DbType.UInt64:
                    throw new ArgumentException("DbType.UInt64 has no corresponding SQL Server datatype");
                case DbType.VarNumeric:
                    throw new ArgumentException("DbType.VarNumeric has no corresponding SQL Server datatype");
                case DbType.Xml:
                    sqlType = SqlDbType.Xml;
                    break;
                default:
                    throw new ArgumentException(dbType + " has no corresponding SQL Server datatype");
            }

            return sqlType;
        }
    }
}