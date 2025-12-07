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
	public class TeacherClassDetailsController : BaseController
	{
		private readonly ITeacherClassDetailsService _service;
		private readonly ITeacherService _teacherService;
		private readonly IClassService _classService;
		private readonly ISectionService _sectionService;
		private readonly ISubjectService _subjectService;
		private readonly ISchoolService _schoolService;
		private new readonly ILogger<TeacherClassDetailsController> _logger;

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
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		private void PopulateDropdowns(TeacherClassDetailsViewModel vm)
		{
			if (!CurrentSchoolId.HasValue || CurrentSchoolId == Guid.Empty)
				throw new InvalidOperationException("School ID is required");

			var schoolId = CurrentSchoolId.Value; // Get the non-nullable Guid

			var teachers = _teacherService.GetAll(schoolId);
			var classes = _classService.GetAll(schoolId);
			var sections = _sectionService.GetAll(schoolId);
			var subjects = _subjectService.GetAll(schoolId);

			vm.Teachers = teachers.Select(t => new SelectListItem { 
				Value = t.Id.ToString(), 
				Text = string.Join(" ", new[] { t.FirstName, t.LastName }.Where(x => !string.IsNullOrWhiteSpace(x))), 
				Selected = t.Id == (vm.TeacherId)
			}).ToList();

			vm.Classes = classes.Select(c => new SelectListItem { 
				Value = c.Id.ToString(), 
				Text = c.Name, 
				Selected = c.Id == (vm.ClassId)
			}).ToList();

			vm.Sections = sections.Select(s => new SelectListItem { 
				Value = s.Id.ToString(), 
				Text = s.Name, 
				Selected = s.Id == (vm.SectionId)
			}).ToList();

			vm.Subjects = subjects.Select(su => new SelectListItem { 
				Value = su.Id.ToString(), 
				Text = su.SubjectName, 
				Selected = su.Id == (vm.SubjectId)
			}).ToList();
		}

		[HttpGet]
		[Route("")]
		[Route("Index")]
		public IActionResult Index()
		{
			if (CurrentSchoolId == Guid.Empty)
				throw new InvalidOperationException("School ID is required");

			var list = _service.GetAll();
			var teachers = _teacherService.GetAll(CurrentSchoolId);
			var classes = _classService.GetAll(CurrentSchoolId);
			var sections = _sectionService.GetAll(CurrentSchoolId!);
			var subjects = _subjectService.GetAll(CurrentSchoolId!);

			var result = list.Select(item => new TeacherClassDetailsListItemViewModel
			{
				Id = item.Id,
				TeacherName = teachers.FirstOrDefault(t => t.Id == item.TeacherId) is { } t 
					? string.Join(" ", new[] { t.FirstName, t.LastName }.Where(x => !string.IsNullOrWhiteSpace(x))) 
					: string.Empty,
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

			// Since all these properties are non-nullable Guids, we can assign them directly
			// Model validation ensures required fields are provided
			var entity = new TeacherClassDetails
			{
				Id = Guid.Empty,
				TeacherId = model.TeacherId,
				ClassId = model.ClassId,
				SectionId = model.SectionId,
				SubjectId = model.SubjectId,
				IsActive = model.IsActive,
				CompanyId = companyId ?? throw new InvalidOperationException("Company ID is required"),
				SchoolId = model.SchoolId,
				CreatedBy = userId ?? throw new InvalidOperationException("User ID is required"),
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

			var entity = new TeacherClassDetails
			{
				Id = id,
				TeacherId = model.TeacherId,
				ClassId = model.ClassId,
				SectionId = model.SectionId,
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
			try
			{
				var item = _service.GetById(id);
				if (item == null)
				{
					_logger.LogWarning("Delete: Teacher class details with ID {Id} not found", id);
					return NotFound();
				}
				return View(item);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error in Delete action for ID {Id}", id);
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}

		[HttpPost]
		[Route("Delete/{id}")]
		[ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult ConfirmDelete(Guid id)
		{
			try
			{
				if (!_service.Delete(id))
				{
					_logger.LogWarning("Failed to delete teacher class details with ID {Id}", id);
					TempData["ErrorMessage"] = "Failed to delete the record. Please try again.";
					return RedirectToAction("Delete", new { id });
				}
				_logger.LogInformation("Successfully deleted teacher class details with ID {Id}", id);
				return RedirectToAction("Index");
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error in ConfirmDelete for ID {Id}", id);
				TempData["ErrorMessage"] = "An error occurred while deleting the record.";
				return RedirectToAction("Delete", new { id });
			}
		}
	}
}
