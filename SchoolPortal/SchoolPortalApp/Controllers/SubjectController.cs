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
	[Route("Subject")]
	public class SubjectController : BaseController
	{
		private readonly ISubjectService _service;
		private readonly ISchoolService _schoolService;
		private readonly IClassService _classService;
		private readonly ILogger<SubjectController> _logger;

		public SubjectController(ISubjectService service, ISchoolService schoolService, IClassService classService, ILogger<SubjectController> logger)
		{
			_service = service;
			_schoolService = schoolService;
			_classService = classService;
			_logger = logger;
		}

		private void PopulateDropdowns(SubjectViewModel vm)
		{
			var schools = _schoolService.GetAll();
			vm.Schools = schools.Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Name, Selected = s.Id == vm.SchoolId }).ToList();

			var classes = _classService.GetAll();
			vm.Classes = classes
				.Where(c => c != null && !string.IsNullOrEmpty(c.Name))
				.Select(c => new SelectListItem
				{
					Value = c.Id.ToString(),
					Text = c.Name,
					Selected = c.Id == vm.ClassId
				}).ToList();
		}

		[HttpGet]
		[Route("")]
		[Route("Index")]
		public IActionResult Index(Guid? classId)
		{
			var list = _service.GetAll();
			var schools = _schoolService.GetAll();

			// Apply class filter if a specific class is selected
			if (classId.HasValue && classId.Value != Guid.Empty)
			{
				list = list.Where(x => x.ClassId == classId.Value).ToList();
			}

			// Prepare class dropdown data
			var classes = _classService.GetAll();
			var classItems = classes
				.Where(c => c != null && !string.IsNullOrEmpty(c.Name))
				.Select(c => new SelectListItem
				{
					Value = c.Id.ToString(),
					Text = c.Name,
					Selected = classId.HasValue && c.Id == classId.Value
				})
				.ToList();

			ViewBag.Classes = classItems;
			ViewBag.SelectedClassId = classId;

			var result = list.Select(item =>
			{
				var school = schools.FirstOrDefault(s => s.Id == item.SchoolId);
				var className = _classService.ClassNameById(item.ClassId);
				if (string.IsNullOrWhiteSpace(className))
				{
					className = "-";
				}

				return new SubjectListItemViewModel
				{
					Id = item.Id,
					SubjectName = item.SubjectName,
					ClassId = item.ClassId,
					ClassName = className,
					IsScholastic = item.IsScholastic ?? false,
					IsActive = item.IsActive,
					SchoolName = school?.Name ?? string.Empty
				};
			}).ToList();

			return View(result);
		}

		[HttpGet]
		[Route("FilterByClass")]
		public IActionResult FilterByClass(Guid? classId)
		{
			var list = _service.GetAll();
			var schools = _schoolService.GetAll();

			if (classId.HasValue && classId.Value != Guid.Empty)
			{
				list = list.Where(x => x.ClassId == classId.Value).ToList();
			}

			var result = list.Select(item =>
			{
				var school = schools.FirstOrDefault(s => s.Id == item.SchoolId);
				var className = _classService.ClassNameById(item.ClassId);
				if (string.IsNullOrWhiteSpace(className))
				{
					className = "-";
				}

				return new SubjectListItemViewModel
				{
					Id = item.Id,
					SubjectName = item.SubjectName,
					ClassId = item.ClassId,
					ClassName = className,
					IsScholastic = item.IsScholastic ?? false,
					IsActive = item.IsActive,
					SchoolName = school?.Name ?? string.Empty
				};
			}).ToList();

			return PartialView("_SubjectTable", result);
		}

		[HttpGet]
		[Route("Details/{id}")]
		public IActionResult Details(Guid id)
		{
			var item = _service.GetById(id);
			if (item == null) return NotFound();
			var className = _classService.ClassNameById(item.ClassId);
			ViewBag.ClassName = className;
			return View(item);
		}

		[HttpGet]
		[Route("Create")]
		public IActionResult Create()
		{
			var vm = new SubjectViewModel();
			PopulateDropdowns(vm);
			return View(vm);
		}

		[HttpPost]
		[Route("Create")]
		[ValidateAntiForgeryToken]
		public IActionResult Create(SubjectViewModel model)
		{
			if (!ModelState.IsValid)
			{
				PopulateDropdowns(model);
				return View(model);
			}

			var userId = CurrentUserId;
			var companyId = CurrentCompanyId;
			var schoolId = CurrentSchoolId;
			if (!companyId.HasValue || !schoolId.HasValue || !userId.HasValue)
			{
				ModelState.AddModelError(string.Empty, "Missing required session data.");
				PopulateDropdowns(model);
				return View(model);
			}

			var entity = new SubjectMaster
			{
				Id = Guid.Empty,
				SubjectName = model.SubjectName,
				IsScholastic = model.IsScholastic,
				IsActive = model.IsActive,
				ClassId = model.ClassId,
				CompanyId = companyId.Value,
				SchoolId = schoolId.Value,
				CreatedBy = userId.Value,
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
				IsScholastic = item.IsScholastic ?? false,
				IsActive = item.IsActive,
				ClassId = item.ClassId,
				SchoolId = item.SchoolId
			};
			PopulateDropdowns(vm);
			return View(vm);
		}

		[HttpPost]
		[Route("Edit/{id}")]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Guid id, SubjectViewModel model)
		{
			if (id != model.Id) return BadRequest();

			var schoolId = CurrentSchoolId;
			if (schoolId.HasValue)
			{
				ModelState.Remove(nameof(SubjectViewModel.SchoolId));
				model.SchoolId = schoolId.Value;
			}

			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var userId = CurrentUserId;
			if (!userId.HasValue || model.SchoolId == Guid.Empty)
			{
				ModelState.AddModelError(string.Empty, "Please login to update subject.");
				return View(model);
			}

			var entity = new SubjectMaster
			{
				Id = id,
				SubjectName = model.SubjectName,
				IsScholastic = model.IsScholastic,
				IsActive = model.IsActive,
				ClassId = model.ClassId,
				SchoolId = model.SchoolId,
				ModifiedBy = userId.Value,
				ModifiedDate = DateTime.UtcNow
			};

			if (!_service.Update(entity))
			{
				ModelState.AddModelError(string.Empty, "Failed to update subject.");
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
				TempData["ErrorMessage"] = "Failed to delete subject.";
				return RedirectToAction("Delete", new { id });
			}
			return RedirectToAction("Index");
		}
	}
}
