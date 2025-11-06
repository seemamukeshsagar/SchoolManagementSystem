using System;
using System.Collections.Generic;
using System.Data;
using SchoolPortal.DBAccess;
using SchoolPortal.Entities.Models;
using SchoolPortal.Services.IServices;

namespace SchoolPortal.Services
{
    public class EmpTypeService : IEmpTypeService
    {
        private static EmpTypeMaster Map(DataRow r)
        {
            var e = new EmpTypeMaster();
            if (r.Table.Columns.Contains("Id") && Guid.TryParse(r["Id"].ToString(), out var id)) e.Id = id;
            e.TypeName = r.Table.Columns.Contains("TypeName") ? r["TypeName"].ToString() ?? string.Empty : string.Empty;
            e.Description = r.Table.Columns.Contains("Description") ? r["Description"].ToString() ?? string.Empty : string.Empty;
            if (r.Table.Columns.Contains("CompanyId") && Guid.TryParse(r["CompanyId"].ToString(), out var companyId)) e.CompanyId = companyId;
            if (r.Table.Columns.Contains("SchoolId") && Guid.TryParse(r["SchoolId"].ToString(), out var schoolId)) e.SchoolId = schoolId;
            if (r.Table.Columns.Contains("IsActive") && bool.TryParse(r["IsActive"].ToString(), out var active)) e.IsActive = active;
            if (r.Table.Columns.Contains("IsDeleted") && bool.TryParse(r["IsDeleted"].ToString(), out var deleted)) e.IsDeleted = deleted;
            if (r.Table.Columns.Contains("CreatedBy") && Guid.TryParse(r["CreatedBy"].ToString(), out var createdBy)) e.CreatedBy = createdBy;
            if (r.Table.Columns.Contains("CreatedDate") && DateTime.TryParse(r["CreatedDate"].ToString(), out var createdDate)) e.CreatedDate = createdDate;
            if (r.Table.Columns.Contains("ModifiedBy") && Guid.TryParse(r["ModifiedBy"].ToString(), out var modifiedBy)) e.ModifiedBy = modifiedBy;
            if (r.Table.Columns.Contains("ModifiedDate") && DateTime.TryParse(r["ModifiedDate"].ToString(), out var modifiedDate)) e.ModifiedDate = modifiedDate;
            e.Status = r.Table.Columns.Contains("Status") ? r["Status"].ToString() ?? string.Empty : e.Status;
            e.StatusMessage = r.Table.Columns.Contains("StatusMessage") ? r["StatusMessage"].ToString() ?? string.Empty : e.StatusMessage;
            return e;
        }

        public List<EmpTypeMaster> GetAll()
        {
            var list = new List<EmpTypeMaster>();
            Proc p = new Proc("EmpTypeMaster_GetAll");
            var dt = new DataTable();
            p.Exec(dt);
            foreach (DataRow r in dt.Rows)
            {
                list.Add(Map(r));
            }
            return list;
        }

        public EmpTypeMaster? GetById(Guid id)
        {
            Proc p = new Proc("EmpTypeMaster_GetById");
            p["@Id"] = id;
            var dt = new DataTable();
            p.Exec(dt);
            if (dt.Rows.Count == 0) return null;
            return Map(dt.Rows[0]);
        }

        public Guid Create(EmpTypeMaster empType)
        {
            Proc p = new Proc("EmpTypeMaster_Create");
            p["@TypeName"] = empType.TypeName;
            p["@Description"] = empType.Description ?? string.Empty;
            p["@CompanyId"] = empType.CompanyId;
            p["@SchoolId"] = empType.SchoolId;
            p["@IsActive"] = empType.IsActive;
            p["@CreatedBy"] = empType.CreatedBy;
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

        public bool Update(EmpTypeMaster empType)
        {
            Proc p = new Proc("EmpTypeMaster_Update");
            p["@Id"] = empType.Id;
            p["@TypeName"] = empType.TypeName;
            p["@Description"] = empType.Description ?? string.Empty;
            p["@IsActive"] = empType.IsActive;
            p["@ModifiedBy"] = empType.ModifiedBy ?? Guid.Empty;
            p.Exec();
            var ret = p.Parameters["@RETURN_VALUE"].Value;
            int code = ret == null || ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
            return code == 1;
        }

        public bool Delete(Guid id)
        {
            Proc p = new Proc("EmpTypeMaster_Delete");
            p["@Id"] = id;
            p.Exec();
            var ret = p.Parameters["@RETURN_VALUE"].Value;
            int code = ret == null || ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
            return code == 1;
        }
    }
}

