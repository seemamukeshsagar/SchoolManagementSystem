using System;
using System.Collections.Generic;
using System.Data;
using SchoolPortal.DBAccess;
using SchoolPortal.Entities.Models;
using SchoolPortal.Services.IServices;

namespace SchoolPortal.Services
{
	public class SubjectService : ISubjectService
	{
		private static SubjectMaster Map(DataRow r)
		{
			var s = new SubjectMaster();
			if (r.Table.Columns.Contains("Id") && Guid.TryParse(r["Id"].ToString(), out var id)) s.Id = id;
			s.SubjectName = r.Table.Columns.Contains("SubjectName") ? r["SubjectName"].ToString() ?? string.Empty : string.Empty;
			if (r.Table.Columns.Contains("ClassId") && Guid.TryParse(r["ClassId"].ToString(), out var classId)) s.ClassId = classId;
			if (r.Table.Columns.Contains("IsScholastic") && bool.TryParse(r["IsScholastic"].ToString(), out var scholastic)) s.IsScholastic = scholastic;
			if (r.Table.Columns.Contains("IsActive") && bool.TryParse(r["IsActive"].ToString(), out var active)) s.IsActive = active;
			if (r.Table.Columns.Contains("IsDeleted") && bool.TryParse(r["IsDeleted"].ToString(), out var deleted)) s.IsDeleted = deleted;
			if (r.Table.Columns.Contains("CompanyId") && Guid.TryParse(r["CompanyId"].ToString(), out var companyId)) s.CompanyId = companyId;
			if (r.Table.Columns.Contains("SchoolId") && Guid.TryParse(r["SchoolId"].ToString(), out var schoolId)) s.SchoolId = schoolId;
			if (r.Table.Columns.Contains("CreatedBy") && Guid.TryParse(r["CreatedBy"].ToString(), out var createdBy)) s.CreatedBy = createdBy;
			if (r.Table.Columns.Contains("CreatedDate") && DateTime.TryParse(r["CreatedDate"].ToString(), out var createdDate)) s.CreatedDate = createdDate;
			if (r.Table.Columns.Contains("ModifiedBy") && Guid.TryParse(r["ModifiedBy"].ToString(), out var modifiedBy)) s.ModifiedBy = modifiedBy;
			if (r.Table.Columns.Contains("ModifiedDate") && DateTime.TryParse(r["ModifiedDate"].ToString(), out var modifiedDate)) s.ModifiedDate = modifiedDate;
			s.Status = r.Table.Columns.Contains("Status") ? r["Status"].ToString() ?? string.Empty : string.Empty;
			s.StatusMessage = r.Table.Columns.Contains("StatusMessage") ? r["StatusMessage"].ToString() ?? string.Empty : string.Empty;
			return s;
		}

		public List<SubjectMaster> GetAll()
		{
			var list = new List<SubjectMaster>();
			Proc p = new Proc("Subject_GetAll");
			var dt = new DataTable();
			p.Exec(dt);
			foreach (DataRow r in dt.Rows)
			{
				list.Add(Map(r));
			}
			return list;
		}

		public List<SubjectMaster> GetAll(Guid? schoolId)
		{
			var list = new List<SubjectMaster>();
			Proc p = new Proc("Subject_GetAll");
			p["@SchoolId"] = schoolId ?? (object)DBNull.Value;
			var dt = new DataTable();
			p.Exec(dt);
			foreach (DataRow r in dt.Rows)
			{
				list.Add(Map(r));
			}
			return list;
		}

		public SubjectMaster? GetById(Guid id)
		{
			Proc p = new Proc("Subject_GetById");
			p["@Id"] = id;
			var dt = new DataTable();
			p.Exec(dt);
			if (dt.Rows.Count == 0) return null;
			return Map(dt.Rows[0]);
		}

		public Guid Create(SubjectMaster subject)
		{
			Proc p = new Proc("Subject_Create");
			p["@SubjectName"] = subject.SubjectName;
			p["@ClassId"] = subject.ClassId;
			p["@IsScholastic"] = subject.IsScholastic ?? false;
			p["@IsActive"] = subject.IsActive;
			p["@CompanyId"] = subject.CompanyId;
			p["@SchoolId"] = subject.SchoolId;
			p["@CreatedBy"] = subject.CreatedBy;
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

		public bool Update(SubjectMaster subject)
		{
			Proc p = new Proc("Subject_Update");
			p["@Id"] = subject.Id;
			p["@SubjectName"] = subject.SubjectName;
			p["@ClassId"] = subject.ClassId;
			p["@IsScholastic"] = subject.IsScholastic ?? false;
			p["@IsActive"] = subject.IsActive;
			p["@SchoolId"] = subject.SchoolId;
			p["@ModifiedBy"] = subject.ModifiedBy ?? Guid.Empty;
			p.Exec();
			var ret = p.Parameters["@RETURN_VALUE"].Value;
			int code = ret == null || ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
			return code == 1;
		}

		public bool Delete(Guid id)
		{
			Proc p = new Proc("Subject_Delete");
			p["@Id"] = id;
			p.Exec();
			var ret = p.Parameters["@RETURN_VALUE"].Value;
			int code = ret == null || ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
			return code == 1;
		}

		public List<SubjectMaster> GetByClassId(Guid id)
		{             
			var list = new List<SubjectMaster>();
			Proc p = new Proc("Subject_GetByClassId");
			p["@ClassId"] = id;
			var dt = new DataTable();
			p.Exec(dt);
			foreach (DataRow r in dt.Rows)
			{
				list.Add(Map(r));
			}
			return list;
		}

		public List<SubjectMaster> GetSubjectsByClassId(Guid classId)
		{
			var subjects = new List<SubjectMaster>();
			try
			{
				Proc p = new Proc("Subject_GetByClassId");
				p["@ClassId"] = classId;
				var dt = new DataTable();
				p.Exec(dt);

				foreach (DataRow r in dt.Rows)
				{
					var idObj = r["Id"];
					Guid id = Guid.Empty;
					if (idObj != null && Guid.TryParse(idObj.ToString(), out var parsedId))
					{
						id = parsedId;
					}
					else
					{
						continue; // Skip this row if Id is null or invalid
					}

					subjects.Add(new SubjectMaster
					{
						Id = id,
						SubjectName = r["SubjectName"]?.ToString() ?? string.Empty,
						// Map other properties as needed
					});
				}
			}
			catch (Exception)
			{
				throw;
			}
			return subjects;
		}
	}
}
