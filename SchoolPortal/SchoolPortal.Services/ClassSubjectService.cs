using System;
using System.Collections.Generic;
using System.Data;
using SchoolPortal.DBAccess;
using SchoolPortal.Entities.Models;
using SchoolPortal.Services.IServices;

namespace SchoolPortal.Services
{
    public class ClassSubjectService : IClassSubjectService
    {
        // In ClassSubjectService.cs
        private static ClassSubjectDetail MapClassSubject(DataRow r)
        {
            var cs = new ClassSubjectDetail();
            
            // Handle potential null values with null-conditional and null-coalescing operators
            if (r.Table.Columns.Contains("Id") && r["Id"] != DBNull.Value && Guid.TryParse(r["Id"].ToString(), out var id)) 
                cs.Id = id;
            
            if (r.Table.Columns.Contains("ClassMasterId") && r["ClassMasterId"] != DBNull.Value && Guid.TryParse(r["ClassMasterId"].ToString(), out var classId)) 
                cs.ClassMasterId = classId;
            
            if (r.Table.Columns.Contains("SubjectId") && r["SubjectId"] != DBNull.Value && Guid.TryParse(r["SubjectId"].ToString(), out var subjectId)) 
                cs.SubjectId = subjectId;
            
            if (r.Table.Columns.Contains("IsActive") && r["IsActive"] != DBNull.Value && bool.TryParse(r["IsActive"].ToString(), out var isActive)) 
                cs.IsActive = isActive;
            
            if (r.Table.Columns.Contains("IsDeleted") && r["IsDeleted"] != DBNull.Value && bool.TryParse(r["IsDeleted"].ToString(), out var isDeleted)) 
                cs.IsDeleted = isDeleted;
            
            if (r.Table.Columns.Contains("CompanyId") && r["CompanyId"] != DBNull.Value && Guid.TryParse(r["CompanyId"].ToString(), out var companyId)) 
                cs.CompanyId = companyId;
            
            if (r.Table.Columns.Contains("SchoolId") && r["SchoolId"] != DBNull.Value && Guid.TryParse(r["SchoolId"].ToString(), out var schoolId)) 
                cs.SchoolId = schoolId;
            
            if (r.Table.Columns.Contains("CreatedBy") && r["CreatedBy"] != DBNull.Value && Guid.TryParse(r["CreatedBy"].ToString(), out var createdBy)) 
                cs.CreatedBy = createdBy;
            
            if (r.Table.Columns.Contains("CreatedDate") && r["CreatedDate"] != DBNull.Value && DateTime.TryParse(r["CreatedDate"].ToString(), out var createdDate)) 
                cs.CreatedDate = createdDate;
            
            if (r.Table.Columns.Contains("ModifiedBy") && r["ModifiedBy"] != DBNull.Value && Guid.TryParse(r["ModifiedBy"].ToString(), out var modifiedBy)) 
                cs.ModifiedBy = modifiedBy;
            
            if (r.Table.Columns.Contains("ModifiedDate") && r["ModifiedDate"] != DBNull.Value && DateTime.TryParse(r["ModifiedDate"].ToString(), out var modifiedDate)) 
                cs.ModifiedDate = modifiedDate;
            
            cs.Status = r.Table.Columns.Contains("Status") && r["Status"] != DBNull.Value ? r["Status"].ToString() ?? string.Empty : string.Empty;
            cs.StatusMessage = r.Table.Columns.Contains("StatusMessage") && r["StatusMessage"] != DBNull.Value ? r["StatusMessage"].ToString() ?? string.Empty : string.Empty;
            
            // Safely handle related entities
            if (r.Table.Columns.Contains("ClassName") && r["ClassName"] != DBNull.Value) 
            {
                cs.ClassMaster = new ClassMaster { 
                    Name = r["ClassName"]?.ToString() ?? string.Empty 
                };
            }
            
            if (r.Table.Columns.Contains("SubjectName") && r["SubjectName"] != DBNull.Value) 
            {
                cs.Subject = new SubjectMaster { 
                    SubjectName = r["SubjectName"]?.ToString() ?? string.Empty 
                };
            }

            return cs;
        }

        public List<ClassSubjectDetail> GetAll()
        {
            var list = new List<ClassSubjectDetail>();
            Proc p = new Proc("ClassSubject_GetAll");
            var dt = new DataTable();
            p.Exec(dt);
            foreach (DataRow r in dt.Rows)
            {
                list.Add(MapClassSubject(r));
            }
            return list;
        }

        public ClassSubjectDetail? GetById(Guid id)
        {
            Proc p = new Proc("ClassSubject_GetById");
            p["@Id"] = id;
            var dt = new DataTable();
            p.Exec(dt);
            if (dt.Rows.Count == 0) return null;
            return dt.Rows.Count > 0 ? MapClassSubject(dt.Rows[0]) : null;
        }

        public List<ClassSubjectDetail> GetByClassId(Guid classId)
        {
            var list = new List<ClassSubjectDetail>();
            Proc p = new Proc("ClassSubject_GetByClassId");
            p["@ClassMasterId"] = classId;
            var dt = new DataTable();
            p.Exec(dt);
            foreach (DataRow r in dt.Rows)
            {
                list.Add(MapClassSubject(r));
            }
            return list;
        }

        public List<ClassSubjectDetail> GetBySubjectId(Guid subjectId)
        {
            var list = new List<ClassSubjectDetail>();
            Proc p = new Proc("ClassSubject_GetBySubjectId");
            p["@SubjectId"] = subjectId;
            var dt = new DataTable();
            p.Exec(dt);
            foreach (DataRow r in dt.Rows)
            {
                list.Add(MapClassSubject(r));
            }
            return list;
        }

        public Guid Create(ClassSubjectDetail classSubject)
        {
            Proc p = new Proc("ClassSubject_Create");
            p["@ClassMasterId"] = classSubject.ClassMasterId;
            p["@SubjectId"] = classSubject.SubjectId;
            p["@IsActive"] = classSubject.IsActive;
            p["@CompanyId"] = classSubject.CompanyId;
            p["@SchoolId"] = classSubject.SchoolId;
            p["@CreatedBy"] = classSubject.CreatedBy;
            
            var dt = new DataTable();
            p.Exec(dt);
            
            if (dt.Rows.Count > 0)
            {
                var idObj = dt.Rows[0]["Id"];
                if (idObj != null && Guid.TryParse(idObj.ToString(), out var newIdFromSelect))
                {
                    return newIdFromSelect;
                }
            }
            return Guid.Empty;
        }

        public bool Update(ClassSubjectDetail classSubject)
        {
            Proc p = new Proc("ClassSubject_Update");
            p["@Id"] = classSubject.Id;
            p["@ClassMasterId"] = classSubject.ClassMasterId;
            p["@SubjectId"] = classSubject.SubjectId;
            p["@IsActive"] = classSubject.IsActive;
            p["@ModifiedBy"] = classSubject.ModifiedBy ?? Guid.Empty;
            
            p.Exec();
            var ret = p.Parameters["@RETURN_VALUE"].Value;
            int code = ret == null || ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
            return code == 1;
        }

        public bool Delete(Guid id)
        {
            Proc p = new Proc("ClassSubject_Delete");
            p["@Id"] = id;
            p.Exec();
            var ret = p.Parameters["@RETURN_VALUE"].Value;
            int code = ret == null || ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
            return code == 1;
        }
    }
}