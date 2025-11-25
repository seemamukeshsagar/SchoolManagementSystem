using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using SchoolPortal.DBAccess;
using SchoolPortal.Entities.Models;
using SchoolPortal.Services.IServices;

namespace SchoolPortal.Services
{
	public class UserDetailsService : IUserDetailsService
	{
		private readonly ILookupService _lookupService;
		private readonly IRoleMasterService _roleService;

		public UserDetailsService(ILookupService lookupService, IRoleMasterService roleService)
		{
			_lookupService = lookupService;
			_roleService = roleService;
		}

		public List<UserDetailsListViewModel> GetAll()
		{
			var list = new List<UserDetailsListViewModel>();
			Proc p = new Proc("UserDetails_GetAll");
			var dt = new DataTable();
			p.Exec(dt);

			// Get all lookup data first
			var designations = _lookupService.GetDesignations()?.ToDictionary(d => d.Id, d => d.Name) ?? new Dictionary<Guid, string>();
			var roles = _roleService.GetAll()?.ToDictionary(r => r.Id, r => r.Name) ?? new Dictionary<Guid, string>();
			var companies = _lookupService.GetCompanies()?.ToDictionary(c => c.Id, c => c.Name) ?? new Dictionary<Guid, string>();
			var schools = _lookupService.GetSchools()?.ToDictionary(s => s.Id, s => s.Name) ?? new Dictionary<Guid, string>();

			foreach (DataRow row in dt.Rows)
			{
				var user = new UserDetailsListViewModel
				{
					Id = row.Table.Columns.Contains("Id") && Guid.TryParse(row["Id"]?.ToString(), out var id) ? id : Guid.Empty,
					UserName = row.Table.Columns.Contains("UserName") ? row["UserName"]?.ToString() ?? string.Empty : string.Empty,
					FirstName = row.Table.Columns.Contains("FirstName") ? row["FirstName"]?.ToString() ?? string.Empty : string.Empty,
					LastName = row.Table.Columns.Contains("LastName") ? row["LastName"]?.ToString() ?? string.Empty : string.Empty,
					EmailAddress = row.Table.Columns.Contains("EmailAddress") ? row["EmailAddress"]?.ToString() ?? string.Empty : string.Empty,
					IsActive = row.Table.Columns.Contains("IsActive") && bool.TryParse(row["IsActive"]?.ToString(), out var isActive) && isActive,
					DesignationId = row.Table.Columns.Contains("DesignationId") && Guid.TryParse(row["DesignationId"]?.ToString(), out var desigId) ? desigId : Guid.Empty,
					UserRoleId = row.Table.Columns.Contains("UserRoleId") && Guid.TryParse(row["UserRoleId"]?.ToString(), out var roleId) ? roleId : (Guid?)null,
					CompanyId = row.Table.Columns.Contains("CompanyId") && Guid.TryParse(row["CompanyId"]?.ToString(), out var compId) ? compId : (Guid?)null,
					SchoolId = row.Table.Columns.Contains("SchoolId") && Guid.TryParse(row["SchoolId"]?.ToString(), out var schoolId) ? schoolId : (Guid?)null
				};

				// Set name fields from lookups
				if (user.UserRoleId.HasValue && roles.TryGetValue(user.UserRoleId.Value, out var roleName))
				{
					user.RoleName = roleName;
				}

				if (designations.TryGetValue(user.DesignationId, out var designationName))
				{
					user.DesignationName = designationName;
				}

				if (user.CompanyId.HasValue && companies.TryGetValue(user.CompanyId.Value, out var companyName))
				{
					user.CompanyName = companyName;
				}

				if (user.SchoolId.HasValue && schools.TryGetValue(user.SchoolId.Value, out var schoolName))
				{
					user.SchoolName = schoolName;
				}

				list.Add(user);
			}

			return list;
		}

		public UserDetails? GetById(Guid id)
		{
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