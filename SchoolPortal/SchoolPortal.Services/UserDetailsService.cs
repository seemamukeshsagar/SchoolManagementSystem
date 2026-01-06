using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Microsoft.Extensions.DependencyInjection;
using SchoolPortal.DBAccess;
using SchoolPortal.Entities.Models;
using SchoolPortal.Services.IServices;
using SchoolPortalApp.Models;
using System.Text.Json;
using System.Threading.RateLimiting;
using SchoolPortal.Services.Models;

namespace SchoolPortal.Services
{
    public class UserDetailsService : IUserDetailsService
    {
        private readonly ILookupService _lookupService;
        private readonly IRoleMasterService _roleService;
        private readonly ILogger<UserDetailsService> _logger;
        public UserDetailsService(
            ILookupService lookupService,
            IRoleMasterService roleService,
            ILogger<UserDetailsService> logger)
        {
            _lookupService = lookupService ?? throw new ArgumentNullException(nameof(lookupService));
            _roleService = roleService ?? throw new ArgumentNullException(nameof(roleService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<List<UserDetailsListViewModel>> GetAllAsync()
        {
            var list = new List<UserDetailsListViewModel>();

            try
            {
                _logger.LogInformation("Starting to fetch all user details");

                // Load lookup dictionaries asynchronously
                var designationsTask = Task.Run(() => _lookupService.GetDesignations()?
                    .ToDictionary(d => d.Id, d => d.Name ?? string.Empty)
                    ?? new Dictionary<Guid, string>());

                var rolesTask = _roleService.GetAllAsync();

                var companiesTask = Task.Run(() => _lookupService.GetCompanies()?
                    .ToDictionary(c => c.Id, c => c.Name ?? string.Empty)
                    ?? new Dictionary<Guid, string>());

                var schoolsTask = Task.Run(() => _lookupService.GetSchools()?
                    .ToDictionary(s => s.Id, s => s.Name ?? string.Empty)
                    ?? new Dictionary<Guid, string>());

                // Wait for all tasks to complete
                await Task.WhenAll(designationsTask, rolesTask, companiesTask, schoolsTask);

                var designations = await designationsTask;
                var roles = (await rolesTask)?.ToDictionary(r => r.Id, r => r.Name ?? string.Empty)
                    ?? new Dictionary<Guid, string>();
                var companies = await companiesTask;
                var schools = await schoolsTask;

                Proc p = new Proc("UserDetails_GetAll");
                var dt = new DataTable();
                p.Exec(dt);

                _logger.LogInformation($"Retrieved {dt.Rows.Count} users from database");

                // Log column names for debugging
                var columnNames = dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName);
                _logger.LogInformation($"Retrieved columns: {string.Join(", ", columnNames)}");

                foreach (DataRow row in dt.Rows)
                {
                    try
                    {
                        var userId = Guid.TryParse(row["Id"]?.ToString(), out var id) ? id : Guid.Empty;
                        var userRoleId = row.Table.Columns.Contains("UserRoleId") && Guid.TryParse(row["UserRoleId"]?.ToString(), out var roleId)
                            ? roleId
                            : (Guid?)null;

                        var user = new UserDetailsListViewModel
                        {
                            Id = userId,
                            UserName = row["UserName"]?.ToString() ?? string.Empty,
                            FirstName = row["FirstName"]?.ToString() ?? string.Empty,
                            LastName = row["LastName"]?.ToString() ?? string.Empty,
                            EmailAddress = row["EmailAddress"]?.ToString() ?? string.Empty,
                            IsActive = bool.TryParse(row["IsActive"]?.ToString(), out var isActive) && isActive,
                            DesignationName = row["DesignationName"]?.ToString() ?? string.Empty,
                            RoleName = userRoleId.HasValue && roles.TryGetValue(userRoleId.Value, out var roleName)
                                ? roleName
                                : string.Empty,
                            CompanyName = row.Table.Columns.Contains("CompanyId") &&
                                         Guid.TryParse(row["CompanyId"]?.ToString(), out var companyId) &&
                                         companies.TryGetValue(companyId, out var companyName)
                                ? companyName
                                : string.Empty,
                            SchoolName = row.Table.Columns.Contains("SchoolId") &&
                                        Guid.TryParse(row["SchoolId"]?.ToString(), out var schoolId) &&
                                        schools.TryGetValue(schoolId, out var schoolName)
                                ? schoolName
                                : string.Empty
                        };

                        list.Add(user);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, $"Error processing user row: {ex.Message}");
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UserDetailsService.GetAllAsync: {Message}", ex.Message);
                throw;
            }
        }

        public List<UserDetailsListViewModel> GetAll()
        {
            return GetAllAsync().GetAwaiter().GetResult();
        }

        public async Task<UserDetailsViewModel?> GetUserDetailsByIdAsync(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    _logger?.LogWarning("Empty GUID provided to GetById");
                    return null;
                }

                Proc p = new Proc("UserDetails_GetById");
                p["@Id"] = id;
                var dt = new DataTable();
                p.Exec(dt);
                if (dt.Rows.Count == 0) return null;

                var row = dt.Rows[0];
                var user = new UserDetailsViewModel
                {
                    Id = row.Table.Columns.Contains("Id") && Guid.TryParse(row["Id"]?.ToString(), out var idVal) ? idVal : Guid.Empty,
                    UserName = row.Table.Columns.Contains("UserName") ? row["UserName"]?.ToString() ?? string.Empty : string.Empty,
                    FirstName = row.Table.Columns.Contains("FirstName") ? row["FirstName"]?.ToString() ?? string.Empty : string.Empty,
                    LastName = row.Table.Columns.Contains("LastName") ? row["LastName"]?.ToString() ?? string.Empty : string.Empty,
                    EmailAddress = row.Table.Columns.Contains("EmailAddress") ? row["EmailAddress"]?.ToString() ?? string.Empty : string.Empty,
                    IsActive = row.Table.Columns.Contains("IsActive") && bool.TryParse(row["IsActive"]?.ToString(), out var isActive) && isActive,
                    DesignationId = row.Table.Columns.Contains("DesignationId") && Guid.TryParse(row["DesignationId"]?.ToString(), out var desigId) ? desigId : Guid.Empty,
                    DesignationName = row.Table.Columns.Contains("DesignationName") ? row["DesignationName"]?.ToString() ?? string.Empty : string.Empty,
                    UserRoleId = row.Table.Columns.Contains("UserRoleId") && Guid.TryParse(row["UserRoleId"]?.ToString(), out var roleId) ? roleId : null,
                    CompanyId = row.Table.Columns.Contains("CompanyId") && Guid.TryParse(row["CompanyId"]?.ToString(), out var compId) ? compId : null,
                    CompanyName = row.Table.Columns.Contains("CompanyName") ? row["CompanyName"]?.ToString() ?? string.Empty : string.Empty,
                    SchoolId = row.Table.Columns.Contains("SchoolId") && Guid.TryParse(row["SchoolId"]?.ToString(), out var schoolId) ? schoolId : null,
                    SchoolName = row.Table.Columns.Contains("SchoolName") ? row["SchoolName"]?.ToString() ?? string.Empty : string.Empty,
                    IsSuperUser = row.Table.Columns.Contains("IsSuperUser") && bool.TryParse(row["IsSuperUser"]?.ToString(), out var isSuperUser) && isSuperUser
                };

                // Get role name and privileges if UserRoleId exists
                if (user.UserRoleId.HasValue)
                {
                    var role = await _roleService.GetByIdAsync(user.UserRoleId.Value);
                    if (role != null)
                    {
                        user.RoleName = role.Name ?? string.Empty;

                        // Get role privileges
                        var privileges = await _roleService.GetRolePrivilegesAsync(role.Id);
                        user.Privileges = privileges.Select(p => p.Name).ToList();
                    }
                }

                return user;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error getting user details for ID: {UserId}", id);
                return null;
            }
        }

        public UserDetails? GetById(Guid id)
        {
            var viewModel = GetUserDetailsByIdAsync(id).GetAwaiter().GetResult();
            return MapToEntity(viewModel)!;
        }

        public async Task<Guid> CreateAsync(UserDetails entity)
        {
            try
            {
                return await Task.Run(() =>
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

                    if (dt.Rows.Count > 0 && dt.Rows[0]["Id"] != DBNull.Value)
                    {
                        return Guid.Parse(dt.Rows[0]["Id"]!.ToString()!);
                    }
                    return Guid.Empty;
                });
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error creating user");
                throw;
            }
        }

