using System;
using System.Data.SqlClient;
using Properties = SchoolPortal.DBAccess.Properties;

namespace SchoolPortal.DBAccess
{
	public class ReadOnlyConnectionManager : ConnectionManager
	{
		private IQueryLogger _queryLogger;

		public new IQueryLogger QueryLogger
		{
			get { return _queryLogger; }
			set { _queryLogger = value; }
		}

		private static readonly ReadOnlyConnectionManager _defaultConnectionManager = new ReadOnlyConnectionManager(Properties.Settings.Default.ReadOnlyDefaultConnectionString);

		public new static ReadOnlyConnectionManager DefaultConnectionManager
		{
			get
			{
				return _defaultConnectionManager;
			}
		}

		public new string ConnectionString { get; private set; }

		public ReadOnlyConnectionManager(string connectionString) : base(connectionString)
		{
			ConnectionString = connectionString;
		}

		public new SqlConnection GetConnection()
		{
			return new SqlConnection(ConnectionString);
		}

		internal void LogQuery(ReadOnlyProc proc)
		{
			if (_queryLogger != null)
			{
				_queryLogger.Log(proc.ToString());
			}
		}
	}
}
