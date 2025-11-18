using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SchoolPortal.Entities.Models;
using SchoolPortalApp.Models;
using SchoolPortal.Services.IServices;

namespace SchoolPortalApp.Controllers
{
    [Route("TimeTableSetupDetails")]
    public class TimeTableSetupDetailsController : Controller
    {
        private readonly ITimeTableSetupDetailsService _service;
        private readonly ILogger<TimeTableSetupDetailsController> _logger;

        public TimeTableSetupDetailsController(
            ITimeTableSetupDetailsService service,
            ILogger<TimeTableSetupDetailsController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            var list = _service.GetAll();
            var result = list.Select(item => new TimeTableSetupDetailsListItemViewModel
            {
                Id = item.Id,
                SchoolStartTime = item.SchoolStartTime,
                SchoolEndTime = item.SchoolEndTime,
                TotalPeriods = item.TotalPeriods,
                PeriodDuration = item.PeriodDuration,
                IsActive = item.IsActive
            }).ToList();

            return View(result);
        }

        [HttpGet]
        [Route("Details/{id}")]
        public IActionResult Details(Guid id)
        {
            var item = _service.GetById(id);
            if (item == null) return NotFound();
            return View(item);
        }

        [HttpGet]
        [Route("Create")]
        public IActionResult Create()
        {
            var vm = new TimeTableSetupDetailsViewModel();
            return View(vm);
        }

        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TimeTableSetupDetailsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userIdStr = HttpContext.Session.GetString("UserId");
            var companyIdStr = HttpContext.Session.GetString("CompanyId");
            var schoolIdStr = HttpContext.Session.GetString("SchoolId");

            if (string.IsNullOrEmpty(companyIdStr) ||
                string.IsNullOrEmpty(schoolIdStr) ||
                string.IsNullOrEmpty(userIdStr))
            {
                ModelState.AddModelError(string.Empty, "Missing required session data.");
                return View(model);
            }

            if (!Guid.TryParse(companyIdStr, out var companyId) ||
                !Guid.TryParse(schoolIdStr, out var schoolId) ||
                !Guid.TryParse(userIdStr, out var userId))
            {
                ModelState.AddModelError(string.Empty, "Invalid session data format.");
                return View(model);
            }

            var entity = new TimeTableSetupDetails
            {
                Id = Guid.Empty,
                SchoolStartTime = model.SchoolStartTime,
                SchoolEndTime = model.SchoolEndTime,
                PeriodStartTime = model.PeriodStartTime,
                TotalPeriods = model.TotalPeriods,
                PeriodDuration = model.PeriodDuration,
                RecessDuration = model.RecessDuration,
                RecessAfterPeriod = model.RecessAfterPeriod,
                FruitRecessDuration = model.FruitRecessDuration,
                FruitRecessAfterPeriod = model.FruitRecessAfterPeriod,
                SessionId = model.SessionId,
                CompanyId = companyId,
                SchoolId = schoolId,
                IsActive = model.IsActive,
                CreatedBy = userId,
                CreatedDate = DateTime.UtcNow
            };

            var newId = _service.Create(entity);
            if (newId == Guid.Empty)
            {
                ModelState.AddModelError(string.Empty, "Failed to create setup.");
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

            var vm = new TimeTableSetupDetailsViewModel
            {
                Id = item.Id,
                SchoolStartTime = item.SchoolStartTime,
                SchoolEndTime = item.SchoolEndTime,
                PeriodStartTime = item.PeriodStartTime,
                TotalPeriods = item.TotalPeriods,
                PeriodDuration = item.PeriodDuration,
                RecessDuration = item.RecessDuration,
                RecessAfterPeriod = item.RecessAfterPeriod,
                FruitRecessDuration = item.FruitRecessDuration,
                FruitRecessAfterPeriod = item.FruitRecessAfterPeriod,
                SessionId = item.SessionId,
                IsActive = item.IsActive,
                SchoolId = item.SchoolId
            };

            return View(vm);
        }

        [HttpPost]
        [Route("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, TimeTableSetupDetailsViewModel model)
        {
            if (id != model.Id) return BadRequest();
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userIdStr = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrWhiteSpace(userIdStr) ||
                !Guid.TryParse(userIdStr, out var userId) ||
                model.SchoolId == Guid.Empty)
            {
                ModelState.AddModelError(string.Empty, "Please login to update.");
                return View(model);
            }

            var entity = new TimeTableSetupDetails
            {
                Id = id,
                SchoolStartTime = model.SchoolStartTime,
                SchoolEndTime = model.SchoolEndTime,
                PeriodStartTime = model.PeriodStartTime,
                TotalPeriods = model.TotalPeriods,
                PeriodDuration = model.PeriodDuration,
                RecessDuration = model.RecessDuration,
                RecessAfterPeriod = model.RecessAfterPeriod,
                FruitRecessDuration = model.FruitRecessDuration,
                FruitRecessAfterPeriod = model.FruitRecessAfterPeriod,
                SessionId = model.SessionId,
                IsActive = model.IsActive,
                SchoolId = model.SchoolId,
                ModifiedBy = userId,
                ModifiedDate = DateTime.UtcNow
            };

            if (!_service.Update(entity))
            {
                ModelState.AddModelError(string.Empty, "Failed to update setup.");
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
                TempData["ErrorMessage"] = "Failed to delete setup.";
                return RedirectToAction("Delete", new { id });
            }
            return RedirectToAction("Index");
        }
    }
}