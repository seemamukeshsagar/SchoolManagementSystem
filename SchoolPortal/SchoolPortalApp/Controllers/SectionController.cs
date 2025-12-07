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
	[Route("Section")]
	public class SectionController : BaseController
	{
		private readonly ISectionService _service;
		private readonly ISchoolService _schoolService;
		private new readonly ILogger<SectionController> _logger;

		public SectionController(ISectionService service, ISchoolService schoolService, ILogger<SectionController> logger)
		{
			_service = service;
			_schoolService = schoolService;
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		private void PopulateDropdowns(SectionViewModel vm)
		{
			var schools = _schoolService.GetAll();
			vm.Schools = schools.Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Name, Selected = s.Id == vm.SchoolId }).ToList();
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
				return new SectionListItemViewModel
				{
					Id = item.Id,
					Name = item.Name,
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
			var vm = new SectionViewModel();
			return View(vm);
		}

		[HttpPost]
		[Route("Create")]
		[ValidateAntiForgeryToken]
		public IActionResult Create(SectionViewModel model)
		{
			var schoolId = CurrentSchoolId;
			if (schoolId.HasValue)
			{
				ModelState.Remove(nameof(SectionViewModel.SchoolId));
				model.SchoolId = schoolId.Value;
			}

			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var userId = CurrentUserId;
			var companyId = CurrentCompanyId;
			if (!userId.HasValue || !companyId.HasValue || model.SchoolId == Guid.Empty)
			{
				ModelState.AddModelError(string.Empty, "Please login and select company to create section.");
				return View(model);
			}

			var entity = new SectionMaster
			{
				Id = Guid.Empty,
				Name = model.Name,
				IsActive = model.IsActive,
				CompanyId = companyId.Value,
				SchoolId = model.SchoolId,
				CreatedBy = userId.Value,
				CreatedDate = DateTime.UtcNow
			};

			var newId = _service.Create(entity);
			if (newId == Guid.Empty)
			{
				ModelState.AddModelError(string.Empty, "Failed to create section.");
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
			var vm = new SectionViewModel
			{
				Id = item.Id,
				Name = item.Name,
				IsActive = item.IsActive,
				SchoolId = item.SchoolId
			};
			return View(vm);
		}

		[HttpPost]
		[Route("Edit/{id}")]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Guid id, SectionViewModel model)
		{
			if (id != model.Id) return BadRequest();

			var schoolId = CurrentSchoolId;
			if (schoolId.HasValue)
			{
				ModelState.Remove(nameof(SectionViewModel.SchoolId));
				model.SchoolId = schoolId.Value;
			}

			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var userId = CurrentUserId;
			if (!userId.HasValue || model.SchoolId == Guid.Empty)
			{
				ModelState.AddModelError(string.Empty, "Please login to update section.");
				return View(model);
			}

			var entity = new SectionMaster
			{
				Id = id,
				Name = model.Name,
				IsActive = model.IsActive,
				SchoolId = model.SchoolId,
				ModifiedBy = userId,
				ModifiedDate = DateTime.UtcNow
			};

			if (!_service.Update(entity))
			{
				ModelState.AddModelError(string.Empty, "Failed to update section.");
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
				TempData["ErrorMessage"] = "Failed to delete section.";
				return RedirectToAction("Delete", new { id });
			}
			return RedirectToAction("Index");
		}
	}
}
