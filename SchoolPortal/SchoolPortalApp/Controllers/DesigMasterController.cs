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
	[Route("Designation")]
	public class DesigMasterController : BaseController
	{
		private readonly IDesigMasterService _service;
		private readonly ISchoolService _schoolService;
		private new readonly ILogger<DesigMasterController> _logger;

		public DesigMasterController(IDesigMasterService service, ISchoolService schoolService, ILogger<DesigMasterController> logger)
		{
			_service = service;
			_schoolService = schoolService;
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		private void PopulateDropdowns(DesigMasterViewModel vm)
		{
			var schools = _schoolService.GetAll();
			vm.Schools = schools.Select(s => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem 
			{ 
				Value = s.Id.ToString(), 
				Text = s.Name, 
				Selected = s.Id == vm.SchoolId 
			}).ToList();
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
				return new DesigMasterListItemViewModel
				{
					Id = item.Id,
					Code = item.Code,
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
			
			var school = _schoolService.GetById(item.SchoolId);
			var vm = new DesigMasterViewModel
			{
				Id = item.Id,
				Code = item.Code,
				Name = item.Name,
				IsActive = item.IsActive,
				SchoolId = item.SchoolId
			};
			ViewBag.SchoolName = school?.Name ?? "N/A";
			return View(vm);
		}

		[HttpGet]
		[Route("Create")]
		public IActionResult Create()
		{
			var vm = new DesigMasterViewModel();
			PopulateDropdowns(vm);
			return View(vm);
		}

		[HttpPost]
		[Route("Create")]
		[ValidateAntiForgeryToken]
		public IActionResult Create(DesigMasterViewModel model)
		{
			// Take SchoolId from session instead of user input
			var schoolId = CurrentSchoolId;
			if (schoolId.HasValue)
			{
				ModelState.Remove(nameof(DesigMasterViewModel.SchoolId));
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
				ModelState.AddModelError(string.Empty, "Please login and select company to create designation.");
				PopulateDropdowns(model);
				return View(model);
			}

			var entity = new DesigMaster
			{
				Id = Guid.Empty,
				Code = model.Code,
				Name = model.Name,
				IsActive = model.IsActive,
				CompanyId = companyId.Value,
				SchoolId = model.SchoolId,
				CreatedBy = userId.Value,
				CreatedDate = DateTime.UtcNow,
				IsDeleted = false
			};

			var newId = _service.Create(entity);
			if (newId == Guid.Empty)
			{
				ModelState.AddModelError(string.Empty, "Failed to create designation.");
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
			
			var vm = new DesigMasterViewModel
			{
				Id = item.Id,
				Code = item.Code,
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
		public IActionResult Edit(Guid id, DesigMasterViewModel model)
		{
			if (id != model.Id) return BadRequest();
			
			// Take SchoolId from session instead of user input
			var schoolId = CurrentSchoolId;
			if (schoolId.HasValue)
			{
				ModelState.Remove(nameof(DesigMasterViewModel.SchoolId));
				model.SchoolId = schoolId.Value;
			}

			if (!ModelState.IsValid)
			{
				PopulateDropdowns(model);
				return View(model);
			}

			var userId = CurrentUserId;
			if (!userId.HasValue || model.SchoolId == Guid.Empty)
			{
				ModelState.AddModelError(string.Empty, "Please login to update designation.");
				PopulateDropdowns(model);
				return View(model);
			}

			var entity = new DesigMaster
			{
				Id = id,
				Code = model.Code,
				Name = model.Name,
				IsActive = model.IsActive,
				SchoolId = model.SchoolId,
				ModifiedBy = userId,
				ModifiedDate = DateTime.UtcNow
			};

			if (!_service.Update(entity))
			{
				ModelState.AddModelError(string.Empty, "Failed to update designation.");
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
			
			var school = _schoolService.GetById(item.SchoolId);
			var vm = new DesigMasterViewModel
			{
				Id = item.Id,
				Code = item.Code,
				Name = item.Name,
				IsActive = item.IsActive,
				SchoolId = item.SchoolId
			};
			ViewBag.SchoolName = school?.Name ?? "N/A";
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
				// If delete fails, return to the delete view with an error message
				ModelState.AddModelError(string.Empty, "Failed to delete designation. Please try again.");
				var item = _service.GetById(id);
				if (item == null) return NotFound();
				
				var school = _schoolService.GetById(item.SchoolId);
				var vm = new DesigMasterViewModel
				{
					Id = item.Id,
					Code = item.Code,
					Name = item.Name,
					IsActive = item.IsActive,
					SchoolId = item.SchoolId
				};
				ViewBag.SchoolName = school?.Name ?? "N/A";
				return View(vm);
			}
			return RedirectToAction("Index");
		}
	}
}