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
	[Route("SubjectCategory")]
	public class SubjectCategoryController : Controller
	{
		private readonly ISubjectCategoryService _service;
		private readonly ISubjectService _subjectService;
		private readonly ISchoolService _schoolService;
		private readonly ILogger<SubjectCategoryController> _logger;

		public SubjectCategoryController(
			ISubjectCategoryService service,
			ISubjectService subjectService,
			ISchoolService schoolService,
			ILogger<SubjectCategoryController> logger)
		{
			_service = service;
			_subjectService = subjectService;
			_schoolService = schoolService;
			_logger = logger;
		}

		private void PopulateDropdowns(SubjectCategoryViewModel vm)
		{
			var subjects = _subjectService.GetAll();
			vm.Subjects = subjects
				.Select(s => new SelectListItem
				{
					Value = s.Id.ToString(),
					Text = s.SubjectName,
					Selected = s.Id == vm.SubjectId
				}).ToList();

			var categories = _service.GetAll();
			vm.Parents = categories
				.Where(c => c.Id != vm.Id)
				.Select(c => new SelectListItem
				{
					Value = c.Id.ToString(),
					Text = c.Name,
					Selected = vm.ParentId.HasValue && c.Id == vm.ParentId.Value
				}).Prepend(new SelectListItem
				{
					Value = string.Empty,
					Text = "-- None --",
					Selected = !vm.ParentId.HasValue
				})
				.ToList();
		}

		[HttpGet]
		[Route("")]
		[Route("Index")]
		public IActionResult Index()
		{
			var list = _service.GetAll();
			var subjects = _subjectService.GetAll();
			var schools = _schoolService.GetAll();
			var result = list.Select(item =>
			{
				var subject = subjects.FirstOrDefault(s => s.Id == item.SubjectId);
				var school = schools.FirstOrDefault(s => s.Id == item.SchoolId);
				return new SubjectCategoryListItemViewModel
				{
					Id = item.Id,
					Name = item.Name,
					SubjectName = subject?.SubjectName ?? string.Empty,
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
			var vm = new SubjectCategoryViewModel();
			PopulateDropdowns(vm);
			return View(vm);
		}

		[HttpPost]
		[Route("Create")]
		[ValidateAntiForgeryToken]
		public IActionResult Create(SubjectCategoryViewModel model)
		{
			var schoolIdStr = HttpContext.Session.GetString("SchoolId");
			if (!string.IsNullOrWhiteSpace(schoolIdStr) && Guid.TryParse(schoolIdStr, out var schoolId))
			{
				ModelState.Remove(nameof(SubjectCategoryViewModel.SchoolId));
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
				ModelState.AddModelError(string.Empty, "Please login and select company to create subject category.");
				PopulateDropdowns(model);
				return View(model);
			}

			var entity = new SubjectCategoryDetails
			{
				Id = Guid.Empty,
				Name = model.Name,
				Description = model.Description ?? string.Empty,
				ParentId = model.ParentId ?? Guid.Empty,
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
				ModelState.AddModelError(string.Empty, "Failed to create subject category.");
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
			var vm = new SubjectCategoryViewModel
			{
				Id = item.Id,
				Name = item.Name,
				Description = item.Description,
				ParentId = item.ParentId == Guid.Empty ? null : item.ParentId,
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
		public IActionResult Edit(Guid id, SubjectCategoryViewModel model)
		{
			if (id != model.Id) return BadRequest();

			var schoolIdStr = HttpContext.Session.GetString("SchoolId");
			if (!string.IsNullOrWhiteSpace(schoolIdStr) && Guid.TryParse(schoolIdStr, out var schoolIdFromSession))
			{
				ModelState.Remove(nameof(SubjectCategoryViewModel.SchoolId));
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
				ModelState.AddModelError(string.Empty, "Please login to update subject category.");
				PopulateDropdowns(model);
				return View(model);
			}

			var entity = new SubjectCategoryDetails
			{
				Id = id,
				Name = model.Name,
				Description = model.Description ?? string.Empty,
				ParentId = model.ParentId ?? Guid.Empty,
				SubjectId = model.SubjectId,
				IsActive = model.IsActive,
				SchoolId = model.SchoolId,
				ModifiedBy = userId,
				ModifiedDate = DateTime.UtcNow
			};

			if (!_service.Update(entity))
			{
				ModelState.AddModelError(string.Empty, "Failed to update subject category.");
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
				TempData["ErrorMessage"] = "Failed to delete subject category.";
				return RedirectToAction("Delete", new { id });
			}
			return RedirectToAction("Index");
		}
	}
}
