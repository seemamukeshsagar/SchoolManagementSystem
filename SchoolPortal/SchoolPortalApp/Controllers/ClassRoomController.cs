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

		[HttpPost]
		[Route("GetClassRoomsData")]
		public IActionResult GetClassRoomsData()
		{
			try
			{
				var requestForm = Request.Form;
				var draw = Convert.ToInt32(requestForm["draw"].FirstOrDefault() ?? "0");
				var start = Convert.ToInt32(requestForm["start"].FirstOrDefault() ?? "0");
				var length = Convert.ToInt32(requestForm["length"].FirstOrDefault() ?? "10");
				var sortColumn = requestForm["columns[" + requestForm["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
				var sortColumnDirection = requestForm["order[0][dir]"].FirstOrDefault();
				var searchValue = requestForm["search[value]"].FirstOrDefault();
				int pageSize = length != -1 ? length : 0;
				int skip = start != 0 ? start : 0;
				int recordsTotal = 0;

				// Get all class rooms
				var classRooms = _service.GetAll().Select(item => new 
				{
					id = item.Id,
					name = item.Name,
					isActive = item.IsActive
				}).ToList();

				// Apply search
				if (!string.IsNullOrEmpty(searchValue))
				{
					classRooms = classRooms.Where(c => 
						(c.name != null && c.name.Contains(searchValue, StringComparison.OrdinalIgnoreCase)) ||
						(c.isActive.ToString().Contains(searchValue, StringComparison.OrdinalIgnoreCase))
					).ToList();
				}

				// Get total count
				recordsTotal = classRooms.Count;

				// Apply sorting
				if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortColumnDirection))
				{
					var propertyInfo = typeof(ClassRoomListItemViewModel).GetProperty(sortColumn, 
						System.Reflection.BindingFlags.IgnoreCase | 
						System.Reflection.BindingFlags.Public | 
						System.Reflection.BindingFlags.Instance);

					if (propertyInfo != null)
					{
						classRooms = sortColumnDirection.ToLower() == "asc"
							? classRooms.OrderBy(x => x.GetType().GetProperty(sortColumn.ToLower())?.GetValue(x, null)).ToList()
							: classRooms.OrderByDescending(x => x.GetType().GetProperty(sortColumn.ToLower())?.GetValue(x, null)).ToList();
					}
				}

				// Apply pagination
				var data = classRooms
					.Skip(skip)
					.Take(pageSize)
					.ToList();

				return Json(new 
				{
					draw = draw,
					recordsFiltered = recordsTotal,
					recordsTotal = recordsTotal,
					data = data
				});
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error loading class rooms data");
				return Json(new { error = "An error occurred while loading class rooms data." });
			}
		}

		[HttpGet]
		[Route("")]
		[Route("Index")]
		public IActionResult Index()
		{
			return View();
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

		// GET: ClassRoom/Delete/5
		[HttpGet("Delete/{id}")]
		public IActionResult Delete(Guid id)
		{
			var item = _service.GetById(id);
			if (item == null) return NotFound();
			return View(item);
		}

		// POST: ClassRoom/Delete/5
		[HttpPost("Delete/{id}")]
		[ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteConfirmed(Guid id)
		{
			try
			{
				var result = _service.Delete(id);
				if (result)
				{
					if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
					{
						return Json(new { success = true, message = "Class room deleted successfully" });
					}
					return RedirectToAction("Index");
				}
				
				if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
				{
					return Json(new { success = false, message = "Failed to delete class room" });
				}
				return View("Delete", _service.GetById(id));
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error deleting class room");
				if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
				{
					return Json(new { success = false, message = "An error occurred while deleting the class room" });
				}
				return View("Error");
			}
		}
	}
}
