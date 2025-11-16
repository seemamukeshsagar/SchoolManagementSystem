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
	[Route("Assessment")]
	public class AssessmentController : Controller
	{
		private readonly IAssesmentMasterService _service;
		private readonly ILogger<AssessmentController> _logger;

		public AssessmentController(IAssesmentMasterService service, ILogger<AssessmentController> logger)
		{
			_service = service;
			_logger = logger;
		}

		[HttpGet]
		[Route("")]
		[Route("Index")]
		public IActionResult Index()
		{
			var list = _service.GetAll();
			var result = list.Select(item => new AssessmentListItemViewModel
			{
				Id = item.Id,
				Name = item.Name ?? string.Empty,
				PercentageWeightage = item.PercentageWeightage,
				FromPeriod = item.FromPeriod,
				ToPeriod = item.ToPeriod,
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

			var vm = new AssessmentViewModel
			{
				Id = item.Id,
				Name = item.Name ?? string.Empty,
				Description = item.Description,
				PercentageWeightage = item.PercentageWeightage,
				FromPeriod = item.FromPeriod,
				ToPeriod = item.ToPeriod,
				IsActive = item.IsActive
			};
			return View(vm);
		}

		[HttpGet]
		[Route("Create")]
		public IActionResult Create()
		{
			var vm = new AssessmentViewModel();
			return View(vm);
		}

		[HttpPost]
		[Route("Create")]
		[ValidateAntiForgeryToken]
		public IActionResult Create(AssessmentViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var userIdStr = HttpContext.Session.GetString("UserId");
			var companyIdStr = HttpContext.Session.GetString("CompanyId");
			var schoolIdStr = HttpContext.Session.GetString("SchoolId");

			if (string.IsNullOrWhiteSpace(userIdStr) || !Guid.TryParse(userIdStr, out var userId) ||
				string.IsNullOrWhiteSpace(companyIdStr) || !Guid.TryParse(companyIdStr, out var companyId) ||
				string.IsNullOrWhiteSpace(schoolIdStr) || !Guid.TryParse(schoolIdStr, out var schoolId))
			{
				ModelState.AddModelError(string.Empty, "Missing required session data.");
				return View(model);
			}

			var entity = new AssesmentMaster
			{
				Id = Guid.Empty,
				Name = model.Name,
				Description = model.Description ?? string.Empty,
				PercentageWeightage = model.PercentageWeightage ?? 0m,
				FromPeriod = model.FromPeriod,
				ToPeriod = model.ToPeriod,
				CompanyId = companyId,
				SchoolId = schoolId,
				IsActive = model.IsActive,
				CreatedBy = userId,
				CreatedDate = DateTime.UtcNow
			};

			var newId = _service.Create(entity);
			if (newId == Guid.Empty)
			{
				ModelState.AddModelError(string.Empty, "Failed to create assessment.");
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

			var vm = new AssessmentViewModel
			{
				Id = item.Id,
				Name = item.Name ?? string.Empty,
				Description = item.Description,
				PercentageWeightage = item.PercentageWeightage,
				FromPeriod = item.FromPeriod,
				ToPeriod = item.ToPeriod,
				IsActive = item.IsActive
			};
			return View(vm);
		}

		[HttpPost]
		[Route("Edit/{id}")]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Guid id, AssessmentViewModel model)
		{
			if (id != model.Id) return BadRequest();
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var userIdStr = HttpContext.Session.GetString("UserId");
			var schoolIdStr = HttpContext.Session.GetString("SchoolId");
			if (string.IsNullOrWhiteSpace(userIdStr) || !Guid.TryParse(userIdStr, out var userId) ||
				string.IsNullOrWhiteSpace(schoolIdStr) || !Guid.TryParse(schoolIdStr, out var schoolId))
			{
				ModelState.AddModelError(string.Empty, "Missing required session data.");
				return View(model);
			}

			var entity = new AssesmentMaster
			{
				Id = id,
				Name = model.Name,
				Description = model.Description ?? string.Empty,
				PercentageWeightage = model.PercentageWeightage ?? 0m,
				FromPeriod = model.FromPeriod,
				ToPeriod = model.ToPeriod,
				SchoolId = schoolId,
				IsActive = model.IsActive,
				ModifiedBy = userId,
				ModifiedDate = DateTime.UtcNow
			};

			if (!_service.Update(entity))
			{
				ModelState.AddModelError(string.Empty, "Failed to update assessment.");
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

			var vm = new AssessmentViewModel
			{
				Id = item.Id,
				Name = item.Name ?? string.Empty,
				Description = item.Description,
				PercentageWeightage = item.PercentageWeightage,
				FromPeriod = item.FromPeriod,
				ToPeriod = item.ToPeriod,
				IsActive = item.IsActive
			};
			return View(vm);
		}

		[HttpPost]
		[Route("Delete/{id}")]
		[ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult ConfirmDelete(Guid id)
		{
			if (!_service.Delete(id))
			{
				ModelState.AddModelError(string.Empty, "Failed to delete assessment.");
				var item = _service.GetById(id);
				if (item == null) return NotFound();
				var vm = new AssessmentViewModel
				{
					Id = item.Id,
					Name = item.Name ?? string.Empty,
					Description = item.Description,
					PercentageWeightage = item.PercentageWeightage,
					FromPeriod = item.FromPeriod,
					ToPeriod = item.ToPeriod,
					IsActive = item.IsActive
				};
				return View(vm);
			}
			return RedirectToAction("Index");
		}
	}
}
