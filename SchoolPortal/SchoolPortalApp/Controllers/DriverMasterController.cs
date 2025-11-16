using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SchoolPortal.Entities.Models;
using SchoolPortal.Services.IServices;
using SchoolPortalApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Collections.Generic;

namespace SchoolPortalApp.Controllers
{
	[Route("DriverMaster")]
	public class DriverMasterController : Controller
	{
		private readonly IDriverMasterService _service;
		private readonly ISchoolService _schoolService;
		private readonly ILookupService _lookup;
		private readonly IDriverDocumentDetailsService _docService;
		private readonly IDriverQualificationDetailsService _qualService;
		private readonly IWebHostEnvironment _env;
		private readonly ILogger<DriverMasterController> _logger;

		public DriverMasterController(IDriverMasterService service, ISchoolService schoolService, ILookupService lookup, IDriverDocumentDetailsService docService, IDriverQualificationDetailsService qualService, IWebHostEnvironment env, ILogger<DriverMasterController> logger)
		{
			_service = service;
			_schoolService = schoolService;
			_lookup = lookup;
			_docService = docService;
			_qualService = qualService;
			_env = env;
			_logger = logger;
		}

		private void PopulateQualifications(Guid selectedId)
		{
			var list = _lookup.GetQualifications() ?? new System.Collections.Generic.List<LookupItem>();
			ViewBag.Qualifications = list.Select(q => new SelectListItem
			{
				Value = q.Id.ToString(),
				Text = q.Name,
				Selected = q.Id == selectedId
			}).ToList();
		}

		private void PopulateLocationLists(DriverMaster model)
		{
			var countries = _lookup.GetCountries() ?? new System.Collections.Generic.List<LookupItem>();
			ViewBag.Countries = countries.Select(c => new SelectListItem
			{
				Value = c.Id.ToString(),
				Text = c.Name,
				Selected = c.Id == model.CountryId
			}).ToList();

			var states = model.CountryId != Guid.Empty ? (_lookup.GetStates(model.CountryId) ?? new System.Collections.Generic.List<LookupItem>()) : new System.Collections.Generic.List<LookupItem>();
			ViewBag.States = states.Select(s => new SelectListItem
			{
				Value = s.Id.ToString(),
				Text = s.Name,
				Selected = s.Id == model.StateId
			}).ToList();

			var cities = model.StateId != Guid.Empty ? (_lookup.GetCities(model.StateId) ?? new System.Collections.Generic.List<LookupItem>()) : new System.Collections.Generic.List<LookupItem>();
			ViewBag.Cities = cities.Select(ci => new SelectListItem
			{
				Value = ci.Id.ToString(),
				Text = ci.Name,
				Selected = ci.Id == model.CityId
			}).ToList();
		}

