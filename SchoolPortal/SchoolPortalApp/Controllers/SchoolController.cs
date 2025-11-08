using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SchoolPortal.Entities.Models;
using SchoolPortalApp.Models;
using SchoolPortal.Services.IServices;

namespace SchoolPortalApp.Controllers
{
	[Route("School")]
	public class SchoolController : Controller
	{
		private readonly ISchoolService _service;
		private readonly ILookupService _lookup;
		private readonly ISchoolContactService _contactService;
		private readonly ILogger<SchoolController> _logger;

		public SchoolController(ISchoolService service, ILookupService lookup, ISchoolContactService contactService, ILogger<SchoolController> logger)
		{
			_service = service;
			_lookup = lookup;
			_contactService = contactService;
			_logger = logger;
		}

		private void PopulateDropdowns(SchoolViewModel vm)
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
				vm.Cities = cities.Select(ci => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = ci.Id.ToString(), Text = ci.Name, Selected = ci.Id == vm.CityId || ci.Id == vm.JudistrictionCityId }).ToList();
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
			var countryDict = countries.ToDictionary(c => c.Id, c => c.Name);

			var stateCache = new Dictionary<Guid, List<LookupItem>>();
			var cityCache = new Dictionary<Guid, List<LookupItem>>();

			var contacts = _contactService.GetAll();
			var contactSchoolIds = new HashSet<Guid>(contacts.Select(c => c.SchoolId));

			var vmList = list.Select(item =>
			{
				string countryName = string.Empty;
				string stateName = string.Empty;
				string cityName = string.Empty;

				if (item.CountryId.HasValue && countryDict.TryGetValue(item.CountryId.Value, out var cName))
				{
					countryName = cName;

					if (item.StateId.HasValue)
					{
						if (!stateCache.TryGetValue(item.CountryId.Value, out var states))
						{
							states = _lookup.GetStates(item.CountryId.Value);
							stateCache[item.CountryId.Value] = states;
						}
						var st = states.FirstOrDefault(s => s.Id == item.StateId.Value);
						if (st != null)
						{
							stateName = st.Name;

							if (item.CityId.HasValue)
							{
								if (!cityCache.TryGetValue(item.StateId.Value, out var cities))
								{
									cities = _lookup.GetCities(item.StateId.Value);
									cityCache[item.StateId.Value] = cities;
								}
								var ct = cities.FirstOrDefault(ci => ci.Id == item.CityId.Value);
								if (ct != null) cityName = ct.Name;
							}
						}
					}
				}

				return new SchoolListItemViewModel
				{
					Id = item.Id,
					Name = item.Name,
					Email = item.Email,
					Address1 = item.Address1,
					Address2 = item.Address2,
					CityName = cityName,
					StateName = stateName,
					CountryName = countryName,
					Phone = item.Phone,
					EstablishmentYear = item.EstablishmentYear,
					HasContact = contactSchoolIds.Contains(item.Id)
				};
			}).ToList();

