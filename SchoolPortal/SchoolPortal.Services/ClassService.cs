using System;
using System.Collections.Generic;
using System.Data;
using SchoolPortal.DBAccess;
using SchoolPortal.Entities.Models;
using SchoolPortal.Services.IServices;

namespace SchoolPortal.Services
{
    public class ClassService : IClassService
    {
        private static ClassMaster Map(DataRow r)
        {
            var c = new ClassMaster();
            if (r.Table.Columns.Contains("Id") && Guid.TryParse(r["Id"].ToString(), out var id)) c.Id = id;
            c.Name = r.Table.Columns.Contains("Name") ? r["Name"].ToString() ?? string.Empty : string.Empty;
            c.ExamAssessment = r.Table.Columns.Contains("ExamAssessment") ? r["ExamAssessment"].ToString() ?? string.Empty : string.Empty;
            if (r.Table.Columns.Contains("IsGradePointApplicable") && bool.TryParse(r["IsGradePointApplicable"].ToString(), out var gpa)) c.IsGradePointApplicable = gpa;
            if (r.Table.Columns.Contains("IsActive") && bool.TryParse(r["IsActive"].ToString(), out var active)) c.IsActive = active;
            if (r.Table.Columns.Contains("IsDeleted") && bool.TryParse(r["IsDeleted"].ToString(), out var deleted)) c.IsDeleted = deleted;
            if (r.Table.Columns.Contains("CompanyId") && Guid.TryParse(r["CompanyId"].ToString(), out var companyId)) c.CompanyId = companyId;
            if (r.Table.Columns.Contains("SchoolId") && Guid.TryParse(r["SchoolId"].ToString(), out var schoolId)) c.SchoolId = schoolId;
            if (r.Table.Columns.Contains("CreatedBy") && Guid.TryParse(r["CreatedBy"].ToString(), out var createdBy)) c.CreatedBy = createdBy;
            if (r.Table.Columns.Contains("CreatedDate") && DateTime.TryParse(r["CreatedDate"].ToString(), out var createdDate)) c.CreatedDate = createdDate;
            if (r.Table.Columns.Contains("ModifiedBy") && Guid.TryParse(r["ModifiedBy"].ToString(), out var modifiedBy)) c.ModifiedBy = modifiedBy;
            if (r.Table.Columns.Contains("ModifiedDate") && DateTime.TryParse(r["ModifiedDate"].ToString(), out var modifiedDate)) c.ModifiedDate = modifiedDate;
            if (r.Table.Columns.Contains("OrderBy") && int.TryParse(r["OrderBy"].ToString(), out var orderBy)) c.OrderBy = orderBy;
            c.Status = r.Table.Columns.Contains("Status") ? r["Status"].ToString() ?? string.Empty : string.Empty;
            c.StatusMessage = r.Table.Columns.Contains("StatusMessage") ? r["StatusMessage"].ToString() ?? string.Empty : string.Empty;
            return c;
        }

        // Helper methods to map DataTable to model classes
        private List<ClassMaster> MapToClassMasterList(DataTable dt)
        {
            var list = new List<ClassMaster>();
            foreach (DataRow row in dt.Rows)
            {
                var item = new ClassMaster
                {
                    Id = row["Id"] != DBNull.Value ? (Guid)row["Id"] : Guid.Empty,
                    Name = row["Name"]?.ToString() ?? string.Empty,
                    ExamAssessment = row["ExamAssessment"]?.ToString() ?? string.Empty,
                    IsGradePointApplicable = row["IsGradePointApplicable"] != DBNull.Value ? (bool)row["IsGradePointApplicable"] : false,
                    IsActive = row["IsActive"] != DBNull.Value ? (bool)row["IsActive"] : false,
                    IsDeleted = row["IsDeleted"] != DBNull.Value ? (bool)row["IsDeleted"] : false,
                    CompanyId = row["CompanyId"] != DBNull.Value ? (Guid)row["CompanyId"] : Guid.Empty,
                    SchoolId = row["SchoolId"] != DBNull.Value ? (Guid)row["SchoolId"] : Guid.Empty,
                    CreatedBy = row["CreatedBy"] != DBNull.Value ? (Guid)row["CreatedBy"] : Guid.Empty,
                    CreatedDate = row["CreatedDate"] != DBNull.Value ? (DateTime)row["CreatedDate"] : DateTime.MinValue,
                    ModifiedBy = row["ModifiedBy"] != DBNull.Value ? (Guid)row["ModifiedBy"] : Guid.Empty,
                    ModifiedDate = row["ModifiedDate"] != DBNull.Value ? (DateTime)row["ModifiedDate"] : DateTime.MinValue,
                    OrderBy = row["OrderBy"] != DBNull.Value ? (int)row["OrderBy"] : 0,
                    Status = row["Status"]?.ToString() ?? string.Empty,
                    StatusMessage = row["StatusMessage"]?.ToString() ?? string.Empty
                };
                list.Add(item);
            }
            return list;
        }

        public List<ClassMaster> GetAll()
        {
            var list = new List<ClassMaster>();
            Proc p = new Proc("Class_GetAll");
            var dt = new DataTable();
            p.Exec(dt);
            foreach (DataRow r in dt.Rows)
            {
                list.Add(Map(r));
            }
            return list;
        }

        public ClassMaster? GetById(Guid id)
        {
            Proc p = new Proc("Class_GetById");
            p["@Id"] = id;
            var dt = new DataTable();
            p.Exec(dt);
            if (dt.Rows.Count == 0) return null;
            return Map(dt.Rows[0]);
        }

        public Guid Create(ClassMaster cls)
        {
            Proc p = new Proc("Class_Create");
            p["@Name"] = cls.Name;
            p["@ExamAssessment"] = cls.ExamAssessment ?? string.Empty;
            p["@IsGradePointApplicable"] = cls.IsGradePointApplicable ?? false;
            p["@IsActive"] = cls.IsActive;
            p["@CompanyId"] = cls.CompanyId;
            p["@SchoolId"] = cls.SchoolId;
            p["@CreatedBy"] = cls.CreatedBy;
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

        public bool Update(ClassMaster cls)
        {
            Proc p = new Proc("Class_Update");
            p["@Id"] = cls.Id;
            p["@Name"] = cls.Name;
            p["@ExamAssessment"] = cls.ExamAssessment ?? string.Empty;
            p["@IsGradePointApplicable"] = cls.IsGradePointApplicable ?? false;
            p["@IsActive"] = cls.IsActive;
            p["@SchoolId"] = cls.SchoolId;
            p["@ModifiedBy"] = cls.ModifiedBy ?? Guid.Empty;
            p.Exec();
            var ret = p.Parameters["@RETURN_VALUE"].Value;
            int code = ret == null || ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
            return code == 1;
        }

        public bool Delete(Guid id)
        {
            Proc p = new Proc("Class_Delete");
            p["@Id"] = id;
            p.Exec();
            var ret = p.Parameters["@RETURN_VALUE"].Value;
            int code = ret == null || ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
            return code == 1;
        }
    }
}
