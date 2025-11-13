using System;
using System.Linq;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolPortal.Entities.Models;
using SchoolPortalApp.Models;
using SchoolPortal.Services.IServices;

namespace SchoolPortalApp.Controllers
{
	[Route("StudentMaster")]
	public class StudentMasterController : Controller
	{
		private readonly IStudentService _service;
		private readonly ISchoolService _schoolService;
		private readonly IClassService _classService;
		private readonly ISectionService _sectionService;
		private readonly ITeacherService _teacherService;
		private readonly ILookupService _lookupService;
		private readonly ICompanyService _companyService;
		private readonly ILogger<StudentMasterController> _logger;
		private readonly IWebHostEnvironment _env;
		private readonly IParentService _parentService;

		public StudentMasterController(
			IStudentService service,
			ISchoolService schoolService,
			IClassService classService,
			ISectionService sectionService,
			ITeacherService teacherService,
			ILookupService lookupService,
			ICompanyService companyService,
			IParentService parentService,
			ILogger<StudentMasterController> logger,
			IWebHostEnvironment env)
		{
			_service = service;
			_schoolService = schoolService;
			_classService = classService;
			_sectionService = sectionService;
			_teacherService = teacherService;
			_lookupService = lookupService;
			_companyService = companyService;
			_logger = logger;
			_env = env;
			_parentService = parentService;
		}

		[HttpGet]
		[Route("GetSectionsByClass/{classId}")]
		public IActionResult GetSectionsByClass(Guid classId)
		{
			try
			{
				var sections = _sectionService.GetSectionsByClassId(classId);
				var result = sections.Select(s => new 
				{ 
					value = s.Id.ToString(), 
					text = s.Name 
				});
				return Json(result);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error getting sections for class {ClassId}", classId);
				return StatusCode(500, "Error loading sections");
			}
		}

		[HttpGet]
		[Route("GetStatesByCountry/{countryId}")]
		public IActionResult GetStatesByCountry(Guid countryId)
		{
			try
			{
				var states = _lookupService.GetStates(countryId);
				var result = states.Select(s => new 
				{ 
					value = s.Id.ToString(), 
					text = s.Name 
				});
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
				var result = cities.Select(c => new 
				{ 
					value = c.Id.ToString(), 
					text = c.Name 
				});
				return Json(result);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error getting cities for state {StateId}", stateId);
				return StatusCode(500, "Error loading cities");
			}
		}

		private void PopulateDropdowns(StudentViewModel vm)
		{
			var schools = _schoolService.GetAll();
			vm.Schools = schools.Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Name, Selected = s.Id == vm.SchoolId }).ToList();

