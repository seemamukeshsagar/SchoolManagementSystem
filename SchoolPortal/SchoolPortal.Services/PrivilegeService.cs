using System;
using System.Collections.Generic;
using System.Data;
using SchoolPortal.DBAccess;
using SchoolPortal.Entities.Models;
using SchoolPortal.Services.IServices;

namespace SchoolPortal.Services
{
    public class PrivilegeService : IPrivilegeService
    {
        private readonly ConnectionManager _connectionManager;

        public PrivilegeService(ConnectionManager connectionManager)
        {
            _connectionManager = connectionManager ?? throw new ArgumentNullException(nameof(connectionManager));
        }

        public IEnumerable<Privileges> GetAll()
        {
            var list = new List<Privileges>();
            using (var p = new Proc(_connectionManager, "sp_Privilege_GetAll"))
            {
                var dt = new DataTable();
                p.Exec(dt);
                foreach (DataRow r in dt.Rows)
                {
                    list.Add(MapPrivilege(r));
                }
            }
            return list;
        }

        public Privileges? GetById(Guid id)
        {
            // Clear parameter cache to avoid mismatches
            Proc.ResetParmCache();
            
            using (var p = new Proc(_connectionManager, "sp_Privilege_GetById"))
            {
                p.Parameters.AddWithValue("Id", id);
                var dt = new DataTable();
                p.Exec(dt);
                return dt.Rows.Count > 0 ? MapPrivilege(dt.Rows[0]) : null;
            }
        }

        public Guid Create(Privileges entity)
        {
            using (var p = new Proc(_connectionManager, "sp_Privilege_Create"))
            {
                p.Parameters.AddWithValue("Id", entity.Id);
                p.Parameters.AddWithValue("PrivilegeName", entity.PrivilegeName);
                p.Parameters.AddWithValue("IsActive", entity.IsActive);
                p.Parameters.AddWithValue("CreatedBy", entity.CreatedBy);
                p.Parameters.AddWithValue("PrivilegeParentId", 
                    entity.PrivilegeParentId.HasValue ? (object)entity.PrivilegeParentId.Value : DBNull.Value);

                var dt = new DataTable();
                p.Exec(dt);
                return dt.Rows.Count > 0 ? (Guid)dt.Rows[0][0] : Guid.Empty;
            }
        }

        public bool Update(Privileges entity)
        {
            if (!entity.ModifiedBy.HasValue)
                throw new ArgumentException("ModifiedBy must be set when updating a privilege");

            using (var p = new Proc(_connectionManager, "sp_Privilege_Update"))
            {
                p.Parameters.AddWithValue("Id", entity.Id);
                p.Parameters.AddWithValue("PrivilegeName", entity.PrivilegeName);
                p.Parameters.AddWithValue("IsActive", entity.IsActive);
                p.Parameters.AddWithValue("ModifiedBy", entity.ModifiedBy.Value);
                p.Parameters.AddWithValue("PrivilegeParentId",
                    entity.PrivilegeParentId.HasValue ? (object)entity.PrivilegeParentId.Value : DBNull.Value);

                p.Exec();
                return true;
            }
        }

        public bool Delete(Guid id)
        {
            using (var p = new Proc(_connectionManager, "sp_Privilege_Delete"))
            {
                p.Parameters.AddWithValue("Id", id);
                p.Exec();
                return true;
            }
        }

        private static Privileges MapPrivilege(DataRow r)
        {
            return new Privileges
            {
                Id = (Guid)r["Id"],
                PrivilegeName = r["PrivilegeName"].ToString(),
                IsActive = (bool)r["IsActive"],
                CreatedBy = (Guid)r["CreatedBy"],
                CreatedDate = (DateTime)r["CreatedDate"],
                ModifiedBy = r.IsNull("ModifiedBy") ? (Guid?)null : (Guid)r["ModifiedBy"],
                ModifiedDate = r.IsNull("ModifiedDate") ? (DateTime?)null : (DateTime)r["ModifiedDate"],
                PrivilegeParentId = r.IsNull("PrivilegeParentId") ? (Guid?)null : (Guid)r["PrivilegeParentId"],
                IsDeleted = r.Table.Columns.Contains("IsDeleted") && (bool)r["IsDeleted"],
                Status = r["Status"]?.ToString() ?? "INC",
                StatusMessage = r["StatusMessage"]?.ToString() ?? "In Process...."
            };
        }
    }
}