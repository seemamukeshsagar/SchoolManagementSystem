using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SchoolPortal.Entities.Models;
using SchoolPortal.Services.IServices;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolPortalApp.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace SchoolPortalApp.Controllers
{
	[Route("TeacherMaster")]
	public class TeacherMasterController : Controller
	{
		private readonly ITeacherService _service;
		private readonly ISchoolService _schoolService;
		private readonly ILogger<TeacherMasterController> _logger;
		private readonly ITeacherDocumentDetailsService _docService;
		private readonly ITeacherQualificationDetailsService _qualService;
		private readonly ILookupService _lookupService;
		private readonly IWebHostEnvironment _env;

		public TeacherMasterController(ITeacherService service, ISchoolService schoolService, ILookupService lookupService, ILogger<TeacherMasterController> logger, ITeacherDocumentDetailsService docService, ITeacherQualificationDetailsService qualService, IWebHostEnvironment env)
		{
			_service = service;
			_schoolService = schoolService;
			_lookupService = lookupService;
			_logger = logger;
			_docService = docService;
			_qualService = qualService;
			_env = env;
		}

		[HttpGet]
		[Route("")]
		[Route("Index")]
		public IActionResult Index()
		{
			var list = _service.GetAll();
			var schools = _schoolService.GetAll();
			var genders = _lookupService.GetGenders();
			var maritalStatuses = _lookupService.GetMaritalStatuses();
			var result = list.Select(item =>
			{
				var school = schools.FirstOrDefault(s => s.Id == item.SchoolId);
				var genderName = string.Empty;
				if (item.Gender.HasValue)
				{
					var g = genders.FirstOrDefault(x => x.Id == item.Gender.Value);
					genderName = g?.Name ?? string.Empty;
				}
				var maritalName = string.Empty;
				if (item.MaritalStatusId.HasValue)
				{
					var ms = maritalStatuses.FirstOrDefault(x => x.Id == item.MaritalStatusId.Value);
					maritalName = ms?.Name ?? string.Empty;
				}
				return new SchoolPortalApp.Models.TeacherListItemViewModel
				{
					Id = item.Id,
					Name = string.Join(" ", new[] { item.FirstName, item.LastName }.Where(x => !string.IsNullOrWhiteSpace(x))),
					Email = item.Email ?? string.Empty,
					Phone = item.Phone ?? string.Empty,
					IsActive = item.IsActive,
					SchoolName = school?.Name ?? string.Empty,
					FirstName = item.FirstName ?? string.Empty,
					LastName = item.LastName ?? string.Empty,
					DOB = item.DOB,
					DOJ = item.DOJ,
					Gender = item.Gender,
					MaritalStatusId = item.MaritalStatusId,
					Image = item.Image ?? string.Empty,
					MobilePhone = item.MobilePhone ?? string.Empty,
					GenderName = genderName,
					MaritalStatusName = maritalName
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
			var vm = new TeacherDetailsViewModel
			{
				Master = item,
				Documents = (_docService.GetAll() ?? new List<TeacherDocumentDetails>()).Where(d => d.TeacherId == id).ToList(),
				Qualifications = (_qualService.GetAll() ?? new List<TeacherQualificationDetails>()).Where(q => q.TeacherId == id).ToList()
			};
			return View(vm);
		}

		[HttpGet]
		[Route("Create")]
		public IActionResult Create()
		{
			var vm = new TeacherAggregateViewModel();
			try
			{
				var countries = _lookupService.GetCountries();
				vm.Countries = countries.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name }).ToList();
				vm.States = Enumerable.Empty<SelectListItem>().ToList();
				vm.Cities = Enumerable.Empty<SelectListItem>().ToList();
				var quals = _lookupService.GetQualifications();
				vm.QualificationItems = quals.Select(q => new SelectListItem { Value = q.Id.ToString(), Text = q.Name }).ToList();
				var genders = _lookupService.GetGenders();
				vm.Genders = genders.Select(g => new SelectListItem { Value = g.Id.ToString(), Text = g.Name }).ToList();
				var maritalStatuses = _lookupService.GetMaritalStatuses();
				vm.MaritalStatuses = maritalStatuses.Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Name }).ToList();
			}
			catch { }
			return View(vm);
		}

		[HttpPost]
		[Route("Create")]
		[ValidateAntiForgeryToken]
		public IActionResult Create(TeacherAggregateViewModel vm)
		{
			var model = vm.Master;
			var schoolIdStr = HttpContext.Session.GetString("SchoolId");
			if (!string.IsNullOrWhiteSpace(schoolIdStr) && Guid.TryParse(schoolIdStr, out var schoolId))
			{
				model.SchoolId = schoolId;
			}

			// Server-side required validations for location fields
			if (!model.CountryId.HasValue || model.CountryId.Value == Guid.Empty)
			{
				ModelState.AddModelError(nameof(TeacherMaster.CountryId), "Country is required.");
			}
			if (!model.StateId.HasValue || model.StateId.Value == Guid.Empty)
			{
				ModelState.AddModelError(nameof(TeacherMaster.StateId), "State is required.");
			}
			if (!model.CityId.HasValue || model.CityId.Value == Guid.Empty)
			{
				ModelState.AddModelError(nameof(TeacherMaster.CityId), "City is required.");
			}

			if (!ModelState.IsValid)
			{
				try
				{
					var countries = _lookupService.GetCountries();
					vm.Countries = countries.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name, Selected = model.CountryId.HasValue && c.Id == model.CountryId.Value }).ToList();
					if (model.CountryId.HasValue && model.CountryId.Value != Guid.Empty)
					{
						var states = _lookupService.GetStates(model.CountryId.Value);
						vm.States = states.Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Name, Selected = model.StateId.HasValue && s.Id == model.StateId.Value }).ToList();
						if (model.StateId.HasValue && model.StateId.Value != Guid.Empty)
						{
							var cities = _lookupService.GetCities(model.StateId.Value);
							vm.Cities = cities.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name, Selected = model.CityId.HasValue && c.Id == model.CityId.Value }).ToList();
						}
						else vm.Cities = Enumerable.Empty<SelectListItem>().ToList();
					}
					else
					{
						vm.States = Enumerable.Empty<SelectListItem>().ToList();
						vm.Cities = Enumerable.Empty<SelectListItem>().ToList();
					}
					var quals = _lookupService.GetQualifications();
					vm.QualificationItems = quals.Select(q => new SelectListItem { Value = q.Id.ToString(), Text = q.Name }).ToList();
					var genders = _lookupService.GetGenders();
					vm.Genders = genders.Select(g => new SelectListItem { Value = g.Id.ToString(), Text = g.Name, Selected = g.Id == (model.Gender ?? Guid.Empty) }).ToList();
					var maritalStatuses = _lookupService.GetMaritalStatuses();
					vm.MaritalStatuses = maritalStatuses.Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Name, Selected = m.Id == (model.MaritalStatusId ?? Guid.Empty) }).ToList();
				}
				catch { }
				return View(vm);
			}

			var userIdStr = HttpContext.Session.GetString("UserId");
			var companyIdStr = HttpContext.Session.GetString("CompanyId");
			if (string.IsNullOrWhiteSpace(userIdStr) || !Guid.TryParse(userIdStr, out var userId) || string.IsNullOrWhiteSpace(companyIdStr) || !Guid.TryParse(companyIdStr, out var companyId) || model.SchoolId == Guid.Empty)
			{
				ModelState.AddModelError(string.Empty, "Please login and select company to create teacher.");
				try
				{
					var countries = _lookupService.GetCountries();
					ViewBag.Countries = countries.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name, Selected = model.CountryId.HasValue && c.Id == model.CountryId.Value }).ToList();
					if (model.CountryId.HasValue && model.CountryId.Value != Guid.Empty)
					{
						var states = _lookupService.GetStates(model.CountryId.Value);
						ViewBag.States = states.Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Name, Selected = model.StateId.HasValue && s.Id == model.StateId.Value }).ToList();
						if (model.StateId.HasValue && model.StateId.Value != Guid.Empty)
						{
							var cities = _lookupService.GetCities(model.StateId.Value);
							ViewBag.Cities = cities.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name, Selected = model.CityId.HasValue && c.Id == model.CityId.Value }).ToList();
						}
						else ViewBag.Cities = Enumerable.Empty<SelectListItem>().ToList();
					}
					else
					{
						ViewBag.States = Enumerable.Empty<SelectListItem>().ToList();
						ViewBag.Cities = Enumerable.Empty<SelectListItem>().ToList();
					}
				}
				catch { }
				return View(model);
			}

			// Normalize optional strings to avoid nulls
			model.Id = Guid.Empty;
			model.FirstName = model.FirstName ?? string.Empty;
			model.LastName = model.LastName ?? string.Empty;
			model.Address = model.Address ?? string.Empty;
			model.ZipCode = model.ZipCode ?? string.Empty;
			model.Image = model.Image ?? string.Empty;
			model.Phone = model.Phone ?? string.Empty;
			model.MobilePhone = model.MobilePhone ?? string.Empty;
			model.YearsOfExperience = model.YearsOfExperience ?? string.Empty;
			model.PreviousSchool = model.PreviousSchool ?? string.Empty;
			model.Salutation = model.Salutation ?? string.Empty;
			model.Email = model.Email ?? string.Empty;
			model.Status = string.IsNullOrWhiteSpace(model.Status) ? "INC" : model.Status;
			model.StatusMessage = string.IsNullOrWhiteSpace(model.StatusMessage) ? "In Process...." : model.StatusMessage;
			model.CompanyId = companyId;
			model.CreatedBy = userId;
			model.CreatedDate = DateTime.UtcNow;
			model.IsDeleted = model.IsDeleted;

			var newId = _service.Create(model);
			if (newId == Guid.Empty)
			{
				ModelState.AddModelError(string.Empty, "Failed to create teacher.");
				try
				{
					var countries = _lookupService.GetCountries();
					vm.Countries = countries.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name, Selected = model.CountryId.HasValue && c.Id == model.CountryId.Value }).ToList();
					if (model.CountryId.HasValue && model.CountryId.Value != Guid.Empty)
					{
						var states = _lookupService.GetStates(model.CountryId.Value);
						vm.States = states.Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Name, Selected = model.StateId.HasValue && s.Id == model.StateId.Value }).ToList();
						if (model.StateId.HasValue && model.StateId.Value != Guid.Empty)
						{
							var cities = _lookupService.GetCities(model.StateId.Value);
							vm.Cities = cities.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name, Selected = model.CityId.HasValue && c.Id == model.CityId.Value }).ToList();
						}
						else vm.Cities = Enumerable.Empty<SelectListItem>().ToList();
					}
					else
					{
						vm.States = Enumerable.Empty<SelectListItem>().ToList();
						vm.Cities = Enumerable.Empty<SelectListItem>().ToList();
					}
					var quals = _lookupService.GetQualifications();
					vm.QualificationItems = quals.Select(q => new SelectListItem { Value = q.Id.ToString(), Text = q.Name }).ToList();
					var genders = _lookupService.GetGenders();
					vm.Genders = genders.Select(g => new SelectListItem { Value = g.Id.ToString(), Text = g.Name, Selected = g.Id == (model.Gender ?? Guid.Empty) }).ToList();
					var maritalStatuses = _lookupService.GetMaritalStatuses();
					vm.MaritalStatuses = maritalStatuses.Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Name, Selected = m.Id == (model.MaritalStatusId ?? Guid.Empty) }).ToList();
				}
				catch { }
				return View(vm);
			}
			// Save child collections
			var docCount = vm.Documents?.Count ?? 0;
			for (int i = 0; i < docCount; i++)
			{
				var d = vm.Documents![i];
				if (d == null || string.IsNullOrWhiteSpace(d.Name)) continue;

				// Save uploaded file if provided
				var file = (vm.DocumentFiles != null && vm.DocumentFiles.Count > i) ? vm.DocumentFiles[i] : null;
				if (file != null && file.Length > 0)
				{
					try
					{
						var uploadsFolder = Path.Combine(_env.WebRootPath ?? string.Empty, "uploads", "teachers", newId.ToString());
						Directory.CreateDirectory(uploadsFolder);
						var safeName = Path.GetFileName(file.FileName);
						var savedName = $"{Guid.NewGuid()}_{safeName}";
						var savePath = Path.Combine(uploadsFolder, savedName);
						using (var stream = new FileStream(savePath, FileMode.Create))
						{
							file.CopyTo(stream);
						}
						d.FileName = $"/uploads/teachers/{newId}/{savedName}";
					}
					catch (Exception ex)
					{
						_logger.LogError(ex, "Failed to save document file for teacher {TeacherId}", newId);
					}
				}

				d.Id = Guid.Empty;
				d.TeacherId = newId;
				d.CompanyId = companyId;
				d.SchoolId = model.SchoolId;
				d.CreatedBy = userId;
				d.CreatedDate = DateTime.UtcNow;
				d.IsDeleted = d.IsDeleted;
				d.IsActive = d.IsActive;
				d.Status = d.Status ?? "INC";
				d.StatusMessage = d.StatusMessage ?? "In Process....";
				_docService.Create(d);
			}

			foreach (var q in vm.Qualifications ?? Enumerable.Empty<TeacherQualificationDetails>())
			{
				if (q == null || q.QualificationId == Guid.Empty) continue;
				q.Id = Guid.Empty;
				q.TeacherId = newId;
				q.CompanyId = companyId;
				q.SchoolId = model.SchoolId;
				q.CreatedBy = userId;
				q.CreatedDate = DateTime.UtcNow;
				q.IsDeleted = q.IsDeleted;
				q.IsActive = q.IsActive;
				q.Status = q.Status ?? "INC";
				q.StatusMessage = q.StatusMessage ?? "In Process....";
				_qualService.Create(q);
			}

			return RedirectToAction("Details", new { id = newId });
		}

		[HttpGet]
		[Route("Edit/{id}")]
		public IActionResult Edit(Guid id)
		{
			var item = _service.GetById(id);
			if (item == null) return NotFound();
			var vm = new TeacherAggregateViewModel { Master = item };
			try
			{
				var countries = _lookupService.GetCountries();
				vm.Countries = countries.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name, Selected = c.Id == (item.CountryId ?? Guid.Empty) }).ToList();
				if (item.CountryId.HasValue && item.CountryId.Value != Guid.Empty)
				{
					var states = _lookupService.GetStates(item.CountryId.Value);
					vm.States = states.Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Name, Selected = s.Id == (item.StateId ?? Guid.Empty) }).ToList();
					if (item.StateId.HasValue && item.StateId.Value != Guid.Empty)
					{
						var cities = _lookupService.GetCities(item.StateId.Value);
						vm.Cities = cities.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name, Selected = c.Id == (item.CityId ?? Guid.Empty) }).ToList();
					}
					else vm.Cities = Enumerable.Empty<SelectListItem>().ToList();
				}
				else
				{
					vm.States = Enumerable.Empty<SelectListItem>().ToList();
					vm.Cities = Enumerable.Empty<SelectListItem>().ToList();
				}
				vm.Documents = (_docService.GetAll() ?? new List<TeacherDocumentDetails>()).Where(d => d.TeacherId == id).ToList();
				vm.Qualifications = (_qualService.GetAll() ?? new List<TeacherQualificationDetails>()).Where(q => q.TeacherId == id).ToList();
				var quals = _lookupService.GetQualifications();
				vm.QualificationItems = quals.Select(q => new SelectListItem { Value = q.Id.ToString(), Text = q.Name }).ToList();
			}
			catch { }
			return View(vm);
		}

		[HttpPost]
		[Route("Edit/{id}")]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Guid id, TeacherAggregateViewModel vm)
		{
			var model = vm.Master;
			if (id != model.Id) return BadRequest();

			var schoolIdStr = HttpContext.Session.GetString("SchoolId");
			if (!string.IsNullOrWhiteSpace(schoolIdStr) && Guid.TryParse(schoolIdStr, out var schoolIdFromSession))
			{
				model.SchoolId = schoolIdFromSession;
			}

			// Server-side required validations for location fields
			if (!model.CountryId.HasValue || model.CountryId.Value == Guid.Empty)
			{
				ModelState.AddModelError(nameof(TeacherMaster.CountryId), "Country is required.");
			}
			if (!model.StateId.HasValue || model.StateId.Value == Guid.Empty)
			{
				ModelState.AddModelError(nameof(TeacherMaster.StateId), "State is required.");
			}
			if (!model.CityId.HasValue || model.CityId.Value == Guid.Empty)
			{
				ModelState.AddModelError(nameof(TeacherMaster.CityId), "City is required.");
			}

			if (!ModelState.IsValid)
			{
				return View(vm);
			}

			var userIdStr = HttpContext.Session.GetString("UserId");
			if (string.IsNullOrWhiteSpace(userIdStr) || !Guid.TryParse(userIdStr, out var userId) || model.SchoolId == Guid.Empty)
			{
				ModelState.AddModelError(string.Empty, "Please login to update teacher.");
				try
				{
					var countries = _lookupService.GetCountries();
					vm.Countries = countries.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name, Selected = model.CountryId.HasValue && c.Id == model.CountryId.Value }).ToList();
					if (model.CountryId.HasValue && model.CountryId.Value != Guid.Empty)
					{
						var states = _lookupService.GetStates(model.CountryId.Value);
						vm.States = states.Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Name, Selected = model.StateId.HasValue && s.Id == model.StateId.Value }).ToList();
						if (model.StateId.HasValue && model.StateId.Value != Guid.Empty)
						{
							var cities = _lookupService.GetCities(model.StateId.Value);
							vm.Cities = cities.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name, Selected = model.CityId.HasValue && c.Id == model.CityId.Value }).ToList();
						}
						else vm.Cities = Enumerable.Empty<SelectListItem>().ToList();
					}
					else
					{
						vm.States = Enumerable.Empty<SelectListItem>().ToList();
						vm.Cities = Enumerable.Empty<SelectListItem>().ToList();
					}
					var quals = _lookupService.GetQualifications();
					vm.QualificationItems = quals.Select(q => new SelectListItem { Value = q.Id.ToString(), Text = q.Name }).ToList();
				}
				catch { }
				return View(vm);
			}

			// Normalize optional strings to avoid nulls
			model.FirstName = model.FirstName ?? string.Empty;
			model.LastName = model.LastName ?? string.Empty;
			model.Address = model.Address ?? string.Empty;
			model.ZipCode = model.ZipCode ?? string.Empty;
			model.Image = model.Image ?? string.Empty;
			model.Phone = model.Phone ?? string.Empty;
			model.MobilePhone = model.MobilePhone ?? string.Empty;
			model.YearsOfExperience = model.YearsOfExperience ?? string.Empty;
			model.PreviousSchool = model.PreviousSchool ?? string.Empty;
			model.Salutation = model.Salutation ?? string.Empty;
			model.Email = model.Email ?? string.Empty;
			model.Status = string.IsNullOrWhiteSpace(model.Status) ? "INC" : model.Status;
			model.StatusMessage = string.IsNullOrWhiteSpace(model.StatusMessage) ? "In Process...." : model.StatusMessage;
			model.ModifiedBy = userId;
			model.ModifiedDate = DateTime.UtcNow;

			if (!_service.Update(model))
			{
				ModelState.AddModelError(string.Empty, "Failed to update teacher.");
				try
				{
					var countries = _lookupService.GetCountries();
					vm.Countries = countries.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name, Selected = model.CountryId.HasValue && c.Id == model.CountryId.Value }).ToList();
					if (model.CountryId.HasValue && model.CountryId.Value != Guid.Empty)
					{
						var states = _lookupService.GetStates(model.CountryId.Value);
						vm.States = states.Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Name, Selected = model.StateId.HasValue && s.Id == model.StateId.Value }).ToList();
						if (model.StateId.HasValue && model.StateId.Value != Guid.Empty)
						{
							var cities = _lookupService.GetCities(model.StateId.Value);
							vm.Cities = cities.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name, Selected = model.CityId.HasValue && c.Id == model.CityId.Value }).ToList();
						}
						else vm.Cities = Enumerable.Empty<SelectListItem>().ToList();
					}
					else
					{
						vm.States = Enumerable.Empty<SelectListItem>().ToList();
						vm.Cities = Enumerable.Empty<SelectListItem>().ToList();
					}
					var quals = _lookupService.GetQualifications();
					vm.QualificationItems = quals.Select(q => new SelectListItem { Value = q.Id.ToString(), Text = q.Name }).ToList();
				}
				catch { }
				return View(vm);
			}

			// Upsert documents
			var dCount = vm.Documents?.Count ?? 0;
			for (int i = 0; i < dCount; i++)
			{
				var d = vm.Documents![i];
				if (d == null || string.IsNullOrWhiteSpace(d.Name)) continue;
				// handle file if uploaded
				var file = (vm.DocumentFiles != null && vm.DocumentFiles.Count > i) ? vm.DocumentFiles[i] : null;
				if (file != null && file.Length > 0)
				{
					try
					{
						var uploadsFolder = Path.Combine(_env.WebRootPath ?? string.Empty, "uploads", "teachers", id.ToString());
						Directory.CreateDirectory(uploadsFolder);
						var safeName = Path.GetFileName(file.FileName);
						var savedName = $"{Guid.NewGuid()}_{safeName}";
						var savePath = Path.Combine(uploadsFolder, savedName);
						using (var stream = new FileStream(savePath, FileMode.Create))
						{
							file.CopyTo(stream);
						}
						d.FileName = $"/uploads/teachers/{id}/{savedName}";
					}
					catch (Exception ex)
					{
						_logger.LogError(ex, "Failed to save document file for teacher {TeacherId}", id);
					}
				}

				if (d.Id == Guid.Empty)
				{
					d.TeacherId = id;
					d.CompanyId = model.CompanyId;
					d.SchoolId = model.SchoolId;
					d.CreatedBy = userId;
					d.CreatedDate = DateTime.UtcNow;
					d.Status = d.Status ?? "INC";
					d.StatusMessage = d.StatusMessage ?? "In Process....";
					_docService.Create(d);
				}
				else
				{
					d.TeacherId = id;
					d.SchoolId = model.SchoolId;
					d.ModifiedBy = userId;
					d.ModifiedDate = DateTime.UtcNow;
					_docService.Update(d);
				}
			}

			// Upsert qualifications
			foreach (var q in vm.Qualifications ?? Enumerable.Empty<TeacherQualificationDetails>())
			{
				if (q == null || q.QualificationId == Guid.Empty) continue;
				if (q.Id == Guid.Empty)
				{
					q.TeacherId = id;
					q.CompanyId = model.CompanyId;
					q.SchoolId = model.SchoolId;
					q.CreatedBy = userId;
					q.CreatedDate = DateTime.UtcNow;
					q.Status = q.Status ?? "INC";
					q.StatusMessage = q.StatusMessage ?? "In Process....";
					_qualService.Create(q);
				}
				else
				{
					q.TeacherId = id;
					q.SchoolId = model.SchoolId;
					q.ModifiedBy = userId;
					q.ModifiedDate = DateTime.UtcNow;
					_qualService.Update(q);
				}
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
			try
			{
				// Delete child records first
				var docs = _docService.GetAll()?.Where(d => d.TeacherId == id).ToList() ?? new List<TeacherDocumentDetails>();
				foreach (var d in docs)
				{
					_docService.Delete(d.Id);
				}

				var quals = _qualService.GetAll()?.Where(q => q.TeacherId == id).ToList() ?? new List<TeacherQualificationDetails>();
				foreach (var q in quals)
				{
					_qualService.Delete(q.Id);
				}

				if (!_service.Delete(id))
				{
					TempData["ErrorMessage"] = "Failed to delete teacher.";
					return RedirectToAction("Delete", new { id });
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error deleting teacher {TeacherId} and related records", id);
				TempData["ErrorMessage"] = "Error deleting teacher and related records.";
				return RedirectToAction("Delete", new { id });
			}

			return RedirectToAction("Index");
		}

		[HttpGet]
		[Route("GetStatesByCountry/{countryId}")]
		public IActionResult GetStatesByCountry(Guid countryId)
		{
			try
			{
				var states = _lookupService.GetStates(countryId);
				var result = states.Select(s => new { value = s.Id.ToString(), text = s.Name });
				return Json(result);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error getting states for country {CountryId}", countryId);
				return StatusCode(500, "Error loading states");
			}
		}

		[HttpGet]
		[Route("GetCitiesByState/{stateId}")]
		public IActionResult GetCitiesByState(Guid stateId)
		{
			try
			{
				var cities = _lookupService.GetCities(stateId);
				var result = cities.Select(c => new { value = c.Id.ToString(), text = c.Name });
				return Json(result);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error getting cities for state {StateId}", stateId);
				return StatusCode(500, "Error loading cities");
			}
		}
	}
}
