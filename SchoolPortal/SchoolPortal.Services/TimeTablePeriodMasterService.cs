using System;
using System.Collections.Generic;
using System.Data;
using SchoolPortal.DBAccess;
using SchoolPortal.Entities.Models;
using SchoolPortal.Services.IServices;

namespace SchoolPortal.Services
{
    public class TimeTablePeriodMasterService : ITimeTablePeriodMasterService
    {
        private static TimeTablePeriodMaster Map(DataRow r)
        {
            var e = new TimeTablePeriodMaster();

            if (r.Table.Columns.Contains("Id") && Guid.TryParse(r["Id"]?.ToString(), out var id)) e.Id = id;
            if (r.Table.Columns.Contains("Description")) e.Description = r["Description"]?.ToString() ?? string.Empty;
            if (r.Table.Columns.Contains("StartTime") && TimeSpan.TryParse(r["StartTime"]?.ToString(), out var start)) e.StartTime = start;
            if (r.Table.Columns.Contains("EndTime") && TimeSpan.TryParse(r["EndTime"]?.ToString(), out var end)) e.EndTime = end;
            if (r.Table.Columns.Contains("SessionId") && Guid.TryParse(r["SessionId"]?.ToString(), out var sessionId)) e.SessionId = sessionId;
            if (r.Table.Columns.Contains("CompanyId") && Guid.TryParse(r["CompanyId"]?.ToString(), out var companyId)) e.CompanyId = companyId;
            if (r.Table.Columns.Contains("SchoolId") && Guid.TryParse(r["SchoolId"]?.ToString(), out var schoolId)) e.SchoolId = schoolId;
            if (r.Table.Columns.Contains("IsActive") && bool.TryParse(r["IsActive"]?.ToString(), out var active)) e.IsActive = active;
            if (r.Table.Columns.Contains("IsDeleted") && bool.TryParse(r["IsDeleted"]?.ToString(), out var deleted)) e.IsDeleted = deleted;
            if (r.Table.Columns.Contains("CreatedBy") && Guid.TryParse(r["CreatedBy"]?.ToString(), out var createdBy)) e.CreatedBy = createdBy;
            if (r.Table.Columns.Contains("CreatedDate") && DateTime.TryParse(r["CreatedDate"]?.ToString(), out var createdDate)) e.CreatedDate = createdDate;
            if (r.Table.Columns.Contains("ModifiedBy") && Guid.TryParse(r["ModifiedBy"]?.ToString(), out var modifiedBy)) e.ModifiedBy = modifiedBy;
            if (r.Table.Columns.Contains("ModifiedDate") && DateTime.TryParse(r["ModifiedDate"]?.ToString(), out var modifiedDate)) e.ModifiedDate = modifiedDate;
            if (r.Table.Columns.Contains("Status")) e.Status = r["Status"]?.ToString() ?? string.Empty;
            if (r.Table.Columns.Contains("StatusMessage")) e.StatusMessage = r["StatusMessage"]?.ToString() ?? string.Empty;
            if (r.Table.Columns.Contains("PeriodNumber")) e.PeriodNumber = r["PeriodNumber"]?.ToString() ?? string.Empty;

            return e;
        }

        public List<TimeTablePeriodMaster> GetAll()
        {
            var list = new List<TimeTablePeriodMaster>();
            Proc p = new Proc("TimeTablePeriodMaster_GetAll");
            var dt = new DataTable();
            p.Exec(dt);
            foreach (DataRow r in dt.Rows)
            {
                list.Add(Map(r));
            }
            return list;
        }

        public TimeTablePeriodMaster? GetById(Guid id)
        {
            Proc p = new Proc("TimeTablePeriodMaster_GetById");
            p["@Id"] = id;
            var dt = new DataTable();
            p.Exec(dt);
            if (dt.Rows.Count == 0) return null;
            return Map(dt.Rows[0]);
        }

        public Guid Create(TimeTablePeriodMaster e)
        {
            Proc p = new Proc("TimeTablePeriodMaster_Create");
            p["@Description"] = e.Description ?? string.Empty;
            p["@StartTime"] = e.StartTime;
            p["@EndTime"] = e.EndTime;
            p["@SessionId"] = e.SessionId;
            p["@CompanyId"] = e.CompanyId;
            p["@SchoolId"] = e.SchoolId;
            p["@IsActive"] = e.IsActive;
            p["@PeriodNumber"] = e.PeriodNumber ?? string.Empty;
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

        public bool Update(TimeTablePeriodMaster e)
        {
            Proc p = new Proc("TimeTablePeriodMaster_Update");
            p["@Id"] = e.Id;
            p["@Description"] = e.Description ?? string.Empty;
            p["@StartTime"] = e.StartTime;
            p["@EndTime"] = e.EndTime;
            p["@SessionId"] = e.SessionId;
            p["@IsActive"] = e.IsActive;
            p["@PeriodNumber"] = e.PeriodNumber ?? string.Empty;
            p["@SchoolId"] = e.SchoolId;
            p["@ModifiedBy"] = e.ModifiedBy ?? Guid.Empty;

            p.Exec();
            var ret = p.Parameters["@RETURN_VALUE"].Value;
            int code = ret == null || ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
            return code == 1;
        }

        public bool Delete(Guid id)
        {
            Proc p = new Proc("TimeTablePeriodMaster_Delete");
            p["@Id"] = id;
            p.Exec();
            var ret = p.Parameters["@RETURN_VALUE"].Value;
            int code = ret == null || ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
            return code == 1;
        }
    }
}