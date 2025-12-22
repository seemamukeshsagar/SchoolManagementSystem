#nullable enable

using System;
using System.Collections;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Threading;

namespace SchoolPortal.DBAccess
{
	public delegate object BorrowReader(SqlDataReader reader, object[] args);

	#region Proc class
	public class Proc : IDisposable
	{
		private const int TRANSPORT_ERROR_RETRY_COUNT = 3;
		private static readonly Hashtable _parmCache = Hashtable.Synchronized(new Hashtable());
		private string? _hashkey;
		private readonly ConnectionManager _connectionManager;

		 private bool disposed = false;

		private readonly string _procName = string.Empty;
		private readonly SqlCommand _command;
		public SqlParameterCollection Parameters { get { return (_command.Parameters); } }

		/// <summary>
		/// Gets or sets the Timeout time in seconds for the Proc.
		/// </summary>
		public int Timeout
		{
			get
			{
				if (_command != null) return _command.CommandTimeout;
				return -1;
			}
			set
			{
				if (_command != null)
					_command.CommandTimeout = value;
			}
		}

		public string ProcName
		{
			get { return _procName; }
		}

		#region constructors
		public Proc(string procName)
			: this(ConnectionManager.DefaultConnectionManager, procName, true)
		{
		}

		public Proc(ConnectionManager connectionManager, string procName)
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
		public Proc(ConnectionManager connectionManager, string procName, bool prefetchParms)
		{
			_connectionManager = connectionManager;
			_procName = procName;
			_command = new SqlCommand(_procName)
						{
							CommandType = CommandType.StoredProcedure,
							//CommandTimeout = Properties.Settings.Default.Timeout
							CommandTimeout = 30
						};
			if (prefetchParms)
			{
				GetParms();
			}
		}
		#endregion constructors

		public void SetTypeName(string sqlParameter, string typeName)
		{
			this._command.Parameters[sqlParameter].TypeName = typeName;
		}
		/// <summary>
		/// clears the internal parameter cache
		/// </summary>
		static public void ResetParmCache()
		{
			_parmCache.Clear();
		}

		/// <summary>
		/// the key for this proc in the cache
		/// </summary>
		private string HashKey => _hashkey ??= string.Format("{0}:{1}", _connectionManager.ConnectionString, _procName);

		private void ClearParmsFromCache()
		{
			lock (_parmCache)
			{
				_parmCache.Remove(HashKey);
			}
		}

		/// <summary>
		/// Use this method to derive the command's parameter collection (either from the param cache or from the database)
		/// </summary>
		private void GetParms()
		{
			//critical section start
			lock (_parmCache)
			{
				_hashkey = HashKey;

				//retrieve the parms from the cache
				if (_parmCache[_hashkey] is not ParmList parms)
				{
					// to avoid transaction contension, we'll close/use a new connection
					using (SqlConnection conn = _connectionManager.GetConnection() ?? throw new InvalidOperationException("Connection cannot be null."))
					{
						_command.Connection = conn;

						PrepareConnection(conn);
						SqlCommandBuilder.DeriveParameters(_command);
						conn.Close();

						//deep copy the parms from this cmd object to the cache (to avoid network roundtrip next time around)
						ParmList parms2 = new ParmList(_command.Parameters);
						_parmCache[_hashkey] = parms2;
					}
				}
				else
				{
					//otherwise just deep copy them to the current command object
					parms.CopyToCommand(_command);
				}
			}
		}

		/// <summary>
		/// Indexor used to access the proc's parameters
		/// </summary>
		/// <param name="parmName"></param>
		/// <returns></returns>
		public object this[string parmName]
		{
			get
			{
				try
				{
					return Parameters[parmName].Value;
				}
				catch
				{
					ClearParmsFromCache();  // reset parm cache on error
					throw;
				}
			}
			set
			{
				try
				{
					Parameters[parmName].Value = value;
				}
				catch
				{
					ClearParmsFromCache();
					throw;
				}
			}
		}
		//MB19631 10/13/2010 : Put this in because if we are passing in say a 5mb xml file, the logger chokes the process up
		//so this gives the option to skip to speed up the process

		private bool _skipLog = false;

		public bool SkipLog
		{
			get { return _skipLog; }
			set { _skipLog = value; }
		}


		private void PrepareConnection(SqlConnection conn)
		{
			if (conn.State != ConnectionState.Open)
			{
				conn.Open();

				using (SqlCommand cmd = new SqlCommand("set nocount on", conn))
				{
					cmd.CommandType = CommandType.Text;
					cmd.ExecuteNonQuery();
				}
			}
		}

