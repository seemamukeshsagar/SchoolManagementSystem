using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SchoolPortal.DBAccess
{
    public static class ProcExtensions
    {
        /// <summary>
        /// Gets the SqlCommand from a Proc instance using reflection
        /// </summary>
        private static SqlCommand GetCommand(Proc proc)
        {
            var field = typeof(Proc).GetField("_command", BindingFlags.NonPublic | BindingFlags.Instance);
            return (SqlCommand)field.GetValue(proc);
        }

        /// <summary>
        /// Gets the ConnectionManager from a Proc instance using reflection
        /// </summary>
        private static ConnectionManager GetConnectionManager(Proc proc)
        {
            var field = typeof(Proc).GetField("_connectionManager", BindingFlags.NonPublic | BindingFlags.Instance);
            return (ConnectionManager)field.GetValue(proc);
        }

        /// <summary>
        /// Executes the stored procedure and returns a list of the specified type
        /// </summary>
        public static List<T> Exec<T>(this Proc proc) where T : new()
        {
            var command = GetCommand(proc);
            var connectionManager = GetConnectionManager(proc);
            
            var result = new List<T>();
            using (var conn = connectionManager.GetConnection())
            {
                command.Connection = conn;
                conn.Open();
                
                using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    var properties = typeof(T).GetProperties();
                    
                    while (reader.Read())
                    {
                        var item = new T();
                        
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            var columnName = reader.GetName(i);
                            var property = properties.FirstOrDefault(p => 
                                string.Equals(p.Name, columnName, StringComparison.OrdinalIgnoreCase));
                                
                            if (property != null && !reader.IsDBNull(i))
                            {
                                var value = reader[i];
                                if (value != DBNull.Value)
                                {
                                    try
                                    {
                                        property.SetValue(item, Convert.ChangeType(value, Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType), null);
                                    }
                                    catch
                                    {
                                        // If conversion fails, try to handle nullables
                                        if (property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                                        {
                                            property.SetValue(item, null, null);
                                        }
                                    }
                                }
                            }
                        }
                        
                        result.Add(item);
                    }
                }
            }
            
            return result;
        }

        /// <summary>
        /// Executes the stored procedure and returns the first row as the specified type
        /// </summary>
        public static T ExecSingle<T>(this Proc proc) where T : new()
        {
            return proc.Exec<T>().FirstOrDefault();
        }

        /// <summary>
        /// Executes the stored procedure and returns the number of rows affected
        /// </summary>
        public static int ExecNonQuery(this Proc proc)
        {
            var command = GetCommand(proc);
            var connectionManager = GetConnectionManager(proc);
            
            using (var conn = connectionManager.GetConnection())
            {
                command.Connection = conn;
                conn.Open();
                
                return command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Sets a parameter value on the stored procedure
        /// </summary>
        public static Proc SetParameter(this Proc proc, string name, object value)
        {
            if (proc.Parameters.Contains(name))
            {
                proc.Parameters[name].Value = value ?? DBNull.Value;
            }
            return proc;
        }
    }
}
