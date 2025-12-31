using System;
using System.Text;
using Microsoft.Data.SqlClient;

namespace SchoolPortal.DBAccess
{
	#region DBAccessException class
	[Serializable]
	public class DBAccessException : ApplicationException
	{
		private readonly byte _severity;
		public byte Severity
		{
			get { return _severity; }
		}

		private string _sql = string.Empty;
		public string Sql
		{
			get { return _sql; }
			set { _sql = value; }
		}

		public DBAccessException() { }

		public DBAccessException(string message, Exception inner, Proc proc)
			: base(message, inner)
		{
			_sql = proc.ToString();

			if (inner is SqlException)
			{
				_severity = ((SqlException) inner).Class;
			}
		}

		public DBAccessException(string message, Exception inner, ReadOnlyProc proc)
			: base(message, inner)
		{
			_sql = proc.ToString();

			if (inner is SqlException)
			{
				_severity = ((SqlException)inner).Class;
			}
		}

		public DBAccessException(string message) : base(message) { }
		public DBAccessException(string message, Exception inner) : base(message, inner) { }
#if NET8_0_OR_GREATER
        [Obsolete("This constructor is obsolete. Use the constructor without the SerializationInfo and StreamingContext parameters.")]
#endif
        protected DBAccessException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }


		public override string ToString()
		{
			StringBuilder sb = new StringBuilder(base.ToString());
			sb.Append(Environment.NewLine);
			sb.Append(Environment.NewLine);
			sb.Append("SQL:");
			sb.Append(Environment.NewLine);
			sb.Append(_sql);
			return sb.ToString();
		}
	}
	#endregion DBAccessException class
}
