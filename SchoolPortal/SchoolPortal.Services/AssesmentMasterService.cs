using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Extensions.Logging;
using SchoolPortal.DBAccess;
using SchoolPortal.Entities.Models;
using SchoolPortal.Services.IServices;

namespace SchoolPortal.Services
{
	public class AssesmentMasterService : IAssesmentMasterService
	{
		private readonly ILogger<AssesmentMasterService> _logger;

		public AssesmentMasterService(ILogger<AssesmentMasterService> logger)
		{
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}
		private AssesmentMaster Map(DataRow? row)
		{
			var entity = new AssesmentMaster();
			
			if (row == null || row.Table == null) 
				return entity;
				
			try
			{
				// Handle Id
				if (row.Table.Columns.Contains("Id") && row["Id"] != DBNull.Value)
				{
					if (Guid.TryParse(row["Id"]?.ToString(), out var id))
					{
						entity.Id = id;
					}
				}
				
				// String properties with null checks
				entity.Name = row.Table.Columns.Contains("Name") && row["Name"] != DBNull.Value 
					? row["Name"]?.ToString() ?? string.Empty 
					: string.Empty;
					
				entity.Description = row.Table.Columns.Contains("Description") && row["Description"] != DBNull.Value
					? row["Description"]?.ToString() ?? string.Empty 
					: string.Empty;
					
				// Decimal properties
				if (row.Table.Columns.Contains("PercentageWeightage") && 
				    row["PercentageWeightage"] != DBNull.Value &&
				    decimal.TryParse(row["PercentageWeightage"]?.ToString(), out var weightage))
				{
					entity.PercentageWeightage = weightage;
				}
				
				// DateTime properties
				if (row.Table.Columns.Contains("FromPeriod") && 
				    row["FromPeriod"] != DBNull.Value &&
				    DateTime.TryParse(row["FromPeriod"]?.ToString(), out var fromPeriod))
				{
					entity.FromPeriod = fromPeriod;
				}
				
				if (row.Table.Columns.Contains("ToPeriod") && 
				    row["ToPeriod"] != DBNull.Value &&
				    DateTime.TryParse(row["ToPeriod"]?.ToString(), out var toPeriod))
				{
					entity.ToPeriod = toPeriod;
				}
				
				// Guid properties
				if (row.Table.Columns.Contains("CompanyId") && 
				    row["CompanyId"] != DBNull.Value &&
				    Guid.TryParse(row["CompanyId"]?.ToString(), out var companyId))
				{
					entity.CompanyId = companyId;
				}
				
				if (row.Table.Columns.Contains("SchoolId") && 
				    row["SchoolId"] != DBNull.Value &&
				    Guid.TryParse(row["SchoolId"]?.ToString(), out var schoolId))
				{
					entity.SchoolId = schoolId;
				}
				
				// Boolean properties
				if (row.Table.Columns.Contains("IsActive") && 
				    row["IsActive"] != DBNull.Value &&
				    bool.TryParse(row["IsActive"]?.ToString(), out var isActive))
				{
					entity.IsActive = isActive;
				}
				
				if (row.Table.Columns.Contains("IsDeleted") && 
				    row["IsDeleted"] != DBNull.Value &&
				    bool.TryParse(row["IsDeleted"]?.ToString(), out var isDeleted))
				{
					entity.IsDeleted = isDeleted;
				}
				
				// Created/Modified properties
				if (row.Table.Columns.Contains("CreatedBy") && 
				    row["CreatedBy"] != DBNull.Value &&
				    Guid.TryParse(row["CreatedBy"]?.ToString(), out var createdBy))
				{
					entity.CreatedBy = createdBy;
				}
				
				if (row.Table.Columns.Contains("CreatedDate") && 
				    row["CreatedDate"] != DBNull.Value &&
				    DateTime.TryParse(row["CreatedDate"]?.ToString(), out var createdDate))
				{
					entity.CreatedDate = createdDate;
				}
				
				if (row.Table.Columns.Contains("ModifiedBy") && 
				    row["ModifiedBy"] != DBNull.Value &&
				    Guid.TryParse(row["ModifiedBy"]?.ToString(), out var modifiedBy))
				{
					entity.ModifiedBy = modifiedBy;
				}
				
				if (row.Table.Columns.Contains("ModifiedDate") && 
				    row["ModifiedDate"] != DBNull.Value &&
				    DateTime.TryParse(row["ModifiedDate"]?.ToString(), out var modifiedDate))
				{
					entity.ModifiedDate = modifiedDate;
				}
				
				// Status fields
				entity.Status = row.Table.Columns.Contains("Status") && row["Status"] != DBNull.Value
					? row["Status"]?.ToString() ?? string.Empty 
					: string.Empty;
					
				entity.StatusMessage = row.Table.Columns.Contains("StatusMessage") && row["StatusMessage"] != DBNull.Value
					? row["StatusMessage"]?.ToString() ?? string.Empty 
					: string.Empty;
			}
			catch (Exception ex)
			{
				// Log error if logger is available
				_logger?.LogError(ex, "Error mapping AssesmentMaster data");
			}
			
			return entity;
		}

