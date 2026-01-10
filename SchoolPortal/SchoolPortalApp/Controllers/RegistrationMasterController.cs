using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SchoolPortal.Services.IServices;
using SchoolPortal.Entities.Models;
using SchoolPortal.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolPortalApp.Controllers
{
    [Route("RegistrationMaster")]
    public class RegistrationMasterController : BaseController
    {
        private readonly IRegistrationMasterService _registrationMasterService;
        private readonly IClassService _classService;
        private readonly ISectionService _sectionService;
        private new readonly ILogger<RegistrationMasterController> _logger;

        public RegistrationMasterController(
            IRegistrationMasterService registrationMasterService,
            IClassService classService,
            ISectionService sectionService,
            ILogger<RegistrationMasterController> logger) : base(logger)
        {
            _registrationMasterService = registrationMasterService ?? throw new ArgumentNullException(nameof(registrationMasterService));
            _classService = classService ?? throw new ArgumentNullException(nameof(classService));
            _sectionService = sectionService ?? throw new ArgumentNullException(nameof(sectionService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            try
            {
                var registrations = _registrationMasterService.GetAll() ?? new List<RegistrationMaster>();
                
                var registrationViewModels = registrations.Select(registration => new RegistrationMasterViewModel
                {
                    Id = registration.Id,
                    RegistrationNumber = registration.RegistrationNumber,
                    FirstName = registration.FirstName,
                    LastName = registration.LastName,
                    StudentName = $"{registration.FirstName} {registration.LastName}".Trim(),
                    DOB = registration.DOB,
                    DateOfBirth = registration.DOB,
                    Age = registration.Age,
                    ClassId = registration.ClassId,
                    ClassName = "", // Would need to join with Class table
                    SectionId = Guid.Empty, // Not available in base entity
                    SectionName = "", // Not available in base entity
                    Date = registration.Date,
                    RegistrationDate = registration.Date,
                    SessionId = registration.SessionId,
                    Address1 = registration.Address1,
                    Address2 = registration.Address2,
                    Address = registration.Address1, // Using Address1 as Address
                    CityId = registration.CityId,
                    StateId = registration.StateId,
                    CountryId = registration.CountryId,
                    ZipCode = registration.ZipCode,
                    ContactNumber = registration.ContactNumber,
                    Email = registration.Email,
                    ParentName = "", // Not available in base entity
                    ParentMobile = "", // Not available in base entity
                    PreviousSchool = "", // Not available in base entity
                    IsActive = registration.IsActive,
                    IsDeleted = registration.IsDeleted,
                    CompanyId = registration.CompanyId,
                    SchoolId = registration.SchoolId,
                    CreatedBy = registration.CreatedBy,
                    CreatedDate = registration.CreatedDate,
                    ModifiedBy = registration.ModifiedBy,
                    ModifiedDate = registration.ModifiedDate,
                    Status = registration.Status,
                    StatusMessage = registration.StatusMessage
                }).ToList();

                return View(registrationViewModels);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving registrations");
                TempData["ErrorMessage"] = "An error occurred while retrieving registrations.";
                return View(new List<RegistrationMasterViewModel>());
            }
        }

        [HttpGet]
        [Route("Details/{id}")]
        public async Task<IActionResult> Details(Guid id)
        {
            try
            {
                var registration = await _registrationMasterService.GetByIdAsync(id);
                if (registration == null)
                {
                    return NotFound();
                }

                // Convert to ViewModel with navigation properties
                var classes = _classService.GetAll() ?? new List<ClassMaster>();
                var registrationViewModel = new RegistrationMasterViewModel
                {
                    Id = registration.Id,
                    RegistrationNumber = registration.RegistrationNumber,
                    FirstName = registration.FirstName,
                    LastName = registration.LastName,
                    StudentName = $"{registration.FirstName} {registration.LastName}".Trim(),
                    Gender = "", // Not available in base entity
                    DOB = registration.DOB,
                    DateOfBirth = registration.DOB,
                    Age = registration.Age,
                    ClassId = registration.ClassId,
                    ClassName = classes.FirstOrDefault(c => c.Id == registration.ClassId)?.Name ?? string.Empty,
                    SectionId = Guid.Empty, // Not available in base entity
                    SectionName = "", // Not available in base entity
                    Date = registration.Date,
                    RegistrationDate = registration.Date,
                    SessionId = registration.SessionId,
                    Address1 = registration.Address1,
                    Address2 = registration.Address2,
                    Address = registration.Address1, // Using Address1 as Address
                    CityId = registration.CityId,
                    StateId = registration.StateId,
                    CountryId = registration.CountryId,
                    ZipCode = registration.ZipCode,
                    ContactNumber = registration.ContactNumber,
                    Email = registration.Email,
                    ParentName = "", // Not available in base entity
                    ParentMobile = "", // Not available in base entity
                    PreviousSchool = "", // Not available in base entity
                    IsActive = registration.IsActive,
                    IsDeleted = registration.IsDeleted,
                    CompanyId = registration.CompanyId,
                    SchoolId = registration.SchoolId,
                    CreatedBy = registration.CreatedBy,
                    CreatedDate = registration.CreatedDate,
                    ModifiedBy = registration.ModifiedBy,
                    ModifiedDate = registration.ModifiedDate,
                    Status = registration.Status,
                    StatusMessage = registration.StatusMessage
                };

                return View(registrationViewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving registration details");
                TempData["ErrorMessage"] = "An error occurred while retrieving registration details.";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        [Route("Create")]
        public IActionResult Create()
        {
            PopulateDropdowns();
            return View();
        }

        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RegistrationMasterViewModel registrationViewModel)
        {
            if (!ModelState.IsValid)
            {
                PopulateDropdowns();
                return View(registrationViewModel);
            }

            try
            {
                // Convert ViewModel to entity for database operations
                var registration = new RegistrationMaster
                {
                    Id = Guid.NewGuid(),
                    RegistrationNumber = GenerateRegistrationNumber(),
                    FirstName = registrationViewModel.FirstName,
                    LastName = registrationViewModel.LastName,
                    DOB = registrationViewModel.DOB,
                    Age = registrationViewModel.Age,
                    ClassId = registrationViewModel.ClassId,
                    Date = registrationViewModel.Date,
                    SessionId = registrationViewModel.SessionId,
                    Address1 = registrationViewModel.Address1,
                    Address2 = registrationViewModel.Address2,
                    CityId = registrationViewModel.CityId,
                    StateId = registrationViewModel.StateId,
                    CountryId = registrationViewModel.CountryId,
                    ZipCode = registrationViewModel.ZipCode,
                    ContactNumber = registrationViewModel.ContactNumber,
                    Email = registrationViewModel.Email,
                    IsActive = true,
                    IsDeleted = false,
                    CompanyId = registrationViewModel.CompanyId,
                    SchoolId = registrationViewModel.SchoolId,
                    CreatedBy = registrationViewModel.CreatedBy,
                    CreatedDate = DateTime.UtcNow,
                    Status = "ACT",
                    StatusMessage = "Active"
                };
                
                await _registrationMasterService.CreateAsync(registration);
                TempData["SuccessMessage"] = "Registration created successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating registration");
                ModelState.AddModelError(string.Empty, "Failed to create registration.");
                PopulateDropdowns();
                return View(registrationViewModel);
            }
        }

        [HttpGet]
        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            try
            {
                var registration = await _registrationMasterService.GetByIdAsync(id);
                if (registration == null)
                {
                    return NotFound();
                }

                // Convert to ViewModel for the view
                var registrationViewModel = new RegistrationMasterViewModel
                {
                    Id = registration.Id,
                    RegistrationNumber = registration.RegistrationNumber,
                    FirstName = registration.FirstName,
                    LastName = registration.LastName,
                    DOB = registration.DOB,
                    Age = registration.Age,
                    ClassId = registration.ClassId,
                    Date = registration.Date,
                    SessionId = registration.SessionId,
                    Address1 = registration.Address1,
                    Address2 = registration.Address2,
                    CityId = registration.CityId,
                    StateId = registration.StateId,
                    CountryId = registration.CountryId,
                    ZipCode = registration.ZipCode,
                    ContactNumber = registration.ContactNumber,
                    Email = registration.Email,
                    CompanyId = registration.CompanyId,
                    SchoolId = registration.SchoolId,
                    CreatedBy = registration.CreatedBy,
                    ModifiedBy = registration.ModifiedBy,
                    Status = registration.Status,
                    StatusMessage = registration.StatusMessage
                };

                PopulateDropdowns();
                return View(registrationViewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving registration for edit");
                TempData["ErrorMessage"] = "An error occurred while retrieving registration.";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [Route("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, RegistrationMaster registration)
        {
            if (id != registration.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                PopulateDropdowns();
                return View(registration);
            }

            try
            {
                registration.ModifiedDate = DateTime.UtcNow;
                await _registrationMasterService.UpdateAsync(registration);
                TempData["SuccessMessage"] = "Registration updated successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating registration");
                ModelState.AddModelError(string.Empty, "Failed to update registration.");
                PopulateDropdowns();
                return View(registration);
            }
        }

        [HttpPost]
        [Route("Delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var registration = await _registrationMasterService.GetByIdAsync(id);
                if (registration == null)
                {
                    return NotFound();
                }

                await _registrationMasterService.DeleteAsync(id);
                TempData["SuccessMessage"] = "Registration deleted successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting registration");
                TempData["ErrorMessage"] = "An error occurred while deleting registration.";
                return RedirectToAction(nameof(Index));
            }
        }

        private void PopulateDropdowns()
        {
            try
            {
                ViewBag.Classes = _classService.GetAll()?.Select(c => new { Value = c.Id, Text = c.Name }) ?? Enumerable.Empty<object>();
                ViewBag.Sections = _sectionService.GetAll()?.Select(s => new { Value = s.Id, Text = s.Name }) ?? Enumerable.Empty<object>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error populating dropdowns");
                ViewBag.Classes = new List<object>();
                ViewBag.Sections = new List<object>();
            }
        }

        private string GenerateRegistrationNumber()
        {
            return $"REG{DateTime.Now:yyyyMMddHHmmss}";
        }
    }
}
