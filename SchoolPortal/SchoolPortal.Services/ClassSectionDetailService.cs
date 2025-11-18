// SchoolPortal.Services/ClassSectionDetailService.cs
using System;
using System.Collections.Generic;
using System.Data;
using SchoolPortal.DBAccess;
using SchoolPortal.Entities.Models;
using SchoolPortal.Services.IServices;

namespace SchoolPortal.Services
{
	public class ClassSectionDetailService : IClassSectionDetailService
	{
		private static ClassSectionDetail Map(DataRow r)
		{
			var csd = new ClassSectionDetail();
			if (r.Table.Columns.Contains("Id") && Guid.TryParse(r["Id"].ToString(), out var id)) csd.Id = id;
			if (r.Table.Columns.Contains("ClassMasterId") && Guid.TryParse(r["ClassMasterId"].ToString(), out var classMasterId)) csd.ClassMasterId = classMasterId;
			if (r.Table.Columns.Contains("SectionMasterId") && Guid.TryParse(r["SectionMasterId"].ToString(), out var sectionMasterId)) csd.SectionMasterId = sectionMasterId;
			if (r.Table.Columns.Contains("LocationId") && Guid.TryParse(r["LocationId"].ToString(), out var locationId)) csd.LocationId = locationId;
			if (r.Table.Columns.Contains("IsActive") && bool.TryParse(r["IsActive"].ToString(), out var isActive)) csd.IsActive = isActive;
			if (r.Table.Columns.Contains("IsDeleted") && bool.TryParse(r["IsDeleted"].ToString(), out var isDeleted)) csd.IsDeleted = isDeleted;
			if (r.Table.Columns.Contains("CompanyId") && Guid.TryParse(r["CompanyId"].ToString(), out var companyId)) csd.CompanyId = companyId;
			if (r.Table.Columns.Contains("SchoolId") && Guid.TryParse(r["SchoolId"].ToString(), out var schoolId)) csd.SchoolId = schoolId;
			if (r.Table.Columns.Contains("CreatedBy") && Guid.TryParse(r["CreatedBy"].ToString(), out var createdBy)) csd.CreatedBy = createdBy;
			if (r.Table.Columns.Contains("CreatedDate") && DateTime.TryParse(r["CreatedDate"].ToString(), out var createdDate)) csd.CreatedDate = createdDate;
			if (r.Table.Columns.Contains("ModifiedBy") && r["ModifiedBy"] != DBNull.Value && Guid.TryParse(r["ModifiedBy"].ToString(), out var modifiedBy)) csd.ModifiedBy = modifiedBy;
			if (r.Table.Columns.Contains("ModifiedDate") && r["ModifiedDate"] != DBNull.Value && DateTime.TryParse(r["ModifiedDate"].ToString(), out var modifiedDate)) csd.ModifiedDate = modifiedDate;
			csd.Status = r.Table.Columns.Contains("Status") ? r["Status"].ToString() ?? string.Empty : string.Empty;
			csd.StatusMessage = r.Table.Columns.Contains("StatusMessage") ? r["StatusMessage"].ToString() ?? string.Empty : string.Empty;
			return csd;
		}

		// Helper methods to map DataTable to model classes
		private List<ClassSectionDetail> MapToClassSectionDetailList(DataTable dt)
		{
			var list = new List<ClassSectionDetail>();
			foreach (DataRow row in dt.Rows)
			{
				list.Add(Map(row));
			}
			return list;
		}

		public IEnumerable<ClassSectionDetail> GetAll()
		{
			var list = new List<ClassSectionDetail>();
			Proc p = new Proc("ClassSectionDetail_GetAll");
			var dt = new DataTable();
			p.Exec(dt);
			return MapToClassSectionDetailList(dt);
		}

		public ClassSectionDetail? GetById(Guid id)
		{
			if (id == Guid.Empty)
				return null;

			Proc p = new("ClassSectionDetail_GetById");
			p["@Id"] = id;
			var dt = new DataTable();
			p.Exec(dt);
			return dt.Rows.Count > 0 ? Map(dt.Rows[0]) : null;
		}

		public Guid Create(ClassSectionDetail entity)
		{
			Proc p = new Proc("ClassSectionDetail_Create");
			p["@ClassMasterId"] = entity.ClassMasterId;
			p["@SectionMasterId"] = entity.SectionMasterId;
			p["@LocationId"] = entity.LocationId;
			p["@IsActive"] = entity.IsActive;
			p["@CompanyId"] = entity.CompanyId;
			p["@SchoolId"] = entity.SchoolId;
			p["@CreatedBy"] = entity.CreatedBy;

			var dt = new DataTable();
			p.Exec(dt);

			if (dt.Rows.Count > 0 && dt.Rows[0]["Id"] != DBNull.Value)
			{
				return (Guid)dt.Rows[0]["Id"];
			}

			return Guid.Empty;
		}

		public bool Update(ClassSectionDetail entity)
		{
			Proc p = new Proc("ClassSectionDetail_Update");
			p["@Id"] = entity.Id;
			p["@ClassMasterId"] = entity.ClassMasterId;
			p["@SectionMasterId"] = entity.SectionMasterId;
			p["@LocationId"] = entity.LocationId;
			p["@IsActive"] = entity.IsActive;
			p["@SchoolId"] = entity.SchoolId;
			p["@ModifiedBy"] = entity.ModifiedBy ?? Guid.Empty;

			p.Exec();
			var ret = p.Parameters["@RETURN_VALUE"].Value;
			int code = ret == null || ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
			return code == 1;
		}

		public bool Delete(Guid id)
		{
			Proc p = new Proc("ClassSectionDetail_Delete");
			p["@Id"] = id;
			p.Exec();
			var ret = p.Parameters["@RETURN_VALUE"].Value;
			int code = ret == null || ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
			return code == 1;
		}

		public bool ToggleStatus(Guid id, Guid userId)
		{
			Proc p = new Proc("ClassSectionDetail_ToggleStatus");
			p["@Id"] = id;
			p["@ModifiedBy"] = userId;
			p.Exec();
			var ret = p.Parameters["@RETURN_VALUE"].Value;
			int code = ret == null || ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
			return code == 1;
		}

		public IEnumerable<ClassSectionDetail> GetByClassId(Guid classId)
		{
			Proc p = new Proc("ClassSectionDetail_GetByClassId");
			p["@ClassId"] = classId;
			var dt = new DataTable();
			p.Exec(dt);
			return MapToClassSectionDetailList(dt);
		}

		public IEnumerable<ClassSectionDetail> GetBySectionId(Guid sectionId)
		{
			Proc p = new Proc("ClassSectionDetail_GetBySectionId");
			p["@SectionId"] = sectionId;
			var dt = new DataTable();
			p.Exec(dt);
			return MapToClassSectionDetailList(dt);
		}
	}
}