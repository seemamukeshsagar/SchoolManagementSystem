using System;
using System.Collections.Generic;
using System.Data;
using SchoolPortal.DBAccess;
using SchoolPortal.Entities.Models;
using SchoolPortal.Services.IServices;

namespace SchoolPortal.Services
{
	public class ProfessionMasterService : IProfessionMasterService
	{
		private ProfessionMaster Map(DataRow row)
		{
			if (row == null) return new ProfessionMaster();

			var entity = new ProfessionMaster();
			if (row.Table.Columns.Contains("Id") && Guid.TryParse(row["Id"].ToString(), out var id)) entity.Id = id;
			entity.Name = row.Table.Columns.Contains("Name") ? row["Name"].ToString() ?? string.Empty : string.Empty;
			if (row.Table.Columns.Contains("CompanyId") && Guid.TryParse(row["CompanyId"].ToString(), out var companyId)) entity.CompanyId = companyId;
			if (row.Table.Columns.Contains("SchoolId") && Guid.TryParse(row["SchoolId"].ToString(), out var schoolId)) entity.SchoolId = schoolId;
			if (row.Table.Columns.Contains("IsActive") && bool.TryParse(row["IsActive"].ToString(), out var isActive)) entity.IsActive = isActive;
			if (row.Table.Columns.Contains("IsDeleted") && bool.TryParse(row["IsDeleted"].ToString(), out var isDeleted)) entity.IsDeleted = isDeleted;
			if (row.Table.Columns.Contains("CreatedBy") && Guid.TryParse(row["CreatedBy"].ToString(), out var createdBy)) entity.CreatedBy = createdBy;
			if (row.Table.Columns.Contains("CreatedDate") && DateTime.TryParse(row["CreatedDate"].ToString(), out var createdDate)) entity.CreatedDate = createdDate;
			if (row.Table.Columns.Contains("ModifiedBy") && Guid.TryParse(row["ModifiedBy"].ToString(), out var modifiedBy)) entity.ModifiedBy = modifiedBy;
			if (row.Table.Columns.Contains("ModifiedDate") && DateTime.TryParse(row["ModifiedDate"].ToString(), out var modifiedDate)) entity.ModifiedDate = modifiedDate;
			entity.Status = row.Table.Columns.Contains("Status") ? row["Status"].ToString() ?? string.Empty : string.Empty;
			entity.StatusMessage = row.Table.Columns.Contains("StatusMessage") ? row["StatusMessage"].ToString() ?? string.Empty : string.Empty;
			return entity;
		}

		public List<ProfessionMaster> GetAll()
		{
			Proc p = new Proc("ProfessionMaster_GetAll");
			var dt = new DataTable();
			p.Exec(dt);
			var list = new List<ProfessionMaster>();
			foreach (DataRow r in dt.Rows)
			{
				list.Add(Map(r));
			}
			return list;
		}

		public ProfessionMaster? GetById(Guid id)
		{
			Proc p = new Proc("ProfessionMaster_GetById");
			p["@Id"] = id;
			var dt = new DataTable();
			p.Exec(dt);
			if (dt.Rows.Count == 0) return null;
			return Map(dt.Rows[0]);
		}

		public Guid Create(ProfessionMaster profession)
		{
			Proc p = new Proc("ProfessionMaster_Create");
			p["@Name"] = profession.Name;
			p["@CompanyId"] = profession.CompanyId;
			p["@SchoolId"] = profession.SchoolId;
			p["@IsActive"] = profession.IsActive;
			p["@CreatedBy"] = profession.CreatedBy;
			var dt = new DataTable();
			p.Exec(dt);
			if (dt.Rows.Count > 0)
			{
				var idObj = dt.Rows[0]["Id"];
				if (idObj != null && Guid.TryParse(idObj.ToString(), out var newId))
				{
					return newId;
				}
			}
			return Guid.Empty;
		}

		public bool Update(ProfessionMaster profession)
		{
			Proc p = new Proc("ProfessionMaster_Update");
			p["@Id"] = profession.Id;
			p["@Name"] = profession.Name;
			p["@SchoolId"] = profession.SchoolId;
			p["@IsActive"] = profession.IsActive;
			p["@ModifiedBy"] = profession.ModifiedBy ?? Guid.Empty;
			p.Exec();
			var ret = p.Parameters["@RETURN_VALUE"].Value;
			int code = ret == null || ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
			return code == 1;
		}

		public bool Delete(Guid id)
		{
			Proc p = new Proc("ProfessionMaster_Delete");
			p["@Id"] = id;
			p.Exec();
			var ret = p.Parameters["@RETURN_VALUE"].Value;
			int code = ret == null || ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
			return code == 1;
		}
	}
}
