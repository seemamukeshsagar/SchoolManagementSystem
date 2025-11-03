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
    [Route("Subject")]
    public class SubjectController : Controller
    {
        private readonly ISubjectService _service;
        private readonly ISchoolService _schoolService;
        private readonly ILogger<SubjectController> _logger;

        public SubjectController(ISubjectService service, ISchoolService schoolService, ILogger<SubjectController> logger)
        {
            _service = service;
            _schoolService = schoolService;
            _logger = logger;
        }

        private void PopulateDropdowns(SubjectViewModel vm)
        {
            var schools = _schoolService.GetAll();
            vm.Schools = schools.Select(s => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = s.Id.ToString(), Text = s.Name, Selected = s.Id == vm.SchoolId }).ToList();
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
                return new SubjectListItemViewModel
                {
                    Id = item.Id,
                    SubjectName = item.SubjectName,
                    IsScholastic = item.IsScholastic,
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
            var vm = new SubjectViewModel();
            return View(vm);
        }

        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SubjectViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userIdStr = HttpContext.Session.GetString("UserId");
           
            // Get CompanyId and SchoolId from session
            var companyIdStr = HttpContext.Session.GetString("CompanyId");
            var schoolIdStr = HttpContext.Session.GetString("SchoolId");
            
            // Check if required session values are present
            if (string.IsNullOrEmpty(companyIdStr) || string.IsNullOrEmpty(schoolIdStr) || string.IsNullOrEmpty(userIdStr))
            {
                ModelState.AddModelError(string.Empty, "Missing required session data.");
                PopulateDropdowns(model);
                return View(model);
            }

            // Parse all GUIDs in a single check
            if (!Guid.TryParse(companyIdStr, out var companyId) || 
                !Guid.TryParse(schoolIdStr, out var schoolId) || 
                !Guid.TryParse(userIdStr, out var userId))
            {
                ModelState.AddModelError(string.Empty, "Invalid session data format.");
                PopulateDropdowns(model);
                return View(model);
            }
            
            var entity = new SubjectMaster
            {
                Id = Guid.Empty,
                SubjectName = model.SubjectName,
                IsScholastic = model.IsScholastic ?? false,
                IsActive = model.IsActive,
                CompanyId = companyId,
                SchoolId = schoolId,
                CreatedBy = userId,
                CreatedDate = DateTime.UtcNow
            };

            var newId = _service.Create(entity);
            if (newId == Guid.Empty)
            {
                ModelState.AddModelError(string.Empty, "Failed to create subject.");
                PopulateDropdowns(model);
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
            var vm = new SubjectViewModel
            {
                Id = item.Id,
                SubjectName = item.SubjectName,
                IsScholastic = item.IsScholastic,
                IsActive = item.IsActive,
                SchoolId = item.SchoolId
            };
            return View(vm);
        }

        [HttpPost]
        [Route("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, SubjectViewModel model)
        {
            if (id != model.Id) return BadRequest();

            var schoolIdStr = HttpContext.Session.GetString("SchoolId");
            if (!string.IsNullOrWhiteSpace(schoolIdStr) && Guid.TryParse(schoolIdStr, out var schoolIdFromSession))
            {
                ModelState.Remove(nameof(SubjectViewModel.SchoolId));
                model.SchoolId = schoolIdFromSession;
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userIdStr = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrWhiteSpace(userIdStr) || !Guid.TryParse(userIdStr, out var userId) || model.SchoolId == Guid.Empty)
            {
                ModelState.AddModelError(string.Empty, "Please login to update subject.");
                return View(model);
            }

            var entity = new SubjectMaster
            {
                Id = id,
                SubjectName = model.SubjectName,
                IsScholastic = model.IsScholastic ?? false,
                IsActive = model.IsActive,
                SchoolId = model.SchoolId,
                ModifiedBy = userId,
                ModifiedDate = DateTime.UtcNow
            };

            if (!_service.Update(entity))
            {
                ModelState.AddModelError(string.Empty, "Failed to update subject.");
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
                TempData["ErrorMessage"] = "Failed to delete subject.";
                return RedirectToAction("Delete", new { id });
            }
            return RedirectToAction("Index");
        }
    }
}
