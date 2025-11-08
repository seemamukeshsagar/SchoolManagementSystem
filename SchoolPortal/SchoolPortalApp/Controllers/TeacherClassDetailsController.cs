using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolPortal.Entities.Models;
using SchoolPortalApp.Models;
using SchoolPortal.Services.IServices;

namespace SchoolPortalApp.Controllers
{
	[Route("TeacherClassDetails")]
	public class TeacherClassDetailsController : Controller
	{
		private readonly ITeacherClassDetailsService _service;
		private readonly ITeacherService _teacherService;
		private readonly IClassService _classService;
		private readonly ISectionService _sectionService;
		private readonly ISubjectService _subjectService;
		private readonly ISchoolService _schoolService;
		private readonly ILogger<TeacherClassDetailsController> _logger;

		public TeacherClassDetailsController(
			ITeacherClassDetailsService service,
			ITeacherService teacherService,
			IClassService classService,
			ISectionService sectionService,
			ISubjectService subjectService,
			ISchoolService schoolService,
			ILogger<TeacherClassDetailsController> logger)
		{
			_service = service;
			_teacherService = teacherService;
			_classService = classService;
			_sectionService = sectionService;
			_subjectService = subjectService;
			_schoolService = schoolService;
			_logger = logger;
		}

		private void PopulateDropdowns(TeacherClassDetailsViewModel vm)
		{
			var teachers = _teacherService.GetAll();
			var classes = _classService.GetAll();
			var sections = _sectionService.GetAll();
			var subjects = _subjectService.GetAll();

			vm.Teachers = teachers.Select(t => new SelectListItem { Value = t.Id.ToString(), Text = string.Join(" ", new[] { t.FirstName, t.LastName }.Where(x => !string.IsNullOrWhiteSpace(x))), Selected = t.Id == vm.TeacherId }).ToList();
			vm.Classes = classes.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name, Selected = c.Id == vm.ClassId }).ToList();
			vm.Sections = sections.Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Name, Selected = s.Id == vm.SectionId }).ToList();
			vm.Subjects = subjects.Select(su => new SelectListItem { Value = su.Id.ToString(), Text = su.SubjectName, Selected = su.Id == vm.SubjectId }).ToList();
		}

		[HttpGet]
		[Route("")]
		[Route("Index")]
		public IActionResult Index()
		{
			var list = _service.GetAll();

			var teachers = _teacherService.GetAll();
			var classes = _classService.GetAll();
			var sections = _sectionService.GetAll();
			var subjects = _subjectService.GetAll();

			var result = list.Select(item => new TeacherClassDetailsListItemViewModel
			{
				Id = item.Id,
				TeacherName = teachers.FirstOrDefault(t => t.Id == item.TeacherId) is { } t ? string.Join(" ", new[] { t.FirstName, t.LastName }.Where(x => !string.IsNullOrWhiteSpace(x))) : string.Empty,
				ClassName = classes.FirstOrDefault(c => c.Id == item.ClassId)?.Name ?? string.Empty,
				SectionName = sections.FirstOrDefault(s => s.Id == item.SectionId)?.Name ?? string.Empty,
				SubjectName = subjects.FirstOrDefault(su => su.Id == item.SubjectId)?.SubjectName ?? string.Empty,
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
			var vm = new TeacherClassDetailsViewModel();
			PopulateDropdowns(vm);
			return View(vm);
		}

		[HttpPost]
		[Route("Create")]
		[ValidateAntiForgeryToken]
		public IActionResult Create(TeacherClassDetailsViewModel model)
		{
			var schoolIdStr = HttpContext.Session.GetString("SchoolId");
			if (!string.IsNullOrWhiteSpace(schoolIdStr) && Guid.TryParse(schoolIdStr, out var schoolId))
			{
				model.SchoolId = schoolId;
			}

			if (!ModelState.IsValid)
			{
				PopulateDropdowns(model);
				return View(model);
			}

			var userIdStr = HttpContext.Session.GetString("UserId");
			var companyIdStr = HttpContext.Session.GetString("CompanyId");
			if (string.IsNullOrWhiteSpace(userIdStr) || !Guid.TryParse(userIdStr, out var userId) || string.IsNullOrWhiteSpace(companyIdStr) || !Guid.TryParse(companyIdStr, out var companyId) || model.SchoolId == Guid.Empty)
			{
				ModelState.AddModelError(string.Empty, "Please login and select company to create entry.");
				PopulateDropdowns(model);
				return View(model);
			}

			var entity = new TeacherClassDetails
			{
				Id = Guid.Empty,
				TeacherId = model.TeacherId,
				ClassId = model.ClassId,
				SectionId = model.SectionId,
				SubjectId = model.SubjectId,
				IsActive = model.IsActive,
				CompanyId = companyId,
				SchoolId = model.SchoolId,
				CreatedBy = userId,
				CreatedDate = DateTime.UtcNow
			};

			var newId = _service.Create(entity);
			if (newId == Guid.Empty)
			{
				ModelState.AddModelError(string.Empty, "Failed to create.");
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
			var vm = new TeacherClassDetailsViewModel
			{
				Id = item.Id,
				TeacherId = item.TeacherId,
				ClassId = item.ClassId,
				SectionId = item.SectionId,
				SubjectId = item.SubjectId,
				IsActive = item.IsActive,
				SchoolId = item.SchoolId
			};
			PopulateDropdowns(vm);
			return View(vm);
		}

		[HttpPost]
		[Route("Edit/{id}")]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Guid id, TeacherClassDetailsViewModel model)
		{
			if (id != model.Id) return BadRequest();

			var schoolIdStr = HttpContext.Session.GetString("SchoolId");
			if (!string.IsNullOrWhiteSpace(schoolIdStr) && Guid.TryParse(schoolIdStr, out var schoolIdFromSession))
			{
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
				ModelState.AddModelError(string.Empty, "Please login to update.");
				PopulateDropdowns(model);
				return View(model);
			}

			var entity = new TeacherClassDetails
			{
				Id = id,
				TeacherId = model.TeacherId,
				ClassId = model.ClassId,
				SectionId = model.SectionId,
				SubjectId = model.SubjectId,
				IsActive = model.IsActive,
				SchoolId = model.SchoolId,
				ModifiedBy = userId,
				ModifiedDate = DateTime.UtcNow
			};

			if (!_service.Update(entity))
			{
				ModelState.AddModelError(string.Empty, "Failed to update.");
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
				TempData["ErrorMessage"] = "Failed to delete.";
				return RedirectToAction("Delete", new { id });
			}
			return RedirectToAction("Index");
		}
	}
}
