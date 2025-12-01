// File: SchoolPortalApp/Controllers/AcademicYearController.cs
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SchoolPortal.Entities.Models;
using SchoolPortal.Services.IServices;
using SchoolPortalApp.Models.AcademicYear;

namespace SchoolPortalApp.Controllers
{
    [Route("AcademicYear")]
    public class AcademicYearController : BaseController
    {
        private readonly IAcademicYearService _academicYearService;
        private readonly ILogger<AcademicYearController> _logger;

        public AcademicYearController(
            IAcademicYearService academicYearService,
            ILogger<AcademicYearController> logger)
        {
            _academicYearService = academicYearService ?? throw new ArgumentNullException(nameof(academicYearService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            try
            {
                var academicYears = _academicYearService.GetAll();
                return View(academicYears);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving academic years");
                TempData["ErrorMessage"] = "An error occurred while retrieving academic years.";
                return View(Array.Empty<AcademicYear>());
            }
        }

        [HttpGet]
        [Route("Details/{id}")]
        public IActionResult Details(Guid id)
        {
            try
            {
                var academicYear = _academicYearService.GetById(id);
                if (academicYear == null)
                {
                    TempData["ErrorMessage"] = "Academic year not found.";
                    return RedirectToAction(nameof(Index));
                }

                var viewModel = new AcademicYearViewModel
                {
                    Id = academicYear.Id,
                    AcademicYearName = academicYear.AcademicYearName,
                    StartDate = academicYear.StartDate,
                    EndDate = academicYear.EndDate,
                    IsCurrent = academicYear.IsCurrent,
                    IsActive = academicYear.IsActive
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving academic year with ID: {id}");
                TempData["ErrorMessage"] = "An error occurred while retrieving the academic year details.";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        [Route("Create")]
        public IActionResult Create()
        {
            return View(new AcademicYearViewModel());
        }

        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AcademicYearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var currentUserId = CurrentUserId ?? throw new UnauthorizedAccessException("User not authenticated");

                var academicYear = new AcademicYear
                {
                    Id = Guid.NewGuid(),
                    AcademicYearName = model.AcademicYearName,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    IsCurrent = model.IsCurrent,
                    IsActive = model.IsActive,
                    CreatedBy = currentUserId,
                    CreatedDate = DateTime.UtcNow
                };

                var id = _academicYearService.Create(academicYear);
                TempData["SuccessMessage"] = "Academic year created successfully.";
                return RedirectToAction(nameof(Details), new { id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating academic year");
                ModelState.AddModelError("", "An error occurred while creating the academic year. Please try again.");
                return View(model);
            }
        }

        [HttpGet]
        [Route("Edit/{id}")]
        public IActionResult Edit(Guid id)
        {
            try
            {
                var academicYear = _academicYearService.GetById(id);
                if (academicYear == null)
                {
                    TempData["ErrorMessage"] = "Academic year not found.";
                    return RedirectToAction(nameof(Index));
                }

                var viewModel = new AcademicYearViewModel
                {
                    Id = academicYear.Id,
                    AcademicYearName = academicYear.AcademicYearName,
                    StartDate = academicYear.StartDate,
                    EndDate = academicYear.EndDate,
                    IsCurrent = academicYear.IsCurrent,
                    IsActive = academicYear.IsActive
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving academic year for edit with ID: {id}");
                TempData["ErrorMessage"] = "An error occurred while retrieving the academic year for editing.";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [Route("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, AcademicYearViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var currentUserId = CurrentUserId ?? throw new UnauthorizedAccessException("User not authenticated");
                
                var academicYear = new AcademicYear
                {
                    Id = model.Id,
                    AcademicYearName = model.AcademicYearName,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    IsCurrent = model.IsCurrent,
                    IsActive = model.IsActive,
                    ModifiedBy = currentUserId,
                    ModifiedDate = DateTime.UtcNow
                };

                var success = _academicYearService.Update(academicYear);
                if (!success)
                {
                    TempData["ErrorMessage"] = "Failed to update the academic year.";
                    return View(model);
                }

                TempData["SuccessMessage"] = "Academic year updated successfully.";
                return RedirectToAction(nameof(Details), new { id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating academic year with ID: {id}");
                ModelState.AddModelError("", "An error occurred while updating the academic year. Please try again.");
                return View(model);
            }
        }

        [HttpPost]
        [Route("Delete/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Guid id)
        {
            try
            {
                var success = _academicYearService.Delete(id);
                if (!success)
                {
                    TempData["ErrorMessage"] = "Failed to delete the academic year.";
                    return RedirectToAction(nameof(Details), new { id });
                }

                TempData["SuccessMessage"] = "Academic year deleted successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting academic year with ID: {id}");
                TempData["ErrorMessage"] = "An error occurred while deleting the academic year.";
                return RedirectToAction(nameof(Details), new { id });
            }
        }

        [HttpPost]
        [Route("ToggleStatus/{id}")]
        public IActionResult ToggleStatus(Guid id)
        {
            try
            {
                var success = _academicYearService.ToggleStatus(id);
                return Json(new { success, message = success ? "Status updated successfully." : "Failed to update status." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error toggling status for academic year with ID: {id}");
                return Json(new { success = false, message = "An error occurred while updating the status." });
            }
        }

        [HttpPost]
        [Route("SetCurrent/{id}")]
        public IActionResult SetCurrent(Guid id)
        {
            try
            {
                var academicYear = _academicYearService.GetById(id);
                if (academicYear == null)
                {
                    return Json(new { success = false, message = "Academic year not found." });
                }

                // This will be handled by the service layer
                var success = _academicYearService.Update(new AcademicYear
                {
                    Id = id,
                    IsCurrent = true,
                    ModifiedBy = CurrentUserId ?? Guid.Empty,
                    ModifiedDate = DateTime.UtcNow
                });

                return Json(new { success, message = success ? "Current academic year set successfully." : "Failed to set current academic year." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error setting current academic year with ID: {id}");
                return Json(new { success = false, message = "An error occurred while setting the current academic year." });
            }
        }
    }
}