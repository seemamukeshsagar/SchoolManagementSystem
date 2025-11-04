using System;
using System.Collections.Generic;
using System.Data;
using SchoolPortal.DBAccess;
using SchoolPortal.Entities.Models;
using SchoolPortal.Services.IServices;

namespace SchoolPortal.Services
{
    public class HolidayMasterService : IHolidayMasterService
    {
        private static HolidayMaster Map(DataRow r)
        {
            var h = new HolidayMaster();
            if (r.Table.Columns.Contains("Id") && Guid.TryParse(r["Id"].ToString(), out var id)) h.Id = id;
            h.Name = r.Table.Columns.Contains("Name") ? r["Name"].ToString() ?? string.Empty : string.Empty;
            h.Description = r.Table.Columns.Contains("Description") ? r["Description"].ToString() ?? string.Empty : string.Empty;
            if (r.Table.Columns.Contains("TypeId") && Guid.TryParse(r["TypeId"].ToString(), out var typeId)) h.TypeId = typeId;
            if (r.Table.Columns.Contains("FromDate") && DateTime.TryParse(r["FromDate"].ToString(), out var fromDate)) h.FromDate = fromDate;
            if (r.Table.Columns.Contains("ToDate") && DateTime.TryParse(r["ToDate"].ToString(), out var toDate)) h.ToDate = toDate;
            if (r.Table.Columns.Contains("Year") && Guid.TryParse(r["Year"].ToString(), out var year)) h.Year = year;
            if (r.Table.Columns.Contains("CompanyId") && Guid.TryParse(r["CompanyId"].ToString(), out var companyId)) h.CompanyId = companyId;
            if (r.Table.Columns.Contains("SchoolId") && Guid.TryParse(r["SchoolId"].ToString(), out var schoolId)) h.SchoolId = schoolId;
            if (r.Table.Columns.Contains("IsStaffApplicable") && bool.TryParse(r["IsStaffApplicable"].ToString(), out var isStaffApplicable)) h.IsStaffApplicable = isStaffApplicable;
            if (r.Table.Columns.Contains("SessionId") && Guid.TryParse(r["SessionId"].ToString(), out var sessionId)) h.SessionId = sessionId;
            if (r.Table.Columns.Contains("IsActive") && bool.TryParse(r["IsActive"].ToString(), out var active)) h.IsActive = active;
            if (r.Table.Columns.Contains("IsDeleted") && bool.TryParse(r["IsDeleted"].ToString(), out var deleted)) h.IsDeleted = deleted;
            if (r.Table.Columns.Contains("CreatedBy") && Guid.TryParse(r["CreatedBy"].ToString(), out var createdBy)) h.CreatedBy = createdBy;
            if (r.Table.Columns.Contains("CreatedDate") && DateTime.TryParse(r["CreatedDate"].ToString(), out var createdDate)) h.CreatedDate = createdDate;
            if (r.Table.Columns.Contains("ModifiedBy") && Guid.TryParse(r["ModifiedBy"].ToString(), out var modifiedBy)) h.ModifiedBy = modifiedBy;
            if (r.Table.Columns.Contains("ModifiedDate") && DateTime.TryParse(r["ModifiedDate"].ToString(), out var modifiedDate)) h.ModifiedDate = modifiedDate;
            h.Status = r.Table.Columns.Contains("Status") ? r["Status"].ToString() ?? string.Empty : string.Empty;
            h.StatusMessage = r.Table.Columns.Contains("StatusMessage") ? r["StatusMessage"].ToString() ?? string.Empty : string.Empty;
            return h;
        }

        public List<HolidayMaster> GetAll()
        {
            var list = new List<HolidayMaster>();
            Proc p = new Proc("HolidayMaster_GetAll");
            var dt = new DataTable();
            p.Exec(dt);
            foreach (DataRow r in dt.Rows)
            {
                list.Add(Map(r));
            }
            return list;
        }

        public HolidayMaster? GetById(Guid id)
        {
            Proc p = new Proc("HolidayMaster_GetById");
            p["@Id"] = id;
            var dt = new DataTable();
            p.Exec(dt);
            if (dt.Rows.Count == 0) return null;
            return Map(dt.Rows[0]);
        }

        public Guid Create(HolidayMaster holiday)
        {
            Proc p = new Proc("HolidayMaster_Create");
            p["@Name"] = holiday.Name;
            p["@Description"] = holiday.Description ?? string.Empty;
            p["@TypeId"] = holiday.TypeId;
            p["@FromDate"] = holiday.FromDate;
            p["@ToDate"] = holiday.ToDate;
            p["@Year"] = holiday.Year;
            p["@CompanyId"] = holiday.CompanyId;
            p["@SchoolId"] = holiday.SchoolId;
            p["@IsStaffApplicable"] = holiday.IsStaffApplicable ?? false;
            p["@SessionId"] = holiday.SessionId;
            p["@IsActive"] = holiday.IsActive;
            p["@CreatedBy"] = holiday.CreatedBy;
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

        public bool Update(HolidayMaster holiday)
        {
            Proc p = new Proc("HolidayMaster_Update");
            p["@Id"] = holiday.Id;
            p["@Name"] = holiday.Name;
            p["@Description"] = holiday.Description ?? string.Empty;
            p["@TypeId"] = holiday.TypeId;
            p["@FromDate"] = holiday.FromDate;
            p["@ToDate"] = holiday.ToDate;
            p["@Year"] = holiday.Year;
            p["@IsStaffApplicable"] = holiday.IsStaffApplicable ?? false;
            p["@SessionId"] = holiday.SessionId;
            p["@IsActive"] = holiday.IsActive;
            p["@SchoolId"] = holiday.SchoolId;
            p["@ModifiedBy"] = holiday.ModifiedBy ?? Guid.Empty;
            p.Exec();
            var ret = p.Parameters["@RETURN_VALUE"].Value;
            int code = ret == null || ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
            return code == 1;
        }

        public bool Delete(Guid id)
        {
            Proc p = new Proc("HolidayMaster_Delete");
            p["@Id"] = id;
            p.Exec();
            var ret = p.Parameters["@RETURN_VALUE"].Value;
            int code = ret == null || ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
            return code == 1;
        }
    }
}
