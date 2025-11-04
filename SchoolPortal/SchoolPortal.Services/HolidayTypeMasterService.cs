using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using SchoolPortal.DBAccess;
using SchoolPortal.Entities.Models;
using SchoolPortal.Services.IServices;

namespace SchoolPortal.Services
{
    public class HolidayTypeMasterService : IHolidayTypeMasterService
    {
        private static HolidayTypeMaster Map(DataRow r)
        {
            var h = new HolidayTypeMaster();
            if (r.Table.Columns.Contains("Id") && Guid.TryParse(r["Id"].ToString(), out var id)) h.Id = id;
            h.HolidayTypeName = r.Table.Columns.Contains("HolidayTypeName") ? r["HolidayTypeName"].ToString() ?? string.Empty : string.Empty;
            h.HolidayTypeDescription = r.Table.Columns.Contains("HolidayTypeDescription") ? r["HolidayTypeDescription"].ToString() ?? string.Empty : string.Empty;
            if (r.Table.Columns.Contains("CompanyId") && Guid.TryParse(r["CompanyId"].ToString(), out var companyId)) h.CompanyId = companyId;
            if (r.Table.Columns.Contains("SchoolId") && Guid.TryParse(r["SchoolId"].ToString(), out var schoolId)) h.SchoolId = schoolId;
            if (r.Table.Columns.Contains("IsActive") && bool.TryParse(r["IsActive"].ToString(), out var active)) h.IsActive = active;
            if (r.Table.Columns.Contains("IsDeleted") && bool.TryParse(r["IsDeleted"].ToString(), out var deleted)) h.IsDeleted = deleted;
            if (r.Table.Columns.Contains("CreatedBy") && Guid.TryParse(r["CreatedBy"].ToString(), out var createdBy)) h.CreatedBy = createdBy;
            if (r.Table.Columns.Contains("CreatedDate") && DateTime.TryParse(r["CreatedDate"].ToString(), out var createdDate)) h.CreatedDate = createdDate;
            if (r.Table.Columns.Contains("ModifiedBy") && Guid.TryParse(r["ModifiedBy"].ToString(), out var modifiedBy)) h.ModifiedBy = modifiedBy;
            if (r.Table.Columns.Contains("ModifiedDate") && DateTime.TryParse(r["ModifiedDate"].ToString(), out var modifiedDate)) h.ModifiedDate = modifiedDate;
            h.Status = r.Table.Columns.Contains("Status") ? r["Status"].ToString() ?? string.Empty : h.Status;
            h.StatusMessage = r.Table.Columns.Contains("StatusMessage") ? r["StatusMessage"].ToString() ?? string.Empty : h.StatusMessage;
            return h;
        }

        public List<HolidayTypeMaster> GetAll()
        {
            var list = new List<HolidayTypeMaster>();
            Proc p = new Proc("HolidayTypeMaster_GetAll");
            var dt = new DataTable();
            p.Exec(dt);
            foreach (DataRow r in dt.Rows)
            {
                list.Add(Map(r));
            }
            return list;
        }

        public HolidayTypeMaster? GetById(Guid id)
        {
            Proc p = new Proc("HolidayTypeMaster_GetById");
            p["@Id"] = id;
            var dt = new DataTable();
            p.Exec(dt);
            if (dt.Rows.Count == 0) return null;
            return Map(dt.Rows[0]);
        }

        public Guid Create(HolidayTypeMaster holidayType)
        {
            Proc p = new Proc("HolidayTypeMaster_Create");
            p["@HolidayTypeName"] = holidayType.HolidayTypeName;
            p["@HolidayTypeDescription"] = holidayType.HolidayTypeDescription ?? string.Empty;
            p["@CompanyId"] = holidayType.CompanyId;
            p["@SchoolId"] = holidayType.SchoolId;
            p["@IsActive"] = holidayType.IsActive;
            p["@CreatedBy"] = holidayType.CreatedBy;
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

        public bool Update(HolidayTypeMaster holidayType)
        {
            Proc p = new Proc("HolidayTypeMaster_Update");
            p["@Id"] = holidayType.Id;
            p["@HolidayTypeName"] = holidayType.HolidayTypeName;
            p["@HolidayTypeDescription"] = holidayType.HolidayTypeDescription ?? string.Empty;
            p["@IsActive"] = holidayType.IsActive;
            p["@ModifiedBy"] = holidayType.ModifiedBy ?? Guid.Empty;
            p.Exec();
            var ret = p.Parameters["@RETURN_VALUE"].Value;
            int code = ret == null || ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
            return code == 1;
        }

        public bool Delete(Guid id)
        {
            Proc p = new Proc("HolidayTypeMaster_Delete");
            p["@Id"] = id;
            p.Exec();
            var ret = p.Parameters["@RETURN_VALUE"].Value;
            int code = ret == null || ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
            return code == 1;
        }
    }
}
