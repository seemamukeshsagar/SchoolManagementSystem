using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolPortal.Services.Common
{
    public static class DataRowExtensions
    {
        public static string? GetString(this DataRow row, string col) =>
            row.Table.Columns.Contains(col) && row[col] != DBNull.Value
                ? row[col].ToString()
                : null;

        public static Guid GetGuid(this DataRow row, string col) =>
            row.Table.Columns.Contains(col) && row[col] != DBNull.Value
                ? Guid.Parse(row[col].ToString())
                : Guid.Empty;

        public static Guid? GetNullableGuid(this DataRow row, string col)
        {
            return row.Table.Columns.Contains(col) && row[col] != DBNull.Value
                ? Guid.Parse(row[col].ToString())
                : (Guid?)null;
        }

        public static DateTime? GetDateTime(this DataRow row, string col) =>
            row.Table.Columns.Contains(col) && row[col] != DBNull.Value
                ? Convert.ToDateTime(row[col])
                : (DateTime?)null;

        public static decimal? GetDecimal(this DataRow row, string col) =>
            row.Table.Columns.Contains(col) && row[col] != DBNull.Value
                ? Convert.ToDecimal(row[col])
                : (decimal?)null;

        public static bool GetBool(this DataRow row, string col) =>
            row.Table.Columns.Contains(col) && row[col] != DBNull.Value
                ? Convert.ToBoolean(row[col])
                : false;

        public static byte[]? GetBytes(this DataRow row, string col) =>
            row.Table.Columns.Contains(col) && row[col] != DBNull.Value
                ? (byte[])row[col]
                : null;
    }
}
