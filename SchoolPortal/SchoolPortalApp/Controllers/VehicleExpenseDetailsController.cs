using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using SchoolPortalApp.Models;
using SchoolPortal.Services.IServices;
using SchoolPortal.Entities.Models;
using System;
using System.Linq;
using System.Collections.Generic;

namespace SchoolPortalApp.Controllers
{
    [Route("VehicleExpenseDetails")]
    public class VehicleExpenseDetailsController : Controller
    {
        private readonly IVehicleExpenseDetailsService _service;
        private readonly IVehicleMasterService _vehicleService;
        private readonly IVehicleTypeMasterService _vehicleTypeService;
        private readonly ICompanyService _companyService;
        private readonly ISchoolService _schoolService;

        public VehicleExpenseDetailsController(
            IVehicleExpenseDetailsService service,
            IVehicleMasterService vehicleService,
            IVehicleTypeMasterService vehicleTypeService,
            ICompanyService companyService,
            ISchoolService schoolService)
        {
            _service = service;
            _vehicleService = vehicleService;
            _vehicleTypeService = vehicleTypeService;
            _companyService = companyService;
            _schoolService = schoolService;
        }

        private void PopulateDropdowns(VehicleExpenseDetailsViewModel vm)
        {
            // Populate Vehicles dropdown
            var vehicles = _vehicleService.GetAll();
            vm.Vehicles = vehicles.Select(v => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Value = v.Id.ToString(),
                Text = v.VehicleNumber
            }).ToList();

            // Populate VehicleTypes dropdown
            var vehicleTypes = _vehicleTypeService.GetAll();
            vm.VehicleTypes = vehicleTypes.Select(vt => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Value = vt.Id.ToString(),
                Text = vt.VehicleType
            }).ToList();

