using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SchoolPortal.Entities.Models;
using SchoolPortalApp.Models;
using SchoolPortal.Services.IServices;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SchoolPortalApp.Controllers
{
	[Route("Vendor")]
	public class VendorController : Controller
	{
		private readonly IVendorService _service;
		private readonly ILookupService _lookup;
		private readonly ISchoolService _schoolService;
		private readonly ICompanyService _companyService;
		private readonly ILogger<VendorController> _logger;

		public VendorController(IVendorService service, ILookupService lookup, ISchoolService schoolService, ICompanyService companyService, ILogger<VendorController> logger)
		{
			_service = service;
			_lookup = lookup;
			_schoolService = schoolService;
			_companyService = companyService;
			_logger = logger;
		}

		private void PopulateDropdowns(VendorViewModel vm)
		{
			var countries = _lookup.GetCountries();
			vm.Countries = countries.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name, Selected = c.Id == vm.CountryId }).ToList();

			if (vm.CountryId != Guid.Empty)
			{
				var states = _lookup.GetStates(vm.CountryId);
				vm.States = states.Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Name, Selected = s.Id == vm.StateId }).ToList();
			}
			else
			{
				vm.States = Array.Empty<SelectListItem>();
			}

			if (vm.StateId != Guid.Empty)
			{
				var cities = _lookup.GetCities(vm.StateId);
				vm.Cities = cities.Select(ci => new SelectListItem { Value = ci.Id.ToString(), Text = ci.Name, Selected = ci.Id == vm.CityId }).ToList();
			}
			else
			{
				vm.Cities = Array.Empty<SelectListItem>();
			}

			var companies = _lookup.GetCompanies();
			vm.Companies = companies.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name, Selected = c.Id == vm.CompanyId }).ToList();

			var schools = _schoolService.GetAll();
			vm.Schools = schools.Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Name, Selected = s.Id == vm.SchoolId }).ToList();
		}

		[HttpGet]
		[Route("")]
		[Route("Index")]
		public IActionResult Index()
		{
			var list = _service.GetAll();
			var countries = _lookup.GetCountries();
			var companies = _lookup.GetCompanies();
			var schools = _schoolService.GetAll();
			var result = list.Select(item =>
			{
				var country = countries.FirstOrDefault(c => c.Id == item.CountryId);
				var states = _lookup.GetStates(item.CountryId);
				var state = states.FirstOrDefault(s => s.Id == item.StateId);
				var cities = _lookup.GetCities(item.StateId);
				var city = cities.FirstOrDefault(ci => ci.Id == item.CityId);
				var company = companies.FirstOrDefault(c => c.Id == item.CompanyId);
				var school = schools.FirstOrDefault(s => s.Id == item.SchoolId);
				return new VendorListItemViewModel
				{
					Id = item.Id,
					VendorName = item.VendorName,
					EmailId = item.EmailId,
					IsActive = item.IsActive,
					ZipCode = item.ZipCode,
					CountryName = country?.Name ?? string.Empty,
					StateName = state?.Name ?? string.Empty,
					CityName = city?.Name ?? string.Empty,
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

			var countries = _lookup.GetCountries();
			var country = countries.FirstOrDefault(c => c.Id == item.CountryId);
			var states = _lookup.GetStates(item.CountryId);
			var state = states.FirstOrDefault(s => s.Id == item.StateId);
			var cities = _lookup.GetCities(item.StateId);
			var city = cities.FirstOrDefault(ci => ci.Id == item.CityId);
			var company = _companyService.GetById(item.CompanyId ?? Guid.Empty);
			var school = _schoolService.GetById(item.SchoolId ?? Guid.Empty);

			var vm = new VendorDetailsViewModel
			{
				Id = item.Id,
				VendorName = item.VendorName ?? string.Empty,
				EmailId = item.EmailId ?? string.Empty,
				IsActive = item.IsActive,
				Address1 = item.Address1 ?? string.Empty,
				Address2 = item.Address2 ?? string.Empty,
				ZipCode = item.ZipCode ?? string.Empty,
				ContactNumber = item.ContactNumber ?? string.Empty,
				MobileNumber = item.MobileNumber ?? string.Empty,
				CountryName = country?.Name ?? string.Empty,
				StateName = state?.Name ?? string.Empty,
				CityName = city?.Name ?? string.Empty,
				CompanyName = company?.CompanyName ?? string.Empty,
				SchoolName = school?.Name ?? string.Empty,
			};
			return View(vm);
		}

		[HttpGet]
		[Route("Create")]
		public IActionResult Create()
		{
			var vm = new VendorViewModel();
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
		public IActionResult Create(VendorViewModel model)
		{
			if (!ModelState.IsValid)
			{
				PopulateDropdowns(model);
				return View(model);
			}
			var userIdStr = HttpContext.Session.GetString("UserId");
			if (string.IsNullOrWhiteSpace(userIdStr) || !Guid.TryParse(userIdStr, out var userId))
			{
				ModelState.AddModelError(string.Empty, "Please login to create vendor.");
				PopulateDropdowns(model);
				return View(model);
			}

			var entity = new VendorMaster
			{
				Id = Guid.Empty,
				VendorName = model.VendorName,
				Description = model.Description ?? string.Empty,
				Address1 = model.Address1 ?? string.Empty,
				Address2 = model.Address2 ?? string.Empty,
				CityId = model.CityId,
				StateId = model.StateId,
				CountryId = model.CountryId,
				ZipCode = model.ZipCode ?? string.Empty,
				ContactNumber = model.ContactNumber ?? string.Empty,
				MobileNumber = model.MobileNumber ?? string.Empty,
				EmailId = model.EmailId ?? string.Empty,
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
				ModelState.AddModelError(string.Empty, "Failed to create vendor.");
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
			var vm = new VendorViewModel
			{
				Id = item.Id,
				VendorName = item.VendorName,
				Description = item.Description,
				Address1 = item.Address1,
				Address2 = item.Address2,
				CityId = item.CityId,
				StateId = item.StateId,
				CountryId = item.CountryId,
				ZipCode = item.ZipCode,
				ContactNumber = item.ContactNumber,
				MobileNumber = item.MobileNumber,
				EmailId = item.EmailId,
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
		public IActionResult Edit(Guid id, VendorViewModel model)
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
				ModelState.AddModelError(string.Empty, "Please login to update vendor.");
				PopulateDropdowns(model);
				return View(model);
			}

			var entity = new VendorMaster
			{
				Id = id,
				VendorName = model.VendorName,
				Description = model.Description ?? string.Empty,
				Address1 = model.Address1 ?? string.Empty,
				Address2 = model.Address2 ?? string.Empty,
				CityId = model.CityId,
				StateId = model.StateId,
				CountryId = model.CountryId,
				ZipCode = model.ZipCode ?? string.Empty,
				ContactNumber = model.ContactNumber ?? string.Empty,
				MobileNumber = model.MobileNumber ?? string.Empty,
				EmailId = model.EmailId ?? string.Empty,
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
				ModelState.AddModelError(string.Empty, "Failed to update vendor.");
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
				TempData["ErrorMessage"] = "Failed to delete vendor.";
				return RedirectToAction("Delete", new { id });
			}
			return RedirectToAction("Index");
		}

		// JSON endpoints for cascading dropdowns
		[HttpGet]
		[Route("GetStates")]
		public IActionResult GetStates(Guid countryId)
		{
			var list = _lookup.GetStates(countryId).Select(s => new { id = s.Id, name = s.Name });
			return Ok(list);
		}

		[HttpGet]
		[Route("GetCities")]
		public IActionResult GetCities(Guid stateId)
		{
			var list = _lookup.GetCities(stateId).Select(c => new { id = c.Id, name = c.Name });
			return Ok(list);
		}
	}
}