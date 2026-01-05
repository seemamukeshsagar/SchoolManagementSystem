using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using SchoolPortal.DBAccess;
using SchoolPortal.Entities.Models;
using SchoolPortal.Services.IServices;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace SchoolPortal.Services.Services
{
	public class NonTeachingQualificationDetailsService : INonTeachingQualificationDetailsService
	{
		private readonly ILogger<NonTeachingQualificationDetailsService> _logger;
		private readonly IDbConnection _connection;

		public NonTeachingQualificationDetailsService(ILogger<NonTeachingQualificationDetailsService> logger, IDbConnection connection)
		{
			_logger = logger;
			_connection = connection;
		}

		public IEnumerable<NonTeachingQualificationDetails> GetByNonTeachingId(Guid nonTeachingId)
		{
			try
			{
				using (var p = new Proc("sp_NonTeachingQualification_GetByNonTeachingId"))
				{
					p["@NonTeachingId"] = nonTeachingId;
					var dt = new DataTable();
					p.Exec(dt);
					if (dt.Rows.Count == 0) return Enumerable.Empty<NonTeachingQualificationDetails>();
					return (IEnumerable<NonTeachingQualificationDetails>)Map((IDataReader)dt);
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"Error in NonTeachingQualificationDetailsService.GetByNonTeachingId for ID: {nonTeachingId}");
				throw;
			}
		}

		public NonTeachingQualificationDetails? GetQualificationById(Guid id)
		{  
			Proc p = new Proc("sp_NonTeachingQualification_GetById");
			p["@Id"] = id;
			var dt = new DataTable();
			p.Exec(dt);
			if (dt.Rows.Count == 0) return null;
			return Map(dt.Rows[0]);
		}

		public bool Add(NonTeachingQualificationDetails entity)
		{
			try
			{
				using (var p = new Proc("sp_NonTeachingQualification_Insert"))
				{
					p["@Id"] = entity.Id;
					p["@NonTeachingId"] = entity.NonTeachingId;
					p["@QualificationTypeId"] = entity.QualificationTypeId;
					p["@Institution"] = entity.Institution ?? string.Empty;
					p["@BoardUniversity"] = entity.BoardUniversity ?? string.Empty;
					p["@YearOfPassing"] = entity.YearOfPassing ?? string.Empty;
					p["@Percentage"] = entity.Percentage;
					p["@Division"] = entity.Division ?? string.Empty;
					p["@DocumentPath"] = entity.DocumentPath ?? string.Empty;
					p["@IsVerified"] = entity.IsVerified;
					p["@VerifiedBy"] = entity.VerifiedBy ?? (object)DBNull.Value;
					p["@VerifiedOn"] = entity.VerifiedOn ?? (object)DBNull.Value;
					p["@Remarks"] = entity.Remarks ?? string.Empty;
					p["@CreatedBy"] = entity.CreatedBy;

					var dt = new DataTable();
					p.Exec(dt);
					return dt.Rows.Count > 0;
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error in NonTeachingQualificationDetailsService.Add");
				throw;
			}
		}

		public bool Update(NonTeachingQualificationDetails entity)
		{
			try
			{
				using (var p = new Proc("sp_NonTeachingQualification_Update"))
				{
					p["@Id"] = entity.Id;
					p["@QualificationTypeId"] = entity.QualificationTypeId;
					p["@Institution"] = entity.Institution ?? string.Empty;
					p["@BoardUniversity"] = entity.BoardUniversity ?? string.Empty;
					p["@YearOfPassing"] = entity.YearOfPassing ?? string.Empty;
					p["@Percentage"] = entity.Percentage;
					p["@Division"] = entity.Division ?? string.Empty;
					p["@DocumentPath"] = entity.DocumentPath ?? string.Empty;
					p["@IsVerified"] = entity.IsVerified;
					p["@VerifiedBy"] = entity.VerifiedBy ?? (object)DBNull.Value;
					p["@VerifiedOn"] = entity.VerifiedOn ?? (object)DBNull.Value;
					p["@Remarks"] = entity.Remarks ?? string.Empty;
					p["@ModifiedBy"] = entity.ModifiedBy ?? (object)DBNull.Value;
					var dt = new DataTable();
					p.Exec(dt);
					if (dt.Rows.Count > 0)
					{
						return true;
					}
					else
					{
						return false;
					}
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"Error in NonTeachingQualificationDetailsService.Update for ID: {entity.Id}");
				throw;
			}
		}

		public bool Delete(Guid id)
		{
			Proc p = new Proc("sp_NonTeachingQualification_Delete");
			p["@Id"] = id;
			p.Exec();
			var ret = p.Parameters["@RETURN_VALUE"].Value;
			int code = ret == null || ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
			return code == 1;
		}

		private T GetColumnValue<T>(IDataReader reader, string columnName, T defaultValue = default!)
		{
			try 
			{
				int ordinal = reader.GetOrdinal(columnName);
				if (!reader.IsDBNull(ordinal))
				{
					object value = reader.GetValue(ordinal);
					if (value != null)
					{
						return (T)Convert.ChangeType(value, Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T));
					}
				}
			}
			catch (IndexOutOfRangeException)
			{
				// Column doesn't exist, return default value
				_logger?.LogWarning($"Column '{columnName}' not found in the result set");
			}
			catch (Exception ex)
			{
				_logger?.LogError(ex, $"Error reading column '{columnName}'");
			}
			return defaultValue;
		}

		private NonTeachingQualificationDetails Map(IDataReader reader)
		{
			if (reader == null) return new NonTeachingQualificationDetails();

			return new NonTeachingQualificationDetails
			{
				Id = reader["Id"] != DBNull.Value ? (Guid)reader["Id"] : Guid.Empty,
				NonTeachingId = reader["NonTeachingId"] != DBNull.Value ? (Guid)reader["NonTeachingId"] : Guid.Empty,
				QualificationTypeId = reader["QualificationTypeId"] != DBNull.Value ? (Guid)reader["QualificationTypeId"] : Guid.Empty,
				QualificationType = GetColumnValue<string>(reader, "QualificationType") ?? string.Empty,
				Institution = reader["Institution"]?.ToString() ?? string.Empty,
				BoardUniversity = reader["BoardUniversity"]?.ToString() ?? string.Empty,
				YearOfPassing = reader["YearOfPassing"]?.ToString() ?? string.Empty,
				Percentage = reader["Percentage"] != DBNull.Value ? Convert.ToDecimal(reader["Percentage"]) : 0m,
				Division = reader["Division"]?.ToString() ?? string.Empty,
				DocumentPath = reader["DocumentPath"]?.ToString() ?? string.Empty,
				IsActive = GetColumnValue(reader, "IsActive", true),
				IsVerified = reader["IsVerified"] != DBNull.Value && Convert.ToBoolean(reader["IsVerified"]),
				VerifiedBy = reader["VerifiedBy"] != DBNull.Value ? (Guid)reader["VerifiedBy"] : Guid.Empty,
				VerifiedOn = reader["VerifiedOn"] != DBNull.Value ? (DateTime)reader["VerifiedOn"] : DateTime.MinValue,
				Remarks = reader["Remarks"]?.ToString() ?? string.Empty,
				CreatedBy = reader["CreatedBy"] != DBNull.Value ? (Guid)reader["CreatedBy"] : Guid.Empty,
				CreatedDate = reader["CreatedDate"] != DBNull.Value ? Convert.ToDateTime(reader["CreatedDate"]) : DateTime.UtcNow,
				ModifiedBy = GetColumnValue<Guid?>(reader, "ModifiedBy"),
				ModifiedDate = GetColumnValue<DateTime?>(reader, "ModifiedDate")
			};
		}

		private NonTeachingQualificationDetails? Map(DataRow row)
		{
			if (row == null) return null;
			if (row.Table == null) return new NonTeachingQualificationDetails();

			return new NonTeachingQualificationDetails
			{
				Id = row["Id"] != DBNull.Value ? (Guid)row["Id"] : Guid.Empty,
				NonTeachingId = row["NonTeachingId"] != DBNull.Value ? (Guid)row["NonTeachingId"] : Guid.Empty,
				QualificationTypeId = row["QualificationTypeId"] != DBNull.Value ? (Guid)row["QualificationTypeId"] : Guid.Empty,
				QualificationType = row.Table.Columns.Contains("QualificationType") && row["QualificationType"] != DBNull.Value 
					? row["QualificationType"]?.ToString() ?? string.Empty 
					: string.Empty,
				Institution = row.Table.Columns.Contains("Institution") && row["Institution"] != DBNull.Value 
					? row["Institution"]?.ToString() 
					: null,
				BoardUniversity = row.Table.Columns.Contains("BoardUniversity") && row["BoardUniversity"] != DBNull.Value 
					? row["BoardUniversity"]?.ToString() 
					: null,
				YearOfPassing = row.Table.Columns.Contains("YearOfPassing") && row["YearOfPassing"] != DBNull.Value 
					? row["YearOfPassing"]?.ToString() 
					: null,
				Percentage = row.Table.Columns.Contains("Percentage") && row["Percentage"] != DBNull.Value 
					? Convert.ToDecimal(row["Percentage"]) 
					: 0m,
				Division = row.Table.Columns.Contains("Division") && row["Division"] != DBNull.Value 
					? row["Division"]?.ToString() 
					: null,
				DocumentPath = row.Table.Columns.Contains("DocumentPath") && row["DocumentPath"] != DBNull.Value 
					? row["DocumentPath"]?.ToString() 
					: null,
				IsActive = row.Table.Columns.Contains("IsActive") && row["IsActive"] != DBNull.Value 
					&& Convert.ToBoolean(row["IsActive"]),
				IsVerified = row.Table.Columns.Contains("IsVerified") && row["IsVerified"] != DBNull.Value 
					&& Convert.ToBoolean(row["IsVerified"]),
				VerifiedBy = row.Table.Columns.Contains("VerifiedBy") && row["VerifiedBy"] != DBNull.Value 
					? (Guid)row["VerifiedBy"] 
					: Guid.Empty,
				VerifiedOn = row.Table.Columns.Contains("VerifiedOn") && row["VerifiedOn"] != DBNull.Value 
					? Convert.ToDateTime(row["VerifiedOn"]) 
					: (DateTime?)null,
				Remarks = row.Table.Columns.Contains("Remarks") && row["Remarks"] != DBNull.Value
					? row["Remarks"]?.ToString() ?? string.Empty
					: string.Empty,
				CreatedBy = row.Table.Columns.Contains("CreatedBy") && row["CreatedBy"] != DBNull.Value 
					? (Guid)row["CreatedBy"] 
					: Guid.Empty,
				CreatedDate = row.Table.Columns.Contains("CreatedDate") && row["CreatedDate"] != DBNull.Value 
					? Convert.ToDateTime(row["CreatedDate"]) 
					: DateTime.UtcNow,
				ModifiedBy = row.Table.Columns.Contains("ModifiedBy") && row["ModifiedBy"] != DBNull.Value 
					? (Guid)row["ModifiedBy"] 
					: Guid.Empty,
				ModifiedDate = row.Table.Columns.Contains("ModifiedDate") && row["ModifiedDate"] != DBNull.Value 
					? (DateTime?)Convert.ToDateTime(row["ModifiedDate"]) 
					: null
			};
		}
	}
}