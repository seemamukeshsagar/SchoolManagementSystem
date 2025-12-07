using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SchoolPortal.Entities.Models;
using SchoolPortalApp.Models;
using SchoolPortal.Services.IServices;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SchoolPortalApp.Controllers
{
	[Route("HolidayMaster")]
	public class HolidayMasterController : BaseController
	{
		private readonly IHolidayMasterService _service;
		private new readonly ILogger<HolidayMasterController> _logger;
		private readonly IHolidayTypeMasterService _holidayTypeService;

		public HolidayMasterController(IHolidayMasterService service, ILogger<HolidayMasterController> logger, IHolidayTypeMasterService holidayTypeService)
		{
			_service = service;
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
			_holidayTypeService = holidayTypeService;
		}

		private void PopulateDropdowns(HolidayViewModel vm)
		{
			try
			{
				var companyId = CurrentCompanyId;
				var schoolId = CurrentSchoolId;
				var items = _holidayTypeService.GetAll().AsQueryable();

				if (companyId.HasValue)
				{
					items = items.Where(x => x.CompanyId == companyId.Value);
				}
				if (schoolId.HasValue)
				{
					items = items.Where(x => x.SchoolId == schoolId.Value);
				}

				vm.HolidayTypes = items
					.Where(x => x.IsActive)
					.OrderBy(x => x.HolidayTypeName)
					.Select(x => new SelectListItem
					{
						Value = x.Id.ToString(),
						Text = x.HolidayTypeName,
						Selected = x.Id == vm.TypeId
					})
					.ToList();
			}
			catch
			{
				vm.HolidayTypes = Enumerable.Empty<SelectListItem>();
			}
		}

		[HttpGet]
		[Route("")]
		[Route("Index")]
		public IActionResult Index()
		{
			var list = _service.GetAll();
			var result = list.Select(item => new HolidayListItemViewModel
			{
				Id = item.Id,
				Name = item.Name,
				FromDate = item.FromDate,
				ToDate = item.ToDate,
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
			var vm = new HolidayViewModel();
			PopulateDropdowns(vm);
			return View(vm);
		}

		[HttpPost]
		[Route("Create")]
		[ValidateAntiForgeryToken]
		public IActionResult Create(HolidayViewModel model)
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

			var entity = new HolidayMaster
			{
				Id = Guid.Empty,
				Name = model.Name,
				Description = model.Description ?? string.Empty,
				TypeId = model.TypeId,
				FromDate = model.FromDate,
				ToDate = model.ToDate,
				Year = model.Year,
				CompanyId = companyId.Value,
				SchoolId = schoolId.Value,
				IsStaffApplicable = model.IsStaffApplicable ?? false,
				SessionId = model.SessionId,
				IsActive = model.IsActive,
				CreatedBy = userId.Value,
				CreatedDate = DateTime.UtcNow
			};

			var newId = _service.Create(entity);
			if (newId == Guid.Empty)
			{
				ModelState.AddModelError(string.Empty, "Failed to create holiday.");
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
			var vm = new HolidayViewModel
			{
				Id = item.Id,
				Name = item.Name,
				Description = item.Description,
				TypeId = item.TypeId,
				FromDate = item.FromDate,
				ToDate = item.ToDate,
				Year = item.Year,
				IsStaffApplicable = item.IsStaffApplicable,
				SessionId = item.SessionId,
				IsActive = item.IsActive,
				SchoolId = item.SchoolId
			};
			PopulateDropdowns(vm);
			return View(vm);
		}

		[HttpPost]
		[Route("Edit/{id}")]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Guid id, HolidayViewModel model)
		{
			if (id != model.Id) return BadRequest();
			if (!ModelState.IsValid)
			{
				PopulateDropdowns(model);
				return View(model);
			}

			var userId = CurrentUserId;
			if (!userId.HasValue || model.SchoolId == Guid.Empty)
			{
				ModelState.AddModelError(string.Empty, "Please login to update holiday.");
				PopulateDropdowns(model);
				return View(model);
			}

			var entity = new HolidayMaster
			{
				Id = id,
				Name = model.Name,
				Description = model.Description ?? string.Empty,
				TypeId = model.TypeId,
				FromDate = model.FromDate,
				ToDate = model.ToDate,
				Year = model.Year,
				IsStaffApplicable = model.IsStaffApplicable ?? false,
				SessionId = model.SessionId,
				IsActive = model.IsActive,
				SchoolId = model.SchoolId,
				ModifiedBy = userId.Value,
				ModifiedDate = DateTime.UtcNow
			};

			if (!_service.Update(entity))
			{
				ModelState.AddModelError(string.Empty, "Failed to update holiday.");
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
				TempData["ErrorMessage"] = "Failed to delete holiday.";
				return RedirectToAction("Delete", new { id });
			}
			return RedirectToAction("Index");
		}
	}
}
