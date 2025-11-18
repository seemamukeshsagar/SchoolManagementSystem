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
    [Route("SessionMaster")]
    public class SessionMasterController : Controller
    {
        private readonly ISessionMasterService _service;
        private readonly ILogger<SessionMasterController> _logger;

        public SessionMasterController(
            ISessionMasterService service,
            ILogger<SessionMasterController> logger)
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
            var result = list.Select(item => new SessionMasterListItemViewModel
            {
                Id = item.Id,
                Value = item.Value ?? string.Empty,
                Description = item.Description,
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
            var vm = new SessionMasterViewModel();
            return View(vm);
        }

        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SessionMasterViewModel model)
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

            var entity = new SessionMaster
            {
                Id = Guid.Empty,
                Value = model.Value,
                Description = model.Description ?? string.Empty,
                CompanyId = companyId,
                SchoolId = schoolId,
                IsActive = model.IsActive,
                CreatedBy = userId,
                CreatedDate = DateTime.UtcNow
            };

            var newId = _service.Create(entity);
            if (newId == Guid.Empty)
            {
                ModelState.AddModelError(string.Empty, "Failed to create session.");
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

            var vm = new SessionMasterViewModel
            {
                Id = item.Id,
                Value = item.Value,
                Description = item.Description,
                IsActive = item.IsActive,
                SchoolId = item.SchoolId
            };

            return View(vm);
        }

        [HttpPost]
        [Route("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, SessionMasterViewModel model)
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

            var entity = new SessionMaster
            {
                Id = id,
                Value = model.Value,
                Description = model.Description ?? string.Empty,
                IsActive = model.IsActive,
                SchoolId = model.SchoolId,
                ModifiedBy = userId,
                ModifiedDate = DateTime.UtcNow
            };

            if (!_service.Update(entity))
            {
                ModelState.AddModelError(string.Empty, "Failed to update session.");
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
                TempData["ErrorMessage"] = "Failed to delete session.";
                return RedirectToAction("Delete", new { id });
            }
            return RedirectToAction("Index");
        }
    }
}