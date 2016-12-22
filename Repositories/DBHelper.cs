using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Threading;
using Utility;

namespace GZJD.DCIM.Data.DBUtility.DAL
{
    public class DBHelper : IDisposable
    {
        private SqlConnection connection;

        /// Default constructor which uses the "DefaultConnection" connectionString
        /// </summary>
        public DBHelper()
            : this("DefaultConnection")
        {
        }

        /// <summary>
        /// Constructor which takes the connection string name
        /// </summary>
        /// <param name="connectionStringName"></param>
        public DBHelper(string connectionStringName)
        {
            string connectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
            connection = new SqlConnection(connectionString);
        }

        /// <summary>
        /// Executes a non-query SQL statement
        /// </summary>
        /// <param name="commandText">The SQL query to execute</param>
        /// <param name="parameters">Optional parameters to pass to the query</param>
        /// <returns>The count of records affected by the SQL statement</returns>
        public int Execute(string commandText, Dictionary<string, object> parameters)
        {
            int result = 0;

            if (string.IsNullOrEmpty(commandText))
            {
                throw new ArgumentException("Command text cannot be null or empty.");
            }

            try
            {
                EnsureConnectionOpen();
                var command = CreateCommand(commandText, parameters);
                result = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                LogHelper.LogException("执行数据库操作失败！，信息：", ex);
            }
            finally
            {
                connection.Close();
            }

            return result;
        }

        /// <summary>
        /// Executes a SQL query that returns a single scalar value as the result.
        /// </summary>
        /// <param name="commandText">The SQL query to execute</param>
        /// <param name="parameters">Optional parameters to pass to the query</param>
        /// <returns></returns>
        public object QueryValue(string commandText, Dictionary<string, object> parameters)
        {
            object result = null;

            if (string.IsNullOrEmpty(commandText))
            {
                throw new ArgumentException("Command text cannot be null or empty.");
            }

            try
            {
                EnsureConnectionOpen();
                var command = CreateCommand(commandText, parameters);
                result = command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                LogHelper.LogException("执行数据库操作失败！，信息：", ex);
            }
            finally
            {
                EnsureConnectionClosed();
            }

            return result;
        }

        /// <summary>
        /// Executes a SQL query that returns a list of rows as the result.
        /// </summary>
        /// <param name="commandText">The SQL query to execute</param>
        /// <param name="parameters">Parameters to pass to the SQL query</param>
        /// <returns>A list of a Dictionary of Key, values pairs representing the 
        /// ColumnName and corresponding value</returns>
        public List<Dictionary<string, string>> Query(string commandText, Dictionary<string, object> parameters)
        {
            List<Dictionary<string, string>> rows = null;
            if (string.IsNullOrEmpty(commandText))
            {
                throw new ArgumentException("Command text cannot be null or empty.");
            }

            try
            {
                EnsureConnectionOpen();
                var command = CreateCommand(commandText, parameters);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    rows = new List<Dictionary<string, string>>();
                    while (reader.Read())
                    {
                        var row = new Dictionary<string, string>();
                        for (var i = 0; i < reader.FieldCount; i++)
                        {
                            var columnName = reader.GetName(i);
                            var columnValue = string.Empty;
                            var fieldType = reader.GetFieldType(i);
                            switch (fieldType.Name)
                            {
                                case "Int":
                                    columnValue = reader.IsDBNull(i) ? null : reader.GetInt32(i).ToString();
                                    break;
                                case "String":
                                    columnValue = reader.IsDBNull(i) ? null : reader.GetString(i);
                                    break;
                                case "DateTime":
                                    columnValue = reader.IsDBNull(i) ? null : reader.GetDateTime(i).ToString();
                                    break;
                                default:
                                    columnValue = reader.IsDBNull(i) ? null : reader.GetString(i);
                                    break;
                            }
                            row.Add(columnName, columnValue);
                        }
                        rows.Add(row);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException("执行数据库操作失败！，信息：", ex);
            }
            finally
            {
                EnsureConnectionClosed();
            }

            return rows;
        }

        /// <summary>
        /// Opens a connection if not open
        /// </summary>
        private void EnsureConnectionOpen()
        {
            var retries = 3;
            if (connection.State == ConnectionState.Open)
            {
                return;
            }
            else
            {
                while (retries >= 0 && connection.State != ConnectionState.Open)
                {
                    connection.Open();
                    retries--;
                    Thread.Sleep(30);
                }
            }
        }

        /// <summary>
        /// Closes a connection if open
        /// </summary>
        public void EnsureConnectionClosed()
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Creates a SQLCommand with the given parameters
        /// </summary>
        /// <param name="commandText">The SQL query to execute</param>
        /// <param name="parameters">Parameters to pass to the SQL query</param>
        /// <returns></returns>
        private SqlCommand CreateCommand(string commandText, Dictionary<string, object> parameters)
        {
            SqlCommand command = connection.CreateCommand();
            command.CommandText = commandText;
            AddParameters(command, parameters);

            return command;
        }

        /// <summary>
        /// Adds the parameters to a SQL command
        /// </summary>
        /// <param name="commandText">The SQL query to execute</param>
        /// <param name="parameters">Parameters to pass to the SQL query</param>
        private static void AddParameters(SqlCommand command, Dictionary<string, object> parameters)
        {
            if (parameters == null)
            {
                return;
            }

            foreach (var param in parameters)
            {
                var parameter = command.CreateParameter();
                parameter.ParameterName = param.Key;
                parameter.Value = param.Value ?? DBNull.Value;
                command.Parameters.Add(parameter);
            }
        }

        /// <summary>
        /// Helper method to return query a string value 
        /// </summary>
        /// <param name="commandText">The SQL query to execute</param>
        /// <param name="parameters">Parameters to pass to the SQL query</param>
        /// <returns>The string value resulting from the query</returns>
        public string GetStrValue(string commandText, Dictionary<string, object> parameters)
        {
            string value = QueryValue(commandText, parameters) as string;
            return value;
        }

        public void Dispose()
        {
            if (connection != null)
            {
                connection.Dispose();
                connection = null;
            }
        }
    }
}
