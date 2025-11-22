using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.IO;
using System.Linq;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using SchoolPortal.DBAccess;
using SchoolPortal.Entities.Models;
using SchoolPortal.Services.IServices;
using SchoolPortalApp.Models;

namespace SchoolPortalApp.Controllers
{
	[Route("TeacherMaster")]
	public class TeacherMasterController : BaseController
	{
		private readonly ITeacherService _service;
		private readonly ISchoolService _schoolService;
		private readonly ILogger<TeacherMasterController> _logger;
		private readonly ITeacherDocumentDetailsService _docService;
		private readonly ITeacherQualificationDetailsService _qualService;
		private readonly ILookupService _lookupService;
		private readonly IWebHostEnvironment _env;
		private readonly IEmpService _empService;

		public TeacherMasterController(ITeacherService service, ISchoolService schoolService, ILookupService lookupService, ILogger<TeacherMasterController> logger, ITeacherDocumentDetailsService docService, ITeacherQualificationDetailsService qualService, IWebHostEnvironment env, IEmpService empService)
		{
			_service = service;
			_schoolService = schoolService;
			_lookupService = lookupService;
			_logger = logger;
			_docService = docService;
			_qualService = qualService;
			_env = env;
			_empService = empService;
		}

		[HttpGet]
		[Route("")]
		[Route("Index")]
		public IActionResult Index()
		{
			var schoolId = CurrentSchoolId;
			List<TeacherMaster> list;
			if (schoolId.HasValue)
			{
				list = _service.GetAll(schoolId.Value);
			}
			else
			{
				list = _service.GetAll();
			}
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
			var schoolId = CurrentSchoolId;
			if (schoolId.HasValue)
			{
				model.SchoolId = schoolId.Value;
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

			var userId = CurrentUserId;
			var companyId = CurrentCompanyId;
			if (!userId.HasValue || !companyId.HasValue || model.SchoolId == Guid.Empty)
			{
				ModelState.AddModelError(string.Empty, "Please login and select company to create teacher.");
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
				}
				catch { }
				return View(vm);
			}

			// Normalize optional strings to avoid nulls
			model.Id = Guid.Empty;
			model.FirstName = model.FirstName ?? string.Empty;
			model.LastName = model.LastName ?? string.Empty;
			model.Address = model.Address ?? string.Empty;
			model.ZipCode = model.ZipCode ?? string.Empty;
			if (vm.ImageFile != null && vm.ImageFile.Length > 0)
			{
				// Save teacher image under /uploads/teachers
				var uploadsFolder = Path.Combine(_env.WebRootPath ?? string.Empty, "uploads", "teachers");
				Directory.CreateDirectory(uploadsFolder);
				var safeName = Path.GetFileName(vm.ImageFile.FileName);
				var savedName = $"{Guid.NewGuid()}_{safeName}";
				var savePath = Path.Combine(uploadsFolder, savedName);
				using (var stream = new FileStream(savePath, FileMode.Create))
				{
					vm.ImageFile.CopyTo(stream);
				}
				model.Image = $"/uploads/teachers/{savedName}";
			}
			else
			{
				model.Image = model.Image ?? string.Empty;
			}
			model.Phone = model.Phone ?? string.Empty;
			model.MobilePhone = model.MobilePhone ?? string.Empty;
			model.YearsOfExperience = model.YearsOfExperience ?? string.Empty;
			model.PreviousSchool = model.PreviousSchool ?? string.Empty;
			model.Salutation = model.Salutation ?? string.Empty;
			model.Email = model.Email ?? string.Empty;
			model.Status = string.IsNullOrWhiteSpace(model.Status) ? "INC" : model.Status;
			model.StatusMessage = string.IsNullOrWhiteSpace(model.StatusMessage) ? "In Process...." : model.StatusMessage;
			model.CompanyId = companyId.Value;
			model.CreatedBy = userId.Value;
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
				}
				catch { }
				return View(vm);
			}
			
