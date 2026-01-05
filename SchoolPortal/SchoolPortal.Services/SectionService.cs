// In SectionService.cs
using System;
using System.Collections.Generic;
using System.Data;
using SchoolPortal.DBAccess;
using SchoolPortal.Entities.Models;
using SchoolPortal.Services.IServices;

namespace SchoolPortal.Services
{
	public class SectionService : ISectionService
	{
		private static SectionMaster Map(DataRow r)
		{
			var s = new SectionMaster();
			if (r.Table.Columns.Contains("Id") && Guid.TryParse(r["Id"].ToString(), out var id)) 
				s.Id = id;
			s.Name = r.Table.Columns.Contains("Name") ? r["Name"].ToString() ?? string.Empty : string.Empty;
			if (r.Table.Columns.Contains("IsActive") && bool.TryParse(r["IsActive"].ToString(), out var active)) 
				s.IsActive = active;
			if (r.Table.Columns.Contains("IsDeleted") && bool.TryParse(r["IsDeleted"].ToString(), out var deleted)) 
				s.IsDeleted = deleted;
			if (r.Table.Columns.Contains("CompanyId") && Guid.TryParse(r["CompanyId"].ToString(), out var companyId)) 
				s.CompanyId = companyId;
			if (r.Table.Columns.Contains("SchoolId") && Guid.TryParse(r["SchoolId"].ToString(), out var schoolId)) 
				s.SchoolId = schoolId;
			if (r.Table.Columns.Contains("CreatedBy") && Guid.TryParse(r["CreatedBy"].ToString(), out var createdBy)) 
				s.CreatedBy = createdBy;
			if (r.Table.Columns.Contains("CreatedDate") && DateTime.TryParse(r["CreatedDate"].ToString(), out var createdDate)) 
				s.CreatedDate = createdDate;
			if (r.Table.Columns.Contains("ModifiedBy") && Guid.TryParse(r["ModifiedBy"].ToString(), out var modifiedBy)) 
				s.ModifiedBy = modifiedBy;
			if (r.Table.Columns.Contains("ModifiedDate") && DateTime.TryParse(r["ModifiedDate"].ToString(), out var modifiedDate)) 
				s.ModifiedDate = modifiedDate;
			s.Status = r.Table.Columns.Contains("Status") ? r["Status"].ToString() ?? string.Empty : string.Empty;
			s.StatusMessage = r.Table.Columns.Contains("StatusMessage") ? r["StatusMessage"].ToString() ?? string.Empty : string.Empty;
			return s;
		}

		public List<SectionMaster> GetAll()
		{
			var list = new List<SectionMaster>();
			using (Proc p = new Proc("Section_GetAll"))
			{
				var dt = new DataTable();
				p.Exec(dt);
				foreach (DataRow r in dt.Rows)
				{
					list.Add(Map(r));
				}
			}
			return list;
		}

		public List<SectionMaster> GetAll(Guid? schoolId)
		{
			var list = new List<SectionMaster>();
			using (Proc p = new Proc("Section_GetBySchoolId"))
			{
				p["@SchoolId"] = schoolId.HasValue ? (object)schoolId.Value : DBNull.Value;
				var dt = new DataTable();
				p.Exec(dt);
				foreach (DataRow r in dt.Rows)
				{
					list.Add(Map(r));
				}
			}
			return list;
		}

		public SectionMaster? GetById(Guid id)
		{
			using (Proc p = new Proc("Section_GetById"))
			{
				p["@Id"] = id;
				var dt = new DataTable();
				p.Exec(dt);
				if (dt.Rows.Count > 0)
				{
					return Map(dt.Rows[0]);
				}
			}
			return null;
		}

		public async Task<SectionMaster?> GetByIdAsync(Guid id)
		{
			return await Task.Run(() => GetById(id));
		}

		private async Task<IList<SectionMaster>> GetSectionsByClassIdAsync(Guid? classId)
		{
			if (!classId.HasValue)
				return new List<SectionMaster>();

			var sections = await GetByClassIdAsync(classId.Value);
			return sections.ToList();
		}   

		public async Task<IEnumerable<SectionMaster>> GetByClassIdAsync(Guid classId)
		{
			return await Task.Run(() => 
			{
				var list = new List<SectionMaster>();
				using (Proc p = new Proc("Section_GetByClassId"))
				{
					p["@ClassId"] = classId;
					var dt = new DataTable();
					p.Exec(dt);
					foreach (DataRow r in dt.Rows)
					{
						list.Add(Map(r));
					}
				}
				return list;
			});
		}

		public Guid Create(SectionMaster section)
		{
			using (Proc p = new Proc("Section_Create"))
			{
				p["@Id"] = section.Id = Guid.NewGuid();
				p["@Name"] = section.Name;
				p["@IsActive"] = section.IsActive;
				p["@CompanyId"] = section.CompanyId;
				p["@SchoolId"] = section.SchoolId;
				p["@CreatedBy"] = section.CreatedBy;
				p.Exec();
				return section.Id;
			}
		}

		public bool Update(SectionMaster section)
		{
			using (Proc p = new Proc("Section_Update"))
			{
				p["@Id"] = section.Id;
				p["@Name"] = section.Name;
				p["@IsActive"] = section.IsActive;
				p["@ModifiedBy"] = section.ModifiedBy ?? Guid.Empty;
				p.Exec();
				return true;
			}
		}

		public bool Delete(Guid id)
		{
			using (Proc p = new Proc("Section_Delete"))
			{
				p["@Id"] = id;
				p.Exec();
				return true;
			}
		}

		public string SectionNameById(Guid id)
		{
			var section = GetById(id);
			return section?.Name ?? string.Empty;
		}

		SectionMaster? ISectionService.GetById(Guid id)
		{
			throw new NotImplementedException();
		}

		List<SectionMaster> ISectionService.GetSectionsByClassId(Guid? classId)
		{
			throw new NotImplementedException();
		}

		IEnumerable<SectionMaster> ISectionService.GetByClassId(Guid classId)
		{
			throw new NotImplementedException();
		}
	}
}