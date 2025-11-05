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
            if (r.Table.Columns.Contains("Id") && Guid.TryParse(r["Id"].ToString(), out var id)) s.Id = id;
            s.Name = r.Table.Columns.Contains("Name") ? r["Name"].ToString() ?? string.Empty : string.Empty;
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

        private List<SectionMaster> MapToSectionMasterList(DataTable dt)
        {
            var list = new List<SectionMaster>();
            foreach (DataRow row in dt.Rows)
            {
                var item = new SectionMaster
                {
                    Id = row["Id"] != DBNull.Value ? (Guid)row["Id"] : Guid.Empty,
                    Name = row["Name"]?.ToString() ?? string.Empty,
                    IsActive = row["IsActive"] != DBNull.Value ? (bool)row["IsActive"] : false,
                    IsDeleted = row["IsDeleted"] != DBNull.Value ? (bool)row["IsDeleted"] : false,
                    CompanyId = row["CompanyId"] != DBNull.Value ? (Guid)row["CompanyId"] : Guid.Empty,
                    SchoolId = row["SchoolId"] != DBNull.Value ? (Guid)row["SchoolId"] : Guid.Empty,
                    CreatedBy = row["CreatedBy"] != DBNull.Value ? (Guid)row["CreatedBy"] : Guid.Empty,
                    CreatedDate = row["CreatedDate"] != DBNull.Value ? (DateTime)row["CreatedDate"] : DateTime.MinValue,
                    ModifiedBy = row["ModifiedBy"] != DBNull.Value ? (Guid)row["ModifiedBy"] : Guid.Empty,
                    ModifiedDate = row["ModifiedDate"] != DBNull.Value ? (DateTime)row["ModifiedDate"] : DateTime.MinValue,
                    Status = row["Status"]?.ToString() ?? string.Empty,
                    StatusMessage = row["StatusMessage"]?.ToString() ?? string.Empty
                };
                list.Add(item);
            }
            return list;
        }

        public List<SectionMaster> GetAll()
        {
            var list = new List<SectionMaster>();
            Proc p = new Proc("Section_GetAll");
            var dt = new DataTable();
            p.Exec(dt);
            foreach (DataRow r in dt.Rows)
            {
                list.Add(Map(r));
            }
            return list;
        }

        public SectionMaster? GetById(Guid id)
        {
            Proc p = new Proc("Section_GetById");
            p["@Id"] = id;
            var dt = new DataTable();
            p.Exec(dt);
            if (dt.Rows.Count == 0) return null;
            return Map(dt.Rows[0]);
        }

        public Guid Create(SectionMaster section)
        {
            Proc p = new Proc("Section_Create");
            p["@Name"] = section.Name;
            p["@IsActive"] = section.IsActive;
            p["@CompanyId"] = section.CompanyId;
            p["@SchoolId"] = section.SchoolId;
            p["@CreatedBy"] = section.CreatedBy;
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

        public bool Update(SectionMaster section)
        {
            Proc p = new Proc("Section_Update");
            p["@Id"] = section.Id;
            p["@Name"] = section.Name;
            p["@IsActive"] = section.IsActive;
            p["@SchoolId"] = section.SchoolId;
            p["@ModifiedBy"] = section.ModifiedBy ?? Guid.Empty;
            p.Exec();
            var ret = p.Parameters["@RETURN_VALUE"].Value;
            int code = ret == null || ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
            return code == 1;
        }

        public List<SectionMaster> GetSectionsByClassId(Guid classId)
        {
            try
            {
                var list = new List<SectionMaster>();
                Proc p = new Proc("Section_GetByClassId");
                p["@ClassId"] = classId;
                var dt = new DataTable();
                p.Exec(dt);
                foreach (DataRow r in dt.Rows)
                {
                    list.Add(Map(r));
                }
                return list;
            }
            catch
            {
                // Fallback: avoid throwing to prevent 500 in controller
                return new List<SectionMaster>();
            }
        }

        public bool Delete(Guid id)
        {
            Proc p = new Proc("Section_Delete");
            p["@Id"] = id;
            p.Exec();
            var ret = p.Parameters["@RETURN_VALUE"].Value;
            int code = ret == null || ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
            return code == 1;
        }

        public string SectionNameById(Guid id)
        {
            Proc p = new Proc("Section_GetById");
            p["@Id"] = id;
            var dt = new DataTable();
            p.Exec(dt);
            if (dt.Rows.Count == 0) return string.Empty;
            var nameObj = dt.Rows[0]["Name"];
            return nameObj?.ToString() ?? string.Empty;
        }
    }
}
