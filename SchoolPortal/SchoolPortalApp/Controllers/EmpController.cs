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
	[Route("Emp")]
	public class EmpController : Controller
	{
		private readonly IEmpService _service;
		private readonly ILookupService _lookup;
		private readonly ILogger<EmpController> _logger;
		private const string DefaultStatus = "Active";

		public EmpController(IEmpService service, ILookupService lookup, ILogger<EmpController> logger)
		{
			_service = service;
			_lookup = lookup;
			_logger = logger;
		}

		private void PopulateDropdowns(EmpViewModel vm)
		{
			vm.Departments = _lookup.GetDepartments()
				.Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name })
				.ToList();
			vm.Designations = _lookup.GetDesignations()
				.Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name })
				.ToList();

			vm.Genders = _lookup.GetGenders()
				.Select(g => new SelectListItem { Value = g.Id.ToString(), Text = g.Name, Selected = vm.GenderId.HasValue && vm.GenderId.Value == g.Id })
				.ToList();

			vm.PaymentModes = _lookup.GetPaymentModes()
				.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name, Selected = vm.PaymentModeId.HasValue && vm.PaymentModeId.Value == x.Id })
				.ToList();
			vm.EmployeeTypes = _lookup.GetEmployeeTypes()
				.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name, Selected = vm.EmployeeTypeId.HasValue && vm.EmployeeTypeId.Value == x.Id })
				.ToList();
			vm.EmployeeCategories = _lookup.GetEmployeeCategories()
				.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name, Selected = vm.CategoryId.HasValue && vm.CategoryId.Value == x.Id })
				.ToList();
			vm.Grades = _lookup.GetGrades()
				.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name, Selected = vm.GradeId.HasValue && vm.GradeId.Value == x.Id })
				.ToList();
			vm.BloodGroups = _lookup.GetBloodGroups()
				.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name, Selected = vm.BloodGroupId.HasValue && vm.BloodGroupId.Value == x.Id })
				.ToList();

			// Current address dropdowns
			vm.CurrentCountries = _lookup.GetCountries()
				.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name, Selected = vm.CurrentCountryId.HasValue && vm.CurrentCountryId.Value == c.Id })
				.ToList();
			if (vm.CurrentCountryId.HasValue && vm.CurrentCountryId.Value != Guid.Empty)
			{
				vm.CurrentStates = _lookup.GetStates(vm.CurrentCountryId.Value)
					.Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Name, Selected = vm.CurrentStateId.HasValue && vm.CurrentStateId.Value == s.Id })
					.ToList();
			}
			else vm.CurrentStates = new();
			if (vm.CurrentStateId.HasValue && vm.CurrentStateId.Value != Guid.Empty)
			{
				vm.CurrentCities = _lookup.GetCities(vm.CurrentStateId.Value)
					.Select(ci => new SelectListItem { Value = ci.Id.ToString(), Text = ci.Name, Selected = vm.CurrentCityId.HasValue && vm.CurrentCityId.Value == ci.Id })
					.ToList();
			}
			else vm.CurrentCities = new();

			// Permanent address dropdowns
			vm.PermanentCountries = _lookup.GetCountries()
				.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name, Selected = vm.PermanentCountryId.HasValue && vm.PermanentCountryId.Value == c.Id })
				.ToList();
			if (vm.PermanentCountryId.HasValue && vm.PermanentCountryId.Value != Guid.Empty)
			{
				vm.PermanentStates = _lookup.GetStates(vm.PermanentCountryId.Value)
					.Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Name, Selected = vm.PermanentStateId.HasValue && vm.PermanentStateId.Value == s.Id })
					.ToList();
			}
			else vm.PermanentStates = new();
			if (vm.PermanentStateId.HasValue && vm.PermanentStateId.Value != Guid.Empty)
			{
				vm.PermanentCities = _lookup.GetCities(vm.PermanentStateId.Value)
					.Select(ci => new SelectListItem { Value = ci.Id.ToString(), Text = ci.Name, Selected = vm.PermanentCityId.HasValue && vm.PermanentCityId.Value == ci.Id })
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
			var list = _service.GetAll();
			var departments = _lookup.GetDepartments();
			var designations = _lookup.GetDesignations();
			var result = list.Select(item =>
			{
				var department = departments.FirstOrDefault(d => d.Id == item.DepartmentId);
				var designation = designations.FirstOrDefault(d => d.Id == item.DesignationId);
				return new EmpListItemViewModel
				{
					Id = item.Id,
					FirstName = item.FirstName ?? string.Empty,
					LastName = item.LastName ?? string.Empty,
					DepartmentName = department?.Name ?? string.Empty,
					DesignationName = designation?.Name ?? string.Empty,
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

			var departments = _lookup.GetDepartments();
			var designations = _lookup.GetDesignations();
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
		public IActionResult Create(EmpViewModel model)
		{
			var companyIdStr = HttpContext.Session.GetString("CompanyId");
			var schoolIdStr = HttpContext.Session.GetString("SchoolId");
			if (string.IsNullOrWhiteSpace(companyIdStr) || !Guid.TryParse(companyIdStr, out var companyId) ||
				string.IsNullOrWhiteSpace(schoolIdStr) || !Guid.TryParse(schoolIdStr, out var schoolId))
			{
				ModelState.AddModelError(string.Empty, "Please login and select company and school before creating the entry.");
				PopulateDropdowns(model);
				return View(model);
			}

			ModelState.Remove(nameof(EmpViewModel.CompanyId));
			ModelState.Remove(nameof(EmpViewModel.SchoolId));

			if (!ModelState.IsValid)
			{
				PopulateDropdowns(model);
				return View(model);
			}

			var userIdStr = HttpContext.Session.GetString("UserId");
			if (string.IsNullOrWhiteSpace(userIdStr) || !Guid.TryParse(userIdStr, out var userId))
			{
				ModelState.AddModelError(string.Empty, "Please login to create employee.");
				PopulateDropdowns(model);
				return View(model);
			}

			var entity = new EmpMaster
			{
				Id = Guid.Empty,
				FirstName = model.FirstName,
				LastName = model.LastName,
				DOB = model.DOB,
				DOJ = model.DOJ,
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
				CompanyId = companyId,
				SchoolId = schoolId,
				IsActive = model.IsActive,
				CreatedBy = userId,
				CreatedDate = DateTime.UtcNow,
				Status = model.Status ?? DefaultStatus,
				StatusMessage = model.StatusMessage ?? string.Empty
			};

			var newId = _service.Create(entity);
			if (newId == Guid.Empty)
			{
				ModelState.AddModelError(string.Empty, "Failed to create employee.");
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

			var companyIdStr = HttpContext.Session.GetString("CompanyId");
			var schoolIdStr = HttpContext.Session.GetString("SchoolId");
			if (string.IsNullOrWhiteSpace(companyIdStr) || !Guid.TryParse(companyIdStr, out var companyId) ||
				string.IsNullOrWhiteSpace(schoolIdStr) || !Guid.TryParse(schoolIdStr, out var schoolId))
			{
				ModelState.AddModelError(string.Empty, "Please login and select company and school before updating the entry.");
				PopulateDropdowns(model);
				return View(model);
			}

			ModelState.Remove(nameof(EmpViewModel.CompanyId));
			ModelState.Remove(nameof(EmpViewModel.SchoolId));

			if (!ModelState.IsValid)
			{
				PopulateDropdowns(model);
				return View(model);
			}

			var userIdStr = HttpContext.Session.GetString("UserId");
			if (string.IsNullOrWhiteSpace(userIdStr) || !Guid.TryParse(userIdStr, out var userId))
			{
				ModelState.AddModelError(string.Empty, "Please login to update employee.");
				PopulateDropdowns(model);
				return View(model);
			}

			var existingItem = _service.GetById(id);
			if (existingItem == null) return NotFound();

			var entity = new EmpMaster
			{
				Id = id,
				FirstName = model.FirstName ?? string.Empty,
				LastName = model.LastName ?? string.Empty,
				DOB = model.DOB,
				DOJ = model.DOJ,
				ProbationStartDate = model.ProbationStartDate,
				ProbationPeriod = model.ProbationPeriod,
				ConfirmationDate = model.ConfirmationDate,
				PANNumber = model.PANNumber ?? string.Empty,
				ESICNumber = model.ESICNumber ?? string.Empty,
				PFNumeber = model.PFNumeber ?? string.Empty,
				CurrentAddress1 = model.CurrentAddress1 ?? string.Empty,
				CurrentAddress2 = model.CurrentAddress2 ?? string.Empty,
				CurrentCityId = model.CurrentCityId,
				CurrentStateId = model.CurrentStateId,
				CurrentCountryId = model.CurrentCountryId,
				CurrentZipCode = model.CurrentZipCode ?? string.Empty,
				PermanentAddress1 = model.PermanentAddress1 ?? string.Empty,
				PermanentAddress2 = model.PermanentAddress2 ?? string.Empty,
				PermanentCityId = model.PermanentCityId,
				PermanentStateId = model.PermanentStateId,
				PermanentCountryId = model.PermanentCountryId,
				PermanentZipCode = model.PermanentZipCode ?? string.Empty,
				PhoneNumber = model.PhoneNumber ?? string.Empty,
				MobileNumber = model.MobileNumber ?? string.Empty,
				EmailId = model.EmailId ?? string.Empty,
				DepartmentId = model.DepartmentId,
				DesignationId = model.DesignationId,
				PaymentModeId = model.PaymentModeId,
				EmployeeTypeId = model.EmployeeTypeId,
				CategoryId = model.CategoryId,
				BankAccountNumber = model.BankAccountNumber ?? string.Empty,
				BankName = model.BankName ?? string.Empty,
				GenderId = model.GenderId,
				BloodGroupId = model.BloodGroupId,
				GradeId = model.GradeId,
				Image = model.Image ?? string.Empty,
				EmployeeOldId = model.EmployeeOldId,
				FathersName = model.FathersName ?? string.Empty,
				MothersName = model.MothersName ?? string.Empty,
				Description = model.Description ?? string.Empty,
				LicenceNumber = model.LicenceNumber ?? string.Empty,
				LicenceIssueDate = model.LicenceIssueDate,
				LicenceValidUpto = model.LicenceValidUpto,
				LicenceDescription = model.LicenceDescription ?? string.Empty,
				LicenceImage = model.LicenceImage ?? string.Empty,
				LicenceType = model.LicenceType ?? string.Empty,
				Salutation = model.Salutation ?? string.Empty,
				DateOfLeaving = model.DateOfLeaving,
				MaritalStatus = model.MaritalStatus ?? string.Empty,
				YearsOfExperience = model.YearsOfExperience ?? string.Empty,
				PrevioudSchoolCompany = model.PrevioudSchoolCompany ?? string.Empty,
				AadhaarNumber = model.AadhaarNumber ?? string.Empty,
				MathUpToClass = model.MathUpToClass,
				EnglishUptoClass = model.EnglishUptoClass,
				SSTUptoClass = model.SSTUptoClass,
				CompanyId = companyId,
				SchoolId = schoolId,
				IsActive = model.IsActive,
				CreatedBy = existingItem.CreatedBy,
				CreatedDate = existingItem.CreatedDate,
				ModifiedBy = userId,
				ModifiedDate = DateTime.UtcNow,
				Status = model.Status ?? DefaultStatus,
				StatusMessage = model.StatusMessage ?? string.Empty
			};

			if (!_service.Update(entity))
			{
				ModelState.AddModelError(string.Empty, "Failed to update employee.");
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
				TempData["ErrorMessage"] = "Failed to delete employee.";
				return RedirectToAction("Delete", new { id });
			}
			return RedirectToAction("Index");
		}
	}
}
