using DocumentFormat.OpenXml.Drawing.Charts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SchoolPortal.Entities.Models;
using SchoolPortal.Services;
using SchoolPortal.Services.IServices;
using SchoolPortalApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SchoolPortalApp.Controllers
{
    [Route("TimeTablePeriod")]
    public class TimeTablePeriodController : BaseController
    {
        private readonly ITimeTablePeriodService _service;
        private readonly ILogger<TimeTablePeriodController> _logger;

        public TimeTablePeriodController(
            ITimeTablePeriodService service,
            ILogger<TimeTablePeriodController> logger)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            try
            {
                var periods = _service.GetAllAsync();
                return View(periods);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving timetable periods");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("Details/{id}")]
        public IActionResult Details(Guid id)
        {
            try
            {
                var period = _service.GetById(id);
                if (period == null)
                {
                    return NotFound();
                }
                return View(period);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving timetable period with ID: {id}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("Create")]
        public IActionResult Create()
        {
            return View(new TimeTableClassPeriodDetails());
        }

        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TimeTableClassPeriodDetails model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var userId = CurrentUserId;
                if (!userId.HasValue)
                {
                    ModelState.AddModelError(string.Empty, "Please login to create a timetable period.");
                    return View(model);
                }

                model.CreatedBy = userId.Value;
                model.CreatedDate = DateTime.UtcNow;
                model.IsActive = true;
                model.IsDeleted = false;

                var newId = _service.CreateAsync(model);
                if (newId == null)
                {
                    ModelState.AddModelError(string.Empty, "Failed to create timetable period.");
                    return View(model);
                }

                return RedirectToAction("Details", new { id = newId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating timetable period");
                ModelState.AddModelError(string.Empty, "An error occurred while creating the timetable period.");
                return View(model);
            }
        }

        [HttpGet]
        [Route("Edit/{id}")]
        public IActionResult Edit(Guid id)
        {
            try
            {
                var period = _service.GetById(id);
                if (period == null)
                {
                    return NotFound();
                }
                return View(period);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving timetable period for edit with ID: {id}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, TimeTableClassPeriodDetails model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var userId = CurrentUserId;
                if (!userId.HasValue)
                {
                    ModelState.AddModelError(string.Empty, "Please login to update the timetable period.");
                    return View(model);
                }

                model.ModifiedBy = userId;
                model.ModifiedDate = DateTime.UtcNow;

                try
                {
                    _service.UpdateAsync(model);
                }
                catch (Exception )
                {
                    ModelState.AddModelError(string.Empty, "Failed to update timetable period.");
                    return View(model);
                }                

                return RedirectToAction("Details", new { id = model.Id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating timetable period with ID: {id}");
                ModelState.AddModelError(string.Empty, "An error occurred while updating the timetable period.");
                return View(model);
            }
        }

        [HttpGet]
        [Route("Delete/{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                var period = _service.GetById(id);
                if (period == null)
                {
                    return NotFound();
                }
                return View(period);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving timetable period for deletion with ID: {id}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("Delete/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            try
            {
                var userId = CurrentUserId;
                if (!userId.HasValue)
                {
                    return Unauthorized();
                }

                try
                {
                    _service.DeleteAsync(id);
                }
                catch (Exception )
                { 
                    return StatusCode(StatusCodes.Status500InternalServerError, "Failed to delete timetable period.");
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting timetable period with ID: {id}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while deleting the timetable period.");
            }
        }
    }
}