			return View(vmList);
		}

		[HttpGet]
		[Route("Details/{id}")]
		public IActionResult Details(Guid id)
		{
			var item = _service.GetById(id);
			if (item == null) return NotFound();
			var countries = _lookup.GetCountries();
			var countryDict = countries.ToDictionary(c => c.Id, c => c.Name);

			string countryName = string.Empty;
			string stateName = string.Empty;
			string cityName = string.Empty;

			if (item.CountryId.HasValue && countryDict.TryGetValue(item.CountryId.Value, out var cName))
			{
				countryName = cName;
				if (item.StateId.HasValue)
				{
					var states = _lookup.GetStates(item.CountryId.Value);
					var st = states.FirstOrDefault(s => s.Id == item.StateId.Value);
					if (st != null)
					{
						stateName = st.Name;
						if (item.CityId.HasValue)
						{
							var cities = _lookup.GetCities(item.StateId.Value);
							var ct = cities.FirstOrDefault(ci => ci.Id == item.CityId.Value);
							if (ct != null) cityName = ct.Name;
						}
					}
				}
			}

			string jurisCountryName = string.Empty;
			string jurisStateName = string.Empty;
			string jurisCityName = string.Empty;

			if (item.JudistrictionCountryId.HasValue && countryDict.TryGetValue(item.JudistrictionCountryId.Value, out var jcName))
			{
				jurisCountryName = jcName;
				if (item.JudistrictionStateId.HasValue)
				{
					var jStates = _lookup.GetStates(item.JudistrictionCountryId.Value);
					var jst = jStates.FirstOrDefault(s => s.Id == item.JudistrictionStateId.Value);
					if (jst != null)
					{
						jurisStateName = jst.Name;
						if (item.JudistrictionCityId.HasValue)
						{
							var jCities = _lookup.GetCities(item.JudistrictionStateId.Value);
							var jct = jCities.FirstOrDefault(ci => ci.Id == item.JudistrictionCityId.Value);
							if (jct != null) jurisCityName = jct.Name;
						}
					}
				}
			}

			var vm = new SchoolDetailsViewModel
			{
				Id = item.Id,
				Name = item.Name,
				Description = item.Description,
				Email = item.Email,
				Address1 = item.Address1,
				Address2 = item.Address2,
				CityName = cityName,
				StateName = stateName,
				CountryName = countryName,
				ZipCode = item.ZipCode,
				Phone = item.Phone,
				EstablishmentYear = item.EstablishmentYear,
				Mobile = item.Mobile,
				JudistrictionCityName = jurisCityName,
				JudistrictionStateName = jurisStateName,
				JudistrictionCountryName = jurisCountryName
			};

			return View(vm);
		}

		[HttpGet]
		[Route("Create")]
		public IActionResult Create()
		{
			var vm = new SchoolViewModel();
			PopulateDropdowns(vm);
			return View(vm);
		}

		[HttpPost]
		[Route("Create")]
		[ValidateAntiForgeryToken]
		public IActionResult Create(SchoolViewModel model)
		{
			if (!ModelState.IsValid)
			{
				PopulateDropdowns(model);
				return View(model);
			}
			var userIdStr = HttpContext.Session.GetString("UserId");
			var companyIdStr = HttpContext.Session.GetString("CompanyId");
			if (string.IsNullOrWhiteSpace(userIdStr) || !Guid.TryParse(userIdStr, out var userId) || string.IsNullOrWhiteSpace(companyIdStr) || !Guid.TryParse(companyIdStr, out var companyId))
			{
				ModelState.AddModelError(string.Empty, "Please login and select company to create school.");
				PopulateDropdowns(model);
				return View(model);
			}

			var entity = new SchoolMaster
			{
				Id = Guid.Empty,
				Name = model.Name,
				Description = model.Description ?? string.Empty,
				Email = model.Email ?? string.Empty,
				Address1 = model.Address1 ?? string.Empty,
				Address2 = model.Address2 ?? string.Empty,
				CityId = model.CityId,
				StateId = model.StateId,
				CountryId = model.CountryId,
				ZipCode = model.ZipCode ?? string.Empty,
				Phone = model.Phone ?? string.Empty,
				EstablishmentYear = model.EstablishmentYear ?? string.Empty,
				Mobile = model.Mobile ?? string.Empty,
				JudistrictionCityId = model.JudistrictionCityId,
				JudistrictionStateId = model.StateId,
				JudistrictionCountryId = model.CountryId,
				IsActive = model.IsActive,
				CompanyId = companyId,
				CreatedBy = userId,
				CreatedDate = DateTime.UtcNow
			};

			var newId = _service.Create(entity);
			if (newId == Guid.Empty)
			{
				ModelState.AddModelError(string.Empty, "Failed to create school.");
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
			var vm = new SchoolViewModel
			{
				Id = item.Id,
				Name = item.Name,
				Description = item.Description,
				Email = item.Email,
				Address1 = item.Address1,
				Address2 = item.Address2,
				CityId = item.CityId ?? Guid.Empty,
				StateId = item.StateId ?? Guid.Empty,
				CountryId = item.CountryId ?? Guid.Empty,
				ZipCode = item.ZipCode,
				EstablishmentYear = item.EstablishmentYear,
				JudistrictionCityId = item.JudistrictionCityId ?? Guid.Empty,
				IsActive = item.IsActive,
				Phone = item.Phone,
				Mobile = item.Mobile
			};
			PopulateDropdowns(vm);
			return View(vm);
		}

		[HttpPost]
		[Route("Edit/{id}")]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Guid id, SchoolViewModel model)
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
				ModelState.AddModelError(string.Empty, "Please login to update school.");
				PopulateDropdowns(model);
				return View(model);
			}

			var entity = new SchoolMaster
			{
				Id = id,
				Name = model.Name,
				Description = model.Description ?? string.Empty,
				Email = model.Email ?? string.Empty,
				Address1 = model.Address1 ?? string.Empty,
				Address2 = model.Address2 ?? string.Empty,
				CityId = model.CityId,
				StateId = model.StateId,
				CountryId = model.CountryId,
				ZipCode = model.ZipCode ?? string.Empty,
				Phone = model.Phone ?? string.Empty,
				EstablishmentYear = model.EstablishmentYear ?? string.Empty,
				Mobile = model.Mobile ?? string.Empty,
				JudistrictionCityId = model.JudistrictionCityId,
				JudistrictionStateId = model.StateId,
				JudistrictionCountryId = model.CountryId,
				IsActive = model.IsActive,
				ModifiedBy = userId,
				ModifiedDate = DateTime.UtcNow
			};

			if (!_service.Update(entity))
			{
				ModelState.AddModelError(string.Empty, "Failed to update school.");
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
				TempData["ErrorMessage"] = "Failed to delete school.";
				return RedirectToAction("Delete", new { id });
			}
			return RedirectToAction("Index");
		}

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
