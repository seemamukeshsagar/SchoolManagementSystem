using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SchoolPortal.Entities.Models;
using SchoolPortalApp.Models;
using SchoolPortal.Services.IServices;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace SchoolPortalApp.Controllers
{
	[Route("Emp")]
	public class EmpController : BaseController
	{
		private readonly IEmpService _service;
		private readonly ILookupService _lookup;
		private readonly IDeptMasterService _deptService;
		private readonly IDeptDesigDetailsService _deptDesigService;
		private readonly ICleanerMasterService _cleanerService;
		private readonly IDriverMasterService _driverService;
		private readonly ITeacherService _teacherService;
		private new readonly ILogger<EmpController> _logger;
		private readonly IWebHostEnvironment _env;
		private const string DefaultStatus = "Active";

		public EmpController(IEmpService service, ILookupService lookup, IDeptMasterService deptService, IDeptDesigDetailsService deptDesigService, ILogger<EmpController> logger, IWebHostEnvironment env, ICleanerMasterService cleanerService, IDriverMasterService driverService, ITeacherService teacherService)
		{
			_service = service;
			_lookup = lookup;
			_deptService = deptService;
			_deptDesigService = deptDesigService;
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
			_env = env;
			_cleanerService = cleanerService;
			_driverService = driverService;
			_teacherService = teacherService;
		}

		private void PopulateDropdowns(EmpViewModel vm)
		{
			var depts = vm.SchoolId != Guid.Empty
				? _deptService.GetBySchool(vm.SchoolId)
				: _deptService.GetAll();
			// Fallback to all departments if school-specific query returns nothing
			if (depts == null || !depts.Any())
			{
				depts = _deptService.GetAll();
			}
			depts = (depts ?? new List<DeptMaster>()).Where(d => d != null && d.IsActive).ToList();
			vm.Departments = depts
				.Select(d => new SelectListItem
				{
					Value = d.Id.ToString(),
					Text = d.DeptName ?? string.Empty,
					Selected = vm.DepartmentId.HasValue && vm.DepartmentId.Value == d.Id
				})
				.ToList();
			var designations = _lookup.GetDesignations() ?? new List<LookupItem>();
			vm.Designations = designations
				.Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name ?? string.Empty })
				.ToList();
			var genders = _lookup.GetGenders() ?? new List<LookupItem>();
			vm.Genders = genders
				.Select(g => new SelectListItem { Value = g.Id.ToString(), Text = g.Name ?? string.Empty, Selected = vm.GenderId.HasValue && vm.GenderId.Value == g.Id })
				.ToList();
			var paymentModes = vm.SchoolId != Guid.Empty
				? (_lookup.GetPaymentModes(vm.SchoolId) ?? new List<LookupItem>())
				: new List<LookupItem>();
			// Fallback to global list if school-specific list is empty
			if (paymentModes == null || paymentModes.Count == 0)
			{
				paymentModes = _lookup.GetPaymentModes() ?? new List<LookupItem>();
			}
			vm.PaymentModes = (paymentModes ?? new List<LookupItem>())
				.OrderBy(x => x.Name)
				.Select(x => new SelectListItem
				{
					Value = x.Id.ToString(),
					Text = x.Name ?? string.Empty,
					Selected = vm.PaymentModeId.HasValue && vm.PaymentModeId.Value == x.Id
				})
				.ToList();
			var empTypes = vm.SchoolId != Guid.Empty
				? (_lookup.GetEmployeeTypes(vm.SchoolId) ?? new List<LookupItem>())
				: (_lookup.GetEmployeeTypes() ?? new List<LookupItem>());
			if (empTypes.Count == 0)
			{
				empTypes = _lookup.GetEmployeeTypes() ?? new List<LookupItem>();
			}
			vm.EmployeeTypes = empTypes
				.OrderBy(x => x.Name)
				.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name ?? string.Empty, Selected = vm.EmployeeTypeId.HasValue && vm.EmployeeTypeId.Value == x.Id })
				.ToList();
			var empCats = _lookup.GetEmployeeCategories() ?? new List<LookupItem>();
			// Fallback: if no employee categories defined, use generic categories
			if (empCats == null || empCats.Count == 0)
			{
				empCats = _lookup.GetCategories() ?? new List<LookupItem>();
			}
			vm.EmployeeCategories = (empCats ?? new List<LookupItem>())
				.Select(x => new SelectListItem
				{
					Value = x.Id.ToString(),
					Text = x.Name ?? string.Empty,
					Selected = vm.CategoryId.HasValue && vm.CategoryId.Value == x.Id
				})
				.ToList();
			var grades = vm.SchoolId != Guid.Empty
				? (_lookup.GetGrades(vm.SchoolId) ?? new List<LookupItem>())
				: (_lookup.GetGrades() ?? new List<LookupItem>());
			if (grades.Count == 0)
			{
				grades = _lookup.GetGrades() ?? new List<LookupItem>();
			}
			vm.Grades = grades
				.OrderBy(x => x.Name)
				.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name ?? string.Empty, Selected = vm.GradeId.HasValue && vm.GradeId.Value == x.Id })
				.ToList();
			var bloodGroups = _lookup.GetBloodGroups() ?? new List<LookupItem>();
			vm.BloodGroups = bloodGroups
				.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name ?? string.Empty, Selected = vm.BloodGroupId.HasValue && vm.BloodGroupId.Value == x.Id })
				.ToList();

			var maritalStatuses = _lookup.GetMaritalStatuses() ?? new List<LookupItem>();
			vm.MaritalStatuses = maritalStatuses
				.OrderBy(x => x.Name)
				.Select(x => new SelectListItem { Value = x.Name ?? string.Empty, Text = x.Name ?? string.Empty, Selected = string.Equals(vm.MaritalStatus ?? string.Empty, x.Name ?? string.Empty, StringComparison.OrdinalIgnoreCase) })
				.ToList();

			// Current address dropdowns
			var countries = _lookup.GetCountries() ?? new List<LookupItem>();
			vm.CurrentCountries = countries
				.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name ?? string.Empty, Selected = vm.CurrentCountryId.HasValue && vm.CurrentCountryId.Value == c.Id })
				.ToList();
			if (vm.CurrentCountryId.HasValue && vm.CurrentCountryId.Value != Guid.Empty)
			{
				var states = _lookup.GetStates(vm.CurrentCountryId.Value) ?? new List<LookupItem>();
				vm.CurrentStates = states
					.Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Name ?? string.Empty, Selected = vm.CurrentStateId.HasValue && vm.CurrentStateId.Value == s.Id })
					.ToList();
			}
			else vm.CurrentStates = new();
			if (vm.CurrentStateId.HasValue && vm.CurrentStateId.Value != Guid.Empty)
			{
				var cities = _lookup.GetCities(vm.CurrentStateId.Value) ?? new List<LookupItem>();
				vm.CurrentCities = cities
					.Select(ci => new SelectListItem { Value = ci.Id.ToString(), Text = ci.Name ?? string.Empty, Selected = vm.CurrentCityId.HasValue && vm.CurrentCityId.Value == ci.Id })
					.ToList();
			}
			else vm.CurrentCities = new();

			// Permanent address dropdowns
			vm.PermanentCountries = countries
				.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name ?? string.Empty, Selected = vm.PermanentCountryId.HasValue && vm.PermanentCountryId.Value == c.Id })
				.ToList();
			if (vm.PermanentCountryId.HasValue && vm.PermanentCountryId.Value != Guid.Empty)
			{
				var pStates = _lookup.GetStates(vm.PermanentCountryId.Value) ?? new List<LookupItem>();
				vm.PermanentStates = pStates
					.Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Name ?? string.Empty, Selected = vm.PermanentStateId.HasValue && vm.PermanentStateId.Value == s.Id })
					.ToList();
			}
			else vm.PermanentStates = new();
			if (vm.PermanentStateId.HasValue && vm.PermanentStateId.Value != Guid.Empty)
			{
				var pCities = _lookup.GetCities(vm.PermanentStateId.Value) ?? new List<LookupItem>();
				vm.PermanentCities = pCities
					.Select(ci => new SelectListItem { Value = ci.Id.ToString(), Text = ci.Name ?? string.Empty, Selected = vm.PermanentCityId.HasValue && vm.PermanentCityId.Value == ci.Id })
					.ToList();
			}
			else vm.PermanentCities = new();

			vm.LicenceTypes = new()
			{
				new SelectListItem { Value = "", Text = "-- Select Licence Type --", Selected = string.IsNullOrWhiteSpace(vm.LicenceType) },
				new SelectListItem { Value = "Two Wheeler", Text = "Two Wheeler", Selected = vm.LicenceType == "Two Wheeler" },
				new SelectListItem { Value = "LMV", Text = "LMV", Selected = vm.LicenceType == "LMV" },
				new SelectListItem { Value = "HMV", Text = "HMV", Selected = vm.LicenceType == "HMV" },
				new SelectListItem { Value = "Transport", Text = "Transport", Selected = vm.LicenceType == "Transport" }
			};
		}

		[HttpGet]
		[Route("")]
		[Route("Index")]
		public IActionResult Index()
		{
			var list = _service.GetAll() ?? new System.Collections.Generic.List<EmpMaster>();
			var departments = _lookup.GetDepartments() ?? new System.Collections.Generic.List<LookupItem>();
			var designations = _lookup.GetDesignations() ?? new System.Collections.Generic.List<LookupItem>();
			var deptDict = departments
				.Where(d => d != null)
				.GroupBy(d => d.Id)
				.Select(g => g.First())
				.ToDictionary(d => d.Id, d => d.Name ?? string.Empty);
			var desigDict = designations
				.Where(d => d != null)
				.GroupBy(d => d.Id)
				.Select(g => g.First())
				.ToDictionary(d => d.Id, d => d.Name ?? string.Empty);

			var result = list.Where(item => item != null).Select(item =>
			{
				deptDict.TryGetValue(item.DepartmentId ?? Guid.Empty, out var deptName);
				desigDict.TryGetValue(item.DesignationId ?? Guid.Empty, out var desigName);
				return new EmpListItemViewModel
				{
					Id = item.Id,
					FirstName = item.FirstName ?? string.Empty,
					LastName = item.LastName ?? string.Empty,
					DepartmentName = deptName ?? string.Empty,
					DesignationName = desigName ?? string.Empty,
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

			var departments = _lookup.GetDepartments() ?? new System.Collections.Generic.List<LookupItem>();
			var designations = _lookup.GetDesignations() ?? new System.Collections.Generic.List<LookupItem>();
			var department = departments.FirstOrDefault(d => d.Id == item.DepartmentId);
			var designation = designations.FirstOrDefault(d => d.Id == item.DesignationId);

			var vm = new EmpDetailsViewModel
			{
				Id = item.Id,
				FirstName = item.FirstName ?? string.Empty,
				LastName = item.LastName ?? string.Empty,
				DepartmentName = department?.Name ?? string.Empty,
				DesignationName = designation?.Name ?? string.Empty,
				IsActive = item.IsActive,
				Status = item.Status ?? DefaultStatus,
				StatusMessage = item.StatusMessage ?? string.Empty
			};
			return View(vm);
		}

		[HttpGet("GetDesignationsByDepartment/{departmentId:guid}")]
		public IActionResult GetDesignationsByDepartment(Guid departmentId)
		{
			try
			{
				var companyId = CurrentCompanyId;
				var schoolId = CurrentSchoolId;

				var allMappings = _deptDesigService.GetAll() ?? new System.Collections.Generic.List<DeptDesigDetails>();
				// First, filter by department and active
				var deptMappings = allMappings.Where(m => m != null && m.IsActive && m.DepartmentId == departmentId).ToList();
				// If none at all for the department, return empty
				if (deptMappings.Count == 0)
				{
					return Json(Array.Empty<object>());
				}

				// Apply company/school scoping if present
				var scoped = deptMappings.AsEnumerable();
				if (companyId.HasValue) scoped = scoped.Where(m => m.CompanyId == companyId.Value);
				if (schoolId.HasValue) scoped = scoped.Where(m => m.SchoolId == schoolId.Value);

				var effective = scoped.Any() ? scoped : deptMappings;

				var desigIds = effective
					.Select(m => m.DesignationId)
					.Distinct()
					.ToHashSet();

				var allDesignations = _lookup.GetDesignations() ?? new System.Collections.Generic.List<LookupItem>();
				var result = allDesignations
					.Where(d => d != null && desigIds.Contains(d.Id))
					.OrderBy(d => d.Name)
					.Select(d => new { value = d.Id.ToString(), text = d.Name ?? string.Empty });
				return Json(result);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error getting designations for department {DepartmentId}", departmentId);
				return Json(Array.Empty<object>());
			}
		}

		[HttpGet]
		[Route("Create")]
		public IActionResult Create()
		{
			var vm = new EmpViewModel
			{
				IsActive = true,
				Status = DefaultStatus,
				StatusMessage = string.Empty
			};
			var companyId = CurrentCompanyId;
			var schoolId = CurrentSchoolId;
			if (companyId.HasValue)
			{
				vm.CompanyId = companyId.Value;
			}
			if (schoolId.HasValue)
			{
				vm.SchoolId = schoolId.Value;
			}
			PopulateDropdowns(vm);
			return View(vm);
		}

		[HttpPost]
		[Route("Create")]
		[ValidateAntiForgeryToken]
		public IActionResult Create(EmpViewModel model)
		{
			// Validate session scope
			var companyId = CurrentCompanyId;
			var schoolId = CurrentSchoolId;
			if (!companyId.HasValue || !schoolId.HasValue)
			{
				ModelState.AddModelError(string.Empty, "Please login and select company and school before creating the entry.");
				PopulateDropdowns(model);
				return View(model);
			}
			
			// Validate user
			var userId = CurrentUserId;
			if (!userId.HasValue)
			{
				ModelState.AddModelError(string.Empty, "Please login to create employee.");
				PopulateDropdowns(model);
				return View(model);
			}

			// Map view model to entity
			var entity = new EmpMaster
			{
				Id = Guid.Empty,
				FirstName = model.FirstName,
				LastName = model.LastName,
				DOB = model.DOB,
				DOJ = model.DOJ ?? DateTime.UtcNow.Date,

				ProbationStartDate = model.ProbationStartDate,
				ProbationPeriod = model.ProbationPeriod,
				ConfirmationDate = model.ConfirmationDate,
				PANNumber = model.PANNumber,
				ESICNumber = model.ESICNumber,
				PFNumeber = model.PFNumeber,
				CurrentAddress1 = model.CurrentAddress1,
				CurrentAddress2 = model.CurrentAddress2,
				CurrentCityId = model.CurrentCityId,
				CurrentStateId = model.CurrentStateId,
				CurrentCountryId = model.CurrentCountryId,
				CurrentZipCode = model.CurrentZipCode,
				PermanentAddress1 = model.PermanentAddress1,
				PermanentAddress2 = model.PermanentAddress2,
				PermanentCityId = model.PermanentCityId,
				PermanentStateId = model.PermanentStateId,
				PermanentCountryId = model.PermanentCountryId,
				PermanentZipCode = model.PermanentZipCode,
				PhoneNumber = model.PhoneNumber,
				MobileNumber = model.MobileNumber,
				EmailId = model.EmailId,
				DepartmentId = model.DepartmentId,
				DesignationId = model.DesignationId,
				PaymentModeId = model.PaymentModeId,
				EmployeeTypeId = model.EmployeeTypeId,
				CategoryId = model.CategoryId,
				BankAccountNumber = model.BankAccountNumber,
				BankName = model.BankName,
				GenderId = model.GenderId,
				BloodGroupId = model.BloodGroupId,
				GradeId = model.GradeId,
				Image = model.Image,
				EmployeeOldId = model.EmployeeOldId,
				FathersName = model.FathersName,
				MothersName = model.MothersName,
				Description = model.Description,
				LicenceNumber = model.LicenceNumber,
				LicenceIssueDate = model.LicenceIssueDate,
				LicenceValidUpto = model.LicenceValidUpto,
				LicenceDescription = model.LicenceDescription,
				LicenceImage = model.LicenceImage,
				LicenceType = model.LicenceType,
				Salutation = model.Salutation,
				DateOfLeaving = model.DateOfLeaving,
				MaritalStatus = model.MaritalStatus,
				YearsOfExperience = model.YearsOfExperience,
				PrevioudSchoolCompany = model.PrevioudSchoolCompany,
				AadhaarNumber = model.AadhaarNumber,
				MathUpToClass = model.MathUpToClass,
				EnglishUptoClass = model.EnglishUptoClass,
				SSTUptoClass = model.SSTUptoClass,
				CompanyId = companyId.Value,
				SchoolId = schoolId.Value,
				IsActive = model.IsActive,
				IsDeleted = false,
				CreatedBy = userId.Value,
				CreatedDate = DateTime.UtcNow,
				Status = model.Status ?? DefaultStatus,
				StatusMessage = model.StatusMessage ?? string.Empty
			};

			// Handle file uploads (ImageFile, LicenceImageFile)
			try
			{
				var uploadsRoot = Path.Combine(_env.WebRootPath ?? string.Empty, "uploads", "employees");
				if (!Directory.Exists(uploadsRoot)) Directory.CreateDirectory(uploadsRoot);

				if (model.ImageFile != null && model.ImageFile.Length > 0)
				{
					var ext = Path.GetExtension(model.ImageFile.FileName);
					var fileName = $"emp_{Guid.NewGuid():N}{ext}";
					var filePath = Path.Combine(uploadsRoot, fileName);
					using var stream = new FileStream(filePath, FileMode.Create);
					model.ImageFile.CopyTo(stream);
					entity.Image = $"/uploads/employees/{fileName}";
				}

				if (model.LicenceImageFile != null && model.LicenceImageFile.Length > 0)
				{
					var ext = Path.GetExtension(model.LicenceImageFile.FileName);
					var fileName = $"lic_{Guid.NewGuid():N}{ext}";
					var filePath = Path.Combine(uploadsRoot, fileName);
					using var stream = new FileStream(filePath, FileMode.Create);
					model.LicenceImageFile.CopyTo(stream);
					entity.LicenceImage = $"/uploads/employees/{fileName}";
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error saving uploaded files for employee create");
			}

			// Persist employee
			var newId = _service.Create(entity);
			if (newId == Guid.Empty)
			{
				ModelState.AddModelError(string.Empty, "Failed to create employee.");
				PopulateDropdowns(model);
				return View(model);
			}

			// Maintain related masters based on EmployeeType
			try
			{
				if (model.EmployeeTypeId.HasValue)
				{
					var empTypes = _lookup.GetEmployeeTypes() ?? new System.Collections.Generic.List<LookupItem>();
					var selectedType = empTypes.FirstOrDefault(x => x.Id == model.EmployeeTypeId.Value);

					if (selectedType != null && string.Equals(selectedType.Name?.Trim(), "Cleaner", StringComparison.OrdinalIgnoreCase))
					{
						var fullName = string.Join(" ", new[] { model.FirstName, model.LastName }.Where(s => !string.IsNullOrWhiteSpace(s))).Trim();
						var cleaner = new CleanerMaster
						{
							Id = Guid.Empty,
							Name = fullName,
							Image = entity.Image ?? string.Empty,
							FatherName = model.FathersName ?? string.Empty,
							Description = model.Description ?? string.Empty,
							IsActive = model.IsActive,
							IsDeleted = false,
							CompanyId = entity.CompanyId,
							SchoolId = entity.SchoolId,
							CreatedBy = entity.CreatedBy,
							CreatedDate = DateTime.UtcNow,
							Status = entity.Status ?? "Active",
							StatusMessage = entity.StatusMessage ?? string.Empty
						};
						_cleanerService.Create(cleaner);
					}
					else if (selectedType != null && string.Equals(selectedType.Name?.Trim(), "Driver", StringComparison.OrdinalIgnoreCase))
					{
						var driver = new DriverMaster
						{
							Id = Guid.Empty,
							FirstName = model.FirstName ?? string.Empty,
							LastName = model.LastName ?? string.Empty,
							DateOfBirth = model.DOB,
							FathersName = model.FathersName ?? string.Empty,
							MothersName = model.MothersName ?? string.Empty,
							QualificationId = Guid.Empty,
							Address1 = model.CurrentAddress1 ?? string.Empty,
							Address2 = model.CurrentAddress2 ?? string.Empty,
							CityId = model.CurrentCityId ?? Guid.Empty,
							StateId = model.CurrentStateId ?? Guid.Empty,
							CountryId = model.CurrentCountryId ?? Guid.Empty,
							ZipCode = model.CurrentZipCode ?? string.Empty,
							MobileNumber = model.MobileNumber ?? string.Empty,
							PhoneNumber = model.PhoneNumber ?? string.Empty,
							DriverImage = entity.Image ?? string.Empty,
							LicenceNumber = model.LicenceNumber ?? string.Empty,
							LicenceIssueDate = model.LicenceIssueDate,
							LicenceValidUptoDate = model.LicenceValidUpto,
							LicenceDescription = model.LicenceDescription ?? string.Empty,
							LicenceImage = entity.LicenceImage ?? string.Empty,
							LicenceType = model.LicenceType ?? string.Empty,
							CompanyId = entity.CompanyId,
							SchoolId = entity.SchoolId,
							IsActive = model.IsActive,
							IsDeleted = false,
							CreatedBy = entity.CreatedBy,
							CreatedDate = DateTime.UtcNow,
							Status = entity.Status ?? "Active",
							StatusMessage = entity.StatusMessage ?? string.Empty
						};
						_driverService.Create(driver);
					}
					else if (selectedType != null && string.Equals(selectedType.Name?.Trim(), "Teacher", StringComparison.OrdinalIgnoreCase))
					{
						var teacher = new TeacherMaster
						{
							Id = Guid.Empty,
							FirstName = model.FirstName ?? string.Empty,
							LastName = model.LastName ?? string.Empty,
							DOB = model.DOB,
							DOJ = model.DOJ ?? DateTime.UtcNow.Date,
							Address = ($"{model.CurrentAddress1 ?? string.Empty} {model.CurrentAddress2 ?? string.Empty}").Trim(),
							Email = model.EmailId ?? string.Empty,
							Phone = model.PhoneNumber ?? string.Empty,
							MobilePhone = model.MobileNumber ?? string.Empty,
							IsActive = model.IsActive,
							IsDeleted = false,
							CompanyId = entity.CompanyId,
							SchoolId = entity.SchoolId,
							CreatedBy = entity.CreatedBy,
							CreatedDate = DateTime.UtcNow,
							Status = entity.Status ?? "Active",
							StatusMessage = entity.StatusMessage ?? string.Empty
						};
						_teacherService.Create(teacher);
					}
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error maintaining related masters on employee create {EmployeeId}", newId);
			}

			return RedirectToAction("Details", new { id = newId });
		}

		[HttpGet]
		[Route("Edit/{id}")]
		public IActionResult Edit(Guid id)
		{
			var item = _service.GetById(id);
			if (item == null) return NotFound();
			var vm = new EmpViewModel
			{
				Id = item.Id,
				FirstName = item.FirstName ?? string.Empty,
				LastName = item.LastName ?? string.Empty,
				DOB = item.DOB,
				DOJ = item.DOJ,
				ProbationStartDate = item.ProbationStartDate,
				ProbationPeriod = item.ProbationPeriod,
				ConfirmationDate = item.ConfirmationDate,
				PANNumber = item.PANNumber ?? string.Empty,
				ESICNumber = item.ESICNumber ?? string.Empty,
				PFNumeber = item.PFNumeber ?? string.Empty,
				CurrentAddress1 = item.CurrentAddress1 ?? string.Empty,
				CurrentAddress2 = item.CurrentAddress2 ?? string.Empty,
				CurrentCityId = item.CurrentCityId,
				CurrentStateId = item.CurrentStateId,
				CurrentCountryId = item.CurrentCountryId,
				CurrentZipCode = item.CurrentZipCode ?? string.Empty,
				PermanentAddress1 = item.PermanentAddress1 ?? string.Empty,
				PermanentAddress2 = item.PermanentAddress2 ?? string.Empty,
				PermanentCityId = item.PermanentCityId,
				PermanentStateId = item.PermanentStateId,
				PermanentCountryId = item.PermanentCountryId,
				PermanentZipCode = item.PermanentZipCode ?? string.Empty,
				PhoneNumber = item.PhoneNumber ?? string.Empty,
				MobileNumber = item.MobileNumber ?? string.Empty,
				EmailId = item.EmailId ?? string.Empty,
				DepartmentId = item.DepartmentId,
				DesignationId = item.DesignationId,
				PaymentModeId = item.PaymentModeId,
				EmployeeTypeId = item.EmployeeTypeId,
				CategoryId = item.CategoryId,
				BankAccountNumber = item.BankAccountNumber ?? string.Empty,
				BankName = item.BankName ?? string.Empty,
				GenderId = item.GenderId,
				BloodGroupId = item.BloodGroupId,
				GradeId = item.GradeId,
				Image = item.Image ?? string.Empty,
				EmployeeOldId = item.EmployeeOldId,
				FathersName = item.FathersName ?? string.Empty,
				MothersName = item.MothersName ?? string.Empty,
				Description = item.Description ?? string.Empty,
				LicenceNumber = item.LicenceNumber ?? string.Empty,
				LicenceIssueDate = item.LicenceIssueDate,
				LicenceValidUpto = item.LicenceValidUpto,
				LicenceDescription = item.LicenceDescription ?? string.Empty,
				LicenceImage = item.LicenceImage ?? string.Empty,
				LicenceType = item.LicenceType ?? string.Empty,
				Salutation = item.Salutation ?? string.Empty,
				DateOfLeaving = item.DateOfLeaving,
				MaritalStatus = item.MaritalStatus ?? string.Empty,
				YearsOfExperience = item.YearsOfExperience ?? string.Empty,
				PrevioudSchoolCompany = item.PrevioudSchoolCompany ?? string.Empty,
				AadhaarNumber = item.AadhaarNumber ?? string.Empty,
				MathUpToClass = item.MathUpToClass,
				EnglishUptoClass = item.EnglishUptoClass,
				SSTUptoClass = item.SSTUptoClass,
				CompanyId = item.CompanyId,
				SchoolId = item.SchoolId,
				IsActive = item.IsActive,
				IsDeleted = item.IsDeleted,
				Status = item.Status ?? DefaultStatus,
				StatusMessage = item.StatusMessage ?? string.Empty
			};
			PopulateDropdowns(vm);
			return View(vm);
		}

		[HttpPost]
		[Route("Edit/{id}")]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Guid id, EmpViewModel model)
		{
			if (id != model.Id) return BadRequest();
			var companyId = CurrentCompanyId;
			var schoolId = CurrentSchoolId;
			if (!companyId.HasValue || !schoolId.HasValue)
			{
				ModelState.AddModelError(string.Empty, "Please login and select company and school before updating the entry.");
				PopulateDropdowns(model);
				return View(model);
			}
			var userId = CurrentUserId;
			if (!userId.HasValue)
			{
				ModelState.AddModelError(string.Empty, "Please login to update employee.");
				PopulateDropdowns(model);
				return View(model);
			}
			var existing = _service.GetById(id);
			if (existing == null) return NotFound();
			existing.FirstName = model.FirstName;
			existing.LastName = model.LastName;
			existing.DOB = model.DOB;
			existing.DOJ = model.DOJ ?? existing.DOJ;
			existing.ProbationStartDate = model.ProbationStartDate;
			existing.ProbationPeriod = model.ProbationPeriod;
			existing.ConfirmationDate = model.ConfirmationDate;
			existing.PANNumber = model.PANNumber;
			existing.ESICNumber = model.ESICNumber;
			existing.PFNumeber = model.PFNumeber;
			existing.CurrentAddress1 = model.CurrentAddress1;
			existing.CurrentAddress2 = model.CurrentAddress2;
			existing.CurrentCityId = model.CurrentCityId;
			existing.CurrentStateId = model.CurrentStateId;
			existing.CurrentCountryId = model.CurrentCountryId;
			existing.CurrentZipCode = model.CurrentZipCode;
			existing.PermanentAddress1 = model.PermanentAddress1;
			existing.PermanentAddress2 = model.PermanentAddress2;
			existing.PermanentCityId = model.PermanentCityId;
			existing.PermanentStateId = model.PermanentStateId;
			existing.PermanentCountryId = model.PermanentCountryId;
			existing.PermanentZipCode = model.PermanentZipCode;
			existing.PhoneNumber = model.PhoneNumber;
			existing.MobileNumber = model.MobileNumber;
			existing.EmailId = model.EmailId;
			existing.DepartmentId = model.DepartmentId;
			existing.DesignationId = model.DesignationId;
			existing.PaymentModeId = model.PaymentModeId;
			existing.EmployeeTypeId = model.EmployeeTypeId;
			existing.CategoryId = model.CategoryId;
			existing.BankAccountNumber = model.BankAccountNumber;
			existing.BankName = model.BankName;
			existing.GenderId = model.GenderId;
			existing.BloodGroupId = model.BloodGroupId;
			existing.GradeId = model.GradeId;
			existing.EmployeeOldId = model.EmployeeOldId;
			existing.FathersName = model.FathersName;
			existing.MothersName = model.MothersName;
			existing.Description = model.Description;
			existing.LicenceNumber = model.LicenceNumber;
			existing.LicenceIssueDate = model.LicenceIssueDate;
			existing.LicenceValidUpto = model.LicenceValidUpto;
			existing.LicenceDescription = model.LicenceDescription;
			existing.LicenceType = model.LicenceType;
			existing.Salutation = model.Salutation;
			existing.DateOfLeaving = model.DateOfLeaving;
			existing.MaritalStatus = model.MaritalStatus;
			existing.YearsOfExperience = model.YearsOfExperience;
			existing.PrevioudSchoolCompany = model.PrevioudSchoolCompany;
			existing.AadhaarNumber = model.AadhaarNumber;
			existing.MathUpToClass = model.MathUpToClass;
			existing.EnglishUptoClass = model.EnglishUptoClass;
			existing.SSTUptoClass = model.SSTUptoClass;
			existing.CompanyId = companyId.Value;
			existing.SchoolId = schoolId.Value;
			existing.IsActive = model.IsActive;
			existing.Status = model.Status ?? existing.Status ?? DefaultStatus;
			existing.StatusMessage = model.StatusMessage ?? existing.StatusMessage ?? string.Empty;
			existing.ModifiedBy = userId.Value;
			existing.ModifiedDate = DateTime.UtcNow;
			try
			{
				var uploadsRoot = Path.Combine(_env.WebRootPath ?? string.Empty, "uploads", "employees");
				if (!Directory.Exists(uploadsRoot)) Directory.CreateDirectory(uploadsRoot);
				if (model.ImageFile != null && model.ImageFile.Length > 0)
				{
					var ext = Path.GetExtension(model.ImageFile.FileName);
					var fileName = $"emp_{Guid.NewGuid():N}{ext}";
					var filePath = Path.Combine(uploadsRoot, fileName);
					using var stream = new FileStream(filePath, FileMode.Create);
					model.ImageFile.CopyTo(stream);
					existing.Image = $"/uploads/employees/{fileName}";
				}
				if (model.LicenceImageFile != null && model.LicenceImageFile.Length > 0)
				{
					var ext = Path.GetExtension(model.LicenceImageFile.FileName);
					var fileName = $"lic_{Guid.NewGuid():N}{ext}";
					var filePath = Path.Combine(uploadsRoot, fileName);
					using var stream = new FileStream(filePath, FileMode.Create);
					model.LicenceImageFile.CopyTo(stream);
					existing.LicenceImage = $"/uploads/employees/{fileName}";
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error saving uploaded files for employee edit {EmployeeId}", id);
			}
			if (!_service.Update(existing))
			{
				ModelState.AddModelError(string.Empty, "Failed to update employee.");
				PopulateDropdowns(model);
				return View(model);
			}
			return RedirectToAction("Details", new { id });
		}

		[HttpPost]
		[Route("Delete/{id}")]
		[ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult ConfirmDelete(Guid id)
		{
			if (!_service.Delete(id))
			{
				TempData["ErrorMessage"] = "Failed to delete employee.";
				return RedirectToAction("Delete", new { id });
			}
			return RedirectToAction("Index");
		}
	}
}
