using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SchoolPortal.Entities.Models;
using SchoolPortal.Services.IServices;
using SchoolPortalApp.Models;

/// <summary>
/// 
/// </summary>
namespace SchoolPortalApp.Controllers
{
    [Route("UserDetails")]
    public class UserDetailsController : BaseController
    {
        private readonly IUserDetailsService _service;
        private readonly ILookupService _lookup;
        private readonly IRoleMasterService _roles;
        private new readonly ILogger<UserDetailsController> _logger;

        public UserDetailsController(
            IUserDetailsService service,
            ILookupService lookup,
            IRoleMasterService roles,
            ILogger<UserDetailsController> logger)
        {
            _service = service;
            _lookup = lookup;
            _roles = roles;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        private void PopulateDropdowns(UserDetailsViewModel vm)
        {
            var designations = _lookup.GetDesignations();
            vm.Designations = designations.Select(d => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Value = d.Id.ToString(),
                Text = d.Name,
                Selected = d.Id == vm.DesignationId
            }).ToList();

            var roles = _roles.GetAll();
            vm.Roles = roles.Select(r => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Value = r.Id.ToString(),
                Text = r.Name ?? string.Empty,
                Selected = vm.UserRoleId.HasValue && r.Id == vm.UserRoleId.Value
            }).ToList();

            var companies = _lookup.GetCompanies();
            vm.Companies = companies.Select(c => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name,
                Selected = vm.CompanyId.HasValue && c.Id == vm.CompanyId.Value
            }).ToList();

