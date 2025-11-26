using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SchoolPortal.Entities.Models;
using SchoolPortal.Services.IServices;
using SchoolPortal.Web.Models;
using SchoolPortal.Web.Models.NonTeaching;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolPortal.Web.Controllers
{
    [Authorize]
    public class NonTeachingController : Controller
    {
        private readonly ILogger<NonTeachingController> _logger;
        private readonly INonTeachingService _nonTeachingService;
        private readonly INonTeachingDocumentDetailsService _documentService;
        private readonly INonTeachingQualificationDetailsService _qualificationService;

        public NonTeachingController(
            ILogger<NonTeachingController> logger,
            INonTeachingService nonTeachingService,
            INonTeachingDocumentDetailsService documentService,
            INonTeachingQualificationDetailsService qualificationService)
        {
            _logger = logger;
            _nonTeachingService = nonTeachingService;
            _documentService = documentService;
            _qualificationService = qualificationService;
        }

        // GET: NonTeaching
        public IActionResult Index()
        {
            try
            {
                var nonTeachingList = _nonTeachingService.GetAll();
                return View(nonTeachingList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving non-teaching staff list");
                TempData["ErrorMessage"] = "An error occurred while retrieving the staff list.";
                return View(new List<NonTeachingMaster>());
            }
        }

        // GET: NonTeaching/Details/5
        public IActionResult Details(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    return BadRequest("Invalid ID");
                }

                var nonTeaching = _nonTeachingService.GetById(id);
                if (nonTeaching == null)
                {
                    return NotFound();
                }

                return View(nonTeaching);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving non-teaching staff with ID: {id}");
                TempData["ErrorMessage"] = "An error occurred while retrieving the staff details.";
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: NonTeaching/Create
        public IActionResult Create()
        {
            return View(new NonTeachingViewModel
            {
                Id = Guid.Empty,
                FirstName = string.Empty,
                MiddleName = string.Empty,
                LastName = string.Empty,
                Email = string.Empty,
                Phone = string.Empty,
                MobilePhone = string.Empty,
                Designation = string.Empty,
                Department = string.Empty,
                Qualification = string.Empty,
                IsActive = true,
                IsDeleted = false,
                EmployeeCode = string.Empty,
                DOB = DateTime.Today.AddYears(-25), // Default age 25
                DOJ = DateTime.Today,
                DateOfLeaving = null,
                Address = string.Empty,
                CityId = null,
                StateId = null,
                CountryId = null,
                ZipCode = string.Empty,
                Gender = string.Empty,
                MaritalStatusId = null,
                ImageFile = null,
                Image = Array.Empty<byte>(),
                Salary = null,
                BankAccountNumber = string.Empty,
                BankName = string.Empty,
                IFSCCode = string.Empty,
                PAN = string.Empty,
                AadharNumber = string.Empty,
                EmergencyContactName = string.Empty,
                EmergencyContactNumber = string.Empty,
                EmergencyContactRelation = string.Empty,
                CompanyId = Guid.Empty,
                SchoolId = Guid.Empty,
                CreatedBy = Guid.Empty,
                CreatedDate = DateTime.UtcNow,
                ModifiedBy = null,
                ModifiedDate = null,
                Documents = new List<NonTeachingDocumentDetails>(),
                Qualifications = new List<NonTeachingQualificationDetails>()
            });
        }

        // POST: NonTeaching/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NonTeachingViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (model.ImageFile != null && model.ImageFile.Length > 0)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await model.ImageFile.CopyToAsync(memoryStream).ConfigureAwait(false);
                            model.Image = (byte[])(memoryStream.ToArray());
                        }
                    }

                    var nonTeaching = MapToNonTeachingMaster(model);
                    if (nonTeaching == null)
                    {
                        ModelState.AddModelError("", "Invalid staff data.");
                        return View(model);
                    }
                    nonTeaching.Id = Guid.NewGuid();
                    nonTeaching.CreatedDate = DateTime.UtcNow;
                    nonTeaching.CreatedBy = model.CreatedBy; // Or get the current user ID

                    _nonTeachingService.Add(nonTeaching);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error creating non-teaching staff");
                    ModelState.AddModelError("", "An error occurred while saving the record.");
                }
            }

            return View(model);
        }

        // GET: NonTeaching/Edit/5
        public IActionResult Edit(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    return BadRequest("Invalid ID");
                }

                var nonTeaching = _nonTeachingService.GetById(id);
                if (nonTeaching == null)
                {
                    return NotFound();
                }

                var model = MapToNonTeachingViewModel(nonTeaching);
                return View("Form", model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving non-teaching staff for edit with ID: {id}");
                TempData["ErrorMessage"] = "An error occurred while retrieving the staff details for editing.";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: NonTeaching/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, NonTeachingViewModel model)
        {
            if (id != model.Id)
            {
                return BadRequest("ID mismatch");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingStaff = _nonTeachingService.GetById(id);
                    if (existingStaff == null)
                    {
                        return NotFound();
                    }

                    var nonTeaching = MapToNonTeachingMaster(model);
                    nonTeaching.ModifiedOn = DateTime.UtcNow;

                    if (model.ImageFile != null && model.ImageFile.Length > 0)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await model.ImageFile.CopyToAsync(memoryStream).ConfigureAwait(false);
                            nonTeaching.Image = memoryStream.ToArray();
                        }
                    }
                    else
                    {
                        // Keep the existing image if no new image is uploaded
                        nonTeaching.Image = existingStaff.Image;
                    }

                    _nonTeachingService.Update(nonTeaching);
                    TempData["SuccessMessage"] = "Non-teaching staff updated successfully!";
                    return RedirectToAction(nameof(Details), new { id = nonTeaching.Id });
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error updating non-teaching staff with ID: {id}");
                    ModelState.AddModelError("", "An error occurred while updating the staff member. Please try again.");
                }
            }

            return View("Form", model);
        }

        // GET: NonTeaching/Delete/5
        public IActionResult Delete(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    return BadRequest("Invalid ID");
                }

                var nonTeaching = _nonTeachingService.GetById(id);
                if (nonTeaching == null)
                {
                    return NotFound();
                }

                return View(nonTeaching);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving non-teaching staff for delete with ID: {id}");
                TempData["ErrorMessage"] = "An error occurred while retrieving the staff details for deletion.";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: NonTeaching/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            try
            {
                var nonTeaching = _nonTeachingService.GetById(id);
                if (nonTeaching == null)
                {
                    return NotFound();
                }

                _nonTeachingService.Delete(id);
                TempData["SuccessMessage"] = "Non-teaching staff deleted successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting non-teaching staff with ID: {id}");
                TempData["ErrorMessage"] = "An error occurred while deleting the staff member.";
                return RedirectToAction(nameof(Delete), new { id });
            }
        }

        // GET: NonTeaching/Documents/5
        public IActionResult Documents(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    return BadRequest("Invalid ID");
                }

                var nonTeaching = _nonTeachingService.GetById(id);
                if (nonTeaching == null)
                {
                    return NotFound();
                }

                var model = MapToNonTeachingViewModel(nonTeaching);
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving documents for non-teaching staff with ID: {id}");
                TempData["ErrorMessage"] = "An error occurred while retrieving the documents.";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: NonTeaching/AddDocument
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddDocument(
            Guid nonTeachingId, 
            IFormFile documentFile, 
            string documentType, 
            string documentNumber, 
            DateTime issueDate, 
            DateTime? expiryDate, 
            string description)
        {
            try
            {
                // Validate file
                if (documentFile == null || documentFile.Length == 0)
                {
                    ModelState.AddModelError("", "Please select a file to upload.");
                    return RedirectToAction(nameof(Documents), new { id = nonTeachingId });
                }

                // Check file size (5MB limit)
                if (documentFile.Length > 5 * 1024 * 1024)
                {
                    ModelState.AddModelError("", "The file size should not exceed 5MB.");
                    return RedirectToAction(nameof(Documents), new { id = nonTeachingId });
                }

                // Validate file extension
                var allowedExtensions = new[] { ".pdf", ".jpg", ".jpeg", ".png" };
                var fileExtension = Path.GetExtension(documentFile.FileName).ToLowerInvariant();
                
                if (string.IsNullOrEmpty(fileExtension) || !allowedExtensions.Contains(fileExtension))
                {
                    ModelState.AddModelError("", "Only PDF, JPG, JPEG, and PNG files are allowed.");
                    return RedirectToAction(nameof(Documents), new { id = nonTeachingId });
                }

                // Process the file
                using (var memoryStream = new MemoryStream())
                {
                    await documentFile.CopyToAsync(memoryStream).ConfigureAwait(false);
                    
                    var document = new NonTeachingDocumentDetails
                    {
                        Id = Guid.NewGuid(),
                        NonTeachingId = nonTeachingId,
                        DocumentType = documentType,
                        DocumentNumber = documentNumber,
                        IssueDate = issueDate,
                        ExpiryDate = expiryDate,
                        Description = description,
                        FileName = documentFile.FileName,
                        FileType = fileExtension,
                        FileContent = memoryStream.ToArray(),
                        CreatedDate = DateTime.UtcNow,
                        IsActive = true
                        // Add other required properties
                    };

                    // Save the document using your service
                    _documentService.Add(document);
                }

                TempData["SuccessMessage"] = "Document uploaded successfully!";
                return RedirectToAction(nameof(Documents), new { id = nonTeachingId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error uploading document for non-teaching staff ID: {nonTeachingId}");
                ModelState.AddModelError("", "An error occurred while uploading the document. Please try again.");
                return RedirectToAction(nameof(Documents), new { id = nonTeachingId });
            }
        }

        // POST: NonTeaching/DeleteDocument/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteDocument(Guid id, Guid nonTeachingId)
        {
            try
            {
                _documentService.Delete(id);
                TempData["SuccessMessage"] = "Document deleted successfully!";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting document with ID: {id}");
                TempData["ErrorMessage"] = "An error occurred while deleting the document.";
            }

            return RedirectToAction(nameof(Documents), new { id = nonTeachingId });
        }

        #region Helper Methods

        private NonTeachingMaster? MapToNonTeachingMaster(NonTeachingViewModel model)
        {
            if (model == null) return null;

            var nonTeachingMaster = new NonTeachingMaster
            {
                // Basic Information
                Id = model.Id,
                FirstName = model.FirstName,
                MiddleName = model.MiddleName,
                LastName = model.LastName,
                Email = model.Email,
                Phone = model.Phone,
                MobilePhone = model.MobilePhone,
                EmployeeCode = model.EmployeeCode,
                Designation = model.Designation,
                Department = model.Department,
        
                // Personal Details
                DOB = model.DOB,
                Gender = model.Gender,
                MaritalStatusId = model.MaritalStatusId,
        
                // Address Information
                Address = model.Address,
                CityId = model.CityId,
                StateId = model.StateId,
                CountryId = model.CountryId,
                ZipCode = model.ZipCode,
        
                // Employment Details
                DOJ = model.DOJ,
                DateOfLeaving = model.DateOfLeaving,
                Salary = model.Salary,
        
                // Financial Information
                BankAccountNumber = model.BankAccountNumber,
                BankName = model.BankName,
                IFSCCode = model.IFSCCode,
                PAN = model.PAN,
                AadharNumber = model.AadharNumber,
        
                // Emergency Contact
                EmergencyContactName = model.EmergencyContactName,
                EmergencyContactNumber = model.EmergencyContactNumber,
                EmergencyContactRelation = model.EmergencyContactRelation,
        
                // System Fields
                IsActive = model.IsActive,
                IsDeleted = model.IsDeleted,
                CompanyId = model.CompanyId,
                SchoolId = model.SchoolId,
                CreatedBy = model.CreatedBy,
                CreatedDate = model.CreatedDate,
                ModifiedBy = model.ModifiedBy,
                ModifiedDate = model.ModifiedDate,
                ModifiedOn = model.ModifiedDate,
        
                // Image
                Image = model.Image,
                Qualification = model.Qualification
            };

            // Map Documents if they exist
            if (model.Documents != null)
            {
                nonTeachingMaster.Documents = model.Documents.Select(d => new NonTeachingDocumentDetails
                {
                    Id = d.Id,
                    NonTeachingId = model.Id,
                    DocumentType = d.DocumentType,
                    DocumentTypeId = d.DocumentTypeId,
                    DocumentNumber = d.DocumentNumber,
                    DocumentPath = d.DocumentPath,
                    IssueDate = d.IssueDate,
                    ExpiryDate = d.ExpiryDate,
                    Remarks = d.Remarks,
                    IsVerified = d.IsVerified,
                    VerifiedBy = d.VerifiedBy,
                    VerifiedOn = d.VerifiedOn,
                    FileContent = d.FileContent,
                    FileType = d.FileType,
                    FileName = d.FileName,
                    Description = d.Description,
                    IsActive = d.IsActive,
                    CreatedBy = d.CreatedBy,
                    CreatedDate = d.CreatedDate,
                    ModifiedBy = d.ModifiedBy,
                    ModifiedDate = d.ModifiedDate
                }).ToList();
            }

            // Map Qualifications if they exist
            if (model.Qualifications != null)
            {
                nonTeachingMaster.Qualifications = model.Qualifications.Select(q => new NonTeachingQualificationDetails
                {
                    Id = q.Id,
                    NonTeachingId = model.Id,
                    Qualification = q.Qualification,
                    QualificationTypeId = q.QualificationTypeId,
                    Institution = q.Institution,
                    BoardUniversity = q.BoardUniversity,
                    YearOfPassing = q.YearOfPassing,
                    Percentage = q.Percentage,
                    Division = q.Division,
                    DocumentPath = q.DocumentPath,
                    IsVerified = q.IsVerified,
                    VerifiedBy = q.VerifiedBy,
                    VerifiedOn = q.VerifiedOn,
                    Remarks = q.Remarks,
                    IsActive = q.IsActive,
                    CreatedBy = q.CreatedBy,
                    CreatedDate = q.CreatedDate,
                    ModifiedBy = q.ModifiedBy,
                    ModifiedDate = q.ModifiedDate
                }).ToList();
            }

            return nonTeachingMaster;
        }

        private NonTeachingViewModel MapToNonTeachingViewModel(NonTeachingMaster entity)
        {
            // Return an empty view model when input is null to ensure all code paths return a value
            if (entity == null)
                return new NonTeachingViewModel
                {
                    Id = Guid.Empty,
                    FirstName = string.Empty,
                    MiddleName = string.Empty,
                    LastName = string.Empty,
                    Email = string.Empty,
                    Phone = string.Empty,
                    MobilePhone = string.Empty,
                    Designation = string.Empty,
                    Department = string.Empty,
                    IsActive = true,
                    IsDeleted = false,
                    EmployeeCode = string.Empty,
                    DOB = null,
                    DOJ = null,
                    DateOfLeaving = null,
                    Address = string.Empty,
                    CityId = null,
                    StateId = null,
                    CountryId = null,
                    ZipCode = string.Empty,
                    Gender = string.Empty,
                    MaritalStatusId = null,
                    ImageFile = null,
                    Image = Array.Empty<byte>(),
                    Qualification = string.Empty,
                    Salary = null,
                    BankAccountNumber = string.Empty,
                    BankName = string.Empty,
                    IFSCCode = string.Empty,
                    PAN = string.Empty,
                    AadharNumber = string.Empty,
                    EmergencyContactName = string.Empty,
                    EmergencyContactNumber = string.Empty,
                    EmergencyContactRelation = string.Empty,
                    CompanyId = Guid.Empty,
                    SchoolId = Guid.Empty,
                    CreatedBy = Guid.Empty,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = null,
                    ModifiedDate = null,
                    Documents = new List<NonTeachingDocumentDetails>(),
                    Qualifications = new List<NonTeachingQualificationDetails>()
                };

            var viewModel = new NonTeachingViewModel
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                MiddleName = entity.MiddleName,
                LastName = entity.LastName,
                Email = entity.Email,
                Phone = entity.Phone,
                MobilePhone = entity.MobilePhone,
                Designation = entity.Designation,
                Department = entity.Department,
                IsActive = entity.IsActive,
                IsDeleted = entity.IsDeleted,
                EmployeeCode = entity.EmployeeCode,
                DOB = entity.DOB,
                DOJ = entity.DOJ,
                DateOfLeaving = entity.DateOfLeaving,
                Address = entity.Address,
                CityId = entity.CityId ?? Guid.Empty,
                StateId = entity.StateId ?? Guid.Empty,
                CountryId = entity.CountryId ?? Guid.Empty,
                ZipCode = entity.ZipCode,
                Gender = entity.Gender,
                MaritalStatusId = entity.MaritalStatusId ?? Guid.Empty,
                Image = entity.Image,
                Qualification = entity.Qualification,
                Salary = entity.Salary,
                BankAccountNumber = entity.BankAccountNumber,
                BankName = entity.BankName,
                IFSCCode = entity.IFSCCode,
                PAN = entity.PAN,
                AadharNumber = entity.AadharNumber,
                EmergencyContactName = entity.EmergencyContactName,
                EmergencyContactNumber = entity.EmergencyContactNumber,
                EmergencyContactRelation = entity.EmergencyContactRelation,
                CompanyId = entity.CompanyId,
                SchoolId = entity.SchoolId,
                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate,
                ModifiedBy = entity.ModifiedBy,
                ModifiedDate = entity.ModifiedDate,
                // Map documents safely: project if not null otherwise use empty list to avoid null assignment
                Documents = entity.Documents?.Select(d => new NonTeachingDocumentDetails
                {
                    Id = d.Id,
                    NonTeachingId = entity.Id,
                    DocumentType = d.DocumentType,
                    DocumentTypeId = d.DocumentTypeId,
                    DocumentNumber = d.DocumentNumber,
                    DocumentPath = d.DocumentPath,
                    IssueDate = d.IssueDate,
                    ExpiryDate = d.ExpiryDate,
                    Remarks = d.Remarks,
                    IsVerified = d.IsVerified,
                    VerifiedBy = d.VerifiedBy,
                    VerifiedOn = d.VerifiedOn,
                    FileContent = d.FileContent,
                    FileType = d.FileType,
                    FileName = d.FileName,
                    Description = d.Description,
                    IsActive = d.IsActive,
                    CreatedBy = d.CreatedBy,
                    CreatedDate = d.CreatedDate,
                    ModifiedBy = d.ModifiedBy,
                    ModifiedDate = d.ModifiedDate
                }).ToList() ?? new List<NonTeachingDocumentDetails>(),

                // Map qualifications safely: project if not null otherwise use empty list
                Qualifications = entity.Qualifications?.Select(q => new NonTeachingQualificationDetails
                {
                    Id = q.Id,
                    NonTeachingId = entity.Id,
                    Qualification = q.Qualification,
                    QualificationTypeId = q.QualificationTypeId,
                    Institution = q.Institution,
                    BoardUniversity = q.BoardUniversity,
                    YearOfPassing = q.YearOfPassing,
                    Percentage = q.Percentage,
                    Division = q.Division,
                    DocumentPath = q.DocumentPath,
                    IsVerified = q.IsVerified,
                    VerifiedBy = q.VerifiedBy,
                    VerifiedOn = q.VerifiedOn,
                    Remarks = q.Remarks,
                    IsActive = q.IsActive,
                    CreatedBy = q.CreatedBy,
                    CreatedDate = q.CreatedDate,
                    ModifiedBy = q.ModifiedBy,
                    ModifiedDate = q.ModifiedDate
                }).ToList() ?? new List<NonTeachingQualificationDetails>()
            };

            return viewModel;
        }

        #endregion
    }
}