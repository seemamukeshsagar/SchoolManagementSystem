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
								_defaultConnectionManager = new ConnectionManager($"Data Source={machineName};Initial Catalog=SchoolManagementSystem;Application Name=Unity Enterprise;Integrated Security=True");
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
				if (machineName.Equals("DESKTOP-L9I46P8"))
				{
					connectionString = "Data Source=DESKTOP-L9I46P8;Initial Catalog=SchoolManagementSystem;Application Name=Unity Enterprise;Integrated Security=True";
                }
				else
				{
					//connectionString = connectionString.Replace("${ServerName}", machineName + "\\SQL2026");
					connectionString = "Data Source=" + machineName + ";Initial Catalog=SchoolManagementSystem;Application Name=Unity Enterprise;Integrated Security=True";
                }
				return connectionString;
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine($"Error accessing Settings.Default.DefaultConnectionString: {ex.Message}");
				string machineName = Environment.MachineName;
				return $"Data Source={machineName};Initial Catalog=SchoolManagementSystem;Application Name=Unity Enterprise;Integrated Security=True";
			}
		}

		public void ResetConnectionString()
		{
			ConnectionString = string.Empty;
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
				InitialCatalog = dataBase
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

			ConnectionString = builder.ConnectionString;
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
