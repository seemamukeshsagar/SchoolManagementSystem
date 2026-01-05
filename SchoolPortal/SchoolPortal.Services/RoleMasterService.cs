using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SchoolPortal.DBAccess;
using SchoolPortal.Entities.Models;
using SchoolPortal.Services.IServices;
using SchoolPortal.Services.Models;

namespace SchoolPortal.Services
{
    public class RoleMasterService : IRoleMasterService
    {
        private bool _disposed = false;
        private readonly ILogger<RoleMasterService> _logger;

        public RoleMasterService(ILogger<RoleMasterService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #region Helper Methods

        private static RoleMaster MapRole(DataRow r)
        {
            if (r == null) throw new ArgumentNullException(nameof(r));

            return new RoleMaster
            {
                Id = GetValueOrDefault<Guid>(r, "Id"),
                Name = GetValueOrDefault<string>(r, "Name") ?? string.Empty,
                Description = GetValueOrDefault<string>(r, "Description"),
                IsActive = GetValueOrDefault(r, "IsActive", false),
                IsDeleted = GetValueOrDefault(r, "IsDeleted", false),
                CreatedBy = GetValueOrDefault<Guid>(r, "CreatedBy"),
                CreatedDate = GetValueOrDefault<DateTime>(r, "CreatedDate"),
                ModifiedBy = GetValueOrDefault<Guid?>(r, "ModifiedBy"),
                ModifiedDate = GetValueOrDefault<DateTime?>(r, "ModifiedDate"),
                CompanyId = GetValueOrDefault<Guid>(r, "CompanyId"),
                SchoolId = GetValueOrDefault<Guid>(r, "SchoolId")
            };
        }

        private static T GetValueOrDefault<T>(DataRow row, string columnName, T defaultValue = default!)
        {
            if (row.Table.Columns.Contains(columnName) && row[columnName] != DBNull.Value)
            {
                try
                {
                    return (T)Convert.ChangeType(row[columnName], typeof(T));
                }
                catch
                {
                    return defaultValue;
                }
            }
            return defaultValue;
        }

        #endregion

        #region Synchronous Methods (Kept for backward compatibility)

        public IEnumerable<RoleMaster> GetAll()
        {
            return GetAllAsync().GetAwaiter().GetResult();
        }

        public RoleMaster? GetById(Guid id)
        {
            return GetByIdAsync(id).GetAwaiter().GetResult();
        }

        public Guid Create(RoleMaster entity)
        {
            return CreateAsync(entity).GetAwaiter().GetResult();
        }

        public bool Update(RoleMaster entity)
        {
            return UpdateAsync(entity).GetAwaiter().GetResult();
        }

        public bool Delete(Guid id)
        {
            return DeleteAsync(id).GetAwaiter().GetResult();
        }

        public RoleMaster? GetByRoleName(string? roleName = null)
        {
            return GetByRoleNameAsync(roleName).GetAwaiter().GetResult();
        }

        public (IEnumerable<RoleMaster> items, int totalCount) GetRoles(
            int pageNumber,
            int pageSize,
            string? sortColumn = null,
            string sortDirection = "asc",
            string? searchTerm = null)
        {
            return GetRolesAsync(pageNumber, pageSize, sortColumn, sortDirection, searchTerm)
                .GetAwaiter().GetResult();
        }

        public IEnumerable<object> GetRolePrivileges(Guid roleId)
        {
            return GetRolePrivilegesAsync(roleId).GetAwaiter().GetResult()
                .Select(rp => (object)rp)
                .ToList();
        }

        #endregion

        #region Async Methods

        public async Task<IEnumerable<RoleMaster>> GetAllAsync()
        {
            var list = new List<RoleMaster>();
            using (var p = new Proc("RoleMaster_GetAll"))
            {
                try
                {
                    var dt = new DataTable();
                    await Task.Run(() => p.Exec(dt));
                    return dt.AsEnumerable().Select(MapRole).ToList();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while getting all roles");
                    throw;
                }
            }
        }

        public async Task<RoleMaster?> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Role ID cannot be empty", nameof(id));
            }

            using (var p = new Proc("RoleMaster_GetById"))
            {
                try
                {
                    p["@Id"] = id;
                    var dt = new DataTable();
                    await Task.Run(() => p.Exec(dt));
                    return dt.Rows.Count > 0 ? MapRole(dt.Rows[0]) : null;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while getting role by ID: {RoleId}", id);
                    throw;
                }
            }
        }

