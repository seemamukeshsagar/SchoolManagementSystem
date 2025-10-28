using System;
using System.Collections.Generic;
using System.Data;
using SchoolPortal.DBAccess;
using SchoolPortal.Entities.Models;
using SchoolPortal.Services.IServices;

namespace SchoolPortal.Services
{
    public class TeacherService : ITeacherService
    {
        private static TeacherMaster Map(DataRow r)
        {
            var t = new TeacherMaster();
            if (r.Table.Columns.Contains("Id") && Guid.TryParse(r["Id"].ToString(), out var id)) t.Id = id;
            t.FirstName = r.Table.Columns.Contains("FirstName") ? r["FirstName"].ToString() ?? string.Empty : string.Empty;
            t.LastName = r.Table.Columns.Contains("LastName") ? r["LastName"].ToString() ?? string.Empty : string.Empty;
            if (r.Table.Columns.Contains("DOB") && DateTime.TryParse(r["DOB"].ToString(), out var dob)) t.DOB = dob;
            if (r.Table.Columns.Contains("DOJ") && DateTime.TryParse(r["DOJ"].ToString(), out var doj)) t.DOJ = doj;
            t.Address = r.Table.Columns.Contains("Address") ? r["Address"].ToString() ?? string.Empty : string.Empty;
            t.Email = r.Table.Columns.Contains("Email") ? r["Email"].ToString() ?? string.Empty : string.Empty;
            t.Phone = r.Table.Columns.Contains("Phone") ? r["Phone"].ToString() ?? string.Empty : string.Empty;
            t.MobilePhone = r.Table.Columns.Contains("MobilePhone") ? r["MobilePhone"].ToString() ?? string.Empty : string.Empty;
            if (r.Table.Columns.Contains("IsActive") && bool.TryParse(r["IsActive"].ToString(), out var active)) t.IsActive = active;
            if (r.Table.Columns.Contains("IsDeleted") && bool.TryParse(r["IsDeleted"].ToString(), out var deleted)) t.IsDeleted = deleted;
            if (r.Table.Columns.Contains("CompanyId") && Guid.TryParse(r["CompanyId"].ToString(), out var companyId)) t.CompanyId = companyId;
            if (r.Table.Columns.Contains("SchoolId") && Guid.TryParse(r["SchoolId"].ToString(), out var schoolId)) t.SchoolId = schoolId;
            if (r.Table.Columns.Contains("CreatedBy") && Guid.TryParse(r["CreatedBy"].ToString(), out var createdBy)) t.CreatedBy = createdBy;
            if (r.Table.Columns.Contains("CreatedDate") && DateTime.TryParse(r["CreatedDate"].ToString(), out var createdDate)) t.CreatedDate = createdDate;
            if (r.Table.Columns.Contains("ModifiedBy") && Guid.TryParse(r["ModifiedBy"].ToString(), out var modifiedBy)) t.ModifiedBy = modifiedBy;
            if (r.Table.Columns.Contains("ModifiedDate") && DateTime.TryParse(r["ModifiedDate"].ToString(), out var modifiedDate)) t.ModifiedDate = modifiedDate;
            t.Status = r.Table.Columns.Contains("Status") ? r["Status"].ToString() ?? string.Empty : string.Empty;
            t.StatusMessage = r.Table.Columns.Contains("StatusMessage") ? r["StatusMessage"].ToString() ?? string.Empty : string.Empty;
            return t;
        }

        public List<TeacherMaster> GetAll()
        {
            var list = new List<TeacherMaster>();
            Proc p = new Proc("Teacher_GetAll");
            var dt = new DataTable();
            p.Exec(dt);
            foreach (DataRow r in dt.Rows)
            {
                list.Add(Map(r));
            }
            return list;
        }

        public TeacherMaster? GetById(Guid id)
        {
            Proc p = new Proc("Teacher_GetById");
            p["@Id"] = id;
            var dt = new DataTable();
            p.Exec(dt);
            if (dt.Rows.Count == 0) return null;
            return Map(dt.Rows[0]);
        }

        public Guid Create(TeacherMaster t)
        {
            Proc p = new Proc("Teacher_Create");
            p["@FirstName"] = t.FirstName;
            p["@LastName"] = t.LastName ?? string.Empty;
            p["@DOB"] = t.DOB;
            p["@Email"] = t.Email ?? string.Empty;
            p["@Phone"] = t.Phone ?? string.Empty;
            p["@IsActive"] = t.IsActive;
            p["@CompanyId"] = t.CompanyId;
            p["@SchoolId"] = t.SchoolId;
            p["@CreatedBy"] = t.CreatedBy;
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

        public bool Update(TeacherMaster t)
        {
            Proc p = new Proc("Teacher_Update");
            p["@Id"] = t.Id;
            p["@FirstName"] = t.FirstName;
            p["@LastName"] = t.LastName ?? string.Empty;
            p["@DOB"] = t.DOB;
            p["@Email"] = t.Email ?? string.Empty;
            p["@Phone"] = t.Phone ?? string.Empty;
            p["@IsActive"] = t.IsActive;
            p["@SchoolId"] = t.SchoolId;
            p["@ModifiedBy"] = t.ModifiedBy ?? Guid.Empty;
            p.Exec();
            var ret = p.Parameters["@RETURN_VALUE"].Value;
            int code = ret == null || ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
            return code == 1;
        }

        public bool Delete(Guid id)
        {
            Proc p = new Proc("Teacher_Delete");
            p["@Id"] = id;
            p.Exec();
            var ret = p.Parameters["@RETURN_VALUE"].Value;
            int code = ret == null || ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
            return code == 1;
        }
    }
}