			// Also create the corresponding EmpMaster record
			try
			{
				var emp = new EmpMaster
				{
					Id = newId,
					FirstName = model.FirstName,
					LastName = model.LastName ?? string.Empty,
					EmailId = model.Email ?? string.Empty,
					PhoneNumber = model.Phone ?? string.Empty,
					MobileNumber = model.MobilePhone ?? string.Empty,
					DOB = model.DOB,
					CompanyId = model.CompanyId,
					SchoolId = model.SchoolId,
					IsActive = model.IsActive,
					IsDeleted = model.IsDeleted,
					CreatedBy = userId.Value,
					CreatedDate = DateTime.UtcNow,
					DOJ = model.DOJ ?? DateTime.UtcNow,
					Status = "ACT",
					StatusMessage = "Created from Teacher create form",
					Salutation = model.Salutation ?? string.Empty,
					PrevioudSchoolCompany = model.PreviousSchool ?? string.Empty,
					YearsOfExperience = model.YearsOfExperience ?? string.Empty
				};
				
				// Try to create the EmpMaster record
				_empService.Create(emp);
			}
			catch (Exception empEx)
			{
				_logger.LogWarning(empEx, "Failed to create EmpMaster record for teacher {TeacherId}", newId);
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
				d.CompanyId = companyId.Value;
				d.SchoolId = model.SchoolId;
				d.CreatedBy = userId.Value;
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
				q.CompanyId = companyId.Value;
				q.SchoolId = model.SchoolId;
				q.CreatedBy = userId.Value;
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
				var genders = _lookupService.GetGenders();
				vm.Genders = genders.Select(g => new SelectListItem { Value = g.Id.ToString(), Text = g.Name, Selected = g.Id == (item.Gender ?? Guid.Empty) }).ToList();
				var maritalStatuses = _lookupService.GetMaritalStatuses();
				vm.MaritalStatuses = maritalStatuses.Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Name, Selected = m.Id == (item.MaritalStatusId ?? Guid.Empty) }).ToList();
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
			
			var schoolId = CurrentSchoolId;
			if (schoolId.HasValue)
			{
				model.SchoolId = schoolId.Value;
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
			
			var userId = CurrentUserId;
			if (!userId.HasValue || model.SchoolId == Guid.Empty)
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
					var genders = _lookupService.GetGenders();
					vm.Genders = genders.Select(g => new SelectListItem { Value = g.Id.ToString(), Text = g.Name, Selected = g.Id == (model.Gender ?? Guid.Empty) }).ToList();
					var maritalStatuses = _lookupService.GetMaritalStatuses();
					vm.MaritalStatuses = maritalStatuses.Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Name, Selected = m.Id == (model.MaritalStatusId ?? Guid.Empty) }).ToList();
				}
				catch { }
				return View(vm);
			}

			// Normalize optional strings to avoid nulls
			model.FirstName = model.FirstName ?? string.Empty;
			model.LastName = model.LastName ?? string.Empty;
			model.Address = model.Address ?? string.Empty;
			model.ZipCode = model.ZipCode ?? string.Empty;
			if (vm.ImageFile != null && vm.ImageFile.Length > 0)
			{
				var uploadsFolder = Path.Combine(_env.WebRootPath ?? string.Empty, "uploads", "teachers");
				Directory.CreateDirectory(uploadsFolder);
				var safeName = Path.GetFileName(vm.ImageFile.FileName);
				var savedName = $"{Guid.NewGuid()}_{safeName}";
				var savePath = Path.Combine(uploadsFolder, savedName);
				using (var stream = new FileStream(savePath, FileMode.Create))
				{
					vm.ImageFile.CopyTo(stream);
				}
				model.Image = $"/uploads/teachers/{savedName}";
			}
			else
			{
				model.Image = model.Image ?? string.Empty;
			}
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
					var genders = _lookupService.GetGenders();
					vm.Genders = genders.Select(g => new SelectListItem { Value = g.Id.ToString(), Text = g.Name, Selected = g.Id == (model.Gender ?? Guid.Empty) }).ToList();
					var maritalStatuses = _lookupService.GetMaritalStatuses();
					vm.MaritalStatuses = maritalStatuses.Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Name, Selected = m.Id == (model.MaritalStatusId ?? Guid.Empty) }).ToList();
				}
				catch { }
				return View(vm);
			}
			
			// Also update the corresponding EmpMaster record
			try
			{
				var emp = new EmpMaster
				{
					Id = id,
					FirstName = model.FirstName,
					LastName = model.LastName ?? string.Empty,
					EmailId = model.Email ?? string.Empty,
					PhoneNumber = model.Phone ?? string.Empty,
					MobileNumber = model.MobilePhone ?? string.Empty,
					DOB = model.DOB,
					CompanyId = model.CompanyId,
					SchoolId = model.SchoolId,
					IsActive = model.IsActive,
					IsDeleted = model.IsDeleted,
					ModifiedBy = userId,
					ModifiedDate = DateTime.UtcNow,
					DOJ = model.DOJ ?? DateTime.UtcNow,
					Status = "ACT",
					StatusMessage = "Updated from Teacher edit",
					Salutation = model.Salutation ?? string.Empty,
					PrevioudSchoolCompany = model.PreviousSchool ?? string.Empty,
					YearsOfExperience = model.YearsOfExperience ?? string.Empty
				};
				
				// Try to update the EmpMaster record
				_empService.Update(emp);
			}
			catch (Exception empEx)
			{
				_logger.LogWarning(empEx, "Failed to update EmpMaster record for teacher {TeacherId}", id);
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
					d.CreatedBy = userId.Value;
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
					q.CreatedBy = userId.Value;
					q.CreatedDate = DateTime.UtcNow;
					q.Status = q.Status ?? "INC";
					q.StatusMessage = q.StatusMessage ?? "In Process....";
					_qualService.Create(q);
				}
				else
				{
					q.TeacherId = id;
					q.SchoolId = model.SchoolId;
					q.ModifiedBy = userId.Value;
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
				
				// Also mark the corresponding EmpMaster record as deleted
				try
				{
					var emp = _empService.GetById(id);
					if (emp != null)
					{
						emp.IsDeleted = true;
						emp.ModifiedDate = DateTime.UtcNow;
						_empService.Update(emp);
					}
				}
				catch (Exception empEx)
				{
					_logger.LogWarning(empEx, "Failed to mark EmpMaster record as deleted for teacher {TeacherId}", id);
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
		[Route("DownloadTemplate")]
		public IActionResult DownloadTemplate()
		{
			using (var workbook = new XLWorkbook())
			{
				var worksheet = workbook.Worksheets.Add("Teachers");
				var currentRow = 1;

				// Header row
				worksheet.Cell(currentRow, 1).Value = "First Name";
				worksheet.Cell(currentRow, 2).Value = "Last Name";
				worksheet.Cell(currentRow, 3).Value = "Date of Birth (YYYY-MM-DD)";
				worksheet.Cell(currentRow, 4).Value = "Date of Joining (YYYY-MM-DD)";
				worksheet.Cell(currentRow, 5).Value = "Email";
				worksheet.Cell(currentRow, 6).Value = "Phone";
				worksheet.Cell(currentRow, 7).Value = "Mobile Phone";
				worksheet.Cell(currentRow, 8).Value = "Address";
				worksheet.Cell(currentRow, 9).Value = "City";
				worksheet.Cell(currentRow, 10).Value = "State";
				worksheet.Cell(currentRow, 11).Value = "Country";
				worksheet.Cell(currentRow, 12).Value = "Zip Code";
				worksheet.Cell(currentRow, 13).Value = "Gender (Male/Female/Other)";
				worksheet.Cell(currentRow, 14).Value = "Marital Status (Single/Married/Divorced/Widowed)";
				worksheet.Cell(currentRow, 15).Value = "Years of Experience";
				worksheet.Cell(currentRow, 16).Value = "Previous School";
				worksheet.Cell(currentRow, 17).Value = "Salutation (Mr./Ms./Mrs./Dr.)";
				worksheet.Cell(currentRow, 18).Value = "Is Active (Yes/No)";
				worksheet.Cell(currentRow, 19).Value = "School Name";

				// Format header
				var headerRange = worksheet.Range(1, 1, 1, 19);
				headerRange.Style.Font.Bold = true;
				headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;
				headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

				// Set column widths
				worksheet.Column(1).Width = 15;
				worksheet.Column(2).Width = 15;
				worksheet.Column(3).Width = 20;
				worksheet.Column(4).Width = 20;
				worksheet.Column(5).Width = 25;
				worksheet.Column(6).Width = 15;
				worksheet.Column(7).Width = 15;
				worksheet.Column(8).Width = 25;
				worksheet.Column(9).Width = 15;
				worksheet.Column(10).Width = 15;
				worksheet.Column(11).Width = 15;
				worksheet.Column(12).Width = 12;
				worksheet.Column(13).Width = 15;
				worksheet.Column(14).Width = 25;
				worksheet.Column(15).Width = 20;
				worksheet.Column(16).Width = 20;
				worksheet.Column(17).Width = 15;
				worksheet.Column(18).Width = 12;
				worksheet.Column(19).Width = 20;

				// Add data validation for Gender column
				var genderValidation = worksheet.Range("M2:M1000").CreateDataValidation();
				genderValidation.AllowedValues = XLAllowedValues.List;
				genderValidation.InCellDropdown = true;
				genderValidation.List(string.Join(",", new[] { "Male", "Female", "Other" }));

				// Add data validation for Marital Status column
				var maritalValidation = worksheet.Range("N2:N1000").CreateDataValidation();
				maritalValidation.AllowedValues = XLAllowedValues.List;
				maritalValidation.InCellDropdown = true;
				maritalValidation.List(string.Join(",", new[] { "Single", "Married", "Divorced", "Widowed" }));

				// Add data validation for Salutation column
				var salutationValidation = worksheet.Range("Q2:Q1000").CreateDataValidation();
				salutationValidation.AllowedValues = XLAllowedValues.List;
				salutationValidation.InCellDropdown = true;
				salutationValidation.List(string.Join(",", new[] { "Mr.", "Ms.", "Mrs.", "Dr." }));

				// Add data validation for IsActive column
				var activeValidation = worksheet.Range("R2:R1000").CreateDataValidation();
				activeValidation.AllowedValues = XLAllowedValues.List;
				activeValidation.InCellDropdown = true;
				activeValidation.List(string.Join(",", new[] { "Yes", "No" }));

				// Add sample data
				currentRow++;
				worksheet.Cell(currentRow, 1).Value = "John";
				worksheet.Cell(currentRow, 2).Value = "Doe";
				worksheet.Cell(currentRow, 3).Value = "1985-05-15";
				worksheet.Cell(currentRow, 4).Value = "2020-09-01";
				worksheet.Cell(currentRow, 5).Value = "john.doe@school.edu";
				worksheet.Cell(currentRow, 6).Value = "123-456-7890";
				worksheet.Cell(currentRow, 7).Value = "987-654-3210";
				worksheet.Cell(currentRow, 8).Value = "123 Main St";
				worksheet.Cell(currentRow, 9).Value = "New York";
				worksheet.Cell(currentRow, 10).Value = "NY";
				worksheet.Cell(currentRow, 11).Value = "USA";
				worksheet.Cell(currentRow, 12).Value = "10001";
				worksheet.Cell(currentRow, 13).Value = "Male";
				worksheet.Cell(currentRow, 14).Value = "Married";
				worksheet.Cell(currentRow, 15).Value = "10";
				worksheet.Cell(currentRow, 16).Value = "Previous School Name";
				worksheet.Cell(currentRow, 17).Value = "Mr.";
				worksheet.Cell(currentRow, 18).Value = "Yes";
				worksheet.Cell(currentRow, 19).Value = "Main School";

				currentRow++;
				worksheet.Cell(currentRow, 1).Value = "Jane";
				worksheet.Cell(currentRow, 2).Value = "Smith";
				worksheet.Cell(currentRow, 3).Value = "1990-08-22";
				worksheet.Cell(currentRow, 4).Value = "2021-01-15";
				worksheet.Cell(currentRow, 5).Value = "jane.smith@school.edu";
				worksheet.Cell(currentRow, 6).Value = "555-123-4567";
				worksheet.Cell(currentRow, 7).Value = "555-987-6543";
				worksheet.Cell(currentRow, 8).Value = "456 Oak Ave";
				worksheet.Cell(currentRow, 9).Value = "Los Angeles";
				worksheet.Cell(currentRow, 10).Value = "CA";
				worksheet.Cell(currentRow, 11).Value = "USA";
				worksheet.Cell(currentRow, 12).Value = "90210";
				worksheet.Cell(currentRow, 13).Value = "Female";
				worksheet.Cell(currentRow, 14).Value = "Single";
				worksheet.Cell(currentRow, 15).Value = "5";
				worksheet.Cell(currentRow, 16).Value = "";
				worksheet.Cell(currentRow, 17).Value = "Ms.";
				worksheet.Cell(currentRow, 18).Value = "Yes";
				worksheet.Cell(currentRow, 19).Value = "Main School";

				// Add instructions
				currentRow += 2;
				worksheet.Cell(currentRow, 1).Value = "Instructions:";
				worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
				currentRow++;
				worksheet.Cell(currentRow, 1).Value = "1. Fill in teacher details in the rows below";
				currentRow++;
				worksheet.Cell(currentRow, 1).Value = "2. Required fields: First Name, Date of Birth, Email, School Name";
				currentRow++;
				worksheet.Cell(currentRow, 1).Value = "3. Date format: YYYY-MM-DD";
				currentRow++;
				worksheet.Cell(currentRow, 1).Value = "4. Use dropdown lists where provided for Gender, Marital Status, Salutation, and Is Active";
				currentRow++;
				worksheet.Cell(currentRow, 1).Value = "5. Do not modify or remove the header row";

				// Freeze the header row
				worksheet.SheetView.Freeze(1, 0);

				using (var stream = new MemoryStream())
				{
					workbook.SaveAs(stream);
					var content = stream.ToArray();
					return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Teacher_Import_Template.xlsx");
				}
			}
		}

		[HttpGet]
		[Route("Import")]
		public IActionResult Import()
		{
			return View();
		}

		[HttpPost]
		[Route("Import")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Import(IFormFile excelFile)
		{
			if (excelFile == null || excelFile.Length == 0)
			{
				ModelState.AddModelError("", "Please select an Excel file to upload.");
				return View();
			}

			// Check file extension
			var fileExtension = Path.GetExtension(excelFile.FileName).ToLowerInvariant();
			if (fileExtension != ".xlsx" && fileExtension != ".xls")
			{
				ModelState.AddModelError("", "Please upload a valid Excel file (.xlsx or .xls).");
				return View();
			}

			var userIdStr = HttpContext.Session.GetString("UserId");
			var companyIdStr = HttpContext.Session.GetString("CompanyId");
			var schoolIdStr = HttpContext.Session.GetString("SchoolId");

			if (string.IsNullOrWhiteSpace(userIdStr) || !Guid.TryParse(userIdStr, out var userId) ||
				string.IsNullOrWhiteSpace(companyIdStr) || !Guid.TryParse(companyIdStr, out var companyId) ||
				string.IsNullOrWhiteSpace(schoolIdStr) || !Guid.TryParse(schoolIdStr, out var schoolId))
			{
				ModelState.AddModelError("", "User session expired. Please login again.");
				return View();
			}

			try
			{
				var teachers = new List<TeacherMaster>();
				var schools = _schoolService.GetAll().ToList();

				using (var memoryStream = new MemoryStream())
				{
					await excelFile.CopyToAsync(memoryStream);

					if (memoryStream.Length == 0)
					{
						ModelState.AddModelError("", "The uploaded file is empty.");
						return View();
					}

					memoryStream.Position = 0;

					try
					{
						using (var workbook = new XLWorkbook(memoryStream))
						{
							var worksheet = workbook.Worksheet(1) ?? workbook.Worksheet(0);
							if (worksheet == null)
							{
								_logger.LogWarning("The Excel file does not contain any worksheets");
								ModelState.AddModelError("", "The Excel file does not contain any worksheets.");
								return View();
							}
							
							_logger.LogInformation("Found worksheet: {WorksheetName}", worksheet.Name);

							// Validate header row
							var headerRow = worksheet.Row(1);
							var expectedHeaders = new[] { "First Name", "Last Name", "Date of Birth (YYYY-MM-DD)", "Date of Joining (YYYY-MM-DD)", "Email" };
							for (int i = 0; i < Math.Min(expectedHeaders.Length, 5); i++)
							{
								var cellValue = headerRow.Cell(i + 1).GetString()?.Trim() ?? "";
								if (!cellValue.Equals(expectedHeaders[i], StringComparison.OrdinalIgnoreCase))
								{
									_logger.LogWarning("Header mismatch at column {ColumnIndex}: expected '{Expected}', got '{Actual}'", i + 1, expectedHeaders[i], cellValue);
								}
							}

							var rows = worksheet.RowsUsed().Skip(1); // Skip header row
							_logger.LogInformation("Found {RowCount} rows in Excel file", rows.Count());

							foreach (var row in rows)
							{
								_logger.LogInformation("Processing row {RowNumber}", row.RowNumber());
								// Read cell values
								var firstName = row.Cell(1).GetString()?.Trim() ?? "";
								var lastName = row.Cell(2).GetString()?.Trim() ?? "";
								var dobString = row.Cell(3).GetString()?.Trim() ?? "";
								var dojString = row.Cell(4).GetString()?.Trim() ?? "";
								var email = row.Cell(5).GetString()?.Trim() ?? "";
								var phone = row.Cell(6).GetString()?.Trim() ?? "";
								var mobilePhone = row.Cell(7).GetString()?.Trim() ?? "";
								var address = row.Cell(8).GetString()?.Trim() ?? "";
								var city = row.Cell(9).GetString()?.Trim() ?? "";
								var state = row.Cell(10).GetString()?.Trim() ?? "";
								var country = row.Cell(11).GetString()?.Trim() ?? "";
								var zipCode = row.Cell(12).GetString()?.Trim() ?? "";
								var genderString = row.Cell(13).GetString()?.Trim() ?? "";
								var maritalStatusString = row.Cell(14).GetString()?.Trim() ?? "";
								var yearsOfExperience = row.Cell(15).GetString()?.Trim() ?? "";
								var previousSchool = row.Cell(16).GetString()?.Trim() ?? "";
								var salutation = row.Cell(17).GetString()?.Trim() ?? "";
								var isActiveString = row.Cell(18).GetString()?.Trim() ?? "";
								var schoolName = row.Cell(19).GetString()?.Trim() ?? "";

								_logger.LogInformation("Row data - FirstName: '{FirstName}', LastName: '{LastName}', Email: '{Email}', DOB: '{DOB}', School: '{School}'", firstName, lastName, email, dobString, schoolName);

								// Skip empty rows
								if (string.IsNullOrEmpty(firstName) && string.IsNullOrEmpty(email))
								{
									_logger.LogInformation("Skipping empty row {RowNumber}", row.RowNumber());
									continue;
								}

								// Required fields check
								if (string.IsNullOrEmpty(firstName))
								{
									ModelState.AddModelError("", $"Row {row.RowNumber()}: First Name is required.");
									continue;
								}

								if (string.IsNullOrEmpty(email))
								{
									ModelState.AddModelError("", $"Row {row.RowNumber()}: Email is required.");
									continue;
								}

								// Parse dates
								DateTime? dob = null;
								if (!string.IsNullOrEmpty(dobString) && DateTime.TryParse(dobString, out var parsedDob))
								{
									dob = parsedDob;
									_logger.LogInformation("Parsed DOB: {DOB}", dob);
								}
								else if (!string.IsNullOrEmpty(dobString))
								{
									ModelState.AddModelError("", $"Row {row.RowNumber()}: Invalid Date of Birth format. Use YYYY-MM-DD.");
									continue;
								}
								else
								{
									// DOB is required
									ModelState.AddModelError("", $"Row {row.RowNumber()}: Date of Birth is required.");
									continue;
								}

								DateTime? doj = null;
								if (!string.IsNullOrEmpty(dojString) && DateTime.TryParse(dojString, out var parsedDoj))
								{
									doj = parsedDoj;
									_logger.LogInformation("Parsed DOJ: {DOJ}", doj);
								}
								else if (!string.IsNullOrEmpty(dojString))
								{
									ModelState.AddModelError("", $"Row {row.RowNumber()}: Invalid Date of Joining format. Use YYYY-MM-DD.");
									continue;
								}
								// DOJ is optional, so we don't need to enforce it

								// Parse boolean values
								bool isActive = true;
								if (!string.IsNullOrEmpty(isActiveString))
								{
									isActive = isActiveString.Equals("Yes", StringComparison.OrdinalIgnoreCase);
									_logger.LogInformation("Parsed IsActive: {IsActive}", isActive);
								}

								// Get school
								var teacherSchoolId = schoolId; // Default to user's school
								if (!string.IsNullOrEmpty(schoolName))
								{
									var matchingSchool = schools.FirstOrDefault(s => 
										s.Name.Equals(schoolName, StringComparison.OrdinalIgnoreCase));
									if (matchingSchool != null)
									{
										teacherSchoolId = matchingSchool.Id;
										_logger.LogInformation("Found matching school: {SchoolName} with ID: {SchoolId}", schoolName, teacherSchoolId);
									}
									else
									{
										_logger.LogWarning("No matching school found for: {SchoolName}", schoolName);
									}
								}
								else
								{
									_logger.LogInformation("Using default school ID: {SchoolId}", teacherSchoolId);
								}

								// Create teacher entity
								var teacher = new TeacherMaster
								{
									Id = Guid.NewGuid(),
									FirstName = firstName,
									LastName = lastName,
									DOB = dob ?? DateTime.UtcNow.Date, // Default to today if not provided
									DOJ = doj,
									DateOfLeaving = null,
									Address = address,
									CityId = null, // These will need to be set properly if needed
									StateId = null,
									CountryId = null,
									ZipCode = zipCode,
									Gender = null, // This will need to be set properly if needed
									MaritalStatusId = null, // This will need to be set properly if needed
									Image = string.Empty,
									Email = email,
									Phone = phone,
									MobilePhone = mobilePhone,
									YearsOfExperience = yearsOfExperience,
									PreviousSchool = previousSchool,
									Salutation = salutation,
									CompanyId = companyId,
									SchoolId = teacherSchoolId,
									IsActive = isActive,
									IsDeleted = false,
									CreatedBy = userId,
									CreatedDate = DateTime.UtcNow,
									Status = "INC",
									StatusMessage = "Imported from Excel template"
								};

								_logger.LogInformation("Created teacher entity: {FirstName} {LastName} with SchoolId: {SchoolId}, CompanyId: {CompanyId}", teacher.FirstName, teacher.LastName, teacher.SchoolId, teacher.CompanyId);
								teachers.Add(teacher);
							}
						}
					}
					catch (FileFormatException ex) when (ex.Message.Contains("corrupted"))
					{
						_logger.LogError(ex, "Corrupted Excel file detected");
						ModelState.AddModelError("", "The uploaded Excel file appears to be corrupted. Please ensure it's a valid Excel file and try again.");
						return View();
					}
					catch (Exception ex)
					{
						_logger.LogError(ex, "Invalid Excel file format or reading error");
						ModelState.AddModelError("", "The uploaded file is not a valid Excel file or is corrupted. Please upload a valid .xlsx or .xls file.");
						return View();
					}
				}

				if (!ModelState.IsValid)
				{
					_logger.LogWarning("ModelState is not valid after processing Excel file");
					foreach (var key in ModelState.Keys)
					{
						var state = ModelState[key];
						if (state != null && state.Errors.Count > 0)
						{
							_logger.LogWarning("ModelState error for {Key}: {Errors}", key, string.Join(", ", state.Errors.Select(e => e.ErrorMessage)));
						}
					}
					return View();
				}

				_logger.LogInformation("Processed {TeacherCount} teachers from Excel file", teachers.Count);
				
				if (teachers.Any())
				{
					// Save teachers
					var successCount = 0;
					foreach (var teacher in teachers)
					{
						try
						{
							_logger.LogInformation("Attempting to create teacher: {FirstName} {LastName} with Email: {Email}", teacher.FirstName, teacher.LastName, teacher.Email);
							_logger.LogInformation("Teacher details - DOB: {DOB}, SchoolId: {SchoolId}, CompanyId: {CompanyId}", teacher.DOB, teacher.SchoolId, teacher.CompanyId);
							
							// Validate required fields before creating
							if (string.IsNullOrEmpty(teacher.FirstName))
							{
								_logger.LogWarning("Teacher FirstName is null or empty");
								continue;
							}
							
							if (teacher.DOB == DateTime.MinValue)
							{
								_logger.LogWarning("Teacher DOB is not set");
								continue;
							}
							
							if (teacher.SchoolId == Guid.Empty)
							{
								_logger.LogWarning("Teacher SchoolId is not set");
								continue;
							}
							
							if (teacher.CompanyId == Guid.Empty)
							{
								_logger.LogWarning("Teacher CompanyId is not set");
								continue;
							}
							
							if (teacher.CreatedBy == Guid.Empty)
							{
								_logger.LogWarning("Teacher CreatedBy is not set");
								continue;
							}
							
							var newId = _service.Create(teacher);
							_logger.LogInformation("Teacher service Create returned ID: {NewId}", newId);
							
							if (newId != Guid.Empty)
							{
								_logger.LogInformation("Successfully created teacher: {FirstName} {LastName} with ID: {TeacherId}", teacher.FirstName, teacher.LastName, newId);
								successCount++;
								
								// Also create/update the corresponding EmpMaster record
								try
								{
									var emp = new EmpMaster
									{
										Id = newId, // Use the same ID as the TeacherMaster
										FirstName = teacher.FirstName,
										LastName = teacher.LastName ?? string.Empty,
										EmailId = teacher.Email ?? string.Empty,
										PhoneNumber = teacher.Phone ?? string.Empty,
										MobileNumber = teacher.MobilePhone ?? string.Empty,
										DOB = teacher.DOB,
										DOJ = teacher.DOJ ?? DateTime.UtcNow, // Ensure DOJ is always set
										CompanyId = teacher.CompanyId,
										SchoolId = teacher.SchoolId,
										IsActive = teacher.IsActive,
										IsDeleted = teacher.IsDeleted,
										CreatedBy = teacher.CreatedBy,
										CreatedDate = teacher.CreatedDate,
										Status = "INC",
										StatusMessage = "Created from Teacher import",
										Salutation = teacher.Salutation ?? string.Empty,
										PrevioudSchoolCompany = teacher.PreviousSchool ?? string.Empty,
										YearsOfExperience = teacher.YearsOfExperience ?? string.Empty,
										// Initialize other required fields with default values
										CurrentAddress1 = string.Empty,
										CurrentAddress2 = string.Empty,
										PermanentAddress1 = string.Empty,
										PermanentAddress2 = string.Empty,
										CurrentZipCode = string.Empty,
										PermanentZipCode = string.Empty,
										PANNumber = string.Empty,
										ESICNumber = string.Empty,
										PFNumeber = string.Empty,
										FathersName = string.Empty,
										MothersName = string.Empty,
										Description = string.Empty,
										LicenceNumber = string.Empty,
										LicenceDescription = string.Empty,
										LicenceImage = string.Empty,
										LicenceType = string.Empty,
										DateOfLeaving = teacher.DateOfLeaving,
										MaritalStatus = string.Empty,
										AadhaarNumber = string.Empty,
										MathUpToClass = null,
										EnglishUptoClass = null,
										SSTUptoClass = null,
										CurrentCityId = null,
										CurrentStateId = null,
										CurrentCountryId = null,
										PermanentCityId = null,
										PermanentStateId = null,
										PermanentCountryId = null,
										DepartmentId = null,
										DesignationId = null,
										PaymentModeId = null,
										EmployeeTypeId = null,
										CategoryId = null,
										BankAccountNumber = string.Empty,
										BankName = string.Empty,
										GenderId = null,
										BloodGroupId = null,
										GradeId = null,
										Image = string.Empty,
										EmployeeOldId = null,
										LicenceIssueDate = null,
										LicenceValidUpto = null
									};
									
									// Try to create the EmpMaster record
									// If it already exists, the stored procedure should handle the update
									_logger.LogInformation("Attempting to create EmpMaster record for teacher: {TeacherId}", newId);
									var empId = _empService.Create(emp);
									_logger.LogInformation("EmpMaster service Create returned ID: {EmpId}", empId);
									
									if (empId != Guid.Empty)
									{
										_logger.LogInformation("Successfully created EmpMaster record for teacher: {TeacherId}", newId);
									}
									else
									{
										_logger.LogWarning("Failed to create EmpMaster record for teacher: {TeacherId}", newId);
									}
								}
								catch (Exception empEx)
								{
									_logger.LogWarning(empEx, "Failed to create EmpMaster record for teacher {TeacherId}", newId);
								}
							}
							else
							{
								_logger.LogWarning("Failed to import teacher: {FirstName} {LastName}", teacher.FirstName, teacher.LastName);
							}
						}
						catch (Exception ex)
						{
							_logger.LogError(ex, "Error importing teacher: {FirstName} {LastName}", teacher.FirstName, teacher.LastName);
						}
					}

					if (successCount > 0)
					{
						TempData["SuccessMessage"] = $"{successCount} teachers imported successfully!";
						return RedirectToAction("Index");
					}
					else
					{
						ModelState.AddModelError("", "No teachers were imported successfully. Please check the file format and try again.");
						return View();
					}
				}

				ModelState.AddModelError("", "No valid teachers found in the Excel file.");
				return View();
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error importing teachers from Excel");
				ModelState.AddModelError("", "An error occurred while importing teachers. Please check the file format and try again.");
				return View();
			}
		}

		[HttpGet]
		[Route("TestImport")]
		public IActionResult TestImport()
		{
			try
			{
				// Create a test teacher
				var userIdStr = HttpContext.Session.GetString("UserId");
				var companyIdStr = HttpContext.Session.GetString("CompanyId");
				var schoolIdStr = HttpContext.Session.GetString("SchoolId");

				if (string.IsNullOrWhiteSpace(userIdStr) || !Guid.TryParse(userIdStr, out var userId) ||
					string.IsNullOrWhiteSpace(companyIdStr) || !Guid.TryParse(companyIdStr, out var companyId) ||
					string.IsNullOrWhiteSpace(schoolIdStr) || !Guid.TryParse(schoolIdStr, out var schoolId))
				{
					return Content("User session expired. Please login again.");
				}

				var teacher = new TeacherMaster
				{
					Id = Guid.NewGuid(),
					FirstName = "Test",
					LastName = "Teacher",
					DOB = new DateTime(1980, 1, 1),
					DOJ = DateTime.UtcNow,
					Email = "test.teacher@school.edu",
					Phone = "123-456-7890",
					MobilePhone = "987-654-3210",
					Address = "123 Test St",
					ZipCode = "12345",
					CompanyId = companyId,
					SchoolId = schoolId,
					IsActive = true,
					IsDeleted = false,
					CreatedBy = userId,
					CreatedDate = DateTime.UtcNow,
					Status = "INC",
					StatusMessage = "Test import"
				};

				_logger.LogInformation("Attempting to create test teacher");
				var newId = _service.Create(teacher);
				if (newId != Guid.Empty)
				{
					_logger.LogInformation("Successfully created test teacher with ID: {TeacherId}", newId);
					return Content($"Successfully created test teacher with ID: {newId}");
				}
				else
				{
					_logger.LogWarning("Failed to create test teacher");
					return Content("Failed to create test teacher");
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error creating test teacher");
				return Content($"Error creating test teacher: {ex.Message}");
			}
		}

		//[HttpGet]
		//[Route("TestDatabase")]
		//public IActionResult TestDatabase()
		//{
		//	try
		//	{
		//		_logger.LogInformation("Testing database connection and stored procedures");
				
		//		// Test Teacher_GetAll stored procedure
		//		Proc p = new Proc("Teacher_GetAll_SchoolId");
		//		p[""
		//		var dt = new DataTable();
		//		p.Exec(dt);
		//		_logger.LogInformation("Teacher_GetAll executed successfully, returned {RowCount} rows", dt.Rows.Count);
				
		//		// Test Emp_GetAll stored procedure
		//		Proc p2 = new Proc("Emp_GetAll");
		//		var dt2 = new DataTable();
		//		p2.Exec(dt2);
		//		_logger.LogInformation("Emp_GetAll executed successfully, returned {RowCount} rows", dt2.Rows.Count);
				
		//		return Content($"Database test successful. Teachers: {dt.Rows.Count}, Employees: {dt2.Rows.Count}");
		//	}
		//	catch (Exception ex)
		//	{
		//		_logger.LogError(ex, "Database test failed");
		//		return Content($"Database test failed: {ex.Message}");
		//	}
		//}

		[HttpGet]
		[Route("TestStoredProcedure")]
		public IActionResult TestStoredProcedure()
		{
			try
			{
				_logger.LogInformation("Testing Teacher_Create stored procedure directly");
				
				var userIdStr = HttpContext.Session.GetString("UserId");
				var companyIdStr = HttpContext.Session.GetString("CompanyId");
				var schoolIdStr = HttpContext.Session.GetString("SchoolId");

				if (string.IsNullOrWhiteSpace(userIdStr) || !Guid.TryParse(userIdStr, out var userId) ||
					string.IsNullOrWhiteSpace(companyIdStr) || !Guid.TryParse(companyIdStr, out var companyId) ||
					string.IsNullOrWhiteSpace(schoolIdStr) || !Guid.TryParse(schoolIdStr, out var schoolId))
				{
					return Content("User session expired. Please login again.");
				}

				// Test Teacher_Create stored procedure directly
				Proc p = new Proc("Teacher_Create");
				p["@FirstName"] = "Test";
				p["@LastName"] = "Procedure";
				p["@DOB"] = new DateTime(1980, 1, 1);
				p["@DOJ"] = (object?)DBNull.Value;
				p["@DateOfLeaving"] = (object?)DBNull.Value;
				p["@Address"] = "123 Test St";
				p["@CityId"] = (object?)DBNull.Value;
				p["@StateId"] = (object?)DBNull.Value;
				p["@CountryId"] = (object?)DBNull.Value;
				p["@ZipCode"] = "12345";
				p["@Gender"] = (object?)DBNull.Value;
				p["@MaritalStatusId"] = (object?)DBNull.Value;
				p["@Image"] = "";
				p["@Email"] = "test.procedure@school.edu";
				p["@Phone"] = "123-456-7890";
				p["@MobilePhone"] = "987-654-3210";
				p["@YearsOfExperience"] = "";
				p["@PreviousSchool"] = "";
				p["@Salutation"] = "";
				p["@IsActive"] = true;
				p["@IsDeleted"] = false;
				p["@CompanyId"] = companyId;
				p["@SchoolId"] = schoolId;
				p["@CreatedBy"] = userId;
				p["@Status"] = "INC";
				p["@StatusMessage"] = "Test procedure";
				
				var dt = new DataTable();
				p.Exec(dt);
				_logger.LogInformation("Teacher_Create stored procedure executed, rows returned: {RowCount}", dt.Rows.Count);
				
				if (dt.Rows.Count > 0)
				{
					var idObj = dt.Rows[0]["Id"];
					if (idObj != null && Guid.TryParse(idObj.ToString(), out var newId))
					{
						_logger.LogInformation("Successfully created teacher with ID: {TeacherId}", newId);
						return Content($"Successfully created teacher with ID: {newId}");
					}
					else
					{
						_logger.LogWarning("Failed to parse ID from stored procedure result");
						return Content("Failed to parse ID from stored procedure result");
					}
				}
				else
				{
					_logger.LogWarning("No rows returned from Teacher_Create stored procedure");
					return Content("No rows returned from Teacher_Create stored procedure");
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error testing Teacher_Create stored procedure");
				return Content($"Error testing Teacher_Create stored procedure: {ex.Message}");
			}
		}

		[HttpGet]
		[Route("TestConnection")]
		public IActionResult TestConnection()
		{
			try
			{
				_logger.LogInformation("Testing database connection");
				
				using (var connection = new SqlConnection(SchoolPortal.DBAccess.ConnectionManager.DefaultConnectionManager.ConnectionString))
				{
					connection.Open();
					_logger.LogInformation("Database connection successful");
					
					using (var command = new SqlCommand("SELECT COUNT(*) FROM TeacherMaster", connection))
					{
						var count = (int)command.ExecuteScalar();
						_logger.LogInformation("TeacherMaster table count: {Count}", count);
						return Content($"Database connection successful. TeacherMaster table has {count} records.");
					}
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Database connection test failed");
				return Content($"Database connection test failed: {ex.Message}");
			}
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