        public async Task<Guid> CreateAsync(RoleMaster entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            using (var p = new Proc("RoleMaster_Create"))
            {
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
                    await Task.Run(() => p.Exec(dt));

                    if (dt.Rows.Count > 0 && dt.Rows[0]["Id"] != DBNull.Value)
                    {
                        var idString = dt.Rows[0]["Id"]?.ToString();
                        if (!string.IsNullOrEmpty(idString) && Guid.TryParse(idString, out var id))
                        {
                            _logger.LogInformation("Successfully created role with ID: {RoleId}", id);
                            return id;
                        }
                    }

                    _logger.LogWarning("Failed to create role. No valid ID returned from database.");
                    return Guid.Empty;
                }
                finally
                {
                    p.Dispose();
                }
            }
        }

        public async Task<bool> UpdateAsync(RoleMaster entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            using (var p = new Proc("RoleMaster_Update"))
            {
                try
                {
                    p["@Id"] = entity.Id;
                    p["@Name"] = entity.Name ?? string.Empty;
                    p["@Description"] = entity.Description;
                    p["@IsActive"] = entity.IsActive;
                    p["@ModifiedBy"] = entity.ModifiedBy ?? (object)DBNull.Value;
                    p["@ModifiedDate"] = entity.ModifiedDate ?? DateTime.UtcNow;

                    var dt = new DataTable();
                    await Task.Run(() => p.Exec(dt));

                    bool success = dt.Rows.Count > 0 && dt.Rows[0]["Success"] != DBNull.Value && Convert.ToBoolean(dt.Rows[0]["Success"]);

                    if (success)
                    {
                        _logger.LogInformation("Successfully updated role with ID: {RoleId}", entity.Id);
                    }
                    else
                    {
                        _logger.LogWarning("Failed to update role with ID: {RoleId}", entity.Id);
                    }

                    return success;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while updating role with ID: {RoleId}", entity.Id);
                    throw;
                }
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Role ID cannot be empty", nameof(id));

            using (var p = new Proc("RoleMaster_Delete"))
            {
                try
                {
                    p["@Id"] = id;
                    var dt = new DataTable();
                    await Task.Run(() => p.Exec(dt));

                    bool success = dt.Rows.Count > 0 && dt.Rows[0]["Success"] != DBNull.Value && Convert.ToBoolean(dt.Rows[0]["Success"]);

                    if (success)
                    {
                        _logger.LogInformation("Successfully deleted role with ID: {RoleId}", id);
                    }
                    else
                    {
                        _logger.LogWarning("Failed to delete role with ID: {RoleId}", id);
                    }

                    return success;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while deleting role with ID: {RoleId}", id);
                    throw;
                }
            }
        }

        public async Task<RoleMaster?> GetByRoleNameAsync(string? roleName = null)
        {
            if (string.IsNullOrEmpty(roleName))
            {
                _logger.LogWarning("Empty or null role name provided to GetByRoleName");
                return null;
            }

            using (var p = new Proc("RoleMaster_GetByRoleName"))
            {
                try
                {
                    p["@RoleName"] = roleName;
                    var dt = new DataTable();
                    await Task.Run(() => p.Exec(dt));

                    if (dt.Rows.Count == 0)
                    {
                        _logger.LogInformation("No role found with name: {RoleName}", roleName);
                        return null;
                    }

                    return MapRole(dt.Rows[0]);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error retrieving role by name: {RoleName}", roleName);
                    throw;
                }
            }
        }

        public async Task<(IEnumerable<RoleMaster> items, int totalCount)> GetRolesAsync(
            int pageNumber,
            int pageSize,
            string? sortColumn = null,
            string sortDirection = "asc",
            string? searchTerm = null)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 10;
            if (string.IsNullOrEmpty(sortColumn)) sortColumn = "Name";
            if (string.IsNullOrEmpty(sortDirection)) sortDirection = "asc";

            var items = new List<RoleMaster>();
            int totalCount = 0;

            using (var p = new Proc("RoleMaster_GetPaged"))
            {
                try
                {
                    p["@PageNumber"] = pageNumber;
                    p["@PageSize"] = pageSize;
                    p["@SortColumn"] = sortColumn;
                    p["@SortDirection"] = sortDirection;
                    p["@SearchTerm"] = string.IsNullOrEmpty(searchTerm) ? DBNull.Value : (object)searchTerm;

                    using (var ds = new DataSet())
                    {
                        await Task.Run(() => p.Exec(ds));

                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            items = ds.Tables[0].AsEnumerable().Select(MapRole).ToList();
                        }

                        if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                        {
                            totalCount = Convert.ToInt32(ds.Tables[1].Rows[0]["TotalCount"]);
                        }
                    }

                    return (items, totalCount);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while getting paged roles");
                    throw;
                }
            }
        }

        public async Task<IEnumerable<RolePrivilegeDto>> GetRolePrivilegesAsync(Guid roleId)
        {
            if (roleId == Guid.Empty)
            {
                throw new ArgumentException("Role ID cannot be empty", nameof(roleId));
            }

            using (var p = new Proc("RolePrivilege_GetByRoleId"))
            {
                try
                {
                    p["@RoleId"] = roleId;
                    var dt = new DataTable();
                    await Task.Run(() => p.Exec(dt));

                    return dt.AsEnumerable().Select(row => new RolePrivilegeDto
                    {
                        Id = GetValueOrDefault<Guid>(row, "Id"),
                        RoleId = roleId,
                        PrivilegeId = GetValueOrDefault<Guid>(row, "PrivilegeId"),
                        CanView = GetValueOrDefault(row, "CanView", false),
                        CanAdd = GetValueOrDefault(row, "CanAdd", false),
                        CanEdit = GetValueOrDefault(row, "CanEdit", false),
                        CanDelete = GetValueOrDefault(row, "CanDelete", false),
                        CanPrint = GetValueOrDefault(row, "CanPrint", false),
                        CanExport = GetValueOrDefault(row, "CanExport", false),
                        CanImport = GetValueOrDefault(row, "CanImport", false)
                    }).ToList();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while getting role privileges for role ID: {RoleId}", roleId);
                    throw;
                }
            }
        }

        #endregion

        #region IDisposable Implementation

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Dispose managed resources here if any
                }
                _disposed = true;
            }
        }

        ~RoleMasterService()
        {
            Dispose(false);
        }

        #endregion
    }
}