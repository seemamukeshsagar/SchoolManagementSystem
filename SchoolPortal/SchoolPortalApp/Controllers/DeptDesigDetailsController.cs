using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Schoolortal.Entities.Models;
using SchoolPortalApp.Models;
using SchoolPortal.Services.IServices;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SchoolPortalApp.Controllers
{
    [Route("DeptDesigDetails")]
    public class DeptDesigDetailsController : Controller
    {
        private readonly IDeptDesigDetailsService _service;
        private readonly ILookupService _lookup;
        private readonly ILogger<DeptDesigDetailsController> _logger;
        private const string DefaultStatus = "Active";

        public DeptDesigDetailsController(
            IDeptDesigDetailsService service, 
            ILookupService lookup, 
            ILogger<DeptDesigDetailsController> logger)
        {
            _service = service;
            _lookup = lookup;
            _logger = logger;
        }

        private void PopulateDropdowns(DeptDesigDetailsViewModel vm)
        {
            // Populate dropdowns with data from lookup service
            vm.Departments = _lookup.GetDepartments()
                .Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name })
                .ToList();

            vm.Designations = _lookup.GetDesignations()
                .Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name })
                .ToList();

            vm.Companies = _lookup.GetCompanies()
                .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
                .ToList();

            vm.Schools = _lookup.GetSchools()
                .Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Name })
                .ToList();
        }

        [HttpGet]
        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            var list = _service.GetAll();
            var departments = _lookup.GetDepartments();
            var designations = _lookup.GetDesignations();
            var companies = _lookup.GetCompanies();
            var schools = _lookup.GetSchools();

            var result = list.Select(item =>
            {
                var department = departments.FirstOrDefault(d => d.Id == item.DepartmentId);
                var designation = designations.FirstOrDefault(d => d.Id == item.DesignationId);
                var company = companies.FirstOrDefault(c => c.Id == item.CompanyId);
                var school = schools.FirstOrDefault(s => s.Id == item.SchoolId);

                return new DeptDesigDetailsListItemViewModel
                {
                    Id = item.Id,
                    DepartmentName = department?.Name ?? "N/A",
                    DesignationName = designation?.Name ?? "N/A",
                    CompanyName = company?.Name ?? "N/A",
                    SchoolName = school?.Name ?? "N/A",
                    IsActive = item.IsActive,
                    Status = item.Status ?? DefaultStatus,
                    StatusMessage = item.StatusMessage ?? string.Empty
                };
            }).ToList();

            return View(result);
        }

        [HttpGet]
        [Route("Details/{id}")]
        public IActionResult Details(Guid id)
        {
            var item = _service.GetById(id);
            if (item == null) return NotFound();

            var departments = _lookup.GetDepartments();
            var designations = _lookup.GetDesignations();
            var companies = _lookup.GetCompanies();
            var schools = _lookup.GetSchools();

            var vm = new DeptDesigDetailsViewModel
            {
                Id = item.Id,
                DepartmentId = item.DepartmentId,
                DesignationId = item.DesignationId,
                CompanyId = item.CompanyId,
                SchoolId = item.SchoolId,
                IsActive = item.IsActive,
                Status = item.Status ?? DefaultStatus,
                StatusMessage = item.StatusMessage ?? string.Empty
            };

            PopulateDropdowns(vm);
            return View(vm);
        }

        [HttpGet]
        [Route("Create")]
        public IActionResult Create()
        {
            var vm = new DeptDesigDetailsViewModel 
            { 
                IsActive = true,
                Status = DefaultStatus,
                StatusMessage = string.Empty
            };
            PopulateDropdowns(vm);
            return View(vm);
        }

        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DeptDesigDetailsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                PopulateDropdowns(model);
                return View(model);
            }

            var userIdStr = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrWhiteSpace(userIdStr) || !Guid.TryParse(userIdStr, out var userId))
            {
                ModelState.AddModelError(string.Empty, "Please login to create department designation details.");
                PopulateDropdowns(model);
                return View(model);
            }

            var entity = new DeptDesigDetails
            {
                Id = Guid.Empty,
                DepartmentId = model.DepartmentId,
                DesignationId = model.DesignationId,
                CompanyId = model.CompanyId,
                SchoolId = model.SchoolId,
                IsActive = model.IsActive,
                CreatedBy = userId,
                CreatedDate = DateTime.UtcNow,
                Status = model.Status ?? DefaultStatus,
                StatusMessage = model.StatusMessage ?? string.Empty
            };

            var newId = _service.Create(entity);
            if (newId == Guid.Empty)
            {
                ModelState.AddModelError(string.Empty, "Failed to create department designation details.");
                PopulateDropdowns(model);
                return View(model);
            }

            return RedirectToAction("Details", new { id = newId });
        }

        [HttpGet]
        [Route("Edit/{id}")]
        public IActionResult Edit(Guid id)
        {
            var item = _service.GetById(id);
            if (item == null) return NotFound();

            var vm = new DeptDesigDetailsViewModel
            {
                Id = item.Id,
                DepartmentId = item.DepartmentId,
                DesignationId = item.DesignationId,
                CompanyId = item.CompanyId,
                SchoolId = item.SchoolId,
                IsActive = item.IsActive,
                Status = item.Status ?? DefaultStatus,
                StatusMessage = item.StatusMessage ?? string.Empty
            };

            PopulateDropdowns(vm);
            return View(vm);
        }

        [HttpPost]
        [Route("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, DeptDesigDetailsViewModel model)
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
                ModelState.AddModelError(string.Empty, "Please login to update department designation details.");
                PopulateDropdowns(model);
                return View(model);
            }

            var existingItem = _service.GetById(id);
            if (existingItem == null) return NotFound();

            var entity = new DeptDesigDetails
            {
                Id = id,
                DepartmentId = model.DepartmentId,
                DesignationId = model.DesignationId,
                CompanyId = model.CompanyId,
                SchoolId = model.SchoolId,
                IsActive = model.IsActive,
                CreatedBy = existingItem.CreatedBy,
                CreatedDate = existingItem.CreatedDate,
                ModifiedBy = userId,
                ModifiedDate = DateTime.UtcNow,
                Status = model.Status ?? DefaultStatus,
                StatusMessage = model.StatusMessage ?? string.Empty
            };

            if (!_service.Update(entity))
            {
                ModelState.AddModelError(string.Empty, "Failed to update department designation details.");
                PopulateDropdowns(model);
                return View(model);
            }

            return RedirectToAction("Details", new { id });
        }

        [HttpGet]
        [Route("Delete/{id}")]
        public IActionResult Delete(Guid id)
        {
            var item = _service.GetById(id);
            if (item == null) return NotFound();

            var departments = _lookup.GetDepartments();
            var designations = _lookup.GetDesignations();
            var companies = _lookup.GetCompanies();
            var schools = _lookup.GetSchools();

            var department = departments.FirstOrDefault(d => d.Id == item.DepartmentId);
            var designation = designations.FirstOrDefault(d => d.Id == item.DesignationId);
            var company = companies.FirstOrDefault(c => c.Id == item.CompanyId);
            var school = schools.FirstOrDefault(s => s.Id == item.SchoolId);

            ViewBag.DepartmentName = department?.Name ?? "N/A";
            ViewBag.DesignationName = designation?.Name ?? "N/A";
            ViewBag.CompanyName = company?.Name ?? "N/A";
            ViewBag.SchoolName = school?.Name ?? "N/A";

            return View(item);
        }

        [HttpPost]
        [Route("Delete/{id}")]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmDelete(Guid id)
        {
            if (!_service.Delete(id))
            {
                TempData["ErrorMessage"] = "Failed to delete department designation details.";
                return RedirectToAction("Delete", new { id });
            }

            return RedirectToAction("Index");
        }
    }
}