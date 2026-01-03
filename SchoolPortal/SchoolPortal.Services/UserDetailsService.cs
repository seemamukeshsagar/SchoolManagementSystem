using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using SchoolPortal.DBAccess;
using SchoolPortal.Entities.Models;
using SchoolPortal.Services.IServices;
using Microsoft.Extensions.Logging;

namespace SchoolPortal.Services
{
	public class UserDetailsService : IUserDetailsService
	{
		private readonly ILookupService _lookupService;
		private readonly IRoleMasterService _roleService;

		private readonly ILogger<UserDetailsService> _logger;

		public UserDetailsService(
			ILookupService lookupService, 
			IRoleMasterService roleService,
			ILogger<UserDetailsService> logger)
		{
			_lookupService = lookupService ?? throw new ArgumentNullException(nameof(lookupService));
			_roleService = roleService ?? throw new ArgumentNullException(nameof(roleService));
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		public List<UserDetailsListViewModel> GetAll()
		{
			var list = new List<UserDetailsListViewModel>();

			try
			{
				_logger.LogInformation("Starting to fetch all user details");

				// Load lookup dictionaries
				var designations = _lookupService.GetDesignations()?
					.ToDictionary(d => d.Id, d => d.Name ?? string.Empty) 
					?? new Dictionary<Guid, string>();

				var roles = _roleService.GetAll()?
					.Where(r => r != null && r.Name != null)
					.ToDictionary(r => r.Id, r => r.Name ?? string.Empty)
					?? new Dictionary<Guid, string>();

				var companies = _lookupService.GetCompanies()?
					.ToDictionary(c => c.Id, c => c.Name ?? string.Empty)
					?? new Dictionary<Guid, string>();

				var schools = _lookupService.GetSchools()?
					.ToDictionary(s => s.Id, s => s.Name ?? string.Empty)
					?? new Dictionary<Guid, string>();

				Proc p = new Proc("UserDetails_GetAll");
				var dt = new DataTable();
				p.Exec(dt);

				_logger.LogInformation($"Retrieved {dt.Rows.Count} users from database");

				// Log column names for debugging
				var columnNames = dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName);
				_logger.LogInformation($"Retrieved columns: {string.Join(", ", columnNames)}");

				foreach (DataRow row in dt.Rows)
				{
					try
					{
						var user = new UserDetailsListViewModel
						{
							Id = Guid.TryParse(row["Id"]?.ToString(), out var id) ? id : Guid.Empty,
							UserName = row["UserName"]?.ToString() ?? string.Empty,
							FirstName = row["FirstName"]?.ToString() ?? string.Empty,
							LastName = row["LastName"]?.ToString() ?? string.Empty,
							FullName = row["FullName"]?.ToString() ?? string.Empty,
							EmailAddress = row["EmailAddress"]?.ToString() ?? string.Empty,

							IsActive = bool.TryParse(row["IsActive"]?.ToString(), out var isActive) && isActive,
							DesignationName = row["DesignationName"]?.ToString() ?? string.Empty,
							RoleName = row["RoleName"]?.ToString() ?? string.Empty,
							
							// Address information
							//Address = row["Address"]?.ToString() ?? string.Empty,
							//City = row["City"]?.ToString() ?? string.Empty,
							//State = row["State"]?.ToString() ?? string.Empty
						};

						list.Add(user);
					}
					catch (Exception ex)
					{
						_logger.LogError(ex, $"Error processing user row: {ex.Message}");
					}
				}
				return list;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error in UserDetailsService.GetAll: " + ex.Message);
				throw;
			}
		}

