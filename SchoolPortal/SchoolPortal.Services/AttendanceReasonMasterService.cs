using System;
using System.Collections.Generic;
using System.Data;
using SchoolPortal.DBAccess;
using SchoolPortal.Entities.Models;
using SchoolPortal.Services.IServices;

namespace SchoolPortal.Services
{
	public class AttendanceReasonMasterService : IAttendanceReasonMasterService
	{
		private static AttendanceReasonMaster Map(DataRow r)
		{
			var a = new AttendanceReasonMaster();
			if (r.Table.Columns.Contains("Id") && Guid.TryParse(r["Id"].ToString(), out var id)) a.Id = id;
			a.Code = r.Table.Columns.Contains("Code") ? r["Code"].ToString() ?? string.Empty : string.Empty;
			a.Name = r.Table.Columns.Contains("Name") ? r["Name"].ToString() ?? string.Empty : string.Empty;
			a.Description = r.Table.Columns.Contains("Description") ? r["Description"].ToString() ?? string.Empty : string.Empty;
			if (r.Table.Columns.Contains("CompanyId") && Guid.TryParse(r["CompanyId"].ToString(), out var companyId)) a.CompanyId = companyId;
			if (r.Table.Columns.Contains("SchoolId") && Guid.TryParse(r["SchoolId"].ToString(), out var schoolId)) a.SchoolId = schoolId;
			if (r.Table.Columns.Contains("IsActive") && bool.TryParse(r["IsActive"].ToString(), out var active)) a.IsActive = active;
			if (r.Table.Columns.Contains("IsDeleted") && bool.TryParse(r["IsDeleted"].ToString(), out var deleted)) a.IsDeleted = deleted;
			if (r.Table.Columns.Contains("CreatedBy") && Guid.TryParse(r["CreatedBy"].ToString(), out var createdBy)) a.CreatedBy = createdBy;
			if (r.Table.Columns.Contains("CreatedDate") && DateTime.TryParse(r["CreatedDate"].ToString(), out var createdDate)) a.CreatedDate = createdDate;
			if (r.Table.Columns.Contains("ModifiedBy") && Guid.TryParse(r["ModifiedBy"].ToString(), out var modifiedBy)) a.ModifiedBy = modifiedBy;
			if (r.Table.Columns.Contains("ModifiedDate") && DateTime.TryParse(r["ModifiedDate"].ToString(), out var modifiedDate)) a.ModifiedDate = modifiedDate;
			a.Status = r.Table.Columns.Contains("Status") ? r["Status"].ToString() ?? a.Status : a.Status;
			a.StatusMessage = r.Table.Columns.Contains("StatusMessage") ? r["StatusMessage"].ToString() ?? a.StatusMessage : a.StatusMessage;
			return a;
		}

		public List<AttendanceReasonMaster> GetAll()
		{
			var list = new List<AttendanceReasonMaster>();
			Proc p = new Proc("AttendanceReasonMaster_GetAll");
			var dt = new DataTable();
			p.Exec(dt);
			foreach (DataRow r in dt.Rows)
			{
				list.Add(Map(r));
			}
			return list;
		}

		public AttendanceReasonMaster? GetById(Guid id)
		{
			Proc p = new Proc("AttendanceReasonMaster_GetById");
			p["@Id"] = id;
			var dt = new DataTable();
			p.Exec(dt);
			if (dt.Rows.Count == 0) return null;
			return Map(dt.Rows[0]);
		}

		public Guid Create(AttendanceReasonMaster attendanceReason)
		{
			Proc p = new Proc("AttendanceReasonMaster_Create");
			p["@Code"] = attendanceReason.Code;
			p["@Name"] = attendanceReason.Name;
			p["@Description"] = attendanceReason.Description ?? string.Empty;
			p["@CompanyId"] = attendanceReason.CompanyId;
			p["@SchoolId"] = attendanceReason.SchoolId;
			p["@IsActive"] = attendanceReason.IsActive.HasValue ? (object)attendanceReason.IsActive.Value : DBNull.Value;
			p["@CreatedBy"] = attendanceReason.CreatedBy;
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

		public bool Update(AttendanceReasonMaster attendanceReason)
		{
			Proc p = new Proc("AttendanceReasonMaster_Update");
			p["@Id"] = attendanceReason.Id;
			p["@Code"] = attendanceReason.Code;
			p["@Name"] = attendanceReason.Name;
			p["@Description"] = attendanceReason.Description ?? string.Empty;
			p["@IsActive"] = attendanceReason.IsActive.HasValue ? (object)attendanceReason.IsActive.Value : DBNull.Value;
			p["@ModifiedBy"] = attendanceReason.ModifiedBy ?? Guid.Empty;
			p.Exec();
			var ret = p.Parameters["@RETURN_VALUE"].Value;
			int code = ret == null || ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
			return code == 1;
		}

		public bool Delete(Guid id)
		{
			Proc p = new Proc("AttendanceReasonMaster_Delete");
			p["@Id"] = id;
			p.Exec();
			var ret = p.Parameters["@RETURN_VALUE"].Value;
			int code = ret == null || ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
			return code == 1;
		}
	}
}
