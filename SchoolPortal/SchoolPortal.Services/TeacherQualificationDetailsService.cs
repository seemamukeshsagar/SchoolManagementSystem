using System;
using System.Collections.Generic;
using System.Data;
using SchoolPortal.DBAccess;
using SchoolPortal.Entities.Models;
using SchoolPortal.Services.IServices;

namespace SchoolPortal.Services
{
	public class TeacherQualificationDetailsService : ITeacherQualificationDetailsService
	{
		private static TeacherQualificationDetails Map(DataRow r)
		{
			var e = new TeacherQualificationDetails();
			if (r.Table.Columns.Contains("Id") && Guid.TryParse(r["Id"].ToString(), out var id)) e.Id = id;
			if (r.Table.Columns.Contains("TeacherId") && Guid.TryParse(r["TeacherId"].ToString(), out var teacherId)) e.TeacherId = teacherId;
			if (r.Table.Columns.Contains("QualificationId") && Guid.TryParse(r["QualificationId"].ToString(), out var qId)) e.QualificationId = qId;
			if (r.Table.Columns.Contains("CompanyId") && Guid.TryParse(r["CompanyId"].ToString(), out var companyId)) e.CompanyId = companyId;
			if (r.Table.Columns.Contains("SchoolId") && Guid.TryParse(r["SchoolId"].ToString(), out var schoolId)) e.SchoolId = schoolId;
			if (r.Table.Columns.Contains("IsActive") && bool.TryParse(r["IsActive"].ToString(), out var active)) e.IsActive = active;
			if (r.Table.Columns.Contains("IsDeleted") && bool.TryParse(r["IsDeleted"].ToString(), out var deleted)) e.IsDeleted = deleted;
			if (r.Table.Columns.Contains("CreatedBy") && Guid.TryParse(r["CreatedBy"].ToString(), out var createdBy)) e.CreatedBy = createdBy;
			if (r.Table.Columns.Contains("CreatedDate") && DateTime.TryParse(r["CreatedDate"].ToString(), out var createdDate)) e.CreatedDate = createdDate;
			if (r.Table.Columns.Contains("ModifiedBy") && Guid.TryParse(r["ModifiedBy"].ToString(), out var modifiedBy)) e.ModifiedBy = modifiedBy;
			if (r.Table.Columns.Contains("ModifiedDate") && DateTime.TryParse(r["ModifiedDate"].ToString(), out var modifiedDate)) e.ModifiedDate = modifiedDate;
			e.Status = r.Table.Columns.Contains("Status") ? r["Status"].ToString() ?? string.Empty : string.Empty;
			e.StatusMessage = r.Table.Columns.Contains("StatusMessage") ? r["StatusMessage"].ToString() ?? string.Empty : string.Empty;
			return e;
		}

		public List<TeacherQualificationDetails> GetAll()
		{
			var list = new List<TeacherQualificationDetails>();
			Proc p = new Proc("TeacherQualificationDetails_GetAll");
			var dt = new DataTable();
			p.Exec(dt);
			foreach (DataRow r in dt.Rows)
			{
				list.Add(Map(r));
			}
			return list;
		}

		public TeacherQualificationDetails? GetById(Guid id)
		{
			Proc p = new Proc("TeacherQualificationDetails_GetById");
			p["@Id"] = id;
			var dt = new DataTable();
			p.Exec(dt);
			if (dt.Rows.Count == 0) return null;
			return Map(dt.Rows[0]);
		}

		public Guid Create(TeacherQualificationDetails e)
		{
			Proc p = new Proc("TeacherQualificationDetails_Create");
			p["@TeacherId"] = e.TeacherId;
			p["@QualificationId"] = e.QualificationId;
			p["@IsActive"] = e.IsActive;
			p["@CompanyId"] = e.CompanyId;
			p["@SchoolId"] = e.SchoolId;
			p["@CreatedBy"] = e.CreatedBy;
			p["@Status"] = e.Status ?? string.Empty;
			p["@StatusMessage"] = e.StatusMessage ?? string.Empty;
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

		public bool Update(TeacherQualificationDetails e)
		{
			Proc p = new Proc("TeacherQualificationDetails_Update");
			p["@Id"] = e.Id;
			p["@TeacherId"] = e.TeacherId;
			p["@QualificationId"] = e.QualificationId;
			p["@IsActive"] = e.IsActive;
			p["@SchoolId"] = e.SchoolId;
			p["@ModifiedBy"] = e.ModifiedBy ?? Guid.Empty;
			p.Exec();
			var ret = p.Parameters["@RETURN_VALUE"].Value;
			int code = ret == null || ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
			return code == 1;
		}

		public bool Delete(Guid id)
		{
			Proc p = new Proc("TeacherQualificationDetails_Delete");
			p["@Id"] = id;
			p.Exec();
			var ret = p.Parameters["@RETURN_VALUE"].Value;
			int code = ret == null || ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
			return code == 1;
		}
	}
}
