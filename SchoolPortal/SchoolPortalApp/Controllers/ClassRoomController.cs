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
	[Route("ClassRoom")]
	public class ClassRoomController : BaseController
	{
		private readonly IClassRoomService _service;
		private readonly ISchoolService _schoolService;
		private readonly ILogger<ClassRoomController> _logger;

		public ClassRoomController(IClassRoomService service, ISchoolService schoolService, ILogger<ClassRoomController> logger)
		{
			_service = service;
			_schoolService = schoolService;
			_logger = logger;
		}

		private void PopulateDropdowns(ClassRoomViewModel vm)
		{
			var schools = _schoolService.GetAll();
			vm.Schools = schools.Select(s => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = s.Id.ToString(), Text = s.Name, Selected = s.Id == vm.SchoolId }).ToList();
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
				return new ClassRoomListItemViewModel
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
			var vm = new ClassRoomViewModel();
			return View(vm);
		}

		[HttpPost]
		[Route("Create")]
		[ValidateAntiForgeryToken]
		public IActionResult Create(ClassRoomViewModel model)
		{
			var schoolId = CurrentSchoolId;
			if (schoolId.HasValue)
			{
				ModelState.Remove(nameof(ClassRoomViewModel.SchoolId));
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
				ModelState.AddModelError(string.Empty, "Please login and select company to create class room.");
				return View(model);
			}

			var entity = new ClassRoomMaster
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
				ModelState.AddModelError(string.Empty, "Failed to create class room.");
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
			var vm = new ClassRoomViewModel
			{
				Id = item.Id,
				Name = item.Name,
				IsActive = item.IsActive,
				SchoolId = item.SchoolId
			};
			PopulateDropdowns(vm);
			return View(vm);
		}

		[HttpPost]
		[Route("Edit/{id}")]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Guid id, ClassRoomViewModel model)
		{
			if (id != model.Id) return BadRequest();

			var schoolIdFromSession = CurrentSchoolId;
			if (schoolIdFromSession.HasValue)
			{
				ModelState.Remove(nameof(ClassRoomViewModel.SchoolId));
				model.SchoolId = schoolIdFromSession.Value;
			}

			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var userId = CurrentUserId;
			if (!userId.HasValue || model.SchoolId == Guid.Empty)
			{
				ModelState.AddModelError(string.Empty, "Please login to update class room.");
				return View(model);
			}

			var entity = new ClassRoomMaster
			{
				Id = id,
				Name = model.Name,
				IsActive = model.IsActive,
				SchoolId = model.SchoolId,
				ModifiedBy = userId.Value,
				ModifiedDate = DateTime.UtcNow
			};

			if (!_service.Update(entity))
			{
				ModelState.AddModelError(string.Empty, "Failed to update class room.");
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
				TempData["ErrorMessage"] = "Failed to delete class room.";
				return RedirectToAction("Delete", new { id });
			}
			return RedirectToAction("Index");
		}
	}
}
