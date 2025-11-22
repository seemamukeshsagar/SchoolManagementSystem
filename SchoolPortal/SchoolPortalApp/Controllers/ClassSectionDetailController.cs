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
	[Route("ClassSectionDetail")]
	public class ClassSectionDetailController : BaseController
	{
		private readonly IClassSectionDetailService _service;
		private readonly ILookupService _lookup;
		private readonly ILogger<ClassSectionDetailController> _logger;

		public ClassSectionDetailController(
			IClassSectionDetailService service, 
			ILookupService lookup, 
			ILogger<ClassSectionDetailController> logger)
		{
			_service = service;
			_lookup = lookup;
			_logger = logger;
		}

		private void PopulateDropdowns(ClassSectionDetailViewModel vm)
		{
			// Populate classes dropdown
			var classes = _lookup.GetClasses();
			vm.Classes = classes.Select(c => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem 
			{ 
				Value = c.Id.ToString(), 
				Text = c.Name, 
				Selected = c.Id == vm.ClassMasterId 
			}).ToList();

			// Populate sections dropdown
			var sections = _lookup.GetSections();
			vm.Sections = sections.Select(s => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem 
			{ 
				Value = s.Id.ToString(), 
				Text = s.Name, 
				Selected = s.Id == vm.SectionMasterId 
			}).ToList();

			// Populate locations dropdown
			var locations = _lookup.GetLocations();
			vm.Locations = locations.Select(l => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem 
			{ 
				Value = l.Id.ToString(), 
				Text = l.Name, 
				Selected = l.Id == vm.LocationId 
			}).ToList();
		}

		[HttpGet]
		[Route("")]
		[Route("Index")]
		public IActionResult Index()
		{
			var list = _service.GetAll();
			var classes = _lookup.GetClasses().ToDictionary(c => c.Id, c => c.Name);
			var sections = _lookup.GetSections().ToDictionary(s => s.Id, s => s.Name);
			var locations = _lookup.GetLocations().ToDictionary(l => l.Id, l => l.Name);

			var result = list.Select(item => new ClassSectionDetailListItemViewModel
			{
				Id = item.Id,
				ClassName = classes.TryGetValue(item.ClassMasterId, out var className) ? className : "N/A",
				SectionName = sections.TryGetValue(item.SectionMasterId, out var sectionName) ? sectionName : "N/A",
				LocationName = locations.TryGetValue(item.LocationId, out var locationName) ? locationName : "N/A",
				IsActive = item.IsActive,
				Status = item.Status
			}).ToList();

			return View(result);
		}

		[HttpGet]
		[Route("Details/{id}")]
		public IActionResult Details(Guid id)
		{
			var item = _service.GetById(id);
			if (item == null) return NotFound();

			var classes = _lookup.GetClasses().ToDictionary(c => c.Id, c => c.Name);
			var sections = _lookup.GetSections().ToDictionary(s => s.Id, s => s.Name);
			var locations = _lookup.GetLocations().ToDictionary(l => l.Id, l => l.Name);

			var vm = new ClassSectionDetailListItemViewModel
			{
				Id = item.Id,
				ClassName = classes.TryGetValue(item.ClassMasterId, out var className) ? className : "N/A",
				SectionName = sections.TryGetValue(item.SectionMasterId, out var sectionName) ? sectionName : "N/A",
				LocationName = locations.TryGetValue(item.LocationId, out var locationName) ? locationName : "N/A",
				IsActive = item.IsActive,
				Status = item.Status
			};

			return View(vm);
		}

		[HttpGet]
		[Route("Create")]
		public IActionResult Create()
		{
			var vm = new ClassSectionDetailViewModel
			{
				IsActive = true // Default to active when creating new
			};
			PopulateDropdowns(vm);
			return View(vm);
		}

		[HttpPost]
		[Route("Create")]
		[ValidateAntiForgeryToken]
		public IActionResult Create(ClassSectionDetailViewModel model)
		{
			if (!ModelState.IsValid)
			{
				PopulateDropdowns(model);
				return View(model);
			}

			var userId = CurrentUserId;
			var companyId = CurrentCompanyId;
			var schoolId = CurrentSchoolId;
			if (!userId.HasValue || !companyId.HasValue || !schoolId.HasValue)
			{
				ModelState.AddModelError(string.Empty, "Please login to create a class section.");
				PopulateDropdowns(model);
				return View(model);
			}

			var entity = new ClassSectionDetail
			{
				Id = Guid.Empty,
				ClassMasterId = model.ClassMasterId,
				SectionMasterId = model.SectionMasterId,
				LocationId = model.LocationId,
				IsActive = model.IsActive,
				IsDeleted = false,
				CompanyId = companyId.Value,
				SchoolId = schoolId.Value,
				CreatedBy = userId.Value,
				CreatedDate = DateTime.UtcNow,
				Status = "Active"
			};

			var newId = _service.Create(entity);
			if (newId == Guid.Empty)
			{
				ModelState.AddModelError(string.Empty, "Failed to create class section.");
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

			var vm = new ClassSectionDetailViewModel
			{
				Id = item.Id,
				ClassMasterId = item.ClassMasterId,
				SectionMasterId = item.SectionMasterId,
				LocationId = item.LocationId,
				IsActive = item.IsActive,
				CompanyId = item.CompanyId,
				SchoolId = item.SchoolId
			};

			PopulateDropdowns(vm);
			return View(vm);
		}

		[HttpPost]
		[Route("Edit/{id}")]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Guid id, ClassSectionDetailViewModel model)
		{
			if (id != model.Id) return BadRequest();

			if (!ModelState.IsValid)
			{
				PopulateDropdowns(model);
				return View(model);
			}

			var userId = CurrentUserId;
			if (!userId.HasValue)
			{
				ModelState.AddModelError(string.Empty, "Please login to update class section.");
				PopulateDropdowns(model);
				return View(model);
			}

			var entity = _service.GetById(id);
			if (entity == null) return NotFound();

			entity.ClassMasterId = model.ClassMasterId;
			entity.SectionMasterId = model.SectionMasterId;
			entity.LocationId = model.LocationId;
			entity.IsActive = model.IsActive;
			entity.ModifiedBy = userId.Value;
			entity.ModifiedDate = DateTime.UtcNow;

			var success = _service.Update(entity);
			if (!success)
			{
				ModelState.AddModelError(string.Empty, "Failed to update class section.");
				PopulateDropdowns(model);
				return View(model);
			}

			return RedirectToAction("Details", new { id });
		}

		[HttpPost]
		[Route("ToggleStatus/{id}")]
		[ValidateAntiForgeryToken]
		public IActionResult ToggleStatus(Guid id)
		{
			var userId = CurrentUserId;
			if (!userId.HasValue)
			{
				return Json(new { success = false, message = "Please login to perform this action." });
			}

			var success = _service.ToggleStatus(id, userId.Value);
			return Json(new { success });
		}
	}
}