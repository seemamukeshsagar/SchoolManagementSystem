using System;
using System.Collections.Generic;
using System.Data;
using SchoolPortal.DBAccess;
using SchoolPortal.Entities.Models;
using SchoolPortal.Services.IServices;

namespace SchoolPortal.Services
{
    public class UserDetailsService : IUserDetailsService
    {
        private static UserDetails Map(DataRow r)
        {
            var e = new UserDetails();
            if (r.Table.Columns.Contains("Id") && Guid.TryParse(r["Id"]?.ToString(), out var id)) e.Id = id;
            e.UserName = r.Table.Columns.Contains("UserName") ? r["UserName"]?.ToString() ?? string.Empty : string.Empty;
            e.UserPassword = r.Table.Columns.Contains("UserPassword") ? r["UserPassword"]?.ToString() ?? string.Empty : string.Empty;
            e.FirstName = r.Table.Columns.Contains("FirstName") ? r["FirstName"]?.ToString() ?? string.Empty : string.Empty;
            e.LastName = r.Table.Columns.Contains("LastName") ? r["LastName"]?.ToString() ?? string.Empty : string.Empty;
            e.EmailAddress = r.Table.Columns.Contains("EmailAddress") ? r["EmailAddress"]?.ToString() ?? string.Empty : string.Empty;
            if (r.Table.Columns.Contains("DesignationId") && Guid.TryParse(r["DesignationId"]?.ToString(), out var desig)) e.DesignationId = desig;
            if (r.Table.Columns.Contains("UserRoleId") && Guid.TryParse(r["UserRoleId"]?.ToString(), out var role)) e.UserRoleId = role;
            if (r.Table.Columns.Contains("IsSuperUser") && bool.TryParse(r["IsSuperUser"]?.ToString(), out var su)) e.IsSuperUser = su;
            if (r.Table.Columns.Contains("CompanyId") && Guid.TryParse(r["CompanyId"]?.ToString(), out var comp)) e.CompanyId = comp;
            if (r.Table.Columns.Contains("SchoolId") && Guid.TryParse(r["SchoolId"]?.ToString(), out var school)) e.SchoolId = school;
            if (r.Table.Columns.Contains("IsActive") && bool.TryParse(r["IsActive"]?.ToString(), out var active)) e.IsActive = active;
            if (r.Table.Columns.Contains("IsDeleted") && bool.TryParse(r["IsDeleted"]?.ToString(), out var deleted)) e.IsDeleted = deleted;
            if (r.Table.Columns.Contains("CreatedBy") && Guid.TryParse(r["CreatedBy"]?.ToString(), out var cb)) e.CreatedBy = cb;
            if (r.Table.Columns.Contains("CreatedDate") && DateTime.TryParse(r["CreatedDate"]?.ToString(), out var cd)) e.CreatedDate = cd;
            if (r.Table.Columns.Contains("ModifiedBy") && Guid.TryParse(r["ModifiedBy"]?.ToString(), out var mb)) e.ModifiedBy = mb;
            if (r.Table.Columns.Contains("ModifiedDate") && DateTime.TryParse(r["ModifiedDate"]?.ToString(), out var md)) e.ModifiedDate = md;
            e.Status = r.Table.Columns.Contains("Status") ? r["Status"]?.ToString() ?? string.Empty : e.Status;
            e.StatusMessage = r.Table.Columns.Contains("StatusMessage") ? r["StatusMessage"]?.ToString() ?? string.Empty : e.StatusMessage;
            return e;
        }

        public List<UserDetails> GetAll()
        {
            var list = new List<UserDetails>();
            Proc p = new Proc("UserDetails_GetAll");
            var dt = new DataTable();
            p.Exec(dt);
            foreach (DataRow r in dt.Rows) list.Add(Map(r));
            return list;
        }

        public UserDetails? GetById(Guid id)
        {
            Proc p = new Proc("UserDetails_GetById");
            p["@Id"] = id;
            var dt = new DataTable();
            p.Exec(dt);
            if (dt.Rows.Count == 0) return null;
            return Map(dt.Rows[0]);
        }

        public Guid Create(UserDetails entity)
        {
            Proc p = new Proc("UserDetails_Create");
            p["@UserName"] = entity.UserName ?? string.Empty;
            p["@UserPassword"] = entity.UserPassword ?? string.Empty;
            p["@FirstName"] = entity.FirstName ?? string.Empty;
            p["@LastName"] = entity.LastName ?? string.Empty;
            p["@EmailAddress"] = entity.EmailAddress ?? string.Empty;
            p["@DesignationId"] = entity.DesignationId;
            p["@UserRoleId"] = entity.UserRoleId ?? Guid.Empty;
            p["@IsSuperUser"] = entity.IsSuperUser ?? false;
            p["@CompanyId"] = entity.CompanyId ?? Guid.Empty;
            p["@SchoolId"] = entity.SchoolId ?? Guid.Empty;
            p["@IsActive"] = entity.IsActive;
            p["@CreatedBy"] = entity.CreatedBy;
            var dt = new DataTable();
            p.Exec(dt);
            if (dt.Rows.Count > 0)
            {
                var idObj = dt.Rows[0]["Id"];
                if (idObj != null && Guid.TryParse(idObj.ToString(), out var newId)) return newId;
            }
            return Guid.Empty;
        }

        public bool Update(UserDetails entity)
        {
            Proc p = new Proc("UserDetails_Update");
            p["@Id"] = entity.Id;
            p["@UserName"] = entity.UserName ?? string.Empty;
            p["@UserPassword"] = entity.UserPassword ?? string.Empty;
            p["@FirstName"] = entity.FirstName ?? string.Empty;
            p["@LastName"] = entity.LastName ?? string.Empty;
            p["@EmailAddress"] = entity.EmailAddress ?? string.Empty;
            p["@DesignationId"] = entity.DesignationId;
            p["@UserRoleId"] = entity.UserRoleId ?? Guid.Empty;
            p["@IsSuperUser"] = entity.IsSuperUser ?? false;
            p["@CompanyId"] = entity.CompanyId ?? Guid.Empty;
            p["@SchoolId"] = entity.SchoolId ?? Guid.Empty;
            p["@IsActive"] = entity.IsActive;
            p["@ModifiedBy"] = entity.ModifiedBy ?? Guid.Empty;
            p.Exec();
            var ret = p.Parameters["@RETURN_VALUE"].Value;
            int code = ret == null || ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
            return code == 1;
        }

        public bool Delete(Guid id)
        {
            Proc p = new Proc("UserDetails_Delete");
            p["@Id"] = id;
            p.Exec();
            var ret = p.Parameters["@RETURN_VALUE"].Value;
            int code = ret == null || ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
            return code == 1;
        }
    }
}