using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SchoolPortal.Entities.Models;
using SchoolPortal.Services.IServices;
using SchoolPortalApp.Models;

namespace SchoolPortalApp.Controllers
{
    [Route("CleanerMaster")]
    public class CleanerMasterController : Controller
    {
        private readonly ICleanerMasterService _service;
        private readonly ISchoolService _schoolService;
        private readonly ILogger<CleanerMasterController> _logger;

        public CleanerMasterController(ICleanerMasterService service, ISchoolService schoolService, ILogger<CleanerMasterController> logger)
        {
            _service = service;
            _schoolService = schoolService;
            _logger = logger;
        }

        [HttpGet]
        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            var list = _service.GetAll();
            var schools = _schoolService.GetAll();
            var result = list.Select(item =>
            {
                var school = schools.FirstOrDefault(s => s.Id == item.SchoolId);
                return new CleanerListItemViewModel
                {
                    Id = item.Id,
                    Name = item.Name ?? string.Empty,
                    FatherName = item.FatherName ?? string.Empty,
                    IsActive = item.IsActive,
                    SchoolName = school?.Name ?? string.Empty
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
            return View(item);
        }

        [HttpGet]
        [Route("Create")]
        public IActionResult Create()
        {
            var entity = new CleanerMaster
            {
                IsActive = true,
                IsDeleted = false,
                Status = "INC",
                StatusMessage = "In Process....",
                CreatedDate = DateTime.UtcNow
            };
            return View(entity);
        }

        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CleanerMaster model)
        {
            var schoolIdStr = HttpContext.Session.GetString("SchoolId");
            if (!string.IsNullOrWhiteSpace(schoolIdStr) && Guid.TryParse(schoolIdStr, out var schoolId))
            {
                model.SchoolId = schoolId;
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userIdStr = HttpContext.Session.GetString("UserId");
            var companyIdStr = HttpContext.Session.GetString("CompanyId");
            if (string.IsNullOrWhiteSpace(userIdStr) || !Guid.TryParse(userIdStr, out var userId) || string.IsNullOrWhiteSpace(companyIdStr) || !Guid.TryParse(companyIdStr, out var companyId) || model.SchoolId == Guid.Empty)
            {
                ModelState.AddModelError(string.Empty, "Please login and select company to create cleaner.");
                return View(model);
            }

            // Normalize optional strings
            model.Id = Guid.Empty;
            model.Name = model.Name ?? string.Empty;
            model.Image = model.Image ?? string.Empty;
            model.FatherName = model.FatherName ?? string.Empty;
            model.Description = model.Description ?? string.Empty;
            model.Status = string.IsNullOrWhiteSpace(model.Status) ? "INC" : model.Status;
            model.StatusMessage = string.IsNullOrWhiteSpace(model.StatusMessage) ? "In Process...." : model.StatusMessage;
            model.CompanyId = companyId;
            model.CreatedBy = userId;
            model.CreatedDate = DateTime.UtcNow;

            var newId = _service.Create(model);
            if (newId == Guid.Empty)
            {
                ModelState.AddModelError(string.Empty, "Failed to create cleaner.");
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
            return View(item);
        }

        [HttpPost]
        [Route("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, CleanerMaster model)
        {
            if (id != model.Id) return BadRequest();

            var schoolIdStr = HttpContext.Session.GetString("SchoolId");
            if (!string.IsNullOrWhiteSpace(schoolIdStr) && Guid.TryParse(schoolIdStr, out var schoolIdFromSession))
            {
                model.SchoolId = schoolIdFromSession;
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userIdStr = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrWhiteSpace(userIdStr) || !Guid.TryParse(userIdStr, out var userId) || model.SchoolId == Guid.Empty)
            {
                ModelState.AddModelError(string.Empty, "Please login to update cleaner.");
                return View(model);
            }

            // Normalize optional strings
            model.Name = model.Name ?? string.Empty;
            model.Image = model.Image ?? string.Empty;
            model.FatherName = model.FatherName ?? string.Empty;
            model.Description = model.Description ?? string.Empty;
            model.Status = string.IsNullOrWhiteSpace(model.Status) ? "INC" : model.Status;
            model.StatusMessage = string.IsNullOrWhiteSpace(model.StatusMessage) ? "In Process...." : model.StatusMessage;
            model.ModifiedBy = userId;
            model.ModifiedDate = DateTime.UtcNow;

            if (!_service.Update(model))
            {
                ModelState.AddModelError(string.Empty, "Failed to update cleaner.");
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
                TempData["ErrorMessage"] = "Failed to delete cleaner.";
                return RedirectToAction("Delete", new { id });
            }
            return RedirectToAction("Index");
        }
    }
}
