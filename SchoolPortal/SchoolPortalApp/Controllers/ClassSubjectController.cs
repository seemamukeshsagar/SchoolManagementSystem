// SchoolPortalApp/Controllers/ClassSubjectController.cs
using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolPortal.Entities.Models;
using SchoolPortalApp.Models;
using SchoolPortal.Services.IServices;

namespace SchoolPortalApp.Controllers
{
    [Route("ClassSubject")]
    public class ClassSubjectController : Controller
    {
        private readonly IClassSubjectService _classSubjectService;
        private readonly IClassService _classService;
        private readonly ISubjectService _subjectService;

        public ClassSubjectController(
            IClassSubjectService classSubjectService,
            IClassService classService,
            ISubjectService subjectService)
        {
            _classSubjectService = classSubjectService;
            _classService = classService;
            _subjectService = subjectService;
        }

        private void PopulateDropdowns(ClassSubjectViewModel vm)
        {
            var classes = _classService.GetAll() ?? new List<ClassMaster>();
            var subjects = _subjectService.GetAll() ?? new List<SubjectMaster>();

            vm.Classes = classes
                .Where(c => c != null && !string.IsNullOrEmpty(c.Name))
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList();

            vm.Subjects = subjects
                .Where(s => s != null && !string.IsNullOrEmpty(s.SubjectName))
                .Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.SubjectName
                }).ToList();
        }

        [HttpGet]
        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            var list = _classSubjectService.GetAll() ?? new List<ClassSubjectDetail>();
            var result = list
                .Where(item => item != null)
                .Select(item => new ClassSubjectListItemViewModel
                {
                    Id = item.Id,
                    ClassName = item.ClassMaster?.Name ?? "N/A",
                    SubjectName = item.Subject?.SubjectName ?? "N/A",
                    IsActive = item.IsActive
                }).ToList();
                return View(result);
        }

        [HttpGet]
        [Route("Details/{id}")]
        public IActionResult Details(Guid id)
        {
            var item = _classSubjectService.GetById(id);
            if (item == null)
            {
                return NotFound();
            }

            return View(new ClassSubjectViewModel
            {
                Id = item.Id,
                ClassMasterId = item.ClassMasterId,
                SubjectId = item.SubjectId,
                IsActive = item.IsActive,
                ClassName = item.ClassMaster?.Name,
                SubjectName = item.Subject?.SubjectName
            });
        }

        [HttpGet]
        [Route("Create")]
        public IActionResult Create()
        {
            var vm = new ClassSubjectViewModel();
            PopulateDropdowns(vm);
            return View(vm);
        }

        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ClassSubjectViewModel model)
        {
            if (!ModelState.IsValid)
            {
                PopulateDropdowns(model);
                return View(model);
            }

            var entity = new ClassSubjectDetail
            {
                Id = Guid.NewGuid(),
                ClassMasterId = model.ClassMasterId,
                SubjectId = model.SubjectId,
                IsActive = model.IsActive,
                CreatedDate = DateTime.UtcNow,
                IsDeleted = false
            };

            var newId = _classSubjectService.Create(entity);
            return RedirectToAction("Details", new { id = newId });
        }

        [HttpGet]
        [Route("Edit/{id}")]
        public IActionResult Edit(Guid id)
        {
            var item = _classSubjectService.GetById(id);
            if (item == null) return NotFound();

            var vm = new ClassSubjectViewModel
            {
                Id = item.Id,
                ClassMasterId = item.ClassMasterId,
                SubjectId = item.SubjectId,
                IsActive = item.IsActive,
                ClassName = item.ClassMaster?.Name,
                SubjectName = item.Subject?.SubjectName
            };

            PopulateDropdowns(vm);
            return View(vm);
        }

        [HttpPost]
        [Route("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, ClassSubjectViewModel model)
        {
            if (id != model.Id) return BadRequest();

            if (!ModelState.IsValid)
            {
                PopulateDropdowns(model);
                return View(model);
            }

            var entity = new ClassSubjectDetail
            {
                Id = id,
                ClassMasterId = model.ClassMasterId,
                SubjectId = model.SubjectId,
                IsActive = model.IsActive
            };

            var result = _classSubjectService.Update(entity);
            if (!result)
            {
                ModelState.AddModelError(string.Empty, "Failed to update class-subject mapping.");
                PopulateDropdowns(model);
                return View(model);
            }

            return RedirectToAction("Details", new { id });
        }

        [HttpGet]
        [Route("Delete/{id}")]
        public IActionResult Delete(Guid id)
        {
            var item = _classSubjectService.GetById(id);
            if (item == null) return NotFound();

            var vm = new ClassSubjectViewModel
            {
                Id = item.Id,
                ClassName = item.ClassMaster?.Name,
                SubjectName = item.Subject?.SubjectName,
                IsActive = item.IsActive
            };

            return View(vm);
        }

        [HttpPost]
        [Route("Delete/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var result = _classSubjectService.Delete(id);
            if (!result) return NotFound();
            
            return RedirectToAction("Index");
        }
    }
}