            // Populate Companies dropdown
            var companies = _companyService.GetAll();
            vm.Companies = companies.Select(c => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.CompanyName
            }).ToList();

            // Populate Schools dropdown
            var schools = _schoolService.GetAll();
            vm.Schools = schools.Select(s => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.SchoolName
            }).ToList();
        }

        [HttpGet]
        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            var list = _service.GetAll();

            // Enrich with related names
            var vehicles = _vehicleService.GetAll().ToDictionary(v => v.Id, v => v.VehicleNumber);
            var vehicleTypes = _vehicleTypeService.GetAll().ToDictionary(vt => vt.Id, vt => vt.VehicleType);
            var companies = _companyService.GetAll().ToDictionary(c => c.Id, c => c.CompanyName);
            var schools = _schoolService.GetAll().ToDictionary(s => s.Id, s => s.SchoolName);

            var result = list.Select(item =>
            {
                return new
                {
                    Id = item.Id,
                    Name = item.Name,
                    VehicleNumber = item.VehicleId != Guid.Empty && vehicles.ContainsKey(item.VehicleId) ? vehicles[item.VehicleId] : string.Empty,
                    VehicleType = item.VehicleTypeId != Guid.Empty && vehicleTypes.ContainsKey(item.VehicleTypeId) ? vehicleTypes[item.VehicleTypeId] : string.Empty,
                    ExpenseDate = item.ExpenseDate?.ToString("dd/MM/yyyy") ?? string.Empty,
                    ExpenseAmount = item.ExpenseAmount?.ToString("C") ?? "0.00",
                    CompanyName = item.CompanyId.HasValue && companies.ContainsKey(item.CompanyId.Value) ? companies[item.CompanyId.Value] : string.Empty,
                    SchoolName = item.SchoolId.HasValue && schools.ContainsKey(item.SchoolId.Value) ? schools[item.SchoolId.Value] : string.Empty,
                    IsActive = item.IsActive
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

            var vehicle = _vehicleService.GetById(item.VehicleId);
            var vehicleType = _vehicleTypeService.GetById(item.VehicleTypeId);
            var company = _companyService.GetById(item.CompanyId ?? Guid.Empty);
            var school = _schoolService.GetById(item.SchoolId ?? Guid.Empty);

            var vm = new
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                ExpenseDate = item.ExpenseDate?.ToString("dd/MM/yyyy") ?? string.Empty,
                ExpenseAmount = item.ExpenseAmount?.ToString("C") ?? "0.00",
                VehicleNumber = vehicle?.VehicleNumber ?? string.Empty,
                VehicleType = vehicleType?.VehicleType ?? string.Empty,
                CompanyName = company?.CompanyName ?? string.Empty,
                SchoolName = school?.SchoolName ?? string.Empty,
                IsActive = item.IsActive
            };

            return View(vm);
        }

        [HttpGet]
        [Route("Create")]
        public IActionResult Create()
        {
            var vm = new VehicleExpenseDetailsViewModel();
            // Prefill CompanyId and SchoolId from session
            var companyIdStr = HttpContext.Session.GetString("CompanyId");
            var schoolIdStr = HttpContext.Session.GetString("SchoolId");
            if (!string.IsNullOrWhiteSpace(companyIdStr) && Guid.TryParse(companyIdStr, out var companyId))
            {
                vm.CompanyId = companyId;
            }
            if (!string.IsNullOrWhiteSpace(schoolIdStr) && Guid.TryParse(schoolIdStr, out var schoolId))
            {
                vm.SchoolId = schoolId;
            }
            PopulateDropdowns(vm);
            return View(vm);
        }

        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(VehicleExpenseDetailsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                PopulateDropdowns(model);
                return View(model);
            }

            var userIdStr = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrWhiteSpace(userIdStr) || !Guid.TryParse(userIdStr, out var userId))
            {
                ModelState.AddModelError(string.Empty, "Please login to create vehicle expense.");
                PopulateDropdowns(model);
                return View(model);
            }

            var entity = new VehicleExpenseDetails
            {
                Id = Guid.Empty,
                VehicleId = model.VehicleId,
                VehicleTypeId = model.VehicleTypeId,
                Name = model.Name,
                Description = model.Description ?? string.Empty,
                ExpenseDate = model.ExpenseDate,
                ExpenseAmount = model.ExpenseAmount,
                CompanyId = model.CompanyId,
                SchoolId = model.SchoolId,
                IsActive = model.IsActive,
                CreatedBy = userId,
                CreatedDate = DateTime.UtcNow,
                Status = "ACT",
                StatusMessage = "Active"
            };

            var newId = _service.Create(entity);
            if (newId == Guid.Empty)
            {
                ModelState.AddModelError(string.Empty, "Failed to create vehicle expense.");
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

            var vm = new VehicleExpenseDetailsViewModel
            {
                Id = item.Id,
                VehicleId = item.VehicleId,
                VehicleTypeId = item.VehicleTypeId,
                Name = item.Name,
                Description = item.Description,
                ExpenseDate = item.ExpenseDate,
                ExpenseAmount = item.ExpenseAmount,
                CompanyId = item.CompanyId ?? Guid.Empty,
                SchoolId = item.SchoolId ?? Guid.Empty,
                IsActive = item.IsActive
            };

            PopulateDropdowns(vm);
            return View(vm);
        }

        [HttpPost]
        [Route("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, VehicleExpenseDetailsViewModel model)
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
                ModelState.AddModelError(string.Empty, "Please login to update vehicle expense.");
                PopulateDropdowns(model);
                return View(model);
            }

            var entity = new VehicleExpenseDetails
            {
                Id = id,
                VehicleId = model.VehicleId,
                VehicleTypeId = model.VehicleTypeId,
                Name = model.Name,
                Description = model.Description ?? string.Empty,
                ExpenseDate = model.ExpenseDate,
                ExpenseAmount = model.ExpenseAmount,
                CompanyId = model.CompanyId,
                SchoolId = model.SchoolId,
                IsActive = model.IsActive,
                ModifiedBy = userId,
                ModifiedDate = DateTime.UtcNow
            };

            if (!_service.Update(entity))
            {
                ModelState.AddModelError(string.Empty, "Failed to update vehicle expense.");
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

            var vehicle = _vehicleService.GetById(item.VehicleId);
            var vehicleType = _vehicleTypeService.GetById(item.VehicleTypeId);
            var company = _companyService.GetById(item.CompanyId ?? Guid.Empty);
            var school = _schoolService.GetById(item.SchoolId ?? Guid.Empty);

            var vm = new
            {
                Id = item.Id,
                Name = item.Name,
                VehicleNumber = vehicle?.VehicleNumber ?? string.Empty,
                VehicleType = vehicleType?.VehicleType ?? string.Empty,
                CompanyName = company?.CompanyName ?? string.Empty,
                SchoolName = school?.SchoolName ?? string.Empty,
                ExpenseAmount = item.ExpenseAmount?.ToString("C") ?? "0.00"
            };

            return View(vm);
        }

        [HttpPost]
        [Route("Delete/{id}")]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmDelete(Guid id)
        {
            if (!_service.Delete(id))
            {
                TempData["ErrorMessage"] = "Failed to delete vehicle expense.";
                return RedirectToAction("Delete", new { id });
            }

            return RedirectToAction("Index");
        }
    }
}