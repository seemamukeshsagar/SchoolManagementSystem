#nullable enable
using System;
using Microsoft.Data.SqlClient;
using System.IO;
//using Properties = SchoolPortal.DBAccess.Properties;

namespace SchoolPortal.DBAccess
{
	#region ConnectionManager class
	public class ConnectionManager : IDisposable
	{
		private IQueryLogger? _queryLogger;

		public IQueryLogger? QueryLogger
		{
			get { return _queryLogger; }
			set { _queryLogger = value; }
		}

		private static ConnectionManager? _defaultConnectionManager;
		private static readonly object _lock = new object();

		public static ConnectionManager DefaultConnectionManager
		{
			get
			{
				if (_defaultConnectionManager == null)
				{
					lock (_lock)
					{
						if (_defaultConnectionManager == null)
						{
							try
							{
								_defaultConnectionManager = new ConnectionManager(LoadConnectionStringFromConfig());
							}
							catch (Exception ex)
							{
								System.Diagnostics.Debug.WriteLine($"Error creating DefaultConnectionManager: {ex.Message}");
								string machineName = Environment.MachineName;
								
								// Create fallback connection string with TCP/IP settings
								var fallbackBuilder = new SqlConnectionStringBuilder
								{
									ApplicationName = "Unity Enterprise",
									InitialCatalog = "SchoolManagementSystem",
									IntegratedSecurity = true,
									ConnectTimeout = 30,
									TrustServerCertificate = true,
									Encrypt = false,
									MultipleActiveResultSets = true,
									DataSource = machineName
								};
								
								_defaultConnectionManager = new ConnectionManager(fallbackBuilder.ConnectionString);
							}
						}
					}
				}
				return _defaultConnectionManager;
			}
		}

		public string ConnectionString { get; private set; }

		public ConnectionManager(string connectionString)
		{
			ConnectionString = connectionString;
		}

		private static string LoadConnectionStringFromConfig()
		{
			try
			{
				//string connectionString = Properties.Settings.Default.DefaultConnectionString;
				// Replace any environment variables in the connection string
				string connectionString = string.Empty;
				string machineName = Environment.MachineName;
				
				// Build connection string with TCP/IP protocol and common fallback options
				// Using SqlConnectionStringBuilder for better reliability
				var builder = new SqlConnectionStringBuilder
				{
					ApplicationName = "Unity Enterprise",
					InitialCatalog = "SchoolManagementSystem",
					IntegratedSecurity = true,
					ConnectTimeout = 30,
					TrustServerCertificate = true,
					Encrypt = false, // Set to false for local connections
					MultipleActiveResultSets = true
				};

				if (machineName.Equals("DESKTOP-L9I46P8"))
				{
					// Try machine name first, then localhost as fallback
					builder.DataSource = "DESKTOP-L9I46P8";
				}
				else
				{
					// Try machine name, but also support localhost and named instances
					builder.DataSource = machineName + "\\SQL2025" ;
				}

				// Force TCP/IP protocol by adding Network Library
				connectionString = builder.ConnectionString; // + ";Network Library=dbmssocn";
				return connectionString;
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine($"Error accessing Settings.Default.DefaultConnectionString: {ex.Message}");
				string machineName = Environment.MachineName;
				
				// Fallback connection string with TCP/IP and common settings
				var fallbackBuilder = new SqlConnectionStringBuilder
				{
					ApplicationName = "Unity Enterprise",
					InitialCatalog = "SchoolManagementSystem",
					IntegratedSecurity = true,
					ConnectTimeout = 30,
					TrustServerCertificate = true,
					Encrypt = false,
					MultipleActiveResultSets = true,
					DataSource = machineName + "\\SQL2025"
				};
				
				// Force TCP/IP protocol by adding Network Library
				return fallbackBuilder.ConnectionString + ";Network Library=dbmssocn";
			}
		}

		public void ResetConnectionString()
		{
			ConnectionString = string.Empty;
		}

		/// <summary>
		/// Sets the connection string directly
		/// </summary>
		public void SetConnectionString(string connectionString)
		{
			if (string.IsNullOrWhiteSpace(connectionString))
				throw new ArgumentException("Connection string cannot be null or empty", nameof(connectionString));
			
			ConnectionString = connectionString;
		}

		public void ResetConnectionString(string dataSource, string user, string pw)
		{
			ResetConnectionString(dataSource, user, pw, "SchoolManagementSystem");
		}

