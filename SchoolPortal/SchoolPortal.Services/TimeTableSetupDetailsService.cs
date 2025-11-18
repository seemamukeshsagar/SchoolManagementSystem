using System;
using System.Collections.Generic;
using System.Data;
using SchoolPortal.DBAccess;
using SchoolPortal.Entities.Models;
using SchoolPortal.Services.IServices;

namespace SchoolPortal.Services
{
    public class TimeTableSetupDetailsService : ITimeTableSetupDetailsService
    {
        private static TimeTableSetupDetails Map(DataRow r)
        {
            var e = new TimeTableSetupDetails();

            if (r.Table.Columns.Contains("Id") && Guid.TryParse(r["Id"]?.ToString(), out var id)) e.Id = id;
            if (r.Table.Columns.Contains("SchoolStartTime") && TimeSpan.TryParse(r["SchoolStartTime"]?.ToString(), out var sst)) e.SchoolStartTime = sst;
            if (r.Table.Columns.Contains("SchoolEndTime") && TimeSpan.TryParse(r["SchoolEndTime"]?.ToString(), out var set)) e.SchoolEndTime = set;
            if (r.Table.Columns.Contains("PeriodStartTime") && TimeSpan.TryParse(r["PeriodStartTime"]?.ToString(), out var pst)) e.PeriodStartTime = pst;
            if (r.Table.Columns.Contains("TotalPeriods") && int.TryParse(r["TotalPeriods"]?.ToString(), out var total)) e.TotalPeriods = total;
            if (r.Table.Columns.Contains("PeriodDuration") && int.TryParse(r["PeriodDuration"]?.ToString(), out var pd)) e.PeriodDuration = pd;
            if (r.Table.Columns.Contains("RecessDuration") && int.TryParse(r["RecessDuration"]?.ToString(), out var rd)) e.RecessDuration = rd;
            if (r.Table.Columns.Contains("RecessAfterPeriod") && int.TryParse(r["RecessAfterPeriod"]?.ToString(), out var rap)) e.RecessAfterPeriod = rap;
            if (r.Table.Columns.Contains("FruitRecessDuration") && int.TryParse(r["FruitRecessDuration"]?.ToString(), out var frd)) e.FruitRecessDuration = frd;
            if (r.Table.Columns.Contains("FruitRecessAfterPeriod") && int.TryParse(r["FruitRecessAfterPeriod"]?.ToString(), out var frap)) e.FruitRecessAfterPeriod = frap;
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

            return e;
        }

        public List<TimeTableSetupDetails> GetAll()
        {
            var list = new List<TimeTableSetupDetails>();
            Proc p = new Proc("TimeTableSetupDetails_GetAll");
            var dt = new DataTable();
            p.Exec(dt);
            foreach (DataRow r in dt.Rows)
            {
                list.Add(Map(r));
            }
            return list;
        }

        public TimeTableSetupDetails? GetById(Guid id)
        {
            Proc p = new Proc("TimeTableSetupDetails_GetById");
            p["@Id"] = id;
            var dt = new DataTable();
            p.Exec(dt);
            if (dt.Rows.Count == 0) return null;
            return Map(dt.Rows[0]);
        }

        public Guid Create(TimeTableSetupDetails e)
        {
            Proc p = new Proc("TimeTableSetupDetails_Create");
            p["@SchoolStartTime"] = e.SchoolStartTime;
            p["@SchoolEndTime"] = e.SchoolEndTime;
            p["@PeriodStartTime"] = e.PeriodStartTime;
            p["@TotalPeriods"] = e.TotalPeriods;
            p["@PeriodDuration"] = e.PeriodDuration;
            p["@RecessDuration"] = e.RecessDuration;
            p["@RecessAfterPeriod"] = e.RecessAfterPeriod;
            p["@FruitRecessDuration"] = (object?)e.FruitRecessDuration ?? DBNull.Value;
            p["@FruitRecessAfterPeriod"] = (object?)e.FruitRecessAfterPeriod ?? DBNull.Value;
            p["@SessionId"] = e.SessionId;
            p["@CompanyId"] = e.CompanyId;
            p["@SchoolId"] = e.SchoolId;
            p["@IsActive"] = e.IsActive;
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

        public bool Update(TimeTableSetupDetails e)
        {
            Proc p = new Proc("TimeTableSetupDetails_Update");
            p["@Id"] = e.Id;
            p["@SchoolStartTime"] = e.SchoolStartTime;
            p["@SchoolEndTime"] = e.SchoolEndTime;
            p["@PeriodStartTime"] = e.PeriodStartTime;
            p["@TotalPeriods"] = e.TotalPeriods;
            p["@PeriodDuration"] = e.PeriodDuration;
            p["@RecessDuration"] = e.RecessDuration;
            p["@RecessAfterPeriod"] = e.RecessAfterPeriod;
            p["@FruitRecessDuration"] = (object?)e.FruitRecessDuration ?? DBNull.Value;
            p["@FruitRecessAfterPeriod"] = (object?)e.FruitRecessAfterPeriod ?? DBNull.Value;
            p["@SessionId"] = e.SessionId;
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
            Proc p = new Proc("TimeTableSetupDetails_Delete");
            p["@Id"] = id;
            p.Exec();
            var ret = p.Parameters["@RETURN_VALUE"].Value;
            int code = ret == null || ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
            return code == 1;
        }
    }
}