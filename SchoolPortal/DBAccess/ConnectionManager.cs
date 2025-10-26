#nullable enable
using System;
using System.Data.SqlClient;
using System.IO;
using Properties = SchoolPortal.DBAccess.Properties;

namespace SchoolPortal.DBAccess
{
	#region ConnectionManager class
	public class ConnectionManager
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
								_defaultConnectionManager = new ConnectionManager("Data Source=SAGAR\\SQl2025;Initial Catalog=SchoolManagementSystem;Application Name=Unity Enterprise;Integrated Security=True");
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
				return Properties.Settings.Default.DefaultConnectionString;
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine($"Error accessing Settings.Default.DefaultConnectionString: {ex.Message}");
				return "Data Source=SAGAR\\SQl2025;Initial Catalog=SchoolManagementSystem;Application Name=Unity Enterprise;Integrated Security=True";
			}
		}

		public void ResetConnectionString()
		{
			ConnectionString = string.Empty;
		}

		public void ResetConnectionString(string dataSource, string user, string pw)
		{
			ResetConnectionString(dataSource, user, pw, "Mercury");
		}

		/// <summary>
		/// resets the connection string
		/// </summary>
		public void ResetConnectionString(string dataSource, string user, string pw, string dataBase)
		{
			SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
			{
				ApplicationName = "Unity Enterprise",
				DataSource = dataSource,
				InitialCatalog = dataBase,
				AsynchronousProcessing = true
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
	}
	#endregion ConnectionManager
}
