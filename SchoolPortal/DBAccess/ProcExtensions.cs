using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace SchoolPortal.DBAccess
{
    public static class ProcExtensions
    {
        /// <summary>
        /// Executes the stored procedure and returns the first column of the first row
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static object ExecScalar(this Proc proc)
        {
            var command = GetCommand(proc);
            var connectionManager = GetConnectionManager(proc);
            
            using (var conn = connectionManager.GetConnection())
            {
                command.Connection = conn;
                conn.Open();
                
                return command.ExecuteScalar();
            }
        }

        /// <summary>
        /// Executes the stored procedure and returns a data reader
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SqlDataReader ExecReader(this Proc proc)
        {
            var command = GetCommand(proc);
            var connectionManager = GetConnectionManager(proc);
            
            // Note: The connection will be closed when the reader is disposed
            var conn = connectionManager.GetConnection();
            conn.Open();
            
            command.Connection = conn;
            return command.ExecuteReader(CommandBehavior.CloseConnection);
        }
        /// <summary>
        /// Gets the SqlCommand from a Proc instance using reflection
        /// </summary>
        private static SqlCommand GetCommand(Proc proc)
        {
            if (proc == null)
                throw new ArgumentNullException(nameof(proc));
                
            var field = typeof(Proc).GetField("_command", BindingFlags.NonPublic | BindingFlags.Instance);
            if (field == null)
                throw new InvalidOperationException("Unable to find _command field on Proc class");
                
            var command = field.GetValue(proc) as SqlCommand;
            return command ?? throw new InvalidOperationException("Command is null");
        }

        /// <summary>
        /// Gets the ConnectionManager from a Proc instance using reflection
        /// </summary>
        private static ConnectionManager GetConnectionManager(Proc proc)
        {
            if (proc == null)
                throw new ArgumentNullException(nameof(proc));
                
            var field = typeof(Proc).GetField("_connectionManager", BindingFlags.NonPublic | BindingFlags.Instance);
            if (field == null)
                throw new InvalidOperationException("Unable to find _connectionManager field on Proc class");
                
            var connectionManager = field.GetValue(proc) as ConnectionManager;
            return connectionManager ?? throw new InvalidOperationException("ConnectionManager is null");
        }

        /// <summary>
        /// Executes the stored procedure and returns a list of the specified type
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? ExecSingle<T>(this Proc proc) where T : class, new()
        {
            if (proc == null)
                throw new ArgumentNullException(nameof(proc));
                
            return proc.Exec<T>().FirstOrDefault();
        }

        /// <summary>
        /// Executes the stored procedure and returns the number of rows affected
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
        public static Proc SetParameter(this Proc proc, string name, object? value)
        {
            if (proc == null)
                throw new ArgumentNullException(nameof(proc));
                
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Parameter name cannot be null or empty", nameof(name));
                
            if (proc.Parameters.Contains(name))
            {
                proc.Parameters[name].Value = value ?? DBNull.Value;
            }
            return proc;
        }
    }
}
