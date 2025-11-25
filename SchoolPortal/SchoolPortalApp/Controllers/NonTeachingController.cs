// SchoolPortalApp/Controllers/NonTeachingController.cs
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
    [Route("NonTeaching")]
    public class NonTeachingController : BaseController
    {
        private readonly INonTeachingService _service;
        private readonly ISchoolService _schoolService;
        private readonly ILogger<NonTeachingController> _logger;
        private readonly INonTeachingDocumentDetailsService _docService;
        private readonly INonTeachingQualificationDetailsService _qualService;
        private readonly ILookupService _lookupService;
        private readonly IWebHostEnvironment _env;
        private readonly IEmpService _empService;

        public NonTeachingController(
            INonTeachingService service,
            ISchoolService schoolService,
            ILookupService lookupService,
            ILogger<NonTeachingController> logger,
            INonTeachingDocumentDetailsService docService,
            INonTeachingQualificationDetailsService qualService,
            IWebHostEnvironment env,
            IEmpService empService)
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
        public async Task<IActionResult> Index(int page = 1, int pageSize = 10, string searchTerm = null)
        {
            try
            {
                var schoolId = CurrentSchoolId;
                IEnumerable<NonTeachingMaster> list;
                
                if (schoolId.HasValue)
                {
                    list = await _service.GetBySchoolIdAsync(schoolId.Value);
                }
                else
                {
                    list = await _service.GetAllAsync();
                }

                // Apply search filter if search term is provided
                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    var searchTermLower = searchTerm.ToLower();
                    list = list.Where(x => 
                        (x.FirstName != null && x.FirstName.ToLower().Contains(searchTermLower)) ||
                        (x.LastName != null && x.LastName.ToLower().Contains(searchTermLower)) ||
                        (x.Email != null && x.Email.ToLower().Contains(searchTermLower)) ||
                        (x.EmployeeCode != null && x.EmployeeCode.ToLower().Contains(searchTermLower)) ||
                        (x.Designation != null && x.Designation.ToLower().Contains(searchTermLower))
                    );
                }

                // Apply pagination
                var totalItems = list.Count();
                var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
                var paginatedList = list
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                var viewModelList = paginatedList.Select(item => new NonTeachingListItemViewModel
                {
                    Id = item.Id,
                    Name = $"{item.FirstName} {item.MiddleName} {item.LastName}".Trim(),
                    Email = item.Email ?? string.Empty,
                    Phone = item.Phone ?? item.MobilePhone ?? string.Empty,
                    Designation = item.Designation ?? string.Empty,
                    Department = item.Department ?? string.Empty,
                    IsActive = item.IsActive,
                    EmployeeCode = item.EmployeeCode ?? string.Empty,
                    DOJ = item.DOJ,
                    DateOfLeaving = item.DateOfLeaving
                }).ToList();

                ViewBag.CurrentPage = page;
                ViewBag.TotalPages = totalPages;
                ViewBag.PageSize = pageSize;
                ViewBag.TotalItems = totalItems;
                ViewBag.SearchTerm = searchTerm;

                return View(viewModelList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in NonTeaching/Index");
                TempData["ErrorMessage"] = "An error occurred while retrieving non-teaching staff list.";
                return View(new List<NonTeachingListItemViewModel>());
            }
        }

        [HttpGet]
        [Route("Create")]
        public async Task<IActionResult> Create()
        {
            try
            {
                await PopulateDropdowns();
                return View(new NonTeachingMaster());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in NonTeaching/Create[GET]");
                TempData["ErrorMessage"] = "An error occurred while loading the create form.";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NonTeachingMaster model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    await PopulateDropdowns();
                    return View(model);
                }

                model.Id = Guid.NewGuid();
                model.CompanyId = CurrentCompanyId;
                model.SchoolId = CurrentSchoolId ?? Guid.Empty;
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = DateTime.UtcNow;
                model.IsActive = true;

                var result = await _service.AddAsync(model);

                if (result > 0)
                {
                    TempData["SuccessMessage"] = "Non-teaching staff created successfully.";
                    return RedirectToAction(nameof(Index));
                }

                TempData["ErrorMessage"] = "Failed to create non-teaching staff.";
                await PopulateDropdowns();
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in NonTeaching/Create[POST]");
                TempData["ErrorMessage"] = "An error occurred while creating non-teaching staff.";
                await PopulateDropdowns();
                return View(model);
            }
        }

        [HttpGet]
        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            try
            {
                var model = await _service.GetByIdAsync(id);
                if (model == null)
                {
                    return NotFound();
                }

                await PopulateDropdowns();
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in NonTeaching/Edit[GET] for ID: {id}");
                TempData["ErrorMessage"] = "An error occurred while loading the edit form.";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [Route("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, NonTeachingMaster model)
        {
            try
            {
                if (id != model.Id)
                {
                    return NotFound();
                }

                if (!ModelState.IsValid)
                {
                    await PopulateDropdowns();
                    return View(model);
                }

                var existing = await _service.GetByIdAsync(id);
                if (existing == null)
                {
                    return NotFound();
                }

                // Update properties
                existing.FirstName = model.FirstName;
                existing.MiddleName = model.MiddleName;
                existing.LastName = model.LastName;
                existing.DOB = model.DOB;
                existing.DOJ = model.DOJ;
                existing.DateOfLeaving = model.DateOfLeaving;
                existing.Address = model.Address;
                existing.CityId = model.CityId;
                existing.StateId = model.StateId;
                existing.CountryId = model.CountryId;
                existing.ZipCode = model.ZipCode;
                existing.Gender = model.Gender;
                existing.MaritalStatusId = model.MaritalStatusId;
                existing.Phone = model.Phone;
                existing.MobilePhone = model.MobilePhone;
                existing.Email = model.Email;
                existing.EmployeeCode = model.EmployeeCode;
                existing.Designation = model.Designation;
                existing.Department = model.Department;
                existing.Qualification = model.Qualification;
                existing.Salary = model.Salary;
                existing.BankAccountNumber = model.BankAccountNumber;
                existing.BankName = model.BankName;
                existing.IFSCCode = model.IFSCCode;
                existing.PAN = model.PAN;
                existing.AadharNumber = model.AadharNumber;
                existing.EmergencyContactName = model.EmergencyContactName;
                existing.EmergencyContactNumber = model.EmergencyContactNumber;
                existing.EmergencyContactRelation = model.EmergencyContactRelation;
                existing.IsActive = model.IsActive;
                existing.ModifiedBy = CurrentUserId;
                existing.ModifiedDate = DateTime.UtcNow;

                var result = await _service.UpdateAsync(existing);

                if (result)
                {
                    TempData["SuccessMessage"] = "Non-teaching staff updated successfully.";
                    return RedirectToAction(nameof(Index));
                }

                TempData["ErrorMessage"] = "Failed to update non-teaching staff.";
                await PopulateDropdowns();
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in NonTeaching/Edit[POST] for ID: {id}");
                TempData["ErrorMessage"] = "An error occurred while updating non-teaching staff.";
                await PopulateDropdowns();
                return View(model);
            }
        }

        [HttpGet]
        [Route("Details/{id}")]
        public async Task<IActionResult> Details(Guid id)
        {
            try
            {
                var model = await _service.GetByIdAsync(id);
                if (model == null)
                {
                    return NotFound();
                }

                var viewModel = new NonTeachingDetailsViewModel
                {
                    Id = model.Id,
                    Name = $"{model.FirstName} {model.MiddleName} {model.LastName}".Trim(),
                    Email = model.Email,
                    Phone = model.Phone,
                    MobilePhone = model.MobilePhone,
                    EmployeeCode = model.EmployeeCode,
                    Designation = model.Designation,
                    Department = model.Department,
                    DOB = model.DOB?.ToString("dd/MM/yyyy"),
                    DOJ = model.DOJ?.ToString("dd/MM/yyyy"),
                    DateOfLeaving = model.DateOfLeaving?.ToString("dd/MM/yyyy"),
                    Address = model.Address,
                    Gender = model.GenderLookup?.Name,
                    MaritalStatus = model.MaritalStatus?.Name,
                    Qualification = model.Qualification,
                    Salary = model.Salary,
                    BankName = model.BankName,
                    BankAccountNumber = model.BankAccountNumber,
                    IFSCCode = model.IFSCCode,
                    PAN = model.PAN,
                    AadharNumber = model.AadharNumber,
                    EmergencyContactName = model.EmergencyContactName,
                    EmergencyContactNumber = model.EmergencyContactNumber,
                    EmergencyContactRelation = model.EmergencyContactRelation,
                    IsActive = model.IsActive
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in NonTeaching/Details for ID: {id}");
                TempData["ErrorMessage"] = "An error occurred while retrieving non-teaching staff details.";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [Route("Delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            try
            {
                var result = await _service.DeleteAsync(id, CurrentUserId);
                if (result)
                {
                    TempData["SuccessMessage"] = "Non-teaching staff deleted successfully.";
                }
                else
                {
                    TempData["ErrorMessage"] = "Failed to delete non-teaching staff or record not found.";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in NonTeaching/Delete for ID: {id}");
                TempData["ErrorMessage"] = "An error occurred while deleting non-teaching staff.";
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task PopulateDropdowns()
        {
            try
            {
                var genders = await _lookupService.GetGendersAsync();
                var maritalStatuses = await _lookupService.GetMaritalStatusesAsync();
                var countries = await _lookupService.GetCountriesAsync();
                var states = new List<Lookup>();
                var cities = new List<Lookup>();

                ViewBag.GenderList = new SelectList(genders, "Id", "Name");
                ViewBag.MaritalStatusList = new SelectList(maritalStatuses, "Id", "Name");
                ViewBag.CountryList = new SelectList(countries, "Id", "Name");
                ViewBag.StateList = new SelectList(states, "Id", "Name");
                ViewBag.CityList = new SelectList(cities, "Id", "Name");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error populating dropdowns");
                // Initialize empty select lists in case of error
                ViewBag.GenderList = new SelectList(Enumerable.Empty<SelectListItem>());
                ViewBag.MaritalStatusList = new SelectList(Enumerable.Empty<SelectListItem>());
                ViewBag.CountryList = new SelectList(Enumerable.Empty<SelectListItem>());
                ViewBag.StateList = new SelectList(Enumerable.Empty<SelectListItem>());
                ViewBag.CityList = new SelectList(Enumerable.Empty<SelectListItem>());
            }
        }

        [HttpGet]
        [Route("GetStatesByCountry/{countryId}")]
        public async Task<IActionResult> GetStatesByCountry(Guid countryId)
        {
            try
            {
                var states = await _lookupService.GetStatesByCountryAsync(countryId);
                return Json(states.Select(s => new { Id = s.Id, Name = s.Name }));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting states for country ID: {countryId}");
                return Json(new List<object>());
            }
        }

        [HttpGet]
        [Route("GetCitiesByState/{stateId}")]
        public async Task<IActionResult> GetCitiesByState(Guid stateId)
        {
            try
            {
                var cities = await _lookupService.GetCitiesByStateAsync(stateId);
                return Json(cities.Select(c => new { Id = c.Id, Name = c.Name }));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting cities for state ID: {stateId}");
                return Json(new List<object>());
            }
        }

        [HttpGet]
        [Route("ExportToExcel")]
        public async Task<IActionResult> ExportToExcel()
        {
            try
            {
                var schoolId = CurrentSchoolId;
                List<NonTeachingMaster> data;

                if (schoolId.HasValue)
                {
                    data = await _service.GetBySchoolIdAsync(schoolId.Value);
                }
                else
                {
                    data = await _service.GetAllAsync();
                }

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("NonTeachingStaff");
                    var currentRow = 1;

                    // Header
                    worksheet.Cell(currentRow, 1).Value = "Employee Code";
                    worksheet.Cell(currentRow, 2).Value = "Name";
                    worksheet.Cell(currentRow, 3).Value = "Designation";
                    worksheet.Cell(currentRow, 4).Value = "Department";
                    worksheet.Cell(currentRow, 5).Value = "Email";
                    worksheet.Cell(currentRow, 6).Value = "Phone";
                    worksheet.Cell(currentRow, 7).Value = "Mobile";
                    worksheet.Cell(currentRow, 8).Value = "Status";

                    // Style header
                    var headerRange = worksheet.Range(1, 1, 1, 8);
                    headerRange.Style.Font.Bold = true;
                    headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;

                    // Data
                    foreach (var item in data)
                    {
                        currentRow++;
                        worksheet.Cell(currentRow, 1).Value = item.EmployeeCode;
                        worksheet.Cell(currentRow, 2).Value = $"{item.FirstName} {item.MiddleName} {item.LastName}".Trim();
                        worksheet.Cell(currentRow, 3).Value = item.Designation;
                        worksheet.Cell(currentRow, 4).Value = item.Department;
                        worksheet.Cell(currentRow, 5).Value = item.Email;
                        worksheet.Cell(currentRow, 6).Value = item.Phone;
                        worksheet.Cell(currentRow, 7).Value = item.MobilePhone;
                        worksheet.Cell(currentRow, 8).Value = item.IsActive ? "Active" : "Inactive";
                    }

                    // Auto-fit columns
                    worksheet.Columns().AdjustToContents();

                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        var content = stream.ToArray();
                        return File(
                            content,
                            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                            $"NonTeachingStaff_{DateTime.Now:yyyyMMddHHmmss}.xlsx");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error exporting non-teaching staff to Excel");
                TempData["ErrorMessage"] = "An error occurred while exporting data to Excel.";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [Route("ToggleStatus/{id}")]
        public async Task<IActionResult> ToggleStatus(Guid id)
        {
            if (id == Guid.Empty)
            {
                return Json(new { success = false, error = "Invalid staff ID" });
            }

            try
            {
                var result = await _service.ToggleStatusAsync(id, CurrentUserId);
                if (!result)
                {
                    return Json(new { success = false, error = "Failed to update status. Staff member not found." });
                }
                
                return Json(new { 
                    success = true, 
                    message = "Status updated successfully" 
                });
            }
            catch (ApplicationException ex)
            {
                _logger.LogError(ex, $"Application error toggling status for non-teaching staff ID: {id}");
                return Json(new { 
                    success = false, 
                    error = ex.Message 
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Unexpected error toggling status for non-teaching staff ID: {id}");
                return Json(new { 
                    success = false, 
                    error = "An unexpected error occurred while updating status. Please try again later." 
                });
            }
        }
    }
}