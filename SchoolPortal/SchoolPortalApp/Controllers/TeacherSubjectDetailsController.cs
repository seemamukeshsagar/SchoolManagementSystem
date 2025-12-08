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
	[Route("TeacherSubjectDetails")]
	public class TeacherSubjectDetailsController : BaseController
	{
		private readonly ITeacherSubjectDetailsService _service;
		private readonly ITeacherService _teacherService;
		private readonly IClassService _classService;
		private readonly ISubjectService _subjectService;
		private readonly ISchoolService _schoolService;
		private new readonly ILogger<TeacherSubjectDetailsController> _logger;

		public TeacherSubjectDetailsController(
			ITeacherSubjectDetailsService service,
			ITeacherService teacherService,
			IClassService classService,
			ISubjectService subjectService,
			ISchoolService schoolService,
			ILogger<TeacherSubjectDetailsController> logger)
		{
			_service = service;
			_teacherService = teacherService;
			_classService = classService;
			_subjectService = subjectService;
			_schoolService = schoolService;
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		private void PopulateDropdowns(TeacherSubjectDetailsViewModel vm)
		{
			try
			{
				// Get current school ID
				var schoolId = CurrentSchoolId ?? throw new InvalidOperationException("School ID is required");

				// Get active teachers for the current school
				var teachers = _teacherService.GetAll(schoolId)
					.OrderBy(t => $"{t.FirstName} {t.LastName}")
					.Select(t => new SelectListItem 
					{ 
						Value = t.Id.ToString(), 
						Text = $"{t.FirstName} {t.LastName}".Trim(),
						Selected = t.Id == vm.TeacherId 
					})
					.ToList();

				// Rest of the method remains the same
				var classes = _classService.GetAll()
					.OrderBy(c => c.Name)
					.Select(c => new SelectListItem 
					{ 
						Value = c.Id.ToString(), 
						Text = c.Name,
						Selected = c.Id == vm.ClassId 
					})
					.ToList();

				var subjects = _subjectService.GetAll()
					.OrderBy(s => s.SubjectName)
					.Select(s => new SelectListItem 
					{ 
						Value = s.Id.ToString(), 
						Text = s.SubjectName,
						Selected = s.Id == vm.SubjectId 
					})
					.ToList();

				vm.Teachers = teachers;
				vm.Classes = classes;
				vm.Subjects = subjects;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error populating dropdowns");
				// Initialize empty lists to prevent null reference exceptions
				vm.Teachers = new List<SelectListItem>();
				vm.Classes = new List<SelectListItem>();
				vm.Subjects = new List<SelectListItem>();
			}
		}

		[HttpGet]
		[Route("")]
		[Route("Index")]
		public IActionResult Index()
		{
			var list = _service.GetAll();

			var teachers = _teacherService.GetAll();
			var classes = _classService.GetAll();
			var subjects = _subjectService.GetAll();

			var result = list.Select(item => new TeacherSubjectDetailsListItemViewModel
			{
				Id = item.Id,
				TeacherName = teachers.FirstOrDefault(t => t.Id == item.TeacherId) is { } t ? string.Join(" ", new[] { t.FirstName, t.LastName }.Where(x => !string.IsNullOrWhiteSpace(x))) : string.Empty,
				ClassName = classes.FirstOrDefault(c => c.Id == item.ClassId)?.Name ?? string.Empty,
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
			var vm = new TeacherSubjectDetailsViewModel();
			PopulateDropdowns(vm);
			return View(vm);
		}

		[HttpPost]
		[Route("Create")]
		[ValidateAntiForgeryToken]
		public IActionResult Create(TeacherSubjectDetailsViewModel model)
		{
			var schoolId = CurrentSchoolId;
			if (schoolId.HasValue)
			{
				model.SchoolId = schoolId.Value;
			}

			if (!ModelState.IsValid)
			{
				PopulateDropdowns(model);
				return View(model);
			}

			var userId = CurrentUserId;
			var companyId = CurrentCompanyId;
			if (!userId.HasValue || !companyId.HasValue || model.SchoolId == Guid.Empty)
			{
				ModelState.AddModelError(string.Empty, "Please login and select company to create entry.");
				PopulateDropdowns(model);
				return View(model);
			}

			var entity = new TeacherSubjectDetails
			{
				Id = Guid.Empty,
				TeacherId = model.TeacherId,
				ClassId = model.ClassId,
				SubjectId = model.SubjectId,
				IsActive = model.IsActive,
				CompanyId = companyId.Value,
				SchoolId = model.SchoolId,
				CreatedBy = userId.Value,
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
			var vm = new TeacherSubjectDetailsViewModel
			{
				Id = item.Id,
				TeacherId = item.TeacherId,
				ClassId = item.ClassId,
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
		public IActionResult Edit(Guid id, TeacherSubjectDetailsViewModel model)
		{
			if (id != model.Id) return BadRequest();

			var schoolIdFromSession = CurrentSchoolId;
			if (schoolIdFromSession.HasValue)
			{
				model.SchoolId = schoolIdFromSession.Value;
			}

			if (!ModelState.IsValid)
			{
				PopulateDropdowns(model);
				return View(model);
			}

			var userId = CurrentUserId;
			if (!userId.HasValue || model.SchoolId == Guid.Empty)
			{
				ModelState.AddModelError(string.Empty, "Please login to update.");
				PopulateDropdowns(model);
				return View(model);
			}

			var entity = new TeacherSubjectDetails
			{
				Id = id,
				TeacherId = model.TeacherId,
				ClassId = model.ClassId,
				SubjectId = model.SubjectId,
				IsActive = model.IsActive,
				SchoolId = model.SchoolId,
				ModifiedBy = userId.Value,
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

		[HttpGet]
		[Route("GetSubjectsByClass/{classId}")]
		public IActionResult GetSubjectsByClass(Guid classId)
		{
			try
			{
				var subjects = _subjectService.GetSubjectsByClassId(classId)
					.OrderBy(s => s.SubjectName)
					.Select(s => new { 
						id = s.Id, 
						text = s.SubjectName 
					})
					.ToList();
					
				return Json(subjects);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error getting subjects for class {ClassId}", classId);
				return StatusCode(500, "Error loading subjects");
			}
		}
	}
}