		private void ForceRollback(SqlConnection? conn)
		{
			try
			{
				if (conn != null && conn.State != ConnectionState.Closed)
				{
					using (SqlCommand cmd = new SqlCommand("IF @@TRANCOUNT > 0 ROLLBACK", conn))
					{
						cmd.ExecuteNonQuery();
					}
				}
			}
			catch (Exception)
			{
			}
		}

		public static void Fill(SqlDataReader dr, DataTable table)
		{
			table.Load(dr, LoadOption.OverwriteChanges);
		}

		public static void Fill(SqlDataReader sqlDataReader, DataSet dataSet, params string[] tables)
		{
			dataSet.Load(sqlDataReader, LoadOption.OverwriteChanges, tables);
		}

		#region Async exec methods

		/// <summary>
		/// runs an asynchronous command
		/// </summary>
		/// <param name="errorDel"></param>
		/// <param name="callbackDelegate">callback method to invoke when the query completes</param>
		/// <param name="asyncState">state object passed back to the callback when the query completes</param>
		/// <param name="synchronizationContext"></param>
		public AsyncCommand ExecAsync(object? asyncState, AsyncDelegate callbackDelegate, AsyncErrorDelegate? errorDel, SynchronizationContext? synchronizationContext)
		{
			SqlConnection? conn = null;

			try
			{
				conn = _connectionManager.GetConnection() ?? throw new InvalidOperationException("Connection cannot be null.");
				// we need to ensure the Connection is open prior to attempting
				// to start a transaction
				PrepareConnection(conn);
				_command.Connection = conn;

				if (!_skipLog)
				{

					_connectionManager.LogQuery(this);
				}


				// Although it is not required that you pass the 
				// SqlCommand object as the second parameter in the 
				// BeginExecuteReader call, doing so makes it easier
				// to call EndExecuteReader in the callback procedure.
				AsyncCallback callback = HandleCallback;

				// create the helper object that gets passed to the callback method
				var helper = new AsyncHelper
								{
									State = asyncState,
									ErrorDelegate = errorDel,
									CallbackDelegate = callbackDelegate,
									SynchronizationContext = synchronizationContext,
									Command = new AsyncCommand(_command)
								};

				_command.BeginExecuteReader(callback, helper, CommandBehavior.CloseConnection);

				return helper.Command;

				// NOTE:  can't cleanup the connection here since the callback will need it for access to the datareader
			}
			catch (Exception ex)
			{
				ClearParmsFromCache();  // reset the parm list in the cache so that we'll re-retrieve the parms next time thru

				ForceRollback(conn);

				if (conn != null)
				{
					conn.Close();
				}

				throw new DBAccessException(ex.Message, ex, this);
			}
		}


		/// <summary>
		/// callback invoked when a search completes
		/// </summary>
		/// <param name="result"></param>
		private void HandleCallback(IAsyncResult result)
		{
			AsyncHelper e = (AsyncHelper)result.AsyncState;
			SqlDataReader?	 reader = null;
			try
			{
				// Retrieve the original command object, passed
				// to this procedure in the AsyncState property
				// of the IAsyncResult parameter.
				reader = _command.EndExecuteReader(result);

				// You may not interact with the form and its contents
				// from a different thread, and this callback procedure
				// is all but guaranteed to be running from a different thread
				// than the form. Therefore you cannot simply call code that 
				// fills the grid, like this:
				// FillGrid(reader);
				// Instead, you must call the procedure from the form's thread.
				// One simple way to accomplish this is to call the Invoke
				// method of the form, which calls the delegate you supply
				// from the form's thread. 

				// invoke this delegate to let the caller do his thing.  and the cleanup when he's done.
				e.SynchronizationContext.Send(state => e.CallbackDelegate(e.Command, reader, state), e.State);
			}
			catch (Exception ex)
			{
				//Horrible hack!!! We must call EndExecuteReader to avoid leaking threads, but this throws an exception if the query is cancelled
				if (!ex.Message.Contains("Operation cancelled by user."))
				{


					// Because you are now running code in a separate thread, 
					// if you do not handle the exception here, none of your other
					// code catches the exception. Because there is none of 
					// your code on the call stack in this thread, there is nothing
					// higher up the stack to catch the exception if you do not 
					// handle it here. You can either log the exception or 
					// invoke a delegate (as in the non-error case in this 
					// example) to display the error on the form. In no case
					// can you simply display the error without executing a delegate
					// as in the try block here. 
					// You can create the delegate instance as you 
					// invoke it, like this:
					if (e.ErrorDelegate != null)
					{
						e.ErrorDelegate(e.Command, ex);
					}

				}
			}
			finally
			{
				if (reader != null)
				{
					// Closing the reader also closes the connection,
					// because this reader was created using the 
					// CommandBehavior.CloseConnection value.
					reader.Close();  // cleanup
				}
			}
		}

