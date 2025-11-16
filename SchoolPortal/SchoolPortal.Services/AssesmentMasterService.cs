using System;
using System.Collections.Generic;
using System.Data;
using SchoolPortal.DBAccess;
using SchoolPortal.Entities.Models;
using SchoolPortal.Services.IServices;

namespace SchoolPortal.Services
{
	public class AssesmentMasterService : IAssesmentMasterService
	{
		private AssesmentMaster Map(DataRow row)
		{
			if (row == null) return new AssesmentMaster();

			var entity = new AssesmentMaster();
			if (row.Table.Columns.Contains("Id") && Guid.TryParse(row["Id"].ToString(), out var id)) entity.Id = id;
			entity.Name = row.Table.Columns.Contains("Name") ? row["Name"].ToString() ?? string.Empty : string.Empty;
			entity.Description = row.Table.Columns.Contains("Description") ? row["Description"].ToString() ?? string.Empty : string.Empty;
			if (row.Table.Columns.Contains("PercentageWeightage") && decimal.TryParse(row["PercentageWeightage"].ToString(), out var weightage)) entity.PercentageWeightage = weightage;
			if (row.Table.Columns.Contains("FromPeriod") && DateTime.TryParse(row["FromPeriod"].ToString(), out var fromPeriod)) entity.FromPeriod = fromPeriod;
			if (row.Table.Columns.Contains("ToPeriod") && DateTime.TryParse(row["ToPeriod"].ToString(), out var toPeriod)) entity.ToPeriod = toPeriod;
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

		public List<AssesmentMaster> GetAll()
		{
			Proc p = new Proc("AssesmentMaster_GetAll");
			var dt = new DataTable();
			p.Exec(dt);
			var list = new List<AssesmentMaster>();
			foreach (DataRow r in dt.Rows)
			{
				list.Add(Map(r));
			}
			return list;
		}

		public AssesmentMaster? GetById(Guid id)
		{
			Proc p = new Proc("AssesmentMaster_GetById");
			p["@Id"] = id;
			var dt = new DataTable();
			p.Exec(dt);
			if (dt.Rows.Count == 0) return null;
			return Map(dt.Rows[0]);
		}

		public Guid Create(AssesmentMaster assesment)
		{
			Proc p = new Proc("AssesmentMaster_Create");
			p["@Name"] = assesment.Name;
			p["@Description"] = assesment.Description ?? string.Empty;
			p["@PercentageWeightage"] = assesment.PercentageWeightage ?? 0m;
			p["@FromPeriod"] = assesment.FromPeriod;
			p["@ToPeriod"] = assesment.ToPeriod;
			p["@CompanyId"] = assesment.CompanyId;
			p["@SchoolId"] = assesment.SchoolId;
			p["@IsActive"] = assesment.IsActive;
			p["@CreatedBy"] = assesment.CreatedBy;
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

		public bool Update(AssesmentMaster assesment)
		{
			Proc p = new Proc("AssesmentMaster_Update");
			p["@Id"] = assesment.Id;
			p["@Name"] = assesment.Name;
			p["@Description"] = assesment.Description ?? string.Empty;
			p["@PercentageWeightage"] = assesment.PercentageWeightage ?? 0m;
			p["@FromPeriod"] = assesment.FromPeriod;
			p["@ToPeriod"] = assesment.ToPeriod;
			p["@SchoolId"] = assesment.SchoolId;
			p["@IsActive"] = assesment.IsActive;
			p["@ModifiedBy"] = assesment.ModifiedBy ?? Guid.Empty;
			p.Exec();
			var ret = p.Parameters["@RETURN_VALUE"].Value;
			int code = ret == null || ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
			return code == 1;
		}

		public bool Delete(Guid id)
		{
			Proc p = new Proc("AssesmentMaster_Delete");
			p["@Id"] = id;
			p.Exec();
			var ret = p.Parameters["@RETURN_VALUE"].Value;
			int code = ret == null || ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
			return code == 1;
		}
	}
}
