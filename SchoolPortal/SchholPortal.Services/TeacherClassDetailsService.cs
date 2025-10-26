using System;
using System.Collections.Generic;
using System.Data;
using SchoolPortal.DBAccess;
using Schoolortal.Entities.Models;
using SchoolPortal.Services.IServices;

namespace SchoolPortal.Services
{
    public class TeacherClassDetailsService : ITeacherClassDetailsService
    {
        private static TeacherClassDetails Map(DataRow r)
        {
            var e = new TeacherClassDetails();
            if (r.Table.Columns.Contains("Id") && Guid.TryParse(r["Id"].ToString(), out var id)) e.Id = id;
            if (r.Table.Columns.Contains("TeacherId") && Guid.TryParse(r["TeacherId"].ToString(), out var teacherId)) e.TeacherId = teacherId;
            if (r.Table.Columns.Contains("ClassId") && Guid.TryParse(r["ClassId"].ToString(), out var classId)) e.ClassId = classId;
            if (r.Table.Columns.Contains("SectionId") && Guid.TryParse(r["SectionId"].ToString(), out var sectionId)) e.SectionId = sectionId;
            if (r.Table.Columns.Contains("SubjectId") && Guid.TryParse(r["SubjectId"].ToString(), out var subjectId)) e.SubjectId = subjectId;
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

        public List<TeacherClassDetails> GetAll()
        {
            var list = new List<TeacherClassDetails>();
            Proc p = new Proc("TeacherClassDetails_GetAll");
            var dt = new DataTable();
            p.Exec(dt);
            foreach (DataRow r in dt.Rows)
            {
                list.Add(Map(r));
            }
            return list;
        }

        public TeacherClassDetails? GetById(Guid id)
        {
            Proc p = new Proc("TeacherClassDetails_GetById");
            p["@Id"] = id;
            var dt = new DataTable();
            p.Exec(dt);
            if (dt.Rows.Count == 0) return null;
            return Map(dt.Rows[0]);
        }

        public Guid Create(TeacherClassDetails e)
        {
            Proc p = new Proc("TeacherClassDetails_Create");
            p["@TeacherId"] = e.TeacherId;
            p["@ClassId"] = e.ClassId;
            p["@SectionId"] = e.SectionId;
            p["@SubjectId"] = e.SubjectId;
            p["@IsActive"] = e.IsActive;
            p["@CompanyId"] = e.CompanyId;
            p["@SchoolId"] = e.SchoolId;
            p["@CreatedBy"] = e.CreatedBy;
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

        public bool Update(TeacherClassDetails e)
        {
            Proc p = new Proc("TeacherClassDetails_Update");
            p["@Id"] = e.Id;
            p["@TeacherId"] = e.TeacherId;
            p["@ClassId"] = e.ClassId;
            p["@SectionId"] = e.SectionId;
            p["@SubjectId"] = e.SubjectId;
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
            Proc p = new Proc("TeacherClassDetails_Delete");
            p["@Id"] = id;
            p.Exec();
            var ret = p.Parameters["@RETURN_VALUE"].Value;
            int code = ret == null || ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
            return code == 1;
        }
    }
}