		#endregion Async exec methods


		#region Exec w/ overloads

		/// <summary>
		/// Executes a non-query procedure using the default connection and, by default, wraps in a transaction
		/// </summary>
		public void Exec()
		{
			Exec(true);
		}

		/// <summary>
		/// optionally executes the proc within the context of a transaction
		/// </summary>
		/// <param name="wrapInTransaction"></param>
		public void Exec(bool wrapInTransaction)
		{
			SqlTransaction? tran = null;
			using (SqlConnection conn = _connectionManager.GetConnection())
			{
				try
				{
					// we need to ensure the Connection is open prior to attempting
					// to start a transaction
					PrepareConnection(conn);

					_command.Connection = conn;

					if (wrapInTransaction)
					{
						tran = conn.BeginTransaction();
						_command.Transaction = tran;
					}


					if (!_skipLog)
					{
						_connectionManager.LogQuery(this);
					}

					_command.ExecuteNonQuery();

					if (wrapInTransaction && tran != null)
					{
						tran.Commit();
					}
				}
				catch (Exception ex)
				{
					ClearParmsFromCache();  // reset the parm list in the cache so that we'll re-retrieve the parms next time thru

					if (tran != null)
					{
						tran.Rollback();
					}

					ForceRollback(conn);

					throw new DBAccessException(ex.Message, ex, this);
				}
				finally
				{
					if (tran != null) tran.Dispose();

					if (conn.State != ConnectionState.Closed)
					{
						conn.Close();
					}
				}
			}
		}

		/// <summary>
		/// Executes a non-query procedure
		/// </summary>
		/// <param name="tran"></param>
		public void Exec(SqlTransaction tran)
		{
			if (tran == null)
			{
				throw new ArgumentNullException(nameof(tran), "Transaction cannot be null.");
			}
			
			if (tran.Connection == null)
			{
				throw new ArgumentException("The transaction was rolled back or committed, please provide an open transaction.", nameof(tran));
			}

			try
			{
				_connectionManager.LogQuery(this);
				_command.Connection = tran.Connection;
				_command.Transaction = tran;
				_command.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				ClearParmsFromCache();  // reset the parm list in the cache so that we'll re-retrieve the parms next time thru

				ForceRollback(_command.Connection);

				throw new DBAccessException(ex.Message, ex, this);
			}
		}

		/// <summary>
		/// fills the given dataset from the proc results, use tableMappings to supply a destination for multi-result procs
		/// </summary>
		/// <param name="ds"></param>
		/// <param name="tableMappings"></param>
		public void Exec(DataSet ds, params string[] tableMappings)
		{
			Exec(ds, false, tableMappings);
		}

