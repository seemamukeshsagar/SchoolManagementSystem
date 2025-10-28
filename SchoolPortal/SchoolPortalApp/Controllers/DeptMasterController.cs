using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Schoolortal.Entities.Models;
using SchoolPortalApp.Models;
using SchoolPortal.Services.IServices;

namespace SchoolPortalApp.Controllers
{
    [Route("DeptMaster")]
    public class DeptMasterController : Controller
    {
        private readonly IDeptMasterService _service;
        private readonly ISchoolService _schoolService;
        private readonly ILogger<DeptMasterController> _logger;

        public DeptMasterController(IDeptMasterService service, ISchoolService schoolService, ILogger<DeptMasterController> logger)
        {
            _service = service;
            _schoolService = schoolService;
            _logger = logger;
        }

        private void PopulateDropdowns(DeptMasterViewModel vm)
        {
            var schools = _schoolService.GetAll();
            vm.Schools = schools.Select(s => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem 
            { 
                Value = s.Id.ToString(), 
                Text = s.Name, 
                Selected = s.Id == vm.SchoolId 
            }).ToList();
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
                return new DeptMasterListItemViewModel
                {
                    Id = item.Id,
                    DeptCode = item.DeptCode,
                    DeptName = item.DeptName,
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
            var vm = new DeptMasterViewModel();
            PopulateDropdowns(vm);
            return View(vm);
        }

        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DeptMasterViewModel model)
        {
            var schoolIdStr = HttpContext.Session.GetString("SchoolId");
            if (!string.IsNullOrWhiteSpace(schoolIdStr) && Guid.TryParse(schoolIdStr, out var schoolId))
            {
                ModelState.Remove(nameof(DeptMasterViewModel.SchoolId));
                model.SchoolId = schoolId;
            }

            if (!ModelState.IsValid)
            {
                PopulateDropdowns(model);
                return View(model);
            }

            var userIdStr = HttpContext.Session.GetString("UserId");
            var companyIdStr = HttpContext.Session.GetString("CompanyId");
            if (string.IsNullOrWhiteSpace(userIdStr) || !Guid.TryParse(userIdStr, out var userId) || 
                string.IsNullOrWhiteSpace(companyIdStr) || !Guid.TryParse(companyIdStr, out var companyId) || 
                model.SchoolId == Guid.Empty)
            {
                ModelState.AddModelError(string.Empty, "Please login and select company to create department.");
                PopulateDropdowns(model);
                return View(model);
            }

            var entity = new DeptMaster
            {
                Id = Guid.Empty,
                DeptCode = model.DeptCode,
                DeptName = model.DeptName,
                IsActive = model.IsActive,
                CompanyId = companyId,
                SchoolId = model.SchoolId,
                CreatedBy = userId,
                CreatedDate = DateTime.UtcNow
            };

            var newId = _service.Create(entity);
            if (newId == Guid.Empty)
            {
                ModelState.AddModelError(string.Empty, "Failed to create department.");
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
            
            var vm = new DeptMasterViewModel
            {
                Id = item.Id,
                DeptCode = item.DeptCode,
                DeptName = item.DeptName,
                IsActive = item.IsActive,
                SchoolId = item.SchoolId
            };
            
            PopulateDropdowns(vm);
            return View(vm);
        }

        [HttpPost]
        [Route("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, DeptMasterViewModel model)
        {
            if (id != model.Id) return BadRequest();

            var schoolIdStr = HttpContext.Session.GetString("SchoolId");
            if (!string.IsNullOrWhiteSpace(schoolIdStr) && Guid.TryParse(schoolIdStr, out var schoolIdFromSession))
            {
                ModelState.Remove(nameof(DeptMasterViewModel.SchoolId));
                model.SchoolId = schoolIdFromSession;
            }

            if (!ModelState.IsValid)
            {
                PopulateDropdowns(model);
                return View(model);
            }

            var userIdStr = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrWhiteSpace(userIdStr) || !Guid.TryParse(userIdStr, out var userId) || model.SchoolId == Guid.Empty)
            {
                ModelState.AddModelError(string.Empty, "Please login to update department.");
                PopulateDropdowns(model);
                return View(model);
            }

            var entity = new DeptMaster
            {
                Id = id,
                DeptCode = model.DeptCode,
                DeptName = model.DeptName,
                IsActive = model.IsActive,
                SchoolId = model.SchoolId,
                ModifiedBy = userId,
                ModifiedDate = DateTime.UtcNow
            };

            if (!_service.Update(entity))
            {
                ModelState.AddModelError(string.Empty, "Failed to update department.");
                PopulateDropdowns(model);
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
                TempData["ErrorMessage"] = "Failed to delete department.";
                return RedirectToAction("Delete", new { id });
            }
            return RedirectToAction("Index");
        }
    }
}