		[HttpGet]
		[Route("")]
		[Route("Index")]
		public IActionResult Index()
		{
			var list = _service.GetAll();
			var schools = _schoolService.GetAll();
			var result = list.Select(item =>
			{
				var school = schools.FirstOrDefault(s => s.Id == item.SchoolId);
				return new DriverListItemViewModel
				{
					Id = item.Id,
					Name = string.Join(" ", new[] { item.FirstName, item.LastName }.Where(x => !string.IsNullOrWhiteSpace(x))),
					MobileNumber = item.MobileNumber ?? string.Empty,
					PhoneNumber = item.PhoneNumber ?? string.Empty,
					IsActive = item.IsActive,
					SchoolName = school?.Name ?? string.Empty
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
			return View(item);
		}

		[HttpGet]
		[Route("Create")]
		public IActionResult Create()
		{
			var entity = new DriverMaster
			{
				IsActive = true,
				IsDeleted = false,
				Status = "INC",
				StatusMessage = "In Process....",
				CreatedDate = DateTime.UtcNow
			};
			var vm = new DriverAggregateViewModel { Master = entity };
			try
			{
				var quals = _lookup.GetQualifications() ?? new List<LookupItem>();
				vm.QualificationItems = quals.Select(q => new SelectListItem { Value = q.Id.ToString(), Text = q.Name }).ToList();
				var countries = _lookup.GetCountries() ?? new List<LookupItem>();
				vm.Countries = countries.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name }).ToList();
				vm.States = Enumerable.Empty<SelectListItem>().ToList();
				vm.Cities = Enumerable.Empty<SelectListItem>().ToList();
			}
			catch { }
			return View(vm);
		}

		[HttpPost]
		[Route("Create")]
		[ValidateAntiForgeryToken]
		public IActionResult Create(DriverAggregateViewModel vm)
		{
			var model = vm.Master;
			var schoolIdStr = HttpContext.Session.GetString("SchoolId");
			if (!string.IsNullOrWhiteSpace(schoolIdStr) && Guid.TryParse(schoolIdStr, out var schoolId))
			{
				model.SchoolId = schoolId;
			}

			// Basic validations for location fields similar to Teacher
			if (model.CountryId == Guid.Empty)
			{
				ModelState.AddModelError(nameof(DriverMaster.CountryId), "Country is required.");
			}
			if (model.StateId == Guid.Empty)
			{
				ModelState.AddModelError(nameof(DriverMaster.StateId), "State is required.");
			}
			if (model.CityId == Guid.Empty)
			{
				ModelState.AddModelError(nameof(DriverMaster.CityId), "City is required.");
			}

			// Validate child collections
			if (vm.Documents != null)
			{
				for (int i = 0; i < vm.Documents.Count; i++)
				{
					var d = vm.Documents[i];
					if (d == null || d.IsDeleted) continue;
					if (string.IsNullOrWhiteSpace(d.Name))
					{
						ModelState.AddModelError($"Documents[{i}].Name", "Document name is required.");
					}
				}
			}
			if (vm.Qualifications != null)
			{
				for (int i = 0; i < vm.Qualifications.Count; i++)
				{
					var q = vm.Qualifications[i];
					if (q == null || q.IsDeleted) continue;
					if (q.QualificationId == Guid.Empty)
					{
						ModelState.AddModelError($"Qualifications[{i}].QualificationId", "Qualification is required.");
					}
				}
			}

			if (!ModelState.IsValid)
			{
				try
				{
					var quals = _lookup.GetQualifications() ?? new List<LookupItem>();
					vm.QualificationItems = quals.Select(q => new SelectListItem { Value = q.Id.ToString(), Text = q.Name }).ToList();
					var countries = _lookup.GetCountries() ?? new List<LookupItem>();
					vm.Countries = countries.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name, Selected = c.Id == model.CountryId }).ToList();
					if (model.CountryId != Guid.Empty)
					{
						var states = _lookup.GetStates(model.CountryId) ?? new List<LookupItem>();
						vm.States = states.Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Name, Selected = s.Id == model.StateId }).ToList();
						if (model.StateId != Guid.Empty)
						{
							var cities = _lookup.GetCities(model.StateId) ?? new List<LookupItem>();
							vm.Cities = cities.Select(ci => new SelectListItem { Value = ci.Id.ToString(), Text = ci.Name, Selected = ci.Id == model.CityId }).ToList();
						}
						else vm.Cities = Enumerable.Empty<SelectListItem>().ToList();
					}
					else
					{
						vm.States = Enumerable.Empty<SelectListItem>().ToList();
						vm.Cities = Enumerable.Empty<SelectListItem>().ToList();
					}
				}
				catch { }
				return View(vm);
			}

			var userIdStr = HttpContext.Session.GetString("UserId");
			var companyIdStr = HttpContext.Session.GetString("CompanyId");
			if (string.IsNullOrWhiteSpace(userIdStr) || !Guid.TryParse(userIdStr, out var userId) || string.IsNullOrWhiteSpace(companyIdStr) || !Guid.TryParse(companyIdStr, out var companyId) || model.SchoolId == Guid.Empty)
			{
				ModelState.AddModelError(string.Empty, "Please login and select company to create driver.");
				return View(vm);
			}

			// Handle main images
			if (vm.DriverImageFile != null)
			{
				model.DriverImage = SaveUpload(vm.DriverImageFile, "drivers");
			}
			if (vm.LicenceImageFile != null)
			{
				model.LicenceImage = SaveUpload(vm.LicenceImageFile, "drivers");
			}

			// Normalize optional strings
			model.Id = Guid.Empty;
			model.FirstName = model.FirstName ?? string.Empty;
			model.LastName = model.LastName ?? string.Empty;
			model.FathersName = model.FathersName ?? string.Empty;
			model.MothersName = model.MothersName ?? string.Empty;
			model.Address1 = model.Address1 ?? string.Empty;
			model.Address2 = model.Address2 ?? string.Empty;
			model.ZipCode = model.ZipCode ?? string.Empty;
			model.MobileNumber = model.MobileNumber ?? string.Empty;
			model.PhoneNumber = model.PhoneNumber ?? string.Empty;
			model.DriverImage = model.DriverImage ?? string.Empty;
			model.LicenceNumber = model.LicenceNumber ?? string.Empty;
			model.LicenceDescription = model.LicenceDescription ?? string.Empty;
			model.LicenceImage = model.LicenceImage ?? string.Empty;
			model.LicenceType = model.LicenceType ?? string.Empty;
			model.Status = string.IsNullOrWhiteSpace(model.Status) ? "INC" : model.Status;
			model.StatusMessage = string.IsNullOrWhiteSpace(model.StatusMessage) ? "In Process...." : model.StatusMessage;
			model.CompanyId = companyId;
			model.CreatedBy = userId;
			model.CreatedDate = DateTime.UtcNow;

			var newId = _service.Create(model);
			if (newId == Guid.Empty)
			{
				ModelState.AddModelError(string.Empty, "Failed to create driver.");
				return View(vm);
			}

			// Persist documents
			if (vm.Documents != null && vm.Documents.Count > 0)
			{
				for (int i = 0; i < vm.Documents.Count; i++)
				{
					var d = vm.Documents[i];
					if (d == null) continue;
					if (d.IsDeleted) continue; // skip soft-deleted new rows
					d.DriverId = newId;
					d.CompanyId = companyId;
					d.SchoolId = model.SchoolId;
					d.CreatedBy = userId;
					d.CreatedDate = DateTime.UtcNow;
					d.Status = d.Status ?? "INC";
					d.StatusMessage = d.StatusMessage ?? "In Process....";
					if (vm.DocumentFiles != null && i < vm.DocumentFiles.Count && vm.DocumentFiles[i] != null)
					{
						var saved = SaveUpload(vm.DocumentFiles[i], "drivers");
						d.FileName = saved;
					}
					_docService.Create(d);
				}
			}

			// Persist qualifications
			if (vm.Qualifications != null && vm.Qualifications.Count > 0)
			{
				foreach (var q in vm.Qualifications)
				{
					if (q == null) continue;
					if (q.IsDeleted) continue; // skip soft-deleted new rows
					q.DriverId = newId;
					q.CompanyId = companyId;
					q.SchoolId = model.SchoolId;
					q.CreatedBy = userId;
					q.CreatedDate = DateTime.UtcNow;
					q.Status = q.Status ?? "INC";
					q.StatusMessage = q.StatusMessage ?? "In Process....";
					_qualService.Create(q);
				}
			}

			return RedirectToAction("Details", new { id = newId });
		}

		[HttpGet]
		[Route("GetStates")]
		public IActionResult GetStates(Guid countryId)
		{
			try
			{
				var states = _lookup.GetStates(countryId) ?? new List<LookupItem>();
				var result = states.Select(s => new { id = s.Id, name = s.Name });
				return Json(result);
			}
			catch { return Json(Array.Empty<object>()); }
		}

		[HttpGet]
		[Route("GetCities")]
		public IActionResult GetCities(Guid stateId)
		{
			try
			{
				var cities = _lookup.GetCities(stateId) ?? new List<LookupItem>();
				var result = cities.Select(c => new { id = c.Id, name = c.Name });
				return Json(result);
			}
			catch { return Json(Array.Empty<object>()); }
		}

		private string SaveUpload(IFormFile file, string folder)
		{
			if (file == null || file.Length == 0) return string.Empty;
			var uploadsRoot = Path.Combine(_env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"), folder);
			if (!Directory.Exists(uploadsRoot)) Directory.CreateDirectory(uploadsRoot);
			var fileName = $"{Guid.NewGuid():N}_{Path.GetFileName(file.FileName)}";
			var fullPath = Path.Combine(uploadsRoot, fileName);
			using (var stream = System.IO.File.Create(fullPath))
			{
				file.CopyTo(stream);
			}
			var relative = $"/{folder}/{fileName}";
			return relative;
		}

		[HttpGet]
		[Route("Edit/{id}")]
		public IActionResult Edit(Guid id)
		{
			var item = _service.GetById(id);
			if (item == null) return NotFound();
			var vm = new DriverAggregateViewModel { Master = item };
			try
			{
				// Select lists
				var quals = _lookup.GetQualifications() ?? new List<LookupItem>();
				vm.QualificationItems = quals.Select(q => new SelectListItem { Value = q.Id.ToString(), Text = q.Name, Selected = q.Id == item.QualificationId }).ToList();
				var countries = _lookup.GetCountries() ?? new List<LookupItem>();
				vm.Countries = countries.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name, Selected = c.Id == item.CountryId }).ToList();
				if (item.CountryId != Guid.Empty)
				{
					var states = _lookup.GetStates(item.CountryId) ?? new List<LookupItem>();
					vm.States = states.Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Name, Selected = s.Id == item.StateId }).ToList();
					if (item.StateId != Guid.Empty)
					{
						var cities = _lookup.GetCities(item.StateId) ?? new List<LookupItem>();
						vm.Cities = cities.Select(ci => new SelectListItem { Value = ci.Id.ToString(), Text = ci.Name, Selected = ci.Id == item.CityId }).ToList();
					}
					else vm.Cities = Enumerable.Empty<SelectListItem>().ToList();
				}
				else
				{
					vm.States = Enumerable.Empty<SelectListItem>().ToList();
					vm.Cities = Enumerable.Empty<SelectListItem>().ToList();
				}
				// Existing docs/quals (filter from GetAll for now)
				vm.Documents = (_docService.GetAll() ?? new List<DriverDocumentDetails>()).Where(d => d.DriverId == id && !d.IsDeleted).ToList();
				vm.Qualifications = (_qualService.GetAll() ?? new List<DriverQualificationDetails>()).Where(q => q.DriverId == id && !q.IsDeleted).ToList();
			}
			catch { }
			return View(vm);
		}

		[HttpPost]
		[Route("Edit/{id}")]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Guid id, DriverAggregateViewModel vm)
		{
			if (vm?.Master == null || vm.Master.Id == Guid.Empty || vm.Master.Id != id) return BadRequest();
			var model = vm.Master;
			var existing = _service.GetById(id);
			if (existing == null) return NotFound();
			// Preserve existing image paths by default
			model.DriverImage = existing.DriverImage;
			model.LicenceImage = existing.LicenceImage;

			// Validate location fields
			if (model.CountryId == Guid.Empty)
				ModelState.AddModelError(nameof(DriverMaster.CountryId), "Country is required.");
			if (model.StateId == Guid.Empty)
				ModelState.AddModelError(nameof(DriverMaster.StateId), "State is required.");
			if (model.CityId == Guid.Empty)
				ModelState.AddModelError(nameof(DriverMaster.CityId), "City is required.");

			if (!ModelState.IsValid)
			{
				try
				{
					var quals = _lookup.GetQualifications() ?? new List<LookupItem>();
					vm.QualificationItems = quals.Select(q => new SelectListItem { Value = q.Id.ToString(), Text = q.Name, Selected = q.Id == model.QualificationId }).ToList();
					var countries = _lookup.GetCountries() ?? new List<LookupItem>();
					vm.Countries = countries.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name, Selected = c.Id == model.CountryId }).ToList();
					if (model.CountryId != Guid.Empty)
					{
						var states = _lookup.GetStates(model.CountryId) ?? new List<LookupItem>();
						vm.States = states.Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Name, Selected = s.Id == model.StateId }).ToList();
						if (model.StateId != Guid.Empty)
						{
							var cities = _lookup.GetCities(model.StateId) ?? new List<LookupItem>();
							vm.Cities = cities.Select(ci => new SelectListItem { Value = ci.Id.ToString(), Text = ci.Name, Selected = ci.Id == model.CityId }).ToList();
						}
						else vm.Cities = Enumerable.Empty<SelectListItem>().ToList();
					}
					else
					{
						vm.States = Enumerable.Empty<SelectListItem>().ToList();
						vm.Cities = Enumerable.Empty<SelectListItem>().ToList();
					}
				}
				catch { }
				return View(vm);
			}

			var userIdStr = HttpContext.Session.GetString("UserId");
			var companyIdStr = HttpContext.Session.GetString("CompanyId");
			if (string.IsNullOrWhiteSpace(userIdStr) || !Guid.TryParse(userIdStr, out var userId) || string.IsNullOrWhiteSpace(companyIdStr) || !Guid.TryParse(companyIdStr, out var companyId) || model.SchoolId == Guid.Empty)
			{
				ModelState.AddModelError(string.Empty, "Please login and select company to save driver.");
				return View(vm);
			}

			// Handle main images (keep existing if no new upload)
			if (vm.DriverImageFile != null)
			{
				model.DriverImage = SaveUpload(vm.DriverImageFile, "drivers");
			}
			if (vm.LicenceImageFile != null)
			{
				model.LicenceImage = SaveUpload(vm.LicenceImageFile, "drivers");
			}

			// Normalize some fields
			model.FirstName = model.FirstName ?? string.Empty;
			model.LastName = model.LastName ?? string.Empty;
			model.FathersName = model.FathersName ?? string.Empty;
			model.MothersName = model.MothersName ?? string.Empty;
			model.Address1 = model.Address1 ?? string.Empty;
			model.Address2 = model.Address2 ?? string.Empty;
			model.ZipCode = model.ZipCode ?? string.Empty;
			model.MobileNumber = model.MobileNumber ?? string.Empty;
			model.PhoneNumber = model.PhoneNumber ?? string.Empty;
			model.DriverImage = model.DriverImage ?? string.Empty;
			model.LicenceNumber = model.LicenceNumber ?? string.Empty;
			model.LicenceDescription = model.LicenceDescription ?? string.Empty;
			model.LicenceImage = model.LicenceImage ?? string.Empty;
			model.LicenceType = model.LicenceType ?? string.Empty;
			model.ModifiedBy = userId;
			model.ModifiedDate = DateTime.UtcNow;

			if (!_service.Update(model))
			{
				ModelState.AddModelError(string.Empty, "Failed to save driver.");
				return View(vm);
			}

			// Upsert/Delete Documents
			if (vm.Documents != null && vm.Documents.Count > 0)
			{
				for (int i = 0; i < vm.Documents.Count; i++)
				{
					var d = vm.Documents[i];
					if (d == null) continue;
					d.DriverId = model.Id;
					d.CompanyId = companyId;
					d.SchoolId = model.SchoolId;
					var hasNewFile = vm.DocumentFiles != null && i < vm.DocumentFiles.Count && vm.DocumentFiles[i] != null;
					if (d.Id == Guid.Empty)
					{
						if (d.IsDeleted) continue; // skip brand-new rows marked deleted
						d.CreatedBy = userId;
						d.CreatedDate = DateTime.UtcNow;
						d.Status = d.Status ?? "INC";
						d.StatusMessage = d.StatusMessage ?? "In Process....";
						if (hasNewFile)
						{
							var saved = SaveUpload(vm.DocumentFiles![i], "drivers");
							d.FileName = saved;
						}
						_docService.Create(d);
					}
					else
					{
						if (d.IsDeleted) { _docService.Delete(d.Id); continue; }
						d.ModifiedBy = userId;
						d.ModifiedDate = DateTime.UtcNow;
						if (hasNewFile)
						{
							var saved = SaveUpload(vm.DocumentFiles![i], "drivers");
							d.FileName = saved;
						}
						_docService.Update(d);
					}
				}
			}

			// Upsert/Delete Qualifications
			if (vm.Qualifications != null && vm.Qualifications.Count > 0)
			{
				foreach (var q in vm.Qualifications)
				{
					if (q == null) continue;
					q.DriverId = model.Id;
					q.CompanyId = companyId;
					q.SchoolId = model.SchoolId;
					if (q.Id == Guid.Empty)
					{
						if (q.IsDeleted) continue; // skip brand-new rows marked deleted
						q.CreatedBy = userId;
						q.CreatedDate = DateTime.UtcNow;
						q.Status = q.Status ?? "INC";
						q.StatusMessage = q.StatusMessage ?? "In Process....";
						_qualService.Create(q);
					}
					else
					{
						if (q.IsDeleted) { _qualService.Delete(q.Id); continue; }
						q.ModifiedBy = userId;
						q.ModifiedDate = DateTime.UtcNow;
						_qualService.Update(q);
					}
				}
			}

			return RedirectToAction("Details", new { id = model.Id });
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
				TempData["ErrorMessage"] = "Failed to delete driver.";
				return RedirectToAction("Delete", new { id });
			}
			return RedirectToAction("Index");
		}
	}
}
