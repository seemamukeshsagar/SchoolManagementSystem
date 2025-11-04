using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SchoolPortal.Entities.Models;
using SchoolPortal.Services.IServices;

namespace SchoolPortalApp.Controllers
{
	[Route("HolidayTypeMaster")]
	public class HolidayTypeMasterController : Controller
	{
		private readonly IHolidayTypeMasterService _service;
		private readonly ILogger<HolidayTypeMasterController> _logger;

		public HolidayTypeMasterController(IHolidayTypeMasterService service, ILogger<HolidayTypeMasterController> logger)
		{
			_service = service;
			_logger = logger;
		}

		[HttpGet]
		[Route("")]
		[Route("Index")]
		public IActionResult Index()
		{
			var list = _service.GetAll().Where(x => !x.IsDeleted).OrderBy(x => x.HolidayTypeName).ToList();
			return View(list);
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
			return View(new HolidayTypeMaster());
		}

		[HttpPost]
		[Route("Create")]
		[ValidateAntiForgeryToken]
		public IActionResult Create(HolidayTypeMaster model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var userIdStr = HttpContext.Session.GetString("UserId");
			var companyIdStr = HttpContext.Session.GetString("CompanyId");
			var schoolIdStr = HttpContext.Session.GetString("SchoolId");

			if (string.IsNullOrEmpty(companyIdStr) || string.IsNullOrEmpty(schoolIdStr) || string.IsNullOrEmpty(userIdStr))
			{
				ModelState.AddModelError(string.Empty, "Missing required session data.");
				return View(model);
			}

			if (!Guid.TryParse(companyIdStr, out var companyId) ||
				!Guid.TryParse(schoolIdStr, out var schoolId) ||
				!Guid.TryParse(userIdStr, out var userId))
			{
				ModelState.AddModelError(string.Empty, "Invalid session data format.");
				return View(model);
			}

			var entity = new HolidayTypeMaster
			{
				Id = Guid.Empty,
				HolidayTypeName = model.HolidayTypeName,
				HolidayTypeDescription = model.HolidayTypeDescription,
				CompanyId = companyId,
				SchoolId = schoolId,
				IsActive = model.IsActive,
				IsDeleted = false,
				CreatedBy = userId,
				CreatedDate = DateTime.UtcNow,
				Status = "INC",
				StatusMessage = "In Process...."
			};

			var newId = _service.Create(entity);
			if (newId == Guid.Empty)
			{
				ModelState.AddModelError(string.Empty, "Failed to create holiday type.");
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
			return View(item);
		}

		[HttpPost]
		[Route("Edit/{id}")]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Guid id, HolidayTypeMaster model)
		{
			if (id != model.Id) return BadRequest();
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var userIdStr = HttpContext.Session.GetString("UserId");
			if (string.IsNullOrWhiteSpace(userIdStr) || !Guid.TryParse(userIdStr, out var userId))
			{
				ModelState.AddModelError(string.Empty, "Please login to update holiday type.");
				return View(model);
			}

			var entity = new HolidayTypeMaster
			{
				Id = id,
				HolidayTypeName = model.HolidayTypeName,
				HolidayTypeDescription = model.HolidayTypeDescription,
				IsActive = model.IsActive,
				ModifiedBy = userId,
				ModifiedDate = DateTime.UtcNow
			};

			if (!_service.Update(entity))
			{
				ModelState.AddModelError(string.Empty, "Failed to update holiday type.");
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
				TempData["ErrorMessage"] = "Failed to delete holiday type.";
				return RedirectToAction("Delete", new { id });
			}
			return RedirectToAction("Index");
		}
	}
}