			// Classes
			var classes = _classService.GetAll();
			vm.Classes = classes.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name, Selected = c.Id == vm.ClassId }).ToList();
			// Previous School Classes (reuse class list)
			vm.PreviousSchoolClasses = classes.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name, Selected = vm.PreviousSchoolClassId.HasValue && c.Id == vm.PreviousSchoolClassId.Value }).ToList();
			// Sibling Classes (reuse class list)
			vm.SiblingClasses = classes.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name, Selected = vm.SiblingClassId.HasValue && c.Id == vm.SiblingClassId.Value }).ToList();

			// Sections
			var sections = _sectionService.GetAll();
			vm.Sections = sections.Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Name, Selected = s.Id == vm.SectionId }).ToList();

			// Teachers (Class Teachers)
			var teachers = _teacherService.GetAll();
			vm.ClassTeachers = teachers.Select(t => new SelectListItem { Value = t.Id.ToString(), Text = string.Join(" ", new[] { t.FirstName, t.LastName }.Where(x => !string.IsNullOrWhiteSpace(x))), Selected = vm.ClassTeacherId.HasValue && t.Id == vm.ClassTeacherId.Value }).ToList();

			// Genders
			var genders = _lookupService.GetGenders();
			vm.Genders = genders.Select(g => new SelectListItem { Value = g.Id.ToString(), Text = g.Name, Selected = vm.Gender.HasValue && g.Id == vm.Gender.Value }).ToList();

			// Countries / States / Cities
			var countries = _lookupService.GetCountries();
			vm.Countries = countries.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name, Selected = x.Id == vm.CountryId }).ToList();
			vm.BirthCountries = countries.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name, Selected = x.Id == vm.BirthCountryId }).ToList();
			vm.Nationalities = countries.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name, Selected = x.Id == vm.Nationality }).ToList();

			// Categories (CategoryMaster)
			var categories = _lookupService.GetCategories();
			vm.Categories = categories.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name, Selected = c.Id == vm.CategoryId }).ToList();

			// Blood Groups
			var bloodGroups = _lookupService.GetBloodGroups();
			vm.BloodGroups = bloodGroups.Select(b => new SelectListItem { Value = b.Id.ToString(), Text = b.Name, Selected = b.Id == vm.BloodGroupId }).ToList();

			// Religions
			var religions = _lookupService.GetReligions();
			vm.Religions = religions.Select(r => new SelectListItem { Value = r.Id.ToString(), Text = r.Name, Selected = r.Id == vm.ReligionId }).ToList();

			// School Boards
			var boards = _lookupService.GetSchoolBoards();
			vm.PreviousSchoolBoards = boards.Select(b => new SelectListItem { Value = b.Id.ToString(), Text = b.Name, Selected = b.Id == vm.PreviousSchoolBoardId }).ToList();

			// Fix for cascading dropdowns - Always populate states and cities if IDs are set
			if (vm.CountryId != Guid.Empty)
			{
				var states = _lookupService.GetStates(vm.CountryId);
				vm.States = states.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name, Selected = x.Id == vm.StateId }).ToList();
				if (vm.StateId != Guid.Empty)
				{
					var cities = _lookupService.GetCities(vm.StateId);
					vm.Cities = cities.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name, Selected = x.Id == vm.CityId }).ToList();
				}
				else
				{
					// Initialize empty cities list if no state is selected
					vm.Cities = new List<SelectListItem>();
				}
			}
			else
			{
				// Initialize empty states and cities lists if no country is selected
				vm.States = new List<SelectListItem>();
				vm.Cities = new List<SelectListItem>();
			}

			if (vm.BirthCountryId != Guid.Empty)
			{
				var birthStates = _lookupService.GetStates(vm.BirthCountryId);
				vm.BirthStates = birthStates.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name, Selected = x.Id == vm.BirthStateId }).ToList();
				if (vm.BirthStateId != Guid.Empty)
				{
					var birthCities = _lookupService.GetCities(vm.BirthStateId);
					vm.BirthCities = birthCities.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name, Selected = x.Id == vm.BirthCityId }).ToList();
				}
				else
				{
					// Initialize empty birth cities list if no birth state is selected
					vm.BirthCities = new List<SelectListItem>();
				}
			}
			else
			{
				// Initialize empty birth states and cities lists if no birth country is selected
				vm.BirthStates = new List<SelectListItem>();
				vm.BirthCities = new List<SelectListItem>();
			}

			// Parents tab lookups
			var relationTypes = _lookupService.GetRelationTypes();
			vm.ParentRelationTypes = relationTypes.Select(r => new SelectListItem { Value = r.Id.ToString(), Text = r.Name, Selected = vm.ParentRelationTypeId.HasValue && r.Id == vm.ParentRelationTypeId.Value }).ToList();

			var qualifications = _lookupService.GetQualifications();
			vm.ParentQualifications = qualifications.Select(q => new SelectListItem { Value = q.Id.ToString(), Text = q.Name, Selected = vm.ParentQualificationId.HasValue && q.Id == vm.ParentQualificationId.Value }).ToList();

			// Reuse designations
			var parentDesignations = _lookupService.GetDesignations();
			vm.ParentDesignations = parentDesignations.Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name, Selected = vm.ParentDesignationId.HasValue && d.Id == vm.ParentDesignationId.Value }).ToList();

			// Parent address dropdowns
			vm.ParentCountries = _lookupService.GetCountries()
				.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name, Selected = vm.ParentCountryId.HasValue && c.Id == vm.ParentCountryId.Value }).ToList();
			
			if (vm.ParentCountryId.HasValue && vm.ParentCountryId.Value != Guid.Empty)
			{
				var pstates = _lookupService.GetStates(vm.ParentCountryId.Value);
				vm.ParentStates = pstates.Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Name, Selected = vm.ParentStateId.HasValue && s.Id == vm.ParentStateId.Value }).ToList();
				if (vm.ParentStateId.HasValue && vm.ParentStateId.Value != Guid.Empty)
				{
					var pcities = _lookupService.GetCities(vm.ParentStateId.Value);
					vm.ParentCities = pcities.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name, Selected = vm.ParentCityId.HasValue && c.Id == vm.ParentCityId.Value }).ToList();
				}
				else
				{
					// Initialize empty parent cities list if no parent state is selected
					vm.ParentCities = new List<SelectListItem>();
				}
			}
			else
			{
				// Initialize empty parent states and cities lists if no parent country is selected
				vm.ParentStates = new List<SelectListItem>();
				vm.ParentCities = new List<SelectListItem>();
			}
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
				return new StudentListItemViewModel
				{
					Id = item.Id,
					Name = string.Join(" ", new[] { item.FirstName, item.LastName }.Where(x => !string.IsNullOrWhiteSpace(x))),
					Email = item.Email ?? string.Empty,
					Phone = item.Phone ?? string.Empty,
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
			if (id == Guid.Empty) return BadRequest();

			var item = _service.GetById(id);
			if (item == null) return NotFound();

			var vm = new StudentViewModel
			{
				Id = item.Id,
				RollNumber = item.RollNumber,
				FirstName = item.FirstName,
				LastName = item.LastName,
				Address = item.Address,
				CityId = item.CityId,
				StateId = item.StateId,
				CountryId = item.CountryId,
				ZipCode = item.ZipCode,
				ContactNumber = item.ContactNumber,
				EmergencyContactNumber = item.EmergencyContactNumber,
				DOB = item.DOB,
				DOJ = item.DOJ,
				RegistrationNumber = item.RegistrationNumber,
				ClassId = item.ClassId,
				SectionId = item.SectionId,
				AvailTransport = item.AvailTransport,
				Image = item.Image,
				Email = item.Email,
				Phone = item.Phone,
				CategoryId = item.CategoryId,
				SiblingsIfAny = item.SiblingsIfAny,
				SiblingClassId = item.SiblingClassId,
				Gender = item.Gender,
				DisabilityAny = item.DisabilityAny,
				MedicalAlleryAny = item.MedicalAlleryAny,
				BirthCityId = item.BirthCityId,
				BirthStateId = item.BirthStateId,
				BirthCountryId = item.BirthCountryId,
				PreviousSchoolAttended = item.PreviousSchoolAttended,
				PreviousSchoolClassId = item.PreviousSchoolClassId,
				PreviousSchoolPercentage = item.PreviousSchoolPercentage,
				PreviousSchoolRank = item.PreviousSchoolRank,
				PreviousSchoolBoardId = item.PreviousSchoolBoardId,
				PreviousSchoolFromDate = item.PreviousSchoolFromDate,
				PreviousSchoolToDate = item.PreviousSchoolToDate,
				WithdrawnDate = item.WithdrawnDate,
				WithdrawnReason = item.WithdrawnReason,
				BloodGroupId = item.BloodGroupId,
				Nationality = item.Nationality,
				Hobbies = item.Hobbies,
				ReligionId = item.ReligionId,
				RouteId = item.RouteId,
				RouteStopDetailsId = item.RouteStopDetailsId,
				ClassTeacherId = item.ClassTeacherId,
				RoutePickAndDrop = item.RoutePickAndDrop,
				FeesDiscountCategoryMasterId = item.FeesDiscountCategoryMasterId,
				TutionFees = item.TutionFees,
				AnnualFees = item.AnnualFees,
				TransportFees = item.TransportFees,
				UseTransportFees = item.UseTransportFees,
				SessionId = item.SessionId,
				CompanyId = item.CompanyId,
				SchoolId = item.SchoolId,
				IsActive = item.IsActive,
				IsDeleted = item.IsDeleted,
				Status = item.Status,
				StatusMessage = item.StatusMessage,
				HouseAllotted = item.HouseAllotted
			};

			// Load parent data (Parents tab)
			try
			{
				var parent = _parentService.GetByStudentId(id);
				if (parent != null)
				{
					vm.ParentFirstName = parent.ParentFirstName;
					vm.ParentLastName = parent.ParentLastName;
					vm.ParentDOB = parent.ParentDOB;
					vm.ParentRelationTypeId = parent.RelationTypeId != Guid.Empty ? parent.RelationTypeId : null;
					vm.ParentQualificationId = parent.QualificationId != Guid.Empty ? parent.QualificationId : null;
					vm.ParentDesignationId = parent.DesignationId != Guid.Empty ? parent.DesignationId : null;
					vm.ParentOccupation = parent.Occupation;
					vm.ParentAnnualIncome = parent.AnnualIncome;
					vm.ParentPhone = parent.Phone;
					vm.ParentEmail = parent.Email;
					vm.ParentAddress1 = parent.Address1;
					vm.ParentAddress2 = parent.Address2;
					vm.ParentCountryId = parent.CountryId != Guid.Empty ? parent.CountryId : null;
					vm.ParentStateId = parent.StateId != Guid.Empty ? parent.StateId : null;
					vm.ParentCityId = parent.CityId != Guid.Empty ? parent.CityId : null;
					vm.ParentZipCode = parent.ZipCode;
					vm.ParentIsActive = parent.IsActive;
				}
			}
			catch
			{
				// Non-blocking
			}

			// Populate all dropdowns used across tabs
			PopulateDropdowns(vm);

			// Friendly names for IDs used by tabs
			try
			{
				// Company
				if (item.CompanyId != Guid.Empty)
				{
					ViewBag.CompanyName = _companyService.CompanyNameById(item.CompanyId);
					ViewBag.CompanyId = item.CompanyId;
				}
				else
				{
					ViewBag.CompanyName = string.Empty;
					ViewBag.CompanyId = Guid.Empty;
				}

				// School
				if (item.SchoolId != Guid.Empty)
				{
					ViewBag.SchoolName = _schoolService.SchoolNameById(item.SchoolId);
					ViewBag.SchoolId = item.SchoolId;
				}
				else
				{
					ViewBag.SchoolName = string.Empty;
					ViewBag.SchoolId = Guid.Empty;
				}

				// Country / State / City (Address tab)
				if (item.CountryId != Guid.Empty)
				{
					var countries = _lookupService.GetCountries();
					var country = countries.FirstOrDefault(c => c.Id == item.CountryId);
					ViewBag.CountryName = country?.Name ?? string.Empty;
					ViewBag.CountryId = item.CountryId;
				}
				else
				{
					ViewBag.CountryName = string.Empty;
					ViewBag.CountryId = Guid.Empty;
				}

				if (item.StateId != Guid.Empty && item.CountryId != Guid.Empty)
				{
					var states = _lookupService.GetStates(item.CountryId);
					var state = states.FirstOrDefault(s => s.Id == item.StateId);
					ViewBag.StateName = state?.Name ?? string.Empty;
					ViewBag.StateId = item.StateId;
				}
				else
				{
					ViewBag.StateName = string.Empty;
					ViewBag.StateId = Guid.Empty;
				}

				if (item.CityId != Guid.Empty && item.StateId != Guid.Empty)
				{
					var cities = _lookupService.GetCities(item.StateId);
					var city = cities.FirstOrDefault(c => c.Id == item.CityId);
					ViewBag.CityName = city?.Name ?? string.Empty;
					ViewBag.CityId = item.CityId;
				}
				else
				{
					ViewBag.CityName = string.Empty;
					ViewBag.CityId = Guid.Empty;
				}

				// Class / Section (Academic tab)
				if (item.ClassId != Guid.Empty)
				{
					ViewBag.ClassName = _classService.ClassNameById(item.ClassId);
					ViewBag.ClassId = item.ClassId;
				}
				else
				{
					ViewBag.ClassName = string.Empty;
					ViewBag.ClassId = Guid.Empty;
				}

				if (item.SectionId != Guid.Empty)
				{
					ViewBag.SectionName = _sectionService.SectionNameById(item.SectionId);
					ViewBag.SectionId = item.SectionId;
				}
				else
				{
					ViewBag.SectionName = string.Empty;
					ViewBag.SectionId = Guid.Empty;
				}

				// Category (Additional tab)
				if (item.CategoryId != Guid.Empty)
				{
					var categories = _lookupService.GetCategories();
					var category = categories.FirstOrDefault(c => c.Id == item.CategoryId);
					ViewBag.CategoryName = category?.Name ?? string.Empty;
					ViewBag.CategoryId = item.CategoryId;
				}
				else
				{
					ViewBag.CategoryName = string.Empty;
					ViewBag.CategoryId = Guid.Empty;
				}

				// Medical & Background (Medical tab)
				if (item.BloodGroupId != Guid.Empty)
				{
					var bloodGroups = _lookupService.GetBloodGroups();
					var bg = bloodGroups.FirstOrDefault(b => b.Id == item.BloodGroupId);
					ViewBag.BloodGroupName = bg?.Name ?? string.Empty;
				}
				else
				{
					ViewBag.BloodGroupName = string.Empty;
				}

				if (item.Nationality != Guid.Empty)
				{
					var countries = _lookupService.GetCountries();
					var nat = countries.FirstOrDefault(c => c.Id == item.Nationality);
					ViewBag.NationalityName = nat?.Name ?? string.Empty;
				}
				else
				{
					ViewBag.NationalityName = string.Empty;
				}

				if (item.ReligionId != Guid.Empty)
				{
					var religions = _lookupService.GetReligions();
					var rel = religions.FirstOrDefault(r => r.Id == item.ReligionId);
					ViewBag.ReligionName = rel?.Name ?? string.Empty;
				}
				else
				{
					ViewBag.ReligionName = string.Empty;
				}

				// Birth place names (Medical tab)
				if (item.BirthCountryId != Guid.Empty)
				{
					var countries = _lookupService.GetCountries();
					var bc = countries.FirstOrDefault(c => c.Id == item.BirthCountryId);
					ViewBag.BirthCountryName = bc?.Name ?? string.Empty;
				}
				else
				{
					ViewBag.BirthCountryName = string.Empty;
				}

				if (item.BirthStateId != Guid.Empty && item.BirthCountryId != Guid.Empty)
				{
					var states = _lookupService.GetStates(item.BirthCountryId);
					var bs = states.FirstOrDefault(s => s.Id == item.BirthStateId);
					ViewBag.BirthStateName = bs?.Name ?? string.Empty;
				}
				else
				{
					ViewBag.BirthStateName = string.Empty;
				}

				if (item.BirthCityId != Guid.Empty && item.BirthStateId != Guid.Empty)
				{
					var cities = _lookupService.GetCities(item.BirthStateId);
					var bcit = cities.FirstOrDefault(c => c.Id == item.BirthCityId);
					ViewBag.BirthCityName = bcit?.Name ?? string.Empty;
				}
				else
				{
					ViewBag.BirthCityName = string.Empty;
				}

				// Class teacher (Academic tab)
				var teachers = _teacherService.GetAll();
				var t = teachers.FirstOrDefault(x => x.Id == item.ClassTeacherId);
				ViewBag.ClassTeacherName = t == null ? string.Empty : string.Join(" ", new[] { t.FirstName, t.LastName }.Where(x => !string.IsNullOrWhiteSpace(x)));
			}
			catch
			{
				// Non-blocking
			}

			return View(vm);
		}

		[HttpGet]
		[Route("Create")]
		public IActionResult Create()
		{
			var vm = new StudentViewModel();
			PopulateDropdowns(vm);
			return View(vm);
		}

		[HttpPost]
		[Route("Create")]
		[ValidateAntiForgeryToken]
		public IActionResult Create(StudentViewModel model)
		{
			var schoolIdStr = HttpContext.Session.GetString("SchoolId");
			if (!string.IsNullOrWhiteSpace(schoolIdStr) && Guid.TryParse(schoolIdStr, out var schoolId))
			{
				ModelState.Remove(nameof(StudentViewModel.SchoolId));
				model.SchoolId = schoolId;
			}

			// Server-side required validations for location fields
			if (model.CountryId == Guid.Empty)
			{
				ModelState.AddModelError(nameof(StudentViewModel.CountryId), "Country is required.");
			}
			if (model.StateId == Guid.Empty)
			{
				ModelState.AddModelError(nameof(StudentViewModel.StateId), "State is required.");
			}
			if (model.CityId == Guid.Empty)
			{
				ModelState.AddModelError(nameof(StudentViewModel.CityId), "City is required.");
			}

			var userIdStr = HttpContext.Session.GetString("UserId");
			var companyIdStr = HttpContext.Session.GetString("CompanyId");
			if (string.IsNullOrWhiteSpace(userIdStr) || !Guid.TryParse(userIdStr, out var userId) || string.IsNullOrWhiteSpace(companyIdStr) || !Guid.TryParse(companyIdStr, out var companyId) || model.SchoolId == Guid.Empty)
			{
				ModelState.AddModelError(string.Empty, "Please login and select company to create student.");
				PopulateDropdowns(model);
				return View(model);
			}

			// Handle image upload if provided
			if (model.ImageFile != null && model.ImageFile.Length > 0)
			{
				var uploadsRoot = Path.Combine(_env.WebRootPath ?? string.Empty, "uploads", "students");
				Directory.CreateDirectory(uploadsRoot);
				var fileName = $"{Guid.NewGuid()}{Path.GetExtension(model.ImageFile.FileName)}";
				var fullPath = Path.Combine(uploadsRoot, fileName);
				using (var stream = System.IO.File.Create(fullPath))
				{
					model.ImageFile.CopyTo(stream);
				}
				// store web-relative path
				model.Image = $"/uploads/students/{fileName}";
			}

			var entity = new StudentMaster
			{
				Id = Guid.Empty,
				RollNumber = model.RollNumber,
				FirstName = model.FirstName,
				LastName = model.LastName ?? string.Empty,
				Address = model.Address ?? string.Empty,
				CityId = model.CityId,
				StateId = model.StateId,
				CountryId = model.CountryId,
				ZipCode = model.ZipCode ?? string.Empty,
				ContactNumber = model.ContactNumber ?? string.Empty,
				EmergencyContactNumber = model.EmergencyContactNumber ?? string.Empty,
				DOB = model.DOB,
				DOJ = model.DOJ,
				RegistrationNumber = model.RegistrationNumber ?? string.Empty,
				ClassId = model.ClassId,
				SectionId = model.SectionId,
				AvailTransport = model.AvailTransport,
				Image = model.Image ?? string.Empty,
				Email = model.Email ?? string.Empty,
				CategoryId = model.CategoryId,
				SiblingsIfAny = model.SiblingsIfAny,
				SiblingClassId = model.SiblingClassId,
				Gender = model.Gender,
				DisabilityAny = model.DisabilityAny ?? string.Empty,
				MedicalAlleryAny = model.MedicalAlleryAny ?? string.Empty,
				BirthCityId = model.BirthCityId,
				BirthStateId = model.BirthStateId,
				BirthCountryId = model.BirthCountryId,
				PreviousSchoolAttended = model.PreviousSchoolAttended ?? string.Empty,
				PreviousSchoolClassId = model.PreviousSchoolClassId,
				PreviousSchoolPercentage = model.PreviousSchoolPercentage,
				PreviousSchoolRank = model.PreviousSchoolRank ?? string.Empty,
				PreviousSchoolBoardId = model.PreviousSchoolBoardId,
				PreviousSchoolFromDate = model.PreviousSchoolFromDate,
				PreviousSchoolToDate = model.PreviousSchoolToDate,
				WithdrawnDate = model.WithdrawnDate,
				WithdrawnReason = model.WithdrawnReason ?? string.Empty,
				BloodGroupId = model.BloodGroupId,
				Nationality = model.Nationality,
				Hobbies = model.Hobbies ?? string.Empty,
				ReligionId = model.ReligionId,
				Phone = model.Phone ?? string.Empty,
				RouteId = model.RouteId,
				RouteStopDetailsId = model.RouteStopDetailsId,
				ClassTeacherId = model.ClassTeacherId,
				RoutePickAndDrop = model.RoutePickAndDrop,
				FeesDiscountCategoryMasterId = model.FeesDiscountCategoryMasterId,
				TutionFees = model.TutionFees,
				AnnualFees = model.AnnualFees,
				TransportFees = model.TransportFees,
				UseTransportFees = model.UseTransportFees,
				SessionId = model.SessionId,
				CompanyId = companyId,
				SchoolId = model.SchoolId,
				IsActive = model.IsActive,
				IsDeleted = model.IsDeleted,
				CreatedBy = userId,
				CreatedDate = DateTime.UtcNow,
				Status = model.Status ?? string.Empty,
				StatusMessage = model.StatusMessage ?? string.Empty,
				HouseAllotted = model.HouseAllotted
			};

			var newId = _service.Create(entity);
			if (newId == Guid.Empty)
			{
				ModelState.AddModelError(string.Empty, "Failed to create student.");
				PopulateDropdowns(model);
				return View(model);
			}

			// Save Parent information (non-blocking on error)
			try
			{
				_parentService.CreateForStudent(
					newId,
					model.SchoolId,
					companyId,
					userId,
					model.ParentFirstName,
					model.ParentLastName,
					model.ParentDOB,
					model.ParentRelationTypeId,
					model.ParentQualificationId,
					model.ParentOccupation,
					model.ParentAnnualIncome,
					model.ParentDesignationId,
					model.ParentPhone,
					model.ParentEmail,
					model.ParentAddress1,
					model.ParentAddress2,
					model.ParentCountryId,
					model.ParentStateId,
					model.ParentCityId,
					model.ParentZipCode,
					model.ParentIsActive
				);
			}
			catch (Exception ex)
			{
				_logger.LogWarning(ex, "Student created {StudentId} but failed to save Parent information.", newId);
			}
			return RedirectToAction("Details", new { id = newId });
		}

		[HttpGet]
		[Route("Edit/{id}")]
		public IActionResult Edit(Guid id)
		{
			var item = _service.GetById(id);
			if (item == null) return NotFound();

			var vm = new StudentViewModel
			{
				Id = item.Id,
				RollNumber = item.RollNumber,
				FirstName = item.FirstName,
				LastName = item.LastName,
				Address = item.Address,
				CityId = item.CityId,
				StateId = item.StateId,
				CountryId = item.CountryId,
				ZipCode = item.ZipCode,
				ContactNumber = item.ContactNumber,
				EmergencyContactNumber = item.EmergencyContactNumber,
				DOB = item.DOB,
				DOJ = item.DOJ,
				RegistrationNumber = item.RegistrationNumber,
				ClassId = item.ClassId,
				SectionId = item.SectionId,
				AvailTransport = item.AvailTransport,
				Image = item.Image,
				Email = item.Email,
				Phone = item.Phone, // was missing
				CategoryId = item.CategoryId,
				SiblingsIfAny = item.SiblingsIfAny,
				SiblingClassId = item.SiblingClassId,
				Gender = item.Gender,
				DisabilityAny = item.DisabilityAny,
				MedicalAlleryAny = item.MedicalAlleryAny,
				BirthCityId = item.BirthCityId,
				BirthStateId = item.BirthStateId,
				BirthCountryId = item.BirthCountryId,
				PreviousSchoolAttended = item.PreviousSchoolAttended,
				PreviousSchoolClassId = item.PreviousSchoolClassId,
				PreviousSchoolPercentage = item.PreviousSchoolPercentage,
				PreviousSchoolRank = item.PreviousSchoolRank,
				PreviousSchoolBoardId = item.PreviousSchoolBoardId,
				PreviousSchoolFromDate = item.PreviousSchoolFromDate,
				PreviousSchoolToDate = item.PreviousSchoolToDate,
				WithdrawnDate = item.WithdrawnDate,
				WithdrawnReason = item.WithdrawnReason,
				BloodGroupId = item.BloodGroupId,
				Nationality = item.Nationality,
				Hobbies = item.Hobbies,
				ReligionId = item.ReligionId,
				RouteId = item.RouteId,
				RouteStopDetailsId = item.RouteStopDetailsId,
				ClassTeacherId = item.ClassTeacherId,
				RoutePickAndDrop = item.RoutePickAndDrop,
				FeesDiscountCategoryMasterId = item.FeesDiscountCategoryMasterId,
				TutionFees = item.TutionFees,
				AnnualFees = item.AnnualFees,
				TransportFees = item.TransportFees,
				UseTransportFees = item.UseTransportFees,
				SessionId = item.SessionId,
				CompanyId = item.CompanyId,
				SchoolId = item.SchoolId,
				IsActive = item.IsActive,
				IsDeleted = item.IsDeleted,
				Status = item.Status,
				StatusMessage = item.StatusMessage,
				HouseAllotted = item.HouseAllotted
			};

			// Load parent data (Parents tab)
			try
			{
				var parent = _parentService.GetByStudentId(id);
				if (parent != null)
				{
					vm.ParentFirstName = parent.ParentFirstName;
					vm.ParentLastName = parent.ParentLastName;
					vm.ParentDOB = parent.ParentDOB;
					vm.ParentRelationTypeId = parent.RelationTypeId != Guid.Empty ? parent.RelationTypeId : null;
					vm.ParentQualificationId = parent.QualificationId != Guid.Empty ? parent.QualificationId : null;
					vm.ParentDesignationId = parent.DesignationId != Guid.Empty ? parent.DesignationId : null;
					vm.ParentOccupation = parent.Occupation;
					vm.ParentAnnualIncome = parent.AnnualIncome;
					vm.ParentPhone = parent.Phone;
					vm.ParentEmail = parent.Email;
					vm.ParentAddress1 = parent.Address1;
					vm.ParentAddress2 = parent.Address2;
					vm.ParentCountryId = parent.CountryId != Guid.Empty ? parent.CountryId : null;
					vm.ParentStateId = parent.StateId != Guid.Empty ? parent.StateId : null;
					vm.ParentCityId = parent.CityId != Guid.Empty ? parent.CityId : null;
					vm.ParentZipCode = parent.ZipCode;
					vm.ParentIsActive = parent.IsActive;
				}
			}
			catch
			{
				// non-blocking
			}

			PopulateDropdowns(vm);
			return View(vm);
		}

		[HttpPost]
		[Route("Edit/{id}")]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Guid id, StudentViewModel model)
		{
			if (id != model.Id) return BadRequest();

			var schoolIdStr = HttpContext.Session.GetString("SchoolId");
			if (!string.IsNullOrWhiteSpace(schoolIdStr) && Guid.TryParse(schoolIdStr, out var schoolIdFromSession))
			{
				ModelState.Remove(nameof(StudentViewModel.SchoolId));
				model.SchoolId = schoolIdFromSession;
			}

			if (!ModelState.IsValid)
			{
				PopulateDropdowns(model);
				return View(model);
			}

			var userIdStr = HttpContext.Session.GetString("UserId");
			if (string.IsNullOrWhiteSpace(userIdStr) || !Guid.TryParse(userIdStr, out var userId) || model.SchoolId == Guid.Empty)
			{
				ModelState.AddModelError(string.Empty, "Please login to update student.");
				PopulateDropdowns(model);
				return View(model);
			}

			// Handle image upload if provided (replace existing path)
			if (model.ImageFile != null && model.ImageFile.Length > 0)
			{
				var uploadsRoot = Path.Combine(_env.WebRootPath ?? string.Empty, "uploads", "students");
				Directory.CreateDirectory(uploadsRoot);
				var fileName = $"{Guid.NewGuid()}{Path.GetExtension(model.ImageFile.FileName)}";
				var fullPath = Path.Combine(uploadsRoot, fileName);
				using (var stream = System.IO.File.Create(fullPath))
				{
					model.ImageFile.CopyTo(stream);
				}
				model.Image = $"/uploads/students/{fileName}";
			}

			var entity = new StudentMaster
			{
				Id = id,
				RollNumber = model.RollNumber,
				FirstName = model.FirstName,
				LastName = model.LastName ?? string.Empty,
				Address = model.Address ?? string.Empty,
				CityId = model.CityId,
				StateId = model.StateId,
				CountryId = model.CountryId,
				ZipCode = model.ZipCode ?? string.Empty,
				ContactNumber = model.ContactNumber ?? string.Empty,
				EmergencyContactNumber = model.EmergencyContactNumber ?? string.Empty,
				DOB = model.DOB,
				DOJ = model.DOJ,
				RegistrationNumber = model.RegistrationNumber ?? string.Empty,
				ClassId = model.ClassId,
				SectionId = model.SectionId,
				AvailTransport = model.AvailTransport,
				Image = model.Image ?? string.Empty,
				Email = model.Email ?? string.Empty,
				CategoryId = model.CategoryId,
				SiblingsIfAny = model.SiblingsIfAny,
				SiblingClassId = model.SiblingClassId,
				Gender = model.Gender,
				DisabilityAny = model.DisabilityAny ?? string.Empty,
				MedicalAlleryAny = model.MedicalAlleryAny ?? string.Empty,
				BirthCityId = model.BirthCityId,
				BirthStateId = model.BirthStateId,
				BirthCountryId = model.BirthCountryId,
				PreviousSchoolAttended = model.PreviousSchoolAttended ?? string.Empty,
				PreviousSchoolClassId = model.PreviousSchoolClassId,
				PreviousSchoolPercentage = model.PreviousSchoolPercentage,
				PreviousSchoolRank = model.PreviousSchoolRank ?? string.Empty,
				PreviousSchoolBoardId = model.PreviousSchoolBoardId,
				PreviousSchoolFromDate = model.PreviousSchoolFromDate,
				PreviousSchoolToDate = model.PreviousSchoolToDate,
				WithdrawnDate = model.WithdrawnDate,
				WithdrawnReason = model.WithdrawnReason ?? string.Empty,
				BloodGroupId = model.BloodGroupId,
				Nationality = model.Nationality,
				Hobbies = model.Hobbies ?? string.Empty,
				ReligionId = model.ReligionId,
				Phone = model.Phone ?? string.Empty,
				RouteId = model.RouteId,
				RouteStopDetailsId = model.RouteStopDetailsId,
				ClassTeacherId = model.ClassTeacherId,
				RoutePickAndDrop = model.RoutePickAndDrop,
				FeesDiscountCategoryMasterId = model.FeesDiscountCategoryMasterId,
				TutionFees = model.TutionFees,
				AnnualFees = model.AnnualFees,
				TransportFees = model.TransportFees,
				UseTransportFees = model.UseTransportFees,
				SessionId = model.SessionId,
				SchoolId = model.SchoolId,
				IsActive = model.IsActive,
				IsDeleted = model.IsDeleted,
				ModifiedBy = userId,
				ModifiedDate = DateTime.UtcNow,
				Status = model.Status ?? string.Empty,
				StatusMessage = model.StatusMessage ?? string.Empty,
				HouseAllotted = model.HouseAllotted
			};

			if (!_service.Update(entity))
			{
				ModelState.AddModelError(string.Empty, "Failed to update student.");
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
				TempData["ErrorMessage"] = "Failed to delete student.";
				return RedirectToAction("Delete", new { id });
			}
			return RedirectToAction("Index");
		}
	}
}
