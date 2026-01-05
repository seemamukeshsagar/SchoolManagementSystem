// SchoolPortal.Services/Services/NonTeachingDocumentDetailsService.cs
using DocumentFormat.OpenXml.Office2010.Excel;
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
	public class NonTeachingDocumentDetailsService : INonTeachingDocumentDetailsService
	{
		private readonly ILogger<NonTeachingDocumentDetailsService> _logger;
		private readonly IDbConnection _connection;

		public NonTeachingDocumentDetailsService(ILogger<NonTeachingDocumentDetailsService> logger, IDbConnection connection)
		{
			_logger = logger;
			_connection = connection;
		}

		private IEnumerable<NonTeachingDocumentDetails> Map(DataRow row)
		{
			var documents = new List<NonTeachingDocumentDetails>();
	
			// Handle single row mapping
			if (row != null)
			{
				var document = new NonTeachingDocumentDetails
				{
					Id = row.Field<Guid>("Id"),
					NonTeachingId = row.Field<Guid>("NonTeachingId"),
					DocumentTypeId = row.Field<Guid>("DocumentTypeId"),
					DocumentType = row.Field<string>("DocumentType"),
					DocumentNumber = row.Field<string>("DocumentNumber"),
					DocumentPath = row.Field<string>("DocumentPath"),
					IssueDate = row.Field<DateTime?>("IssueDate"),
					ExpiryDate = row.Field<DateTime?>("ExpiryDate"),
					Remarks = row.Field<string>("Remarks"),
					IsActive = row.Field<bool>("IsActive"),
					IsVerified = row.Field<bool>("IsVerified"),
					VerifiedBy = row.Field<Guid>("VerifiedBy"),
					VerifiedOn = row.Field<DateTime?>("VerifiedOn"),
					CreatedBy = row.Field<Guid>("CreatedBy"),
					CreatedDate = row.Field<DateTime>("CreatedDate"),
					ModifiedBy = row.Field<Guid>("ModifiedBy"),
					ModifiedDate = row.Field<DateTime?>("ModifiedDate")
				};
				documents.Add(document);
			}
	
			return documents;
		}

		private IEnumerable<NonTeachingDocumentDetails> Map(IDataReader reader)
		{
			var documents = new List<NonTeachingDocumentDetails>();
	
			while (reader.Read())
			{
				var document = new NonTeachingDocumentDetails
				{
					Id = reader.GetGuid(reader.GetOrdinal("Id")),
					NonTeachingId = reader.GetGuid(reader.GetOrdinal("NonTeachingId")),
					DocumentTypeId = reader.GetGuid(reader.GetOrdinal("DocumentTypeId")),
					DocumentType = reader.IsDBNull(reader.GetOrdinal("DocumentType")) ? null : reader.GetString(reader.GetOrdinal("DocumentType")),
					DocumentNumber = reader.IsDBNull(reader.GetOrdinal("DocumentNumber")) ? null : reader.GetString(reader.GetOrdinal("DocumentNumber")),
					DocumentPath = reader.IsDBNull(reader.GetOrdinal("DocumentPath")) ? null : reader.GetString(reader.GetOrdinal("DocumentPath")),
					IssueDate = reader.IsDBNull(reader.GetOrdinal("IssueDate")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("IssueDate")),
					ExpiryDate = reader.IsDBNull(reader.GetOrdinal("ExpiryDate")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("ExpiryDate")),
					Remarks = reader.IsDBNull(reader.GetOrdinal("Remarks")) ? null : reader.GetString(reader.GetOrdinal("Remarks")),
					IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive")),
					IsVerified = reader.GetBoolean(reader.GetOrdinal("IsVerified")),
					VerifiedBy = reader.GetGuid(reader.GetOrdinal("VerifiedBy")),
					VerifiedOn = reader.IsDBNull(reader.GetOrdinal("VerifiedOn")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("VerifiedOn")),
					CreatedBy = reader.GetGuid(reader.GetOrdinal("CreatedBy")),
					CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
					ModifiedBy = reader.GetGuid(reader.GetOrdinal("ModifiedBy")),
					ModifiedDate = reader.IsDBNull(reader.GetOrdinal("ModifiedDate")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("ModifiedDate"))
				};
				documents.Add(document);
			}
	
			return documents;
		}
		public IEnumerable<NonTeachingDocumentDetails> GetByNonTeachingId(Guid nonTeachingId)
		{
			Proc p = new Proc("sp_NonTeachingDocument_GetByNonTeachingId");
			p["@Id"] = nonTeachingId;
			var dt = new DataTable();
			p.Exec(dt);
			if (dt.Rows.Count == 0) return Enumerable.Empty<NonTeachingDocumentDetails>();
			return Map(dt.Rows[0]);            
		}

		public NonTeachingDocumentDetails? GetDocumentById(Guid id)
		{
			try
			{
				using (var p = new Proc("sp_NonTeachingDocument_GetById"))
				{
					p["@Id"] = id;
					var dt = new DataTable();
					p.Exec(dt);
					if (dt.Rows.Count == 0) return null;
					return Map(dt.Rows[0]).FirstOrDefault();
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"Error in NonTeachingDocumentDetailsService.GetDocumentById for ID: {id}");
				throw;
			}
		}

		public bool Add(NonTeachingDocumentDetails entity)
		{
			try
			{
				using (var p = new Proc("sp_NonTeachingDocument_Insert"))
				{
					p["@Id"] = entity.Id;
					p["@NonTeachingId"] = entity.NonTeachingId;
					p["@DocumentTypeId"] = entity.DocumentTypeId;
					p["@DocumentType"] = entity.DocumentType ?? (object)DBNull.Value;
					p["@DocumentNumber"] = entity.DocumentNumber ?? (object)DBNull.Value;
					p["@DocumentPath"] = entity.DocumentPath ?? (object)DBNull.Value;
					p["@IssueDate"] = entity.IssueDate ?? (object)DBNull.Value;
					p["@ExpiryDate"] = entity.ExpiryDate ?? (object)DBNull.Value;
					p["@Remarks"] = entity.Remarks ?? (object)DBNull.Value;
					p["@IsActive"] = entity.IsActive;
					p["@IsVerified"] = entity.IsVerified;
					p["@VerifiedBy"] = entity.VerifiedBy;
					p["@VerifiedOn"] = entity.VerifiedOn ?? (object)DBNull.Value;
					p["@CreatedBy"] = entity.CreatedBy;
					p["@CreatedDate"] = entity.CreatedDate;
					p["@ModifiedBy"] = entity.ModifiedBy;
					p["@ModifiedDate"] = entity.ModifiedDate ?? (object)DBNull.Value;

					var dt = new DataTable();
					p.Exec(dt);
					return dt.Rows.Count > 0;
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"Error in NonTeachingDocumentDetailsService.Add for NonTeachingId: {entity.NonTeachingId}");
				throw;
			}
		}

		public bool Update(NonTeachingDocumentDetails entity)
		{
			try
			{
				using (var p = new Proc("sp_NonTeachingDocument_Update"))
				{
					p["@Id"] = entity.Id;
					p["@DocumentTypeId"] = entity.DocumentTypeId;
					p["@DocumentType"] = entity.DocumentType ?? (object)DBNull.Value;
					p["@DocumentNumber"] = entity.DocumentNumber ?? (object)DBNull.Value;
					p["@DocumentPath"] = entity.DocumentPath ?? (object)DBNull.Value;
					p["@IssueDate"] = entity.IssueDate ?? (object)DBNull.Value;
					p["@ExpiryDate"] = entity.ExpiryDate ?? (object)DBNull.Value;
					p["@Remarks"] = entity.Remarks ?? (object)DBNull.Value;
					p["@IsActive"] = entity.IsActive;
					p["@IsVerified"] = entity.IsVerified;
					p["@VerifiedBy"] = entity.VerifiedBy;
					p["@VerifiedOn"] = entity.VerifiedOn ?? (object)DBNull.Value;
					p["@ModifiedBy"] = entity.ModifiedBy;
					p["@ModifiedDate"] = entity.ModifiedDate ?? (object)DBNull.Value;

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
				_logger.LogError(ex, $"Error in NonTeachingDocumentDetailsService.Update for ID: {entity.Id}");
				throw;
			}
		}

		public bool Delete(Guid id)
		{
			try
			{
				using (var p = new Proc("sp_NonTeachingDocument_Delete"))
				{
					p["@Id"] = id;
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
				_logger.LogError(ex, $"Error in NonTeachingDocumentDetailsService.Delete for ID: {id}");
				throw;
			}
		}
	}
}