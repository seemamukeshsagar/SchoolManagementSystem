using System;
using System.Collections;
using System.Text;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Threading;

namespace SchoolPortal.DBAccess
{
	public class ReadOnlyProc : Proc
	{
		public ReadOnlyProc(string procName)
			: this(ReadOnlyConnectionManager.DefaultConnectionManager, procName, true)
		{
		}

		public ReadOnlyProc(ReadOnlyConnectionManager connectionManager, string procName)
			: this(connectionManager, procName, true)
		{
		}

		/// <summary>
		/// Initializes a new instance of the Proc class using the specified connection and
		/// procedure name, and optionally pre-fetches the parameters of the specfieid procedure.
		/// </summary>
		/// <remarks>
		/// If prefetchParms is false, you must manually add the parms to the Parameter collection
		/// of this instance, prior to setting their values using the indexor.
		/// </remarks>
		public ReadOnlyProc(ReadOnlyConnectionManager connectionManager, string procName, bool prefetchParms) : base (connectionManager, procName, prefetchParms)
		{

		}
	}
}