		public UserDetails? GetById(Guid id)
		{
			try
			{
				if (id == Guid.Empty)
				{
					_logger?.LogWarning("Empty GUID provided to GetById");
					return null;
				}
				Proc p = new Proc("UserDetails_GetById");
				p["@Id"] = id;
				var dt = new DataTable();
				p.Exec(dt);
				if (dt.Rows.Count == 0) return null;
				
				var row = dt.Rows[0];
				return new UserDetails
				{
					Id = row.Table.Columns.Contains("Id") && Guid.TryParse(row["Id"]?.ToString(), out var idVal) ? idVal : Guid.Empty,
					UserName = row.Table.Columns.Contains("UserName") ? row["UserName"]?.ToString() : string.Empty,
					FirstName = row.Table.Columns.Contains("FirstName") ? row["FirstName"]?.ToString() : string.Empty,
					LastName = row.Table.Columns.Contains("LastName") ? row["LastName"]?.ToString() : string.Empty,
					EmailAddress = row.Table.Columns.Contains("EmailAddress") ? row["EmailAddress"]?.ToString() : string.Empty,
					IsActive = row.Table.Columns.Contains("IsActive") && bool.TryParse(row["IsActive"]?.ToString(), out var isActive) && isActive,
					DesignationId = row.Table.Columns.Contains("DesignationId") && Guid.TryParse(row["DesignationId"]?.ToString(), out var desigId) ? desigId : Guid.Empty,
					UserRoleId = row.Table.Columns.Contains("UserRoleId") && Guid.TryParse(row["UserRoleId"]?.ToString(), out var roleId) ? roleId : (Guid?)null,
					CompanyId = row.Table.Columns.Contains("CompanyId") && Guid.TryParse(row["CompanyId"]?.ToString(), out var compId) ? compId : (Guid?)null,
					SchoolId = row.Table.Columns.Contains("SchoolId") && Guid.TryParse(row["SchoolId"]?.ToString(), out var schoolId) ? schoolId : (Guid?)null
				};
			}
			catch (Exception ex)
			{
				_logger?.LogError(ex, "Error getting user details for ID: {UserId}", id);
				return null;
			}
		}

		public Guid Create(UserDetails entity)
		{
			Proc p = new Proc("UserDetails_Create");
			p["@UserName"] = entity.UserName ?? string.Empty;
			p["@UserPassword"] = entity.UserPassword ?? string.Empty;
			p["@FirstName"] = entity.FirstName ?? string.Empty;
			p["@LastName"] = entity.LastName ?? string.Empty;
			p["@EmailAddress"] = entity.EmailAddress ?? string.Empty;
			p["@DesignationId"] = entity.DesignationId;
			p["@UserRoleId"] = entity.UserRoleId ?? Guid.Empty;
			p["@IsSuperUser"] = entity.IsSuperUser ?? false;
			p["@CompanyId"] = entity.CompanyId ?? Guid.Empty;
			p["@SchoolId"] = entity.SchoolId ?? Guid.Empty;
			p["@IsActive"] = entity.IsActive;
			p["@CreatedBy"] = entity.CreatedBy;
			var dt = new DataTable();
			p.Exec(dt);
			if (dt.Rows.Count > 0)
			{
				var idObj = dt.Rows[0]["Id"];
				if (idObj != null && Guid.TryParse(idObj.ToString(), out var newId)) return newId;
			}
			return Guid.Empty;
		}

		public bool Update(UserDetails entity)
		{
			try
			{
				Proc p = new Proc("UserDetails_Update");
				p["@Id"] = entity.Id;
				p["@UserName"] = entity.UserName ?? string.Empty;
				p["@UserPassword"] = entity.UserPassword ?? string.Empty;
				p["@FirstName"] = entity.FirstName ?? string.Empty;
				p["@LastName"] = entity.LastName ?? string.Empty;
				p["@EmailAddress"] = entity.EmailAddress ?? string.Empty;
				p["@DesignationId"] = entity.DesignationId;
				p["@UserRoleId"] = entity.UserRoleId ?? Guid.Empty;
				p["@IsSuperUser"] = entity.IsSuperUser ?? false;
				p["@CompanyId"] = entity.CompanyId ?? Guid.Empty;
				p["@SchoolId"] = entity.SchoolId ?? Guid.Empty;
				p["@IsActive"] = entity.IsActive;
				p["@ModifiedBy"] = entity.ModifiedBy ?? Guid.Empty;
				p.Exec();
				var ret = p.Parameters["@RETURN_VALUE"].Value;
				int code = ret == null || ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
				return code == 1;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public bool Delete(Guid id)
		{
			Proc p = new Proc("UserDetails_Delete");
			p["@Id"] = id;
			p.Exec();
			var ret = p.Parameters["@RETURN_VALUE"].Value;
			int code = ret == null || ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
			return code == 1;
		}
	}
}