        public Guid Create(UserDetails entity)
        {
            return CreateAsync(entity).GetAwaiter().GetResult();
        }

        public async Task<bool> UpdateAsync(UserDetails entity)
        {
            try
            {
                return await Task.Run(() =>
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
                });
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error updating user with ID: {UserId}", entity.Id);
                return false;
            }
        }

        public bool Update(UserDetails entity)
        {
            return UpdateAsync(entity).GetAwaiter().GetResult();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                return await Task.Run(() =>
                {
                    Proc p = new Proc("UserDetails_Delete");
                    p["@Id"] = id;
                    p.Exec();
                    var ret = p.Parameters["@RETURN_VALUE"].Value;
                    int code = ret == null || ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
                    return code == 1;
                });
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error deleting user with ID: {UserId}", id);
                return false;
            }
        }

        public bool Delete(Guid id)
        {
            return DeleteAsync(id).GetAwaiter().GetResult();
        }

        public async Task<UserDetailsViewModel?> GetByUsernameOrEmailAsync(string usernameOrEmail)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(usernameOrEmail))
                {
                    _logger?.LogWarning("Empty or null username/email provided to GetByUsernameOrEmail");
                    return null;
                }

                Proc p = new Proc("UserDetails_GetByUsernameOrEmail");
                p["@UsernameOrEmail"] = usernameOrEmail.Trim();

                var dt = new DataTable();
                p.Exec(dt);

                if (dt.Rows.Count == 0)
                {
                    _logger?.LogInformation("No user found with username/email: {UsernameOrEmail}", usernameOrEmail);
                    return null;
                }

                var row = dt.Rows[0];
                var user = new UserDetailsViewModel
                {
                    Id = row.Table.Columns.Contains("Id") && Guid.TryParse(row["Id"]?.ToString(), out var idVal) ? idVal : Guid.Empty,
                    UserName = row.Table.Columns.Contains("UserName") ? row["UserName"]?.ToString() ?? string.Empty : string.Empty,
                    FirstName = row.Table.Columns.Contains("FirstName") ? row["FirstName"]?.ToString() ?? string.Empty : string.Empty,
                    LastName = row.Table.Columns.Contains("LastName") ? row["LastName"]?.ToString() ?? string.Empty : string.Empty,
                    EmailAddress = row.Table.Columns.Contains("EmailAddress") ? row["EmailAddress"]?.ToString() ?? string.Empty : string.Empty,
                    IsActive = row.Table.Columns.Contains("IsActive") && bool.TryParse(row["IsActive"]?.ToString(), out var isActive) && isActive,
                    DesignationId = row.Table.Columns.Contains("DesignationId") && Guid.TryParse(row["DesignationId"]?.ToString(), out var desigId) ? desigId : Guid.Empty,
                    DesignationName = row.Table.Columns.Contains("DesignationName") ? row["DesignationName"]?.ToString() ?? string.Empty : string.Empty,
                    UserRoleId = row.Table.Columns.Contains("UserRoleId") && Guid.TryParse(row["UserRoleId"]?.ToString(), out var roleId) ? roleId : null,
                    CompanyId = row.Table.Columns.Contains("CompanyId") && Guid.TryParse(row["CompanyId"]?.ToString(), out var compId) ? compId : null,
                    CompanyName = row.Table.Columns.Contains("CompanyName") ? row["CompanyName"]?.ToString() ?? string.Empty : string.Empty,
                    SchoolId = row.Table.Columns.Contains("SchoolId") && Guid.TryParse(row["SchoolId"]?.ToString(), out var schoolId) ? schoolId : null,
                    SchoolName = row.Table.Columns.Contains("SchoolName") ? row["SchoolName"]?.ToString() ?? string.Empty : string.Empty,
                    IsSuperUser = row.Table.Columns.Contains("IsSuperUser") && bool.TryParse(row["IsSuperUser"]?.ToString(), out var isSuperUser) && isSuperUser,
                    CreatedDate = row.Table.Columns.Contains("CreatedDate") && DateTime.TryParse(row["CreatedDate"]?.ToString(), out var createdDate) ? createdDate : DateTime.UtcNow,
                    ModifiedDate = row.Table.Columns.Contains("ModifiedDate") && DateTime.TryParse(row["ModifiedDate"]?.ToString(), out var modifiedDate) ? modifiedDate : DateTime.UtcNow
                };

                // Get role name and privileges if UserRoleId exists
                if (user.UserRoleId.HasValue)
                {
                    var role = await _roleService.GetByIdAsync(user.UserRoleId.Value);
                    if (role != null)
                    {
                        user.RoleName = role.Name ?? string.Empty;

                        // Get role privileges
                        var privileges = await _roleService.GetRolePrivilegesAsync(role.Id);
                        user.Privileges = privileges != null
    ? privileges.Select(p => p.Name?.ToString() ?? string.Empty).ToList()
    : new List<string>();
                    }
                }

                return user;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error getting user by username/email: {UsernameOrEmail}", usernameOrEmail);
                return null;
            }
        }

        public UserDetails? GetByUsernameOrEmail(string usernameOrEmail)
        {
            var viewModel = GetByUsernameOrEmailAsync(usernameOrEmail).GetAwaiter().GetResult();
            return MapToEntity(viewModel)!;
        }

        public async Task<UserDetailsViewModel?> GetByUsernameOrEmailAsync(string username, string email)
        {
            if (!string.IsNullOrWhiteSpace(username))
                return await GetByUsernameOrEmailAsync(username);

            return await GetByUsernameOrEmailAsync(email);
        }

        public UserDetails? GetByUsernameOrEmail(string username, string email)
        {
            var viewModel = GetByUsernameOrEmailAsync(username, email).GetAwaiter().GetResult();
            return MapToEntity(viewModel)!;
        }

        private async Task<byte[]> ReadFileAsync(string filePath)
        {
            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            using (var memoryStream = new MemoryStream())
            {
                await fileStream.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }
        // Helper method to get role by name
        private async Task<RoleMaster?> GetRoleByNameAsync(string roleName)
        {
            try
            {
                var roles = await _roleService.GetAllAsync();
                return roles?.FirstOrDefault(r => 
                    string.Equals(r.Name, roleName, StringComparison.OrdinalIgnoreCase));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting role by name: {RoleName}", roleName);
                return null;
            }
        }
        private UserDetails? MapToEntity(UserDetailsViewModel? viewModel)
        {
            if (viewModel == null) return null;
            return new UserDetails
            {
                Id = viewModel.Id,
                UserName = viewModel.UserName,
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                EmailAddress = viewModel.EmailAddress,
                IsActive = viewModel.IsActive,
                DesignationId = viewModel.DesignationId,
                UserRoleId = viewModel.UserRoleId,
                IsSuperUser = viewModel.IsSuperUser,
                CompanyId = viewModel.CompanyId,
                SchoolId = viewModel.SchoolId,
                CreatedDate = viewModel.CreatedDate,
                ModifiedDate = viewModel.ModifiedDate
            };
        }   
    }
}