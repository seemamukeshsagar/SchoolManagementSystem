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
	[Route("TeacherSectionDetails")]
	public class TeacherSectionDetailsController : BaseController
	{
		private readonly ITeacherSectionDetailsService _service;
		private readonly ITeacherService _teacherService;
		private readonly IClassService _classService;
		private readonly ISectionService _sectionService;
		private readonly ISubjectService _subjectService;
		private new readonly ILogger<TeacherSectionDetailsController> _logger;

		public TeacherSectionDetailsController(
			ITeacherSectionDetailsService service,
			ITeacherService teacherService,
			IClassService classService,
			ISectionService sectionService,
			ISubjectService subjectService,
			ILogger<TeacherSectionDetailsController> logger)
		{
			_service = service;
			_teacherService = teacherService;
			_classService = classService;
			_sectionService = sectionService;
			_subjectService = subjectService;
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		private void PopulateDropdowns(TeacherSectionDetailsViewModel vm)
		{
			_logger.LogInformation("Populating dropdowns...");
			
			// Get current school ID and filter teachers by it
			var schoolId = CurrentSchoolId;
			var teachers = schoolId.HasValue 
				? _teacherService.GetAll(schoolId.Value)
				: new List<TeacherMaster>();
				
			_logger.LogInformation($"Found {teachers.Count()} teachers for school: {schoolId}");
			
			var classes = _classService.GetAll();
			_logger.LogInformation($"Found {classes.Count()} classes");
			
			var sections = _sectionService.GetAll();
			_logger.LogInformation($"Found {sections.Count()} sections");
			
			var subjects = _subjectService.GetAll();
			_logger.LogInformation($"Found {subjects.Count()} subjects");

			var teacherList = teachers.ToList();
			_logger.LogInformation($"Creating SelectList for {teacherList.Count} teachers");
			
			vm.Teachers = teacherList
				.Select(t => {
					var text = string.Join(" ", new[] { t.FirstName, t.LastName }.Where(x => !string.IsNullOrWhiteSpace(x)));
					_logger.LogInformation($"Teacher ID: {t.Id}, Name: {text}");
					return new SelectListItem
					{
						Value = t.Id.ToString(),
						Text = text,
						Selected = t.Id == vm.TeacherId
					};
				}).ToList();

			vm.Classes = classes
				.Select(c => new SelectListItem
				{
					Value = c.Id.ToString(),
					Text = c.Name,
					Selected = c.Id == vm.ClassId
				}).ToList();

			vm.Sections = sections
				.Select(s => new SelectListItem
				{
					Value = s.Id.ToString(),
					Text = s.Name,
					Selected = s.Id == vm.SectionId
				}).ToList();

			vm.Subjects = subjects
				.Select(su => new SelectListItem
				{
					Value = su.Id.ToString(),
					Text = su.SubjectName,
					Selected = su.Id == vm.SubjectId
				}).ToList();
		}

		[HttpGet]
		[Route("")]
		[Route("Index")]
		public IActionResult Index()
		{
			var schoolId = CurrentSchoolId;
			if (!schoolId.HasValue)
			{
				return RedirectToAction("Index", "Home"); // Or handle the case when no school is selected
			}

			// Get all teacher section details and filter by school in memory
			var allItems = _service.GetAll();
			var list = allItems.Where(x => x.SchoolId == schoolId.Value).ToList();
			
			// Get all teachers for the current school
			var teachers = _teacherService.GetAll(schoolId.Value).ToDictionary(t => t.Id);
			
			// Get other reference data
			var classes = _classService.GetAll().ToDictionary(c => c.Id);
			var sections = _sectionService.GetAll().ToDictionary(s => s.Id);
			var subjects = _subjectService.GetAll().ToDictionary(s => s.Id);

			var result = list.Select(item => new TeacherSectionDetailsListItemViewModel
			{
				Id = item.Id,
				TeacherName = teachers.TryGetValue(item.TeacherId, out var teacher)
					? string.Join(" ", new[] { teacher.FirstName, teacher.LastName }.Where(x => !string.IsNullOrWhiteSpace(x)))
					: "Teacher not found",
				ClassName = classes.TryGetValue(item.ClassId, out var @class) ? @class.Name : "Class not found",
				SectionName = sections.TryGetValue(item.SectionId, out var section) ? section.Name : "Section not found",
				SubjectName = subjects.TryGetValue(item.SubjectId, out var subject) ? subject.SubjectName : "Subject not found",
				IsClassTeacher = item.IsClassTeacher,
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
			var vm = new TeacherSectionDetailsViewModel();
			PopulateDropdowns(vm);
			return View(vm);
		}

		[HttpPost]
		[Route("Create")]
		[ValidateAntiForgeryToken]
		public IActionResult Create(TeacherSectionDetailsViewModel model)
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

			var entity = new TeacherSectionDetails
			{
				Id = Guid.Empty,
				TeacherId = model.TeacherId,
				ClassId = model.ClassId,
				SectionId = model.SectionId,
				SubjectId = model.SubjectId,
				IsClassTeacher = model.IsClassTeacher,
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

			var vm = new TeacherSectionDetailsViewModel
			{
				Id = item.Id,
				TeacherId = item.TeacherId,
				ClassId = item.ClassId,
				SectionId = item.SectionId,
				SubjectId = item.SubjectId,
				IsClassTeacher = item.IsClassTeacher,
				IsActive = item.IsActive,
				SchoolId = item.SchoolId
			};

			PopulateDropdowns(vm);
			return View(vm);
		}

		[HttpPost]
		[Route("Edit/{id}")]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Guid id, TeacherSectionDetailsViewModel model)
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

			var entity = new TeacherSectionDetails
			{
				Id = id,
				TeacherId = model.TeacherId,
				ClassId = model.ClassId,
				SectionId = model.SectionId,
				SubjectId = model.SubjectId,
				IsClassTeacher = model.IsClassTeacher,
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
        public IActionResult GetSubjectsByClass(Guid classId)
        {
            try
            {
                _logger.LogInformation($"Getting subjects for class ID: {classId}");
                
                if (classId == Guid.Empty)
                {
                    _logger.LogWarning("Empty class ID provided");
                    return Json(new List<SelectListItem>());
                }

                var subjects = _subjectService.GetByClassId(classId);
                _logger.LogInformation($"Found {subjects.Count()} subjects for class ID: {classId}");
                
                var subjectList = subjects.Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.SubjectName
                }).ToList();

                return Json(subjectList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting subjects for class ID: {classId}");
                return StatusCode(500, new { error = "An error occurred while loading subjects." });
            }
        }
    }
}