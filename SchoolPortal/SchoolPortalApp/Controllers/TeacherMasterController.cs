using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SchoolPortal.Entities.Models;
using SchoolPortal.Services.IServices;

namespace SchoolPortalApp.Controllers
{
	[Route("TeacherMaster")]
	public class TeacherMasterController : Controller
	{
		private readonly ITeacherService _service;
		private readonly ISchoolService _schoolService;
		private readonly ILogger<TeacherMasterController> _logger;

		public TeacherMasterController(ITeacherService service, ISchoolService schoolService, ILogger<TeacherMasterController> logger)
		{
			_service = service;
			_schoolService = schoolService;
			_logger = logger;
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
				return new SchoolPortalApp.Models.TeacherListItemViewModel
				{
					Id = item.Id,
					Name = string.Join(" ", new[] { item.FirstName, item.LastName }.Where(x => !string.IsNullOrWhiteSpace(x))),
					Email = item.Email ?? string.Empty,
					Phone = item.Phone ?? string.Empty,
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
			var entity = new TeacherMaster
			{
				DOB = DateTime.UtcNow.Date,
				IsActive = true,
				IsDeleted = false,
				Status = "INC",
				StatusMessage = "In Process...."
			};
			return View(entity);
		}

		[HttpPost]
		[Route("Create")]
		[ValidateAntiForgeryToken]
		public IActionResult Create(TeacherMaster model)
		{
			var schoolIdStr = HttpContext.Session.GetString("SchoolId");
			if (!string.IsNullOrWhiteSpace(schoolIdStr) && Guid.TryParse(schoolIdStr, out var schoolId))
			{
				model.SchoolId = schoolId;
			}

			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var userIdStr = HttpContext.Session.GetString("UserId");
			var companyIdStr = HttpContext.Session.GetString("CompanyId");
			if (string.IsNullOrWhiteSpace(userIdStr) || !Guid.TryParse(userIdStr, out var userId) || string.IsNullOrWhiteSpace(companyIdStr) || !Guid.TryParse(companyIdStr, out var companyId) || model.SchoolId == Guid.Empty)
			{
				ModelState.AddModelError(string.Empty, "Please login and select company to create teacher.");
				return View(model);
			}

			// Normalize optional strings to avoid nulls
			model.Id = Guid.Empty;
			model.FirstName = model.FirstName ?? string.Empty;
			model.LastName = model.LastName ?? string.Empty;
			model.Address = model.Address ?? string.Empty;
			model.ZipCode = model.ZipCode ?? string.Empty;
			model.Image = model.Image ?? string.Empty;
			model.Phone = model.Phone ?? string.Empty;
			model.MobilePhone = model.MobilePhone ?? string.Empty;
			model.YearsOfExperience = model.YearsOfExperience ?? string.Empty;
			model.PreviousSchool = model.PreviousSchool ?? string.Empty;
			model.Salutation = model.Salutation ?? string.Empty;
			model.Email = model.Email ?? string.Empty;
			model.Status = string.IsNullOrWhiteSpace(model.Status) ? "INC" : model.Status;
			model.StatusMessage = string.IsNullOrWhiteSpace(model.StatusMessage) ? "In Process...." : model.StatusMessage;
			model.CompanyId = companyId;
			model.CreatedBy = userId;
			model.CreatedDate = DateTime.UtcNow;
			model.IsDeleted = model.IsDeleted;

			var newId = _service.Create(model);
			if (newId == Guid.Empty)
			{
				ModelState.AddModelError(string.Empty, "Failed to create teacher.");
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
		public IActionResult Edit(Guid id, TeacherMaster model)
		{
			if (id != model.Id) return BadRequest();

			var schoolIdStr = HttpContext.Session.GetString("SchoolId");
			if (!string.IsNullOrWhiteSpace(schoolIdStr) && Guid.TryParse(schoolIdStr, out var schoolIdFromSession))
			{
				model.SchoolId = schoolIdFromSession;
			}

			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var userIdStr = HttpContext.Session.GetString("UserId");
			if (string.IsNullOrWhiteSpace(userIdStr) || !Guid.TryParse(userIdStr, out var userId) || model.SchoolId == Guid.Empty)
			{
				ModelState.AddModelError(string.Empty, "Please login to update teacher.");
				return View(model);
			}

			// Normalize optional strings to avoid nulls
			model.FirstName = model.FirstName ?? string.Empty;
			model.LastName = model.LastName ?? string.Empty;
			model.Address = model.Address ?? string.Empty;
			model.ZipCode = model.ZipCode ?? string.Empty;
			model.Image = model.Image ?? string.Empty;
			model.Phone = model.Phone ?? string.Empty;
			model.MobilePhone = model.MobilePhone ?? string.Empty;
			model.YearsOfExperience = model.YearsOfExperience ?? string.Empty;
			model.PreviousSchool = model.PreviousSchool ?? string.Empty;
			model.Salutation = model.Salutation ?? string.Empty;
			model.Email = model.Email ?? string.Empty;
			model.Status = string.IsNullOrWhiteSpace(model.Status) ? "INC" : model.Status;
			model.StatusMessage = string.IsNullOrWhiteSpace(model.StatusMessage) ? "In Process...." : model.StatusMessage;
			model.ModifiedBy = userId;
			model.ModifiedDate = DateTime.UtcNow;

			if (!_service.Update(model))
			{
				ModelState.AddModelError(string.Empty, "Failed to update teacher.");
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
				TempData["ErrorMessage"] = "Failed to delete teacher.";
				return RedirectToAction("Delete", new { id });
			}
			return RedirectToAction("Index");
		}
	}
}
