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
	[Route("VehicleMaster")]
	public class VehicleMasterController : Controller
	{
		private readonly IVehicleMasterService _service;
		private readonly IVehicleTypeMasterService _vehicleTypeService;
		private readonly ILookupService _lookup;
		private readonly ISchoolService _schoolService;
		private readonly ICompanyService _companyService;
		private readonly ILogger<VehicleMasterController> _logger;

		public VehicleMasterController(IVehicleMasterService service, IVehicleTypeMasterService vehicleTypeService, ILookupService lookup, ISchoolService schoolService, ICompanyService companyService, ILogger<VehicleMasterController> logger)
		{
			_service = service;
			_vehicleTypeService = vehicleTypeService;
			_lookup = lookup;
			_schoolService = schoolService;
			_companyService = companyService;
			_logger = logger;
		}

		private void PopulateDropdowns(VehicleMasterViewModel vm)
		{
			var companies = _lookup.GetCompanies();
			vm.Companies = companies.Select(c => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = c.Id.ToString(), Text = c.Name, Selected = c.Id == vm.CompanyId }).ToList();

			var schools = _schoolService.GetAll();
			vm.Schools = schools.Select(s => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = s.Id.ToString(), Text = s.Name, Selected = s.Id == vm.SchoolId }).ToList();

			var vehicleTypes = _vehicleTypeService.GetAll();
			vm.VehicleTypes = vehicleTypes.Select(vt => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = vt.Id.ToString(), Text = vt.VehicleType, Selected = vt.Id == vm.VehicleTypeId }).ToList();
		}

		[HttpGet]
		[Route("")]
		[Route("Index")]
		public IActionResult Index()
		{
			var list = _service.GetAll();
			var companies = _lookup.GetCompanies();
			var schools = _schoolService.GetAll();
			var vehicleTypes = _vehicleTypeService.GetAll();
			var result = list.Select(item =>
			{
				var company = companies.FirstOrDefault(c => c.Id == item.CompanyId);
				var school = schools.FirstOrDefault(s => s.Id == item.SchoolId);
				var vehicleType = vehicleTypes.FirstOrDefault(vt => vt.Id == item.VehicleTypeId);
				return new VehicleMasterListItemViewModel
				{
					Id = item.Id,
					VehicleNumber = item.VehicleNumber,
					VehicleModel = item.VehicleModel,
					VehicleMake = item.VehicleMake,
					VehicleTypeName = vehicleType?.VehicleType ?? string.Empty,
					RegistrationNumber = item.RegistrationNumber,
					IsActive = item.IsActive,
					CompanyName = company?.Name ?? string.Empty,
					SchoolName = school?.Name ?? string.Empty,
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

			var company = _companyService.GetById(item.CompanyId ?? Guid.Empty);
			var school = _schoolService.GetById(item.SchoolId ?? Guid.Empty);
			var vehicleType = _vehicleTypeService.GetById(item.VehicleTypeId);

			var vm = new VehicleMasterDetailsViewModel
			{
				Id = item.Id,
				VehicleNumber = item.VehicleNumber ?? string.Empty,
				VehicleModel = item.VehicleModel ?? string.Empty,
				VehicleMake = item.VehicleMake ?? string.Empty,
				VehicleTypeName = vehicleType?.VehicleType ?? string.Empty,
				RegistrationNumber = item.RegistrationNumber ?? string.Empty,
				InsuranceCompany = item.InsuranceCompany ?? string.Empty,
				InsurancePremium = item.InsurancePremium,
				SeatingCapacity = item.SeatingCapacity,
				IsActive = item.IsActive,
				CompanyName = company?.CompanyName ?? string.Empty,
				SchoolName = school?.Name ?? string.Empty,
			};
			return View(vm);
		}

		[HttpGet]
		[Route("Create")]
		public IActionResult Create()
		{
			var vm = new VehicleMasterViewModel();
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
		public IActionResult Create(VehicleMasterViewModel model)
		{
			if (!ModelState.IsValid)
			{
				PopulateDropdowns(model);
				return View(model);
			}
			var userIdStr = HttpContext.Session.GetString("UserId");
			if (string.IsNullOrWhiteSpace(userIdStr) || !Guid.TryParse(userIdStr, out var userId))
			{
				ModelState.AddModelError(string.Empty, "Please login to create vehicle.");
				PopulateDropdowns(model);
				return View(model);
			}

			var entity = new VehicleMaster
			{
				Id = Guid.Empty,
				VehicleNumber = model.VehicleNumber,
				VehicleModel = model.VehicleModel,
				VehicleMake = model.VehicleMake,
				VehicleTypeId = model.VehicleTypeId,
				RegistrationNumber = model.RegistrationNumber,
				InsuranceCompany = model.InsuranceCompany ?? string.Empty,
				InsurancePremium = model.InsurancePremium,
				SeatingCapacity = model.SeatingCapacity,
				CompanyId = model.CompanyId,
				SchoolId = model.SchoolId,
				IsActive = model.IsActive,
				CreatedBy = userId,
				CreatedDate = DateTime.UtcNow,
				Status = string.Empty,
				StatusMessage = string.Empty,
			};

			var newId = _service.Create(entity);
			if (newId == Guid.Empty)
			{
				ModelState.AddModelError(string.Empty, "Failed to create vehicle.");
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
			var vm = new VehicleMasterViewModel
			{
				Id = item.Id,
				VehicleNumber = item.VehicleNumber,
				VehicleModel = item.VehicleModel,
				VehicleMake = item.VehicleMake,
				VehicleTypeId = item.VehicleTypeId,
				RegistrationNumber = item.RegistrationNumber,
				InsuranceCompany = item.InsuranceCompany,
				InsurancePremium = item.InsurancePremium,
				SeatingCapacity = item.SeatingCapacity,
				CompanyId = item.CompanyId ?? Guid.Empty,
				SchoolId = item.SchoolId ?? Guid.Empty,
				IsActive = item.IsActive,
			};
			PopulateDropdowns(vm);
			return View(vm);
		}

		[HttpPost]
		[Route("Edit/{id}")]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Guid id, VehicleMasterViewModel model)
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
				ModelState.AddModelError(string.Empty, "Please login to update vehicle.");
				PopulateDropdowns(model);
				return View(model);
			}

			var entity = new VehicleMaster
			{
				Id = id,
				VehicleNumber = model.VehicleNumber,
				VehicleModel = model.VehicleModel,
				VehicleMake = model.VehicleMake,
				VehicleTypeId = model.VehicleTypeId,
				RegistrationNumber = model.RegistrationNumber,
				InsuranceCompany = model.InsuranceCompany ?? string.Empty,
				InsurancePremium = model.InsurancePremium,
				SeatingCapacity = model.SeatingCapacity,
				CompanyId = model.CompanyId,
				SchoolId = model.SchoolId,
				IsActive = model.IsActive,
				ModifiedBy = userId,
				ModifiedDate = DateTime.UtcNow,
				Status = string.Empty,
				StatusMessage = string.Empty,
			};

			if (!_service.Update(entity))
			{
				ModelState.AddModelError(string.Empty, "Failed to update vehicle.");
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
				TempData["ErrorMessage"] = "Failed to delete vehicle.";
				return RedirectToAction("Delete", new { id });
			}
			return RedirectToAction("Index");
		}
	}
}