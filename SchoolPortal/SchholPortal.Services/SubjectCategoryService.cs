using System;
using System.Collections.Generic;
using System.Data;
using SchoolPortal.DBAccess;
using Schoolortal.Entities.Models;
using SchoolPortal.Services.IServices;

namespace SchoolPortal.Services
{
    public class SubjectCategoryService : ISubjectCategoryService
    {
        private static SubjectCategoryDetails Map(DataRow r)
        {
            var s = new SubjectCategoryDetails();
            if (r.Table.Columns.Contains("Id") && Guid.TryParse(r["Id"].ToString(), out var id)) s.Id = id;
            s.Name = r.Table.Columns.Contains("Name") ? r["Name"].ToString() ?? string.Empty : string.Empty;
            s.Description = r.Table.Columns.Contains("Description") ? r["Description"].ToString() ?? string.Empty : string.Empty;
            if (r.Table.Columns.Contains("ParentId") && Guid.TryParse(r["ParentId"].ToString(), out var parentId)) s.ParentId = parentId;
            if (r.Table.Columns.Contains("SubjectId") && Guid.TryParse(r["SubjectId"].ToString(), out var subjectId)) s.SubjectId = subjectId;
            if (r.Table.Columns.Contains("SessionId") && Guid.TryParse(r["SessionId"].ToString(), out var sessionId)) s.SessionId = sessionId;
            if (r.Table.Columns.Contains("CompanyId") && Guid.TryParse(r["CompanyId"].ToString(), out var companyId)) s.CompanyId = companyId;
            if (r.Table.Columns.Contains("SchoolId") && Guid.TryParse(r["SchoolId"].ToString(), out var schoolId)) s.SchoolId = schoolId;
            if (r.Table.Columns.Contains("IsActive") && bool.TryParse(r["IsActive"].ToString(), out var active)) s.IsActive = active;
            if (r.Table.Columns.Contains("IsDeleted") && bool.TryParse(r["IsDeleted"].ToString(), out var deleted)) s.IsDeleted = deleted;
            if (r.Table.Columns.Contains("CreatedBy") && Guid.TryParse(r["CreatedBy"].ToString(), out var createdBy)) s.CreatedBy = createdBy;
            if (r.Table.Columns.Contains("CreatedDate") && DateTime.TryParse(r["CreatedDate"].ToString(), out var createdDate)) s.CreatedDate = createdDate;
            if (r.Table.Columns.Contains("ModifiedBy") && Guid.TryParse(r["ModifiedBy"].ToString(), out var modifiedBy)) s.ModifiedBy = modifiedBy;
            if (r.Table.Columns.Contains("ModifiedDate") && DateTime.TryParse(r["ModifiedDate"].ToString(), out var modifiedDate)) s.ModifiedDate = modifiedDate;
            s.Status = r.Table.Columns.Contains("Status") ? r["Status"].ToString() ?? string.Empty : string.Empty;
            s.StatusMessage = r.Table.Columns.Contains("StatusMessage") ? r["StatusMessage"].ToString() ?? string.Empty : string.Empty;
            return s;
        }

        public List<SubjectCategoryDetails> GetAll()
        {
            var list = new List<SubjectCategoryDetails>();
            Proc p = new Proc("SubjectCategory_GetAll");
            var dt = new DataTable();
            p.Exec(dt);
            foreach (DataRow r in dt.Rows)
            {
                list.Add(Map(r));
            }
            return list;
        }

        public SubjectCategoryDetails? GetById(Guid id)
        {
            Proc p = new Proc("SubjectCategory_GetById");
            p["@Id"] = id;
            var dt = new DataTable();
            p.Exec(dt);
            if (dt.Rows.Count == 0) return null;
            return Map(dt.Rows[0]);
        }

        public Guid Create(SubjectCategoryDetails category)
        {
            Proc p = new Proc("SubjectCategory_Create");
            p["@Name"] = category.Name;
            p["@Description"] = category.Description ?? string.Empty;
            p["@ParentId"] = category.ParentId;
            p["@SubjectId"] = category.SubjectId;
            p["@IsActive"] = category.IsActive;
            p["@CompanyId"] = category.CompanyId;
            p["@SchoolId"] = category.SchoolId;
            p["@CreatedBy"] = category.CreatedBy;
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

        public bool Update(SubjectCategoryDetails category)
        {
            Proc p = new Proc("SubjectCategory_Update");
            p["@Id"] = category.Id;
            p["@Name"] = category.Name;
            p["@Description"] = category.Description ?? string.Empty;
            p["@ParentId"] = category.ParentId;
            p["@SubjectId"] = category.SubjectId;
            p["@IsActive"] = category.IsActive;
            p["@SchoolId"] = category.SchoolId;
            p["@ModifiedBy"] = category.ModifiedBy ?? Guid.Empty;
            p.Exec();
            var ret = p.Parameters["@RETURN_VALUE"].Value;
            int code = ret == null || ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
            return code == 1;
        }

        public bool Delete(Guid id)
        {
            Proc p = new Proc("SubjectCategory_Delete");
            p["@Id"] = id;
            p.Exec();
            var ret = p.Parameters["@RETURN_VALUE"].Value;
            int code = ret == null || ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
            return code == 1;
        }
    }
}
