using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SchoolPortal.Entities.Models;
using SchoolPortalApp.Models;
using SchoolPortal.Services.IServices;

namespace SchoolPortalApp.Controllers
{
    [Route("UserDetails")]
    public class UserDetailsController : Controller
    {
        private readonly IUserDetailsService _service;
        private readonly ILookupService _lookup;
        private readonly IRoleMasterService _roles;
        private readonly ILogger<UserDetailsController> _logger;

        public UserDetailsController(
            IUserDetailsService service,
            ILookupService lookup,
            IRoleMasterService roles,
            ILogger<UserDetailsController> logger)
        {
            _service = service;
            _lookup = lookup;
            _roles = roles;
            _logger = logger;
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
            var list = _service.GetAll();
            var designations = _lookup.GetDesignations();
            var companies = _lookup.GetCompanies();
            var schools = _lookup.GetSchools();
            var roles = _roles.GetAll().ToList();

            var result = list.Select(u =>
            {
                var desigName = designations.FirstOrDefault(d => d.Id == u.DesignationId)?.Name ?? string.Empty;
                var roleName = u.UserRoleId.HasValue ? (roles.FirstOrDefault(r => r.Id == u.UserRoleId.Value)?.Name ?? string.Empty) : string.Empty;
                var companyName = u.CompanyId.HasValue ? (companies.FirstOrDefault(c => c.Id == u.CompanyId.Value)?.Name ?? string.Empty) : string.Empty;
                var schoolName = u.SchoolId.HasValue ? (schools.FirstOrDefault(s => s.Id == u.SchoolId.Value)?.Name ?? string.Empty) : string.Empty;

                return new UserDetailsListItemViewModel
                {
                    Id = u.Id,
                    UserName = u.UserName ?? string.Empty,
                    FullName = $"{u.FirstName} {u.LastName}".Trim(),
                    EmailAddress = u.EmailAddress ?? string.Empty,
                    RoleName = roleName,
                    DesignationName = desigName,
                    CompanyName = companyName,
                    SchoolName = schoolName,
                    IsActive = u.IsActive
                };
            }).ToList();

            return View(result);
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

            var userIdStr = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrWhiteSpace(userIdStr) || !Guid.TryParse(userIdStr, out var userId))
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
                CompanyId = model.CompanyId,
                SchoolId = model.SchoolId,
                IsActive = model.IsActive,
                CreatedBy = userId,
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

            var userIdStr = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrWhiteSpace(userIdStr) || !Guid.TryParse(userIdStr, out var userId))
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
                CompanyId = model.CompanyId,
                SchoolId = model.SchoolId,
                IsActive = model.IsActive,
                ModifiedBy = userId,
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