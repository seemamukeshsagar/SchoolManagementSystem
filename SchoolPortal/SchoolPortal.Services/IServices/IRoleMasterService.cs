using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SchoolPortal.Entities.Models;
using SchoolPortal.Services.Models;

namespace SchoolPortal.Services.IServices
{
    public interface IRoleMasterService : IDisposable
    {
        // Synchronous methods (kept for backward compatibility)
        IEnumerable<RoleMaster> GetAll();
        RoleMaster? GetById(Guid id);
        Guid Create(RoleMaster entity);
        bool Update(RoleMaster entity);
        bool Delete(Guid id);
        RoleMaster? GetByRoleName(string? roleName = null);
        
        (IEnumerable<RoleMaster> items, int totalCount) GetRoles(
            int pageNumber,
            int pageSize,
            string? sortColumn = null,
            string sortDirection = "asc",
            string? searchTerm = null);

        /// <summary>
        /// Gets all privileges for a specific role (synchronous version)
        /// </summary>
        /// <param name="roleId">The ID of the role</param>
        /// <returns>List of role privileges</returns>
        IEnumerable<object> GetRolePrivileges(Guid roleId);

        // New async methods
        /// <summary>
        /// Gets all roles asynchronously
        /// </summary>
        Task<IEnumerable<RoleMaster>> GetAllAsync();

        /// <summary>
        /// Gets a role by ID asynchronously
        /// </summary>
        Task<RoleMaster?> GetByIdAsync(Guid id);

        /// <summary>
        /// Creates a new role asynchronously
        /// </summary>
        Task<Guid> CreateAsync(RoleMaster entity);

        /// <summary>
        /// Updates an existing role asynchronously
        /// </summary>
        Task<bool> UpdateAsync(RoleMaster entity);

        /// <summary>
        /// Deletes a role asynchronously
        /// </summary>
        Task<bool> DeleteAsync(Guid id);

        /// <summary>
        /// Gets a role by name asynchronously
        /// </summary>
        Task<RoleMaster?> GetByRoleNameAsync(string? roleName = null);

        /// <summary>
        /// Gets paginated roles asynchronously
        /// </summary>
        Task<(IEnumerable<RoleMaster> items, int totalCount)> GetRolesAsync(
            int pageNumber,
            int pageSize,
            string? sortColumn = null,
            string sortDirection = "asc",
            string? searchTerm = null);

        /// <summary>
        /// Gets all privileges for a specific role asynchronously
        /// </summary>
        /// <param name="roleId">The ID of the role</param>
        /// <returns>List of role privileges as DTOs</returns>
        Task<IEnumerable<RolePrivilegeDto>> GetRolePrivilegesAsync(Guid roleId);
    }
}