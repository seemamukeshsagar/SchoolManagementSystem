using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data.SqlClient;
using System.Data;

namespace SchoolPortal.DBAccess
{
	#region ParmList
	/// <summary>
	/// this code taken verbatim from the 1.1 version of the Lesco.DBAccess module
	/// </summary>
	public class ParmList
	{
		private readonly ListDictionary _parmList;

		public ParmList(IDataParameterCollection originalParms)
		{
			//bja:tbd2:ListDictionary (singly linked list) bugs me a bit, Hashtable seems a nano faster
			//but Hashtable didn't maintain the order of the parms and AS/400 is sensitive to parm order
			_parmList = new ListDictionary(/*originalParms.Count*/);
			foreach (IDataParameter p in originalParms)
			{
				//bja:tbd:fix blob type parms to their appropriate data type

				if (p is SqlParameter)
				{
					if ((p.DbType == DbType.Xml) && ((SqlParameter)p).Size == 0)
					{
						((SqlParameter)p).Size = int.MaxValue;
					}
				}

				_parmList.Add(p.ParameterName, (p as ICloneable).Clone());
			}
		}

		public void CopyToCommand(IDbCommand com)
		{
			foreach (DictionaryEntry p in _parmList)
			{
				com.Parameters.Add((p.Value as ICloneable).Clone());
			}
		}
	}
	#endregion ParmList
}
