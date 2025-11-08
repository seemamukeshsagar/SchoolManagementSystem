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
	[Route("Company")]
	public class CompanyController : Controller
	{
		private readonly ICompanyService _service;
		private readonly ILookupService _lookup;
		private readonly ILogger<CompanyController> _logger;

		public CompanyController(ICompanyService service, ILookupService lookup, ILogger<CompanyController> logger)
		{
			_service = service;
			_lookup = lookup;
			_logger = logger;
		}

		private void PopulateDropdowns(CompanyViewModel vm)
		{
			var countries = _lookup.GetCountries();
			vm.Countries = countries.Select(c => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = c.Id.ToString(), Text = c.Name, Selected = c.Id == vm.CountryId }).ToList();

			if (vm.CountryId != Guid.Empty)
			{
				var states = _lookup.GetStates(vm.CountryId);
				vm.States = states.Select(s => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = s.Id.ToString(), Text = s.Name, Selected = s.Id == vm.StateId }).ToList();
			}
			else
			{
				vm.States = Array.Empty<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
			}

			if (vm.StateId != Guid.Empty)
			{
				var cities = _lookup.GetCities(vm.StateId);
				vm.Cities = cities.Select(ci => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = ci.Id.ToString(), Text = ci.Name, Selected = ci.Id == vm.CityId || ci.Id == vm.JudistrictionArea }).ToList();
			}
			else
			{
				vm.Cities = Array.Empty<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
			}
		}

		[HttpGet]
		[Route("")]
		[Route("Index")]
		public IActionResult Index()
		{
			var list = _service.GetAll();
			var countries = _lookup.GetCountries();
			var result = list.Select(item =>
			{
				var country = countries.FirstOrDefault(c => c.Id == item.CountryId);
				var states = _lookup.GetStates(item.CountryId);
				var state = states.FirstOrDefault(s => s.Id == item.StateId);
				var cities = _lookup.GetCities(item.StateId);
				var city = cities.FirstOrDefault(ci => ci.Id == item.CityId);
				var juris = cities.FirstOrDefault(ci => ci.Id == item.JudistrictionArea);
				return new CompanyListItemViewModel
				{
					Id = item.Id,
					CompanyName = item.CompanyName,
					Email = item.Email,
					IsActive = item.IsActive,
					ZipCode = item.ZipCode,
					EstablishmentYear = item.EstablishmentYear,
					CountryName = country?.Name ?? string.Empty,
					StateName = state?.Name ?? string.Empty,
					CityName = city?.Name ?? string.Empty,
					JurisdictionAreaName = juris?.Name ?? string.Empty,
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
			var juris = cities.FirstOrDefault(ci => ci.Id == item.JudistrictionArea);

			var vm = new CompanyDetailsViewModel
			{
				Id = item.Id,
				CompanyName = item.CompanyName ?? string.Empty,
				Email = item.Email ?? string.Empty,
				IsActive = item.IsActive,
				Address = item.Address ?? string.Empty,
				ZipCode = item.ZipCode ?? string.Empty,
				EstablishmentYear = item.EstablishmentYear ?? string.Empty,
				CountryName = country?.Name ?? string.Empty,
				StateName = state?.Name ?? string.Empty,
				CityName = city?.Name ?? string.Empty,
				JurisdictionAreaName = juris?.Name ?? string.Empty,
			};
			return View(vm);
		}

		[HttpGet]
		[Route("Create")]
		public IActionResult Create()
		{
			var vm = new CompanyViewModel();
			PopulateDropdowns(vm);
			return View(vm);
		}

		[HttpPost]
		[Route("Create")]
		[ValidateAntiForgeryToken]
		public IActionResult Create(CompanyViewModel model)
		{
			if (!ModelState.IsValid)
			{
				PopulateDropdowns(model);
				return View(model);
			}
			var userIdStr = HttpContext.Session.GetString("UserId");
			if (string.IsNullOrWhiteSpace(userIdStr) || !Guid.TryParse(userIdStr, out var userId))
			{
				ModelState.AddModelError(string.Empty, "Please login to create company.");
				PopulateDropdowns(model);
				return View(model);
			}

			var entity = new CompanyMaster
			{
				Id = Guid.Empty,
				CompanyName = model.CompanyName,
				Description = model.Description ?? string.Empty,
				Address = model.Address ?? string.Empty,
				CityId = model.CityId,
				StateId = model.StateId,
				CountryId = model.CountryId,
				ZipCode = model.ZipCode ?? string.Empty,
				Email = model.Email ?? string.Empty,
				IsActive = model.IsActive,
				CreatedBy = userId,
				CreatedDate = DateTime.UtcNow,
				EstablishmentYear = model.EstablishmentYear ?? string.Empty,
				JudistrictionArea = model.JudistrictionArea,
			};

			var newId = _service.Create(entity);
			if (newId == Guid.Empty)
			{
				ModelState.AddModelError(string.Empty, "Failed to create company.");
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
			var vm = new CompanyViewModel
			{
				Id = item.Id,
				CompanyName = item.CompanyName,
				Description = item.Description,
				Address = item.Address,
				CityId = item.CityId,
				StateId = item.StateId,
				CountryId = item.CountryId,
				ZipCode = item.ZipCode,
				Email = item.Email,
				IsActive = item.IsActive,
				EstablishmentYear = item.EstablishmentYear,
				JudistrictionArea = item.JudistrictionArea,
			};
			PopulateDropdowns(vm);
			return View(vm);
		}

		[HttpPost]
		[Route("Edit/{id}")]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Guid id, CompanyViewModel model)
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
				ModelState.AddModelError(string.Empty, "Please login to update company.");
				PopulateDropdowns(model);
				return View(model);
			}

			var entity = new CompanyMaster
			{
				Id = id,
				CompanyName = model.CompanyName,
				Description = model.Description ?? string.Empty,
				Address = model.Address ?? string.Empty,
				CityId = model.CityId,
				StateId = model.StateId,
				CountryId = model.CountryId,
				ZipCode = model.ZipCode ?? string.Empty,
				Email = model.Email ?? string.Empty,
				IsActive = model.IsActive,
				ModifiedBy = userId,
				ModifiedDate = DateTime.UtcNow,
				EstablishmentYear = model.EstablishmentYear ?? string.Empty,
				JudistrictionArea = model.JudistrictionArea,
			};

			if (!_service.Update(entity))
			{
				ModelState.AddModelError(string.Empty, "Failed to update company.");
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
				TempData["ErrorMessage"] = "Failed to delete company.";
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
