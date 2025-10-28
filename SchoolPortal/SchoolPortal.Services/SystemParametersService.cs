using System;
using System.Collections.Generic;
using System.Data;
using SchoolPortal.DBAccess;
using SchoolPortal.Entities.Models;
using SchoolPortal.Services.IServices;

namespace SchoolPortal.Services
{
    public class SystemParametersService : ISystemParametersService
    {
        private static SystemParameters MapItem(DataRow r)
        {
            var x = new SystemParameters();
            if (r.Table.Columns.Contains("Id") && Guid.TryParse(r["Id"].ToString(), out var id)) x.Id = id;
            x.ParameterName = r.Table.Columns.Contains("ParameterName") ? r["ParameterName"].ToString() ?? string.Empty : string.Empty;
            x.ParameterValue = r.Table.Columns.Contains("ParameterValue") ? r["ParameterValue"].ToString() ?? string.Empty : string.Empty;
            x.Description = r.Table.Columns.Contains("Description") ? r["Description"].ToString() ?? string.Empty : string.Empty;
            if (r.Table.Columns.Contains("CompanyId") && Guid.TryParse(r["CompanyId"].ToString(), out var companyId)) x.CompanyId = companyId;
            if (r.Table.Columns.Contains("SchoolId") && Guid.TryParse(r["SchoolId"].ToString(), out var schoolId)) x.SchoolId = schoolId;
            if (r.Table.Columns.Contains("IsActive") && bool.TryParse(r["IsActive"].ToString(), out var active)) x.IsActive = active;
            if (r.Table.Columns.Contains("IsDeleted") && bool.TryParse(r["IsDeleted"].ToString(), out var deleted)) x.IsDeleted = deleted;
            if (r.Table.Columns.Contains("CreatedBy") && Guid.TryParse(r["CreatedBy"].ToString(), out var createdBy)) x.CreatedBy = createdBy;
            if (r.Table.Columns.Contains("CreatedDate") && DateTime.TryParse(r["CreatedDate"].ToString(), out var createdDate)) x.CreatedDate = createdDate;
            if (r.Table.Columns.Contains("ModifiedBy") && Guid.TryParse(r["ModifiedBy"].ToString(), out var modifiedBy)) x.ModifiedBy = modifiedBy;
            if (r.Table.Columns.Contains("ModifiedDate") && DateTime.TryParse(r["ModifiedDate"].ToString(), out var modifiedDate)) x.ModifiedDate = modifiedDate;
            x.Status = r.Table.Columns.Contains("Status") ? r["Status"].ToString() ?? string.Empty : string.Empty;
            x.StatusMessage = r.Table.Columns.Contains("StatusMessage") ? r["StatusMessage"].ToString() ?? string.Empty : string.Empty;
            return x;
        }

        public List<SystemParameters> GetAll()
        {
            var list = new List<SystemParameters>();
            Proc p = new Proc("SystemParameters_GetAll");
            var dt = new DataTable();
            p.Exec(dt);
            foreach (DataRow r in dt.Rows)
            {
                list.Add(MapItem(r));
            }
            return list;
        }

        public SystemParameters? GetById(Guid id)
        {
            Proc p = new Proc("SystemParameters_GetById");
            p["@Id"] = id;
            var dt = new DataTable();
            p.Exec(dt);
            if (dt.Rows.Count == 0) return null;
            return MapItem(dt.Rows[0]);
        }

        public Guid Create(SystemParameters item)
        {
            Proc p = new Proc("SystemParameters_Create");
            p["@ParameterName"] = item.ParameterName;
            p["@ParameterValue"] = item.ParameterValue ?? string.Empty;
            p["@Description"] = item.Description ?? string.Empty;
            p["@CompanyId"] = item.CompanyId;
            p["@SchoolId"] = item.SchoolId;
            p["@IsActive"] = item.IsActive;
            p["@CreatedBy"] = item.CreatedBy;
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

        public bool Update(SystemParameters item)
        {
            Proc p = new Proc("SystemParameters_Update");
            p["@Id"] = item.Id;
            p["@ParameterName"] = item.ParameterName;
            p["@ParameterValue"] = item.ParameterValue ?? string.Empty;
            p["@Description"] = item.Description ?? string.Empty;
            p["@CompanyId"] = item.CompanyId;
            p["@SchoolId"] = item.SchoolId;
            p["@IsActive"] = item.IsActive;
            p["@ModifiedBy"] = item.ModifiedBy ?? Guid.Empty;
            p.Exec();
            var ret = p.Parameters["@RETURN_VALUE"].Value;
            int code = ret == null || ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
            return code == 1;
        }

        public bool Delete(Guid id)
        {
            Proc p = new Proc("SystemParameters_Delete");
            p["@Id"] = id;
            p.Exec();
            var ret = p.Parameters["@RETURN_VALUE"].Value;
            int code = ret == null || ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
            return code == 1;
        }
    }
}