		public List<AssesmentMaster> GetAll()
		{
			var list = new List<AssesmentMaster>();
			try
			{
				using (var p = new Proc("AssesmentMaster_GetAll"))
				{
					var dt = new DataTable();
					p.Exec(dt);
					
					if (dt.Rows == null) return list;
					
					foreach (DataRow r in dt.Rows)
					{
						if (r == null) continue;
						
						var item = Map(r);
						if (item != null)
						{
							list.Add(item);
						}
					}
				}
			}
			catch (Exception ex)
			{
				_logger?.LogError(ex, "Error in GetAll while fetching assessment masters");
			}
			
			return list;
		}

		public AssesmentMaster? GetById(Guid id)
		{
			try
			{
				using (var p = new Proc("AssesmentMaster_GetById"))
				{
					p["@Id"] = id;
					var dt = new DataTable();
					p.Exec(dt);
					
					if (dt.Rows.Count == 0 || dt.Rows[0] == null) 
						return null;
						
					return Map(dt.Rows[0]);
				}
			}
			catch (Exception ex)
			{
				_logger?.LogError(ex, "Error in GetById for assessment master ID: {Id}", id);
				return null;
			}
		}

		public Guid Create(AssesmentMaster assesment)
		{
			if (assesment == null)
			{
				throw new ArgumentNullException(nameof(assesment));
			}

			try
			{
				using (var p = new Proc("AssesmentMaster_Create"))
				{
					if (string.IsNullOrEmpty(assesment.Name))
					{
						throw new ArgumentException("Assessment name cannot be null or empty", nameof(assesment.Name));
					}

					p["@Name"] = assesment.Name;
					p["@Description"] = assesment.Description ?? string.Empty;
					p["@PercentageWeightage"] = assesment.PercentageWeightage ?? 0m;
					p["@FromPeriod"] = assesment.FromPeriod.HasValue ? (object)assesment.FromPeriod.Value : DBNull.Value;
					p["@ToPeriod"] = assesment.ToPeriod.HasValue ? (object)assesment.ToPeriod.Value : DBNull.Value;
					p["@CompanyId"] = assesment.CompanyId;
					p["@SchoolId"] = assesment.SchoolId;
					p["@IsActive"] = assesment.IsActive;
					p["@CreatedBy"] = assesment.CreatedBy;

					var dt = new DataTable();
					p.Exec(dt);

					if (dt.Rows.Count > 0 && dt.Rows[0]["Id"] != DBNull.Value)
					{
						if (Guid.TryParse(dt.Rows[0]["Id"]?.ToString(), out var newId))
						{
							return newId;
						}
					}

					// _logger?.LogWarning("Failed to create assessment master");
					return Guid.Empty;
				}
			}
			catch (Exception ex) when (ex is ArgumentException || ex is ArgumentNullException)
			{
				throw;
			}
			catch (Exception ex)
			{
				_logger?.LogError(ex, "Error creating assessment master");
				return Guid.Empty;
			}
		}

		public bool Update(AssesmentMaster assesment)
		{
			if (assesment == null)
			{
				throw new ArgumentNullException(nameof(assesment));
			}

			try
			{
				using (var p = new Proc("AssesmentMaster_Update"))
				{
					if (string.IsNullOrEmpty(assesment.Name))
					{
						throw new ArgumentException("Assessment name cannot be null or empty", nameof(assesment.Name));
					}

					p["@Id"] = assesment.Id;
					p["@Name"] = assesment.Name;
					p["@Description"] = assesment.Description ?? string.Empty;
					p["@PercentageWeightage"] = assesment.PercentageWeightage ?? 0m;
					p["@FromPeriod"] = assesment.FromPeriod.HasValue ? (object)assesment.FromPeriod.Value : DBNull.Value;
					p["@ToPeriod"] = assesment.ToPeriod.HasValue ? (object)assesment.ToPeriod.Value : DBNull.Value;
					p["@SchoolId"] = assesment.SchoolId;
					p["@IsActive"] = assesment.IsActive;
					p["@ModifiedBy"] = assesment.ModifiedBy ?? Guid.Empty;

					p.Exec();
					
					var ret = p.Parameters["@RETURN_VALUE"]?.Value;
					int code = ret == null || ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
					return code == 1;
				}
			}
			catch (Exception ex) when (ex is ArgumentException || ex is ArgumentNullException)
			{
				throw;
			}
			catch (Exception ex)
			{
				_logger?.LogError(ex, "Error updating assessment master with ID: {Id}", assesment?.Id);
				return false;
			}
		}

		public bool Delete(Guid id)
		{
			if (id == Guid.Empty)
			{
				throw new ArgumentException("ID cannot be empty", nameof(id));
			}

			try
			{
				using (var p = new Proc("AssesmentMaster_Delete"))
				{
					p["@Id"] = id;
					p.Exec();
					
					var ret = p.Parameters["@RETURN_VALUE"]?.Value;
					int code = ret == null || ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
					return code == 1;
				}
			}
			catch (Exception ex)
			{
				_logger?.LogError(ex, "Error deleting assessment master with ID: {Id}", id);
				return false;
			}
		}
	}
}