            var schools = _lookup.GetSchools();
            vm.Schools = schools.Select(s => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.Name,
                Selected = vm.SchoolId.HasValue && s.Id == vm.SchoolId.Value
            }).ToList();
        }

        [HttpGet]
        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            var users = _service.GetAll();
            return View(users);
        }

        [HttpPost]
        [Route("GetUsersData")]
        public async Task<IActionResult> GetUsersData()
        {
            try
            {
                var requestForm = Request.Form;
                var draw = Convert.ToInt32(requestForm["draw"].FirstOrDefault() ?? "0");
                var start = Convert.ToInt32(requestForm["start"].FirstOrDefault() ?? "0");
                var length = Convert.ToInt32(requestForm["length"].FirstOrDefault() ?? "10");
                var sortColumn = requestForm["columns[" + requestForm["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnDirection = requestForm["order[0][dir]"].FirstOrDefault();
                var searchValue = requestForm["search[value]"].FirstOrDefault() ?? string.Empty;
                int pageSize = length != -1 ? length : 0;
                int skip = start != 0 ? start : 0;
                int recordsTotal = 0;
                // Get all users
                List<UserDetailsListViewModel> users;
                
                // Check if current user is SuperAdministrator
                var currentUser = await _service.GetUserDetailsByIdAsync(CurrentUserId ?? Guid.Empty);
                bool isSuperAdmin = currentUser?.IsSuperUser == true;
                if (isSuperAdmin)
                {
                    // For SuperAdmin, get all users without filtering
                    users = _service.GetAll().ToList();
                }
                else
                {
                    // For other roles, use the existing filtering logic
                    users = _service.GetAll().ToList();
                }
                // Apply search
                if (!string.IsNullOrEmpty(searchValue))
                {
                    users = users.Where(u => 
                        (u.UserName != null && u.UserName.Contains(searchValue, StringComparison.OrdinalIgnoreCase)) ||
                        (u.FullName != null && u.FullName.Contains(searchValue, StringComparison.OrdinalIgnoreCase)) ||
                        (u.EmailAddress != null && u.EmailAddress.Contains(searchValue, StringComparison.OrdinalIgnoreCase)) ||
                        (u.RoleName != null && u.RoleName.Contains(searchValue, StringComparison.OrdinalIgnoreCase)) ||
                        (u.DesignationName != null && u.DesignationName.Contains(searchValue, StringComparison.OrdinalIgnoreCase))
                    ).ToList();
                }
                // Get total count
                recordsTotal = users.Count;
                // Apply sorting
                if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortColumnDirection))
                {
                    var propertyInfo = typeof(UserDetailsListViewModel).GetProperty(sortColumn, 
                        System.Reflection.BindingFlags.IgnoreCase | 
                        System.Reflection.BindingFlags.Public | 
                        System.Reflection.BindingFlags.Instance);
                    if (propertyInfo != null)
                    {
                        if (sortColumnDirection.ToLower() == "asc")
                        {
                            users = users.OrderBy(x => propertyInfo.GetValue(x, null)).ToList();
                        }
                        else
                        {
                            users = users.OrderByDescending(x => propertyInfo.GetValue(x, null)).ToList();
                        }
                    }
                }
                // Apply pagination
                var data = users
                    .Skip(skip)
                    .Take(pageSize)
                    .Select(u => new
                    {
                        id = u.Id,
                        userName = u.UserName ?? string.Empty,
                        fullName = u.FullName ?? string.Empty,
                        emailAddress = u.EmailAddress ?? string.Empty,
                        roleName = u.RoleName ?? string.Empty,
                        designationName = u.DesignationName ?? string.Empty,
                        isActive = u.IsActive
                    })
                    .ToList();
                return Json(new { 
                    draw = draw, 
                    recordsFiltered = recordsTotal, 
                    recordsTotal = recordsTotal, 
                    data = data 
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading users data");
                return Json(new { error = "An error occurred while loading users data." });
            }
        }

        [HttpGet]
        [Route("Details/{id}")]
        public IActionResult Details(Guid id)
        {
            var u = _service.GetById(id);
            if (u == null) return NotFound();

            var designations = _lookup.GetDesignations();
            var companies = _lookup.GetCompanies();
            var schools = _lookup.GetSchools();
            var roles = _roles.GetAll().ToList();

            var vm = new UserDetailsDetailsViewModel
            {
                Id = u.Id,
                UserName = u.UserName ?? string.Empty,
                FullName = $"{u.FirstName} {u.LastName}".Trim(),
                EmailAddress = u.EmailAddress ?? string.Empty,
                RoleName = u.UserRoleId.HasValue ? (roles.FirstOrDefault(r => r.Id == u.UserRoleId.Value)?.Name ?? string.Empty) : string.Empty,
                DesignationName = designations.FirstOrDefault(d => d.Id == u.DesignationId)?.Name ?? string.Empty,
                CompanyName = u.CompanyId.HasValue ? (companies.FirstOrDefault(c => c.Id == u.CompanyId.Value)?.Name ?? string.Empty) : string.Empty,
                SchoolName = u.SchoolId.HasValue ? (schools.FirstOrDefault(s => s.Id == u.SchoolId.Value)?.Name ?? string.Empty) : string.Empty,
                IsSuperUser = u.IsSuperUser ?? false,
                IsActive = u.IsActive
            };
            return View(vm);
        }

        [HttpGet]
        [Route("Create")]
        public IActionResult Create()
        {
            var vm = new UserDetailsViewModel();
            var companyId = CurrentCompanyId;
            var schoolId = CurrentSchoolId;
            if (companyId.HasValue) vm.CompanyId = companyId;
            if (schoolId.HasValue) vm.SchoolId = schoolId;
            PopulateDropdowns(vm);
            return View(vm);
        }

        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(UserDetailsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                PopulateDropdowns(model);
                return View(model);
            }

            var userId = CurrentUserId;
            var companyId = CurrentCompanyId;
            var schoolId = CurrentSchoolId;
            if (!userId.HasValue || !companyId.HasValue || !schoolId.HasValue)
            {
                ModelState.AddModelError(string.Empty, "Please login to create user.");
                PopulateDropdowns(model);
                return View(model);
            }

            var entity = new UserDetails
            {
                Id = Guid.Empty,
                UserName = model.UserName,
                UserPassword = model.UserPassword,
                FirstName = model.FirstName,
                LastName = model.LastName,
                EmailAddress = model.EmailAddress,
                DesignationId = model.DesignationId,
                UserRoleId = model.UserRoleId,
                IsSuperUser = model.IsSuperUser ?? false,
                CompanyId = companyId,
                SchoolId = schoolId,
                IsActive = model.IsActive,
                CreatedBy = userId.Value,
                CreatedDate = DateTime.UtcNow
            };

            var newId = _service.Create(entity);
            if (newId == Guid.Empty)
            {
                ModelState.AddModelError(string.Empty, "Failed to create user.");
                PopulateDropdowns(model);
                return View(model);
            }
            return RedirectToAction("Details", new { id = newId });
        }

        [HttpGet]
        [Route("Edit/{id}")]
        public IActionResult Edit(Guid id)
        {
            var u = _service.GetById(id);
            if (u == null) return NotFound();

            var vm = new UserDetailsViewModel
            {
                Id = u.Id,
                UserName = u.UserName ?? string.Empty,
                UserPassword = u.UserPassword ?? string.Empty,
                FirstName = u.FirstName ?? string.Empty,
                LastName = u.LastName ?? string.Empty,
                EmailAddress = u.EmailAddress ?? string.Empty,
                DesignationId = u.DesignationId,
                UserRoleId = u.UserRoleId,
                IsSuperUser = u.IsSuperUser ?? false,
                CompanyId = u.CompanyId,
                SchoolId = u.SchoolId,
                IsActive = u.IsActive
            };
            PopulateDropdowns(vm);
            return View(vm);
        }

        [HttpPost]
        [Route("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, UserDetailsViewModel model)
        {
            if (id != model.Id) return BadRequest();
            if (!ModelState.IsValid)
            {
                PopulateDropdowns(model);
                return View(model);
            }

            var userId = CurrentUserId;
            var companyId = CurrentCompanyId;
            var schoolId = CurrentSchoolId;
            if (!userId.HasValue || !companyId.HasValue || !schoolId.HasValue)
            {
                ModelState.AddModelError(string.Empty, "Please login to update user.");
                PopulateDropdowns(model);
                return View(model);
            }

            var entity = new UserDetails
            {
                Id = id,
                UserName = model.UserName,
                UserPassword = model.UserPassword,
                FirstName = model.FirstName,
                LastName = model.LastName,
                EmailAddress = model.EmailAddress,
                DesignationId = model.DesignationId,
                UserRoleId = model.UserRoleId,
                IsSuperUser = model.IsSuperUser ?? false,
                CompanyId = companyId,
                SchoolId = schoolId,
                IsActive = model.IsActive,
                ModifiedBy = userId.Value,
                ModifiedDate = DateTime.UtcNow
            };

            if (!_service.Update(entity))
            {
                ModelState.AddModelError(string.Empty, "Failed to update user.");
                PopulateDropdowns(model);
                return View(model);
            }
            return RedirectToAction("Details", new { id });
        }

        [HttpGet]
        [Route("Delete/{id}")]
        public IActionResult Delete(Guid id)
        {
            var u = _service.GetById(id);
            if (u == null) return NotFound();
            return View(u);
        }

        [HttpPost]
        [Route("Delete/{id}")]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmDelete(Guid id)
        {
            if (!_service.Delete(id))
            {
                TempData["ErrorMessage"] = "Failed to delete user.";
                return RedirectToAction("Delete", new { id });
            }
            return RedirectToAction("Index");
        }
    }
}