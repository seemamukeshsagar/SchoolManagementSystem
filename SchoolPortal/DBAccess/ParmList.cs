using System;
using System.Collections;
using System.Collections.Specialized;
using Microsoft.Data.SqlClient;
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
            if (originalParms == null)
                throw new ArgumentNullException(nameof(originalParms));

            //bja:tbd2:ListDictionary (singly linked list) bugs me a bit, Hashtable seems a nano faster
            //but Hashtable didn't maintain the order of the parms and AS/400 is sensitive to parm order
            _parmList = new ListDictionary(/*originalParms.Count*/);
            
            foreach (IDataParameter? p in originalParms)
            {
                if (p == null)
                    continue;
                    
                //bja:tbd:fix blob type parms to their appropriate data type
                if (p is SqlParameter sqlParam)
                {
                    if (p.DbType == DbType.Xml && sqlParam.Size == 0)
                    {
                        sqlParam.Size = int.MaxValue;
                    }
                }

                if (p.ParameterName == null)
                    throw new InvalidOperationException("Parameter name cannot be null");

                var cloneable = p as ICloneable ?? 
                    throw new InvalidOperationException($"Parameter {p.ParameterName} does not implement ICloneable");
                    
                _parmList.Add(p.ParameterName, cloneable.Clone());
            }
        }

        public void CopyToCommand(IDbCommand com)
        {
            if (com == null)
                throw new ArgumentNullException(nameof(com));
                
            foreach (DictionaryEntry p in _parmList)
            {
                if (p.Value is ICloneable cloneable)
                {
                    var clonedValue = cloneable.Clone();
                    if (clonedValue != null)
                    {
                        com.Parameters.Add(clonedValue);
                    }
                }
            }
        }
    }
    #endregion ParmList
}
