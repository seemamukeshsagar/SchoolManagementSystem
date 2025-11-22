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
    [Route("TimeTablePeriodMaster")]
    public class TimeTablePeriodMasterController : BaseController
    {
        private readonly ITimeTablePeriodMasterService _service;
        private readonly ILogger<TimeTablePeriodMasterController> _logger;

        public TimeTablePeriodMasterController(
            ITimeTablePeriodMasterService service,
            ILogger<TimeTablePeriodMasterController> logger)
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
            var result = list.Select(item => new TimeTablePeriodMasterListItemViewModel
            {
                Id = item.Id,
                Description = item.Description ?? string.Empty,
                PeriodNumber = item.PeriodNumber ?? string.Empty,
                StartTime = item.StartTime,
                EndTime = item.EndTime,
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
            var vm = new TimeTablePeriodMasterViewModel();
            return View(vm);
        }

        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TimeTablePeriodMasterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = CurrentUserId;
            var companyId = CurrentCompanyId;
            var schoolId = CurrentSchoolId;
            if (!companyId.HasValue || !schoolId.HasValue || !userId.HasValue)
            {
                ModelState.AddModelError(string.Empty, "Missing required session data.");
                return View(model);
            }

            var entity = new TimeTablePeriodMaster
            {
                Id = Guid.Empty,
                Description = model.Description,
                PeriodNumber = model.PeriodNumber,
                StartTime = model.StartTime,
                EndTime = model.EndTime,
                SessionId = model.SessionId,
                CompanyId = companyId.Value,
                SchoolId = schoolId.Value,
                IsActive = model.IsActive,
                CreatedBy = userId.Value,
                CreatedDate = DateTime.UtcNow
            };

            var newId = _service.Create(entity);
            if (newId == Guid.Empty)
            {
                ModelState.AddModelError(string.Empty, "Failed to create period.");
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

            var vm = new TimeTablePeriodMasterViewModel
            {
                Id = item.Id,
                Description = item.Description,
                PeriodNumber = item.PeriodNumber,
                StartTime = item.StartTime,
                EndTime = item.EndTime,
                SessionId = item.SessionId,
                IsActive = item.IsActive,
                SchoolId = item.SchoolId
            };

            return View(vm);
        }

        [HttpPost]
        [Route("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, TimeTablePeriodMasterViewModel model)
        {
            if (id != model.Id) return BadRequest();
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = CurrentUserId;
            if (!userId.HasValue || model.SchoolId == Guid.Empty)
            {
                ModelState.AddModelError(string.Empty, "Please login to update.");
                return View(model);
            }

            var entity = new TimeTablePeriodMaster
            {
                Id = id,
                Description = model.Description,
                PeriodNumber = model.PeriodNumber,
                StartTime = model.StartTime,
                EndTime = model.EndTime,
                SessionId = model.SessionId,
                IsActive = model.IsActive,
                SchoolId = model.SchoolId,
                ModifiedBy = userId.Value,
                ModifiedDate = DateTime.UtcNow
            };

            if (!_service.Update(entity))
            {
                ModelState.AddModelError(string.Empty, "Failed to update period.");
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
                TempData["ErrorMessage"] = "Failed to delete period.";
                return RedirectToAction("Delete", new { id });
            }
            return RedirectToAction("Index");
        }
    }
}