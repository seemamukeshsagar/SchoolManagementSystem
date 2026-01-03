using Microsoft.Extensions.Configuration;
using System;
using Microsoft.Data.SqlClient;

namespace SchoolPortalApp.Utilities
{
    public static class ConnectionStringHelper
    {
        private static IConfiguration _configuration;

        public static void Initialize(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

         public static string GetConnectionString(string name = "DefaultConnectionString")
        {
            if (_configuration == null)
            {
                throw new InvalidOperationException("ConnectionStringHelper has not been initialized. Call Initialize() first.");
            }

            var connectionString = _configuration.GetConnectionString(name) ?? 
                   throw new InvalidOperationException($"Connection string '{name}' not found in configuration.");

            // Replace placeholders if they exist
            if (connectionString.Contains("{SQL_SERVER}") || connectionString.Contains("{DATABASE_NAME}"))
            {
                // Get server and database from DatabaseSettings or use defaults
                var server = _configuration["DatabaseSettings:Server"];
                var databaseName = _configuration["DatabaseSettings:DatabaseName"] ?? "SchoolManagementSystem";
                
                // If DatabaseSettings:Server not found or is localhost, use machine name with instance
                if (string.IsNullOrEmpty(server) || server.Equals("localhost", StringComparison.OrdinalIgnoreCase))
                {
                    var machineName = Environment.MachineName;
                    // Add instance name if machine is not DESKTOP-L9I46P8
                    if (!machineName.Equals("DESKTOP-L9I46P8", StringComparison.OrdinalIgnoreCase))
                    {
                        server = machineName + "\\SQL2025";
                    }
                    else
                    {
                        server = machineName;
                    }
                }
                else
                {
                    // If server is specified but doesn't have instance name, check if we should add it
                    if (!server.Contains("\\") && !server.Equals("localhost", StringComparison.OrdinalIgnoreCase))
                    {
                        // Add instance name for non-localhost servers
                        server = server + "\\SQL2025";
                    }
                }

                connectionString = connectionString
                    .Replace("{SQL_SERVER}", server)
                    .Replace("{DATABASE_NAME}", databaseName);
            }

            // Force TCP/IP protocol instead of Named Pipes
            // Parse and rebuild the connection string with proper settings
            try
            {
                var builder = new SqlConnectionStringBuilder(connectionString);
                
                // Ensure these settings for better connectivity
                builder.ConnectTimeout = builder.ConnectTimeout > 0 ? builder.ConnectTimeout : 30;
                builder.TrustServerCertificate = true;
                builder.Encrypt = false; // For local connections
                builder.MultipleActiveResultSets = true;

                // Force TCP/IP protocol by using tcp: prefix or 127.0.0.1
                // This prevents Named Pipes from being used
                if (!string.IsNullOrEmpty(builder.DataSource))
                {
                    var dataSource = builder.DataSource;
                    
                    // Remove any existing protocol prefix
                    if (dataSource.StartsWith("tcp:", StringComparison.OrdinalIgnoreCase))
                    {
                        dataSource = dataSource.Substring(4);
                    }
                    else if (dataSource.StartsWith("np:", StringComparison.OrdinalIgnoreCase))
                    {
                        dataSource = dataSource.Substring(3);
                    }
                    
                    // If it's localhost without instance, use 127.0.0.1
                    // Preserve actual machine names (e.g., SAGAR) instead of converting to 127.0.0.1
                    if (dataSource.Equals("localhost", StringComparison.OrdinalIgnoreCase))
                    {
                        // Use 127.0.0.1 which forces TCP/IP
                        builder.DataSource = "127.0.0.1";
                    }
                    else if (dataSource.Contains("\\"))
                    {
                        // Has instance name (e.g., MACHINE\SQL2025 or localhost\SQL2025)
                        var parts = dataSource.Split('\\');
                        if (parts.Length == 2)
                        {
                            var serverName = parts[0];
                            var instanceName = parts[1];
                            
                            // Only convert to 127.0.0.1 if it's localhost
                            // Preserve the original server name (e.g., SAGAR\SQL2025) for actual machine names
                            if (serverName.Equals("localhost", StringComparison.OrdinalIgnoreCase))
                            {
                                builder.DataSource = $"127.0.0.1\\{instanceName}";
                            }
                            else
                            {
                                // Preserve the original server name with instance
                                builder.DataSource = dataSource;
                            }
                        }
                        else
                        {
                            // Use tcp: prefix to force TCP/IP
                            builder.DataSource = $"tcp:{dataSource}";
                        }
                    }
                    else
                    {
                        // Use tcp: prefix to force TCP/IP
                        builder.DataSource = $"tcp:{dataSource}";
                    }
                }

                // Manually add Network Library to force TCP/IP (dbmssocn = TCP/IP)
                // SqlConnectionStringBuilder doesn't expose this, so we add it to the string
                var finalConnectionString = builder.ConnectionString;
                if (!finalConnectionString.Contains("Network Library", StringComparison.OrdinalIgnoreCase) &&
                    !finalConnectionString.Contains("dbmssocn", StringComparison.OrdinalIgnoreCase))
                {
                    finalConnectionString += ";Network Library=dbmssocn";
                }

                return finalConnectionString;
            }
            catch
            {
                // If parsing fails, try to add Network Library manually
                if (!connectionString.Contains("Network Library", StringComparison.OrdinalIgnoreCase) &&
                    !connectionString.Contains("dbmssocn", StringComparison.OrdinalIgnoreCase))
                {
                    return connectionString + ";Network Library=dbmssocn";
                }
                return connectionString;
            }
        }
    }
}