		/// <summary>
		/// resets the connection string
		/// </summary>
		public void ResetConnectionString(string dataSource, string user, string pw, string dataBase)
		{
			SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
			{
				ApplicationName = "SchoolManagementSystem",
				DataSource = dataSource,
				InitialCatalog = dataBase,
				ConnectTimeout = 30,
				TrustServerCertificate = true,
				Encrypt = false, // Set to false for local connections, true for remote
				MultipleActiveResultSets = true
			};

			if (user != string.Empty)
			{
				builder.PersistSecurityInfo = true;
				builder.UserID = user;
				builder.Password = pw;
			}
			else
			{
				builder.IntegratedSecurity = true;
			}

			// Force TCP/IP protocol by adding Network Library
			ConnectionString = builder.ConnectionString + ";Network Library=dbmssocn";
		}

		private string GetDBConnectionInfo()
		{
			if (ConnectionString == string.Empty)
				return string.Empty;

			#region get server name
			string[] arr = ConnectionString.Split(';');
			string svr = string.Empty;
			try
			{
				for (int i = 0; i < arr.Length; i++)
				{
					int x = arr[i].IndexOf("data source", StringComparison.CurrentCultureIgnoreCase);
					if (x >= 0)
					{
						svr = arr[i].Substring(arr[i].IndexOf('=') + 1);
						break;
					}
				}
			}
			catch { }
			#endregion get server name
			return svr;
		}

		public override string ToString()
		{
			return GetDBConnectionInfo();
		}

		public SqlConnection GetConnection()
		{
			return new SqlConnection(ConnectionString);
		}

		/// <summary>
		/// Tests the connection and returns true if successful
		/// </summary>
		public bool TestConnection()
		{
			try
			{
				using (var connection = GetConnection())
				{
					connection.Open();
					return true;
				}
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine($"Connection test failed: {ex.Message}");
				return false;
			}
		}

		/// <summary>
		/// Attempts to find a working connection by trying multiple server name variations
		/// </summary>
		public static string? FindWorkingConnectionString(string databaseName = "SchoolManagementSystem")
		{
			string machineName = Environment.MachineName;
			// Try TCP/IP first (127.0.0.1 and localhost), then machine name variations
			// Include SQL2025 instance name
			var serverNamesToTry = new[]
			{
				"127.0.0.1\\SQL2025",  // Explicit IP with instance - forces TCP/IP
				"localhost\\SQL2025",   // localhost with instance - prefers TCP/IP
				$"{machineName}\\SQL2025",  // Machine name with instance
				"127.0.0.1",  // Explicit IP address - forces TCP/IP
				"localhost",  // Try localhost - typically uses TCP/IP
				"(local)\\SQL2025",
				$"localhost\\SQLEXPRESS",
				$"{machineName}\\SQLEXPRESS",
				"(local)\\SQLEXPRESS",
				$"localhost\\MSSQLSERVER",
				$"{machineName}\\MSSQLSERVER",
				machineName,
				"(local)"
			};

			foreach (var serverName in serverNamesToTry)
			{
				try
				{
					var builder = new SqlConnectionStringBuilder
					{
						ApplicationName = "Unity Enterprise",
						InitialCatalog = databaseName,
						IntegratedSecurity = true,
						ConnectTimeout = 5, // Short timeout for testing
						TrustServerCertificate = true,
						Encrypt = false,
						MultipleActiveResultSets = true,
						DataSource = serverName
					};

					// Force TCP/IP protocol
					var connectionString = builder.ConnectionString; // + ";Network Library=dbmssocn";

					using (var connection = new SqlConnection(connectionString))
					{
						connection.Open();
						// If we get here, the connection works
						// Return with normal timeout
						builder.ConnectTimeout = 30;
						return builder.ConnectionString; // + ";Network Library=dbmssocn";
					}
				}
				catch
				{
					// Try next server name
					continue;
				}
			}

			return null; // No working connection found
		}

		internal void LogQuery(Proc proc)
		{
			if (_queryLogger != null)
			{
				_queryLogger.Log(proc.ToString());
			}
		}

		#region IDisposable Implementation
		private bool _disposed = false;

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				if (disposing)
				{
					// Dispose managed resources here
					// For example:
					// if (_someDisposableResource != null)
					// {
					//     _someDisposableResource.Dispose();
					// }
				}

				// Free unmanaged resources here
				// (if any)
				
				_disposed = true;
			}
		}

		~ConnectionManager()
		{
			Dispose(false);
		}
		#endregion
	}
	#endregion ConnectionManager
}
