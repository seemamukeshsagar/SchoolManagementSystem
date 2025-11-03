using System;
using System.Collections.Generic;
using System.Data;
using SchoolPortal.DBAccess;
using SchoolPortal.Entities.Models;
using SchoolPortal.Services.IServices;

namespace SchoolPortal.Services
{
    public class RoleMasterService : IRoleMasterService
    {
        private static RoleMaster MapRole(DataRow r)
        {
            var role = new RoleMaster();
            if (r.Table.Columns.Contains("Id") && Guid.TryParse(r["Id"].ToString(), out var id)) 
                role.Id = id;
                
            role.Name = r.Table.Columns.Contains("Name") ? r["Name"].ToString() ?? string.Empty : string.Empty;
            role.Description = r.Table.Columns.Contains("Description") ? r["Description"].ToString() : null;
            
            if (r.Table.Columns.Contains("IsActive") && bool.TryParse(r["IsActive"].ToString(), out var isActive)) 
                role.IsActive = isActive;
                
            if (r.Table.Columns.Contains("IsDeleted") && bool.TryParse(r["IsDeleted"].ToString(), out var isDeleted)) 
                role.IsDeleted = isDeleted;
                
            if (r.Table.Columns.Contains("CreatedBy") && Guid.TryParse(r["CreatedBy"].ToString(), out var createdBy)) 
                role.CreatedBy = createdBy;
                
            if (r.Table.Columns.Contains("CreatedDate") && DateTime.TryParse(r["CreatedDate"].ToString(), out var createdDate)) 
                role.CreatedDate = createdDate;
                
            if (r.Table.Columns.Contains("ModifiedBy") && r["ModifiedBy"] != DBNull.Value && 
                Guid.TryParse(r["ModifiedBy"].ToString(), out var modifiedBy))
                role.ModifiedBy = modifiedBy;
                
            if (r.Table.Columns.Contains("ModifiedDate") && r["ModifiedDate"] != DBNull.Value && 
                DateTime.TryParse(r["ModifiedDate"].ToString(), out var modifiedDate))
                role.ModifiedDate = modifiedDate;

            return role;
        }

        public IEnumerable<RoleMaster> GetAll()
        {
            var list = new List<RoleMaster>();
            var p = new Proc("RoleMaster_GetAll");
            try
            {
                var dt = new DataTable();
                p.Exec(dt);
                foreach (DataRow r in dt.Rows)
                {
                    list.Add(MapRole(r));
                }
            }
            finally
            {
                p.Dispose();
            }
            return list;
        }

        public RoleMaster? GetById(Guid id)
        {
            var p = new Proc("RoleMaster_GetById");
            try
            {
                p["@Id"] = id;
                var dt = new DataTable();
                p.Exec(dt);
                return dt.Rows.Count > 0 ? MapRole(dt.Rows[0]) : null;
            }
            finally
            {
                p.Dispose();
            }
        }

        public Guid Create(RoleMaster entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var p = new Proc("RoleMaster_Create");
            try
            {
                p["@Name"] = entity.Name ?? string.Empty;
                p["@Description"] = entity.Description;
                p["@IsActive"] = entity.IsActive;
                p["@CompanyId"] = entity.CompanyId;
                p["@SchoolId"] = entity.SchoolId;
                p["@CreatedBy"] = entity.CreatedBy;
                p["@CreatedDate"] = entity.CreatedDate;

                var dt = new DataTable();
                p.Exec(dt);
                
                if (dt.Rows.Count > 0 && dt.Rows[0]["Id"] != DBNull.Value)
                {
                    var idString = dt.Rows[0]["Id"]?.ToString();
                    if (!string.IsNullOrEmpty(idString) && Guid.TryParse(idString, out var id))
                    {
                        return id;
                    }
                }
                return Guid.Empty;
            }
            finally
            {
                p.Dispose();
            }
        }

        public bool Update(RoleMaster entity)
        {
            if (entity == null)
                return false;

            var p = new Proc("RoleMaster_Update");
            try
            {
                p["@Id"] = entity.Id;
                p["@Name"] = entity.Name ?? string.Empty;
                p["@Description"] = entity.Description;
                p["@IsActive"] = entity.IsActive;
                p["@ModifiedBy"] = entity.ModifiedBy;

                p.Exec();
                var ret = p.Parameters["@RETURN_VALUE"].Value;
                int code = ret == null || ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
                return code == 1;
            }
            finally
            {
                p.Dispose();
            }
        }

        public bool Delete(Guid id)
        {
            var p = new Proc("RoleMaster_Delete");
            try
            {
                p["@Id"] = id;
                p.Exec();
                var ret = p.Parameters["@RETURN_VALUE"].Value;
                int code = ret == null || ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
                return code == 1;
            }
            finally
            {
                p.Dispose();
            }
        }
    }
}