		/// <summary>
		/// fills the given dataset from the proc results, use tableMappings to supply a destination for multi-result procs
		/// </summary>
		/// <param name="ds"></param>
		/// <param name="append"></param>
		/// <param name="tableMappings"></param>
		public void Exec(DataSet ds, bool append, params string[] tableMappings)
		{
			int tryCount = 0;
			bool retry;

			do
			{
				retry = false;

				using (SqlConnection conn = _connectionManager.GetConnection())
				{
					try
					{
						PrepareConnection(conn);
						_command.Connection = conn;
						SqlDataAdapter adapter = new SqlDataAdapter(_command);
						// add table mappings, if any. 
						// when there's no table mappings, ADO.Net will automatically create new, generic tables in the dataset
						for (int i = 0; i < tableMappings.Length; i++)
						{
							if (i == 0)
							{
								adapter.TableMappings.Add("Table", tableMappings[i]);
							}
							else
							{
								adapter.TableMappings.Add(string.Format("Table{0}", i), tableMappings[i]);
							}

							if (!append)
							{
								ds.Tables[tableMappings[i]].Rows.Clear(); // remove all rows before inserting new ones
							}
						}

						_connectionManager.LogQuery(this);
						adapter.Fill(ds);
					}
					catch (ConstraintException ex)
					{
						StringBuilder sb = new StringBuilder();
						for (int i = 0; i < ds.Tables.Count; i++)
						{
							if (ds.Tables[i].HasErrors)
							{
								var r = ds.Tables[i].GetErrors();
								for (int rs = 0; rs < r.Length; rs++)
								{
									if (r[rs].HasErrors)
										sb.AppendLine(r[rs].RowError);
								}
							}

						}
						throw new DBAccessException(sb.ToString() + System.Environment.NewLine + ex.Message, ex, this);

					}
					catch (Exception ex)
					{
						// HACK: TMC 11-30-2006 - we're getting a lot of 'transport level' exceptions when a vpn or wireless connection hiccups
						//   rather than throwing an annoying exception every time, we'll instead retry and, if there's a real issue, a different kind of exception will be throw.
						//   The problem is that when a connection hiccups, the SqlConnection object doesn't know about it and the state remains as 'open' rather than a 'broken' status.
						//   I dug up this thread and looks like MS is punting on it for now :(
						//   http://forums.microsoft.com/TechNet/ShowPost.aspx?PageIndex=3&SiteID=17&PostID=951442
						// Update:  RKF 2/21/2011
						//   The above thread was updated over a several years. Microsoft has proposed a "fix" for what the believe to be causeing the problem
						//   which is "Faulty network hardware is dropping portions of the TCP traffic." and that a possible fix is to disable SYN protection on the server.
						//   http://msdn.microsoft.com/en-us/library/ms187005.aspx (which is probably a bad idea)
						//  
						string transportExceptionText = "A transport-level error has occurred".ToUpper();
						if (ex.Message.ToUpper().Contains(transportExceptionText) && tryCount < TRANSPORT_ERROR_RETRY_COUNT)
						{
							retry = true; // eat exception and retry
							tryCount++;
							SqlConnection.ClearPool(conn);
						}
						else
						{
							ClearParmsFromCache(); // reset the parm list in the cache so that we'll re-retrieve the parms next time thru

							ForceRollback(conn);

							throw new DBAccessException(ex.Message, ex, this);
						}
					}
					finally
					{
						if (conn.State != ConnectionState.Closed)
						{
							conn.Close();
						}
					}
				}
			} while (retry);
		}

		/// <summary>
		/// fills the given datatable from the proc results
		/// </summary>
		/// <param name="dt"></param>
		public void Exec(DataTable dt)
		{
			Exec(dt, false);
		}

		public DataTable Execute(DataTable dt)
		{
			return Execute(dt, false);
		}

		public DataTable Execute(DataTable dt, bool append)
		{
			int tryCount = 0;
			bool retry;

			do
			{
				retry = false;

				using (SqlConnection conn = _connectionManager.GetConnection())
				{
					try
					{
						PrepareConnection(conn);
						_command.Connection = conn;
						SqlDataAdapter adapter = new SqlDataAdapter(_command);

						if (!append)
						{
							dt.Rows.Clear();
						}

						_connectionManager.LogQuery(this);
						adapter.Fill(dt);

					}
					catch (Exception ex)
					{
						// HACK: TMC 11-30-2006 - we're getting a lot of 'transport level' exceptions when a vpn or wireless connection hiccups
						//   rather than throwing an annoying exception every time, we'll instead retry and, if there's a real issue, a different kind of exception will be throw.
						//   The problem is that when a connection hiccups, the SqlConnection object doesn't know about it and the state remains as 'open' rather than a 'broken' status.
						//   I dug up this thread and looks like MS is punting on it for now :(
						//   http://forums.microsoft.com/TechNet/ShowPost.aspx?PageIndex=3&SiteID=17&PostID=951442
						string transportExceptionText = "A transport-level error has occurred".ToUpper();
						if (ex.Message.ToUpper().Contains(transportExceptionText) && tryCount < TRANSPORT_ERROR_RETRY_COUNT)
						{
							retry = true; // eat exception and retry
							tryCount++;
							SqlConnection.ClearPool(conn);
						}
						else
						{
							ClearParmsFromCache(); // reset the parm list in the cache so that we'll re-retrieve the parms next time thru

							ForceRollback(conn);

							throw new DBAccessException(ex.Message, ex, this);
						}
					}
					finally
					{
						if (conn.State != ConnectionState.Closed)
						{
							conn.Close();
						}
					}

				}

			} while (retry);
			return dt;
		}

		public void Exec(DataTable dt, bool append)
		{
			int tryCount = 0;
			bool retry;

			do
			{
				retry = false;

				using (SqlConnection conn = _connectionManager.GetConnection())
				{
					try
					{
						PrepareConnection(conn);
						_command.Connection = conn;
						SqlDataAdapter adapter = new SqlDataAdapter(_command);

						if (!append)
						{
							dt.Rows.Clear();
						}

						_connectionManager.LogQuery(this);
						adapter.Fill(dt);
					}
					catch (Exception ex)
					{
						// HACK: TMC 11-30-2006 - we're getting a lot of 'transport level' exceptions when a vpn or wireless connection hiccups
						//   rather than throwing an annoying exception every time, we'll instead retry and, if there's a real issue, a different kind of exception will be throw.
						//   The problem is that when a connection hiccups, the SqlConnection object doesn't know about it and the state remains as 'open' rather than a 'broken' status.
						//   I dug up this thread and looks like MS is punting on it for now :(
						//   http://forums.microsoft.com/TechNet/ShowPost.aspx?PageIndex=3&SiteID=17&PostID=951442
						string transportExceptionText = "A transport-level error has occurred".ToUpper();
						if (ex.Message.ToUpper().Contains(transportExceptionText) && tryCount < TRANSPORT_ERROR_RETRY_COUNT)
						{
							retry = true; // eat exception and retry
							tryCount++;
							SqlConnection.ClearPool(conn);
						}
						else
						{
							ClearParmsFromCache(); // reset the parm list in the cache so that we'll re-retrieve the parms next time thru

							ForceRollback(conn);

							throw new DBAccessException(ex.Message, ex, this);
						}
					}
					finally
					{
						if (conn.State != ConnectionState.Closed)
						{
							conn.Close();
						}
					}

				}

			} while (retry);
		}
		#endregion Exec w/ overloads

		#region +LoanReader(BorrowReader, bool) method
		public object? LoanReader(BorrowReader del, bool primeReader)
		{
			SqlDataReader? reader = null;
			using (SqlConnection conn = _connectionManager.GetConnection() ?? throw new InvalidOperationException("Connection cannot be null."))
			{
				try
				{
					PrepareConnection(conn);
					_command.Connection = conn;
					_connectionManager.LogQuery(this);
					reader = _command.ExecuteReader();
					
					if (primeReader)
					{
						return reader.Read() ? del(reader, Array.Empty<object>()) : null;
					}
					return del(reader, Array.Empty<object>());
				}
				finally
				{
					if (reader != null)
					{
						reader.Close();
					}
					if (conn != null && conn.State != ConnectionState.Closed)
					{
						conn.Close();
					}
				}
			}
		}
		#endregion

		#region ~LoanReader(BorrowReader, bool, object[]) method
		protected object? LoanReader(BorrowReader del, bool primeReader, object[] args)
		{
			if (args == null) throw new ArgumentNullException(nameof(args));
			
			SqlDataReader? reader = null;
			using (SqlConnection conn = _connectionManager.GetConnection() ?? throw new InvalidOperationException("Connection cannot be null."))
			{
				try
				{
					PrepareConnection(conn);
					_command.Connection = conn;
					_connectionManager.LogQuery(this);
					reader = _command.ExecuteReader();
					
					if (primeReader)
					{
						return reader.Read() ? del(reader, args) : null;
					}
					return del(reader, args);
				}
				finally
				{
					if (reader != null)
					{
						reader.Close();
					}
					if (conn != null && conn.State != ConnectionState.Closed)
					{
						conn.Close();
					}
				}
			}
		}
		#endregion

		public override string ToString()
		{
			if (string.IsNullOrEmpty(ProcName))
			{
				return base.ToString() ?? string.Empty;
			}

			StringBuilder sb = new();
			sb.AppendFormat("exec {0}", ProcName);
			
			for (int i = 0; i < Parameters.Count; i++)
			{
				var param = Parameters[i];
				if (param.ParameterName?.Equals("@RETURN_VALUE", StringComparison.CurrentCultureIgnoreCase) == true)
					continue;

				sb.AppendFormat(" {0} = ", param.ParameterName ?? "@param" + i);
				
				if (param.Value == null || param.Value == DBNull.Value)
				{
					sb.Append("null");
				}
				else if (param.Value is string || param.Value is Guid)
				{
					sb.Append('\'').Append(param.Value).Append('\'');
				}
				else if (param.Value is bool boolVal)
				{
					sb.Append(boolVal ? "1" : "0");
				}
				else if (param.Value is DateTime)
				{
					sb.Append('\'').Append(param.Value).Append('\'');
				}
				else
				{
					sb.Append(param.Value.ToString() ?? "null");
				}

				if (i < Parameters.Count - 1)
				{
					sb.Append(',');
				}
			}

			return sb.ToString();
		}

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // Dispose managed resources
                    _command?.Dispose();
                    _connectionManager?.Dispose();
                }
                disposed = true;
            }
        }

        ~Proc()
        {
            Dispose(false);
        }        
	}

	#endregion Proc class
}
