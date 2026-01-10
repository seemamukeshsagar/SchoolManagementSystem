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
    [Route("ParentMaster")]
    public class ParentMasterController : BaseController
    {
        private readonly IParentMasterService _parentMasterService;
        private readonly IStudentMasterService _studentService;
        private new readonly ILogger<ParentMasterController> _logger;

        public ParentMasterController(
            IParentMasterService parentMasterService,
            IStudentMasterService studentService,
            ILogger<ParentMasterController> logger) : base(logger)
        {
            _parentMasterService = parentMasterService ?? throw new ArgumentNullException(nameof(parentMasterService));
            _studentService = studentService ?? throw new ArgumentNullException(nameof(studentService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            try
            {
                var parents = _parentMasterService.GetAll() ?? new List<ParentMaster>();
                
                var parentViewModels = parents.Select(parent => new ParentMasterViewModel
                {
                    Id = parent.Id,
                    StudentGUID = parent.StudentGUID,
                    ParentFirstName = parent.ParentFirstName,
                    ParentLastName = parent.ParentLastName,
                    FatherName = parent.ParentFirstName, // Using ParentFirstName as FatherName
                    MotherName = "", // Empty since not available in base entity
                    ParentDOB = parent.ParentDOB,
                    QualificationId = parent.QualificationId,
                    Occupation = parent.Occupation,
                    FatherOccupation = parent.Occupation, // Using Occupation as FatherOccupation
                    MotherOccupation = "", // Empty since not available in base entity
                    AnnualIncome = parent.AnnualIncome,
                    DesignationId = parent.DesignationId,
                    Phone = parent.Phone,
                    Mobile = parent.Mobile,
                    FatherMobile = parent.Mobile, // Using Mobile as FatherMobile
                    MotherMobile = "", // Empty since not available in base entity
                    Email = parent.Email,
                    FatherEmail = parent.Email, // Using Email as FatherEmail
                    MotherEmail = "", // Empty since not available in base entity
                    Address1 = parent.Address1,
                    Address2 = parent.Address2,
                    Address = parent.Address1, // Using Address1 as Address
                    CityId = parent.CityId,
                    StateId = parent.StateId,
                    CountryId = parent.CountryId,
                    ZipCode = parent.ZipCode,
                    OfficeAddress1 = parent.OfficeAddress1,
                    OfficeAddress2 = parent.OfficeAddress2,
                    OfficeCityId = parent.OfficeCityId,
                    OfficeStateId = parent.OfficeStateId,
                    OfficeCountryId = parent.OfficeCountryId,
                    OfficeZipCode = parent.OfficeZipCode,
                    IsActive = parent.IsActive,
                    IsDeleted = parent.IsDeleted,
                    CompanyId = parent.CompanyId,
                    SchoolId = parent.SchoolId,
                    CreatedBy = parent.CreatedBy,
                    CreatedDate = parent.CreatedDate,
                    ModifiedBy = parent.ModifiedBy,
                    ModifiedDate = parent.ModifiedDate,
                    Status = parent.Status,
                    StatusMessage = parent.StatusMessage
                }).ToList();

                return View(parentViewModels);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving parents");
                TempData["ErrorMessage"] = "An error occurred while retrieving parents.";
                return View(new List<ParentMasterViewModel>());
            }
        }

        [HttpGet]
        [Route("Details/{id}")]
        public async Task<IActionResult> Details(Guid id)
        {
            try
            {
                var parent = await _parentMasterService.GetByIdAsync(id);
                if (parent == null)
                {
                    return NotFound();
                }

                // Convert to ViewModel with navigation properties
                var parentViewModel = new ParentMasterViewModel
                {
                    Id = parent.Id,
                    StudentGUID = parent.StudentGUID,
                    ParentFirstName = parent.ParentFirstName,
                    ParentLastName = parent.ParentLastName,
                    FatherName = parent.ParentFirstName, // Using ParentFirstName as FatherName
                    MotherName = "", // Empty since not available in base entity
                    ParentDOB = parent.ParentDOB,
                    QualificationId = parent.QualificationId,
                    Occupation = parent.Occupation,
                    FatherOccupation = parent.Occupation, // Using Occupation as FatherOccupation
                    MotherOccupation = "", // Empty since not available in base entity
                    AnnualIncome = parent.AnnualIncome,
                    DesignationId = parent.DesignationId,
                    Phone = parent.Phone,
                    Mobile = parent.Mobile,
                    FatherMobile = parent.Mobile, // Using Mobile as FatherMobile
                    MotherMobile = "", // Empty since not available in base entity
                    Email = parent.Email,
                    FatherEmail = parent.Email, // Using Email as FatherEmail
                    MotherEmail = "", // Empty since not available in base entity
                    Address1 = parent.Address1,
                    Address2 = parent.Address2,
                    Address = parent.Address1, // Using Address1 as Address
                    CityId = parent.CityId,
                    StateId = parent.StateId,
                    CountryId = parent.CountryId,
                    ZipCode = parent.ZipCode,
                    OfficeAddress1 = parent.OfficeAddress1,
                    OfficeAddress2 = parent.OfficeAddress2,
                    OfficeCityId = parent.OfficeCityId,
                    OfficeStateId = parent.OfficeStateId,
                    OfficeCountryId = parent.OfficeCountryId,
                    OfficeZipCode = parent.OfficeZipCode,
                    IsActive = parent.IsActive,
                    IsDeleted = parent.IsDeleted,
                    CompanyId = parent.CompanyId,
                    SchoolId = parent.SchoolId,
                    CreatedBy = parent.CreatedBy,
                    CreatedDate = parent.CreatedDate,
                    ModifiedBy = parent.ModifiedBy,
                    ModifiedDate = parent.ModifiedDate,
                    Status = parent.Status,
                    StatusMessage = parent.StatusMessage
                };

                return View(parentViewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving parent details");
                TempData["ErrorMessage"] = "An error occurred while retrieving parent details.";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        [Route("Create")]
        public IActionResult Create()
        {
            PopulateStudentDropdown();
            return View();
        }

        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ParentMasterViewModel parentViewModel)
        {
            if (!ModelState.IsValid)
            {
                PopulateStudentDropdown();
                return View(parentViewModel);
            }

            try
            {
                // Convert ViewModel to entity for database operations
                var parent = new ParentMaster
                {
                    Id = Guid.NewGuid(),
                    StudentGUID = parentViewModel.StudentGUID,
                    ParentFirstName = parentViewModel.ParentFirstName,
                    ParentLastName = parentViewModel.ParentLastName,
                    ParentDOB = parentViewModel.ParentDOB,
                    QualificationId = parentViewModel.QualificationId,
                    Occupation = parentViewModel.Occupation,
                    AnnualIncome = parentViewModel.AnnualIncome,
                    DesignationId = parentViewModel.DesignationId,
                    Phone = parentViewModel.Phone,
                    Mobile = parentViewModel.Mobile,
                    Email = parentViewModel.Email,
                    Address1 = parentViewModel.Address1,
                    Address2 = parentViewModel.Address2,
                    CityId = parentViewModel.CityId,
                    StateId = parentViewModel.StateId,
                    CountryId = parentViewModel.CountryId,
                    ZipCode = parentViewModel.ZipCode,
                    OfficeAddress1 = parentViewModel.OfficeAddress1,
                    OfficeAddress2 = parentViewModel.OfficeAddress2,
                    OfficeCityId = parentViewModel.OfficeCityId,
                    OfficeStateId = parentViewModel.OfficeStateId,
                    OfficeCountryId = parentViewModel.OfficeCountryId,
                    OfficeZipCode = parentViewModel.OfficeZipCode,
                    IsActive = true,
                    IsDeleted = false,
                    CompanyId = parentViewModel.CompanyId,
                    SchoolId = parentViewModel.SchoolId,
                    CreatedBy = parentViewModel.CreatedBy,
                    CreatedDate = DateTime.UtcNow,
                    Status = "ACT",
                    StatusMessage = "Active"
                };
                
                await _parentMasterService.CreateAsync(parent);
                TempData["SuccessMessage"] = "Parent created successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating parent");
                ModelState.AddModelError(string.Empty, "Failed to create parent.");
                PopulateStudentDropdown();
                return View(parentViewModel);
            }
        }

        [HttpGet]
        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            try
            {
                var parent = await _parentMasterService.GetByIdAsync(id);
                if (parent == null)
                {
                    return NotFound();
                }

                // Convert to ViewModel for the view
                var parentViewModel = new ParentMasterViewModel
                {
                    Id = parent.Id,
                    StudentGUID = parent.StudentGUID,
                    ParentFirstName = parent.ParentFirstName,
                    ParentLastName = parent.ParentLastName,
                    ParentDOB = parent.ParentDOB,
                    QualificationId = parent.QualificationId,
                    Occupation = parent.Occupation,
                    AnnualIncome = parent.AnnualIncome,
                    DesignationId = parent.DesignationId,
                    Phone = parent.Phone,
                    Mobile = parent.Mobile,
                    Email = parent.Email,
                    Address1 = parent.Address1,
                    Address2 = parent.Address2,
                    CityId = parent.CityId,
                    StateId = parent.StateId,
                    CountryId = parent.CountryId,
                    ZipCode = parent.ZipCode,
                    OfficeAddress1 = parent.OfficeAddress1,
                    OfficeAddress2 = parent.OfficeAddress2,
                    OfficeCityId = parent.OfficeCityId,
                    OfficeStateId = parent.OfficeStateId,
                    OfficeCountryId = parent.OfficeCountryId,
                    OfficeZipCode = parent.OfficeZipCode,
                    CompanyId = parent.CompanyId,
                    SchoolId = parent.SchoolId,
                    CreatedBy = parent.CreatedBy,
                    ModifiedBy = parent.ModifiedBy,
                    Status = parent.Status,
                    StatusMessage = parent.StatusMessage
                };

                PopulateStudentDropdown();
                return View(parentViewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving parent for edit");
                TempData["ErrorMessage"] = "An error occurred while retrieving parent.";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [Route("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ParentMaster parent)
        {
            if (id != parent.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                PopulateStudentDropdown();
                return View(parent);
            }

            try
            {
                parent.ModifiedDate = DateTime.UtcNow;
                await _parentMasterService.UpdateAsync(parent);
                TempData["SuccessMessage"] = "Parent updated successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating parent");
                ModelState.AddModelError(string.Empty, "Failed to update parent.");
                PopulateStudentDropdown();
                return View(parent);
            }
        }

        [HttpPost]
        [Route("Delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var parent = await _parentMasterService.GetByIdAsync(id);
                if (parent == null)
                {
                    return NotFound();
                }

                await _parentMasterService.DeleteAsync(id);
                TempData["SuccessMessage"] = "Parent deleted successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting parent");
                TempData["ErrorMessage"] = "An error occurred while deleting parent.";
                return RedirectToAction(nameof(Index));
            }
        }

        private void PopulateStudentDropdown()
        {
            try
            {
                ViewBag.Students = _studentService.GetAll()?.Select(s => new { Value = s.Id, Text = $"{s.FirstName} {s.LastName} ({s.RegistrationNumber})" }) ?? Enumerable.Empty<object>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error populating student dropdown");
                ViewBag.Students = new List<object>();
            }
        }
    }
}
