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
		private new readonly ILogger<ClassSectionDetailController> _logger;

		public ClassSectionDetailController(
			IClassSectionDetailService service, 
			ILookupService lookup, 
			ILogger<ClassSectionDetailController> logger)
		{
			_service = service;
			_lookup = lookup;
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
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

		[HttpPost]
		[Route("GetClassSectionsData")]
		public IActionResult GetClassSectionsData()
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

				// Get all class sections with related data
				var classSections = _service.GetAll()
					.Select(item => new 
					{
						id = item.Id,
						className = item.Class?.Name ?? string.Empty,
						sectionName = item.Section?.Name ?? string.Empty,
						locationName = item.Location?.Name ?? string.Empty,
						isActive = item.IsActive
					}).ToList();

				// Apply search
				if (!string.IsNullOrEmpty(searchValue))
				{
					classSections = classSections.Where(c => 
						(c.className != null && c.className.Contains(searchValue, StringComparison.OrdinalIgnoreCase)) ||
						(c.sectionName != null && c.sectionName.Contains(searchValue, StringComparison.OrdinalIgnoreCase)) ||
						(c.locationName != null && c.locationName.Contains(searchValue, StringComparison.OrdinalIgnoreCase)) ||
						(c.isActive.ToString().Contains(searchValue, StringComparison.OrdinalIgnoreCase))
					).ToList();
				}

				// Get total count
				recordsTotal = classSections.Count;

				// Apply sorting
				if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortColumnDirection))
				{
					var propertyInfo = typeof(ClassSectionDetailListItemViewModel).GetProperty(sortColumn, 
						System.Reflection.BindingFlags.IgnoreCase | 
						System.Reflection.BindingFlags.Public | 
						System.Reflection.BindingFlags.Instance);

					if (propertyInfo != null)
					{
						classSections = sortColumnDirection.ToLower() == "asc"
							? classSections.OrderBy(x => x.GetType().GetProperty(sortColumn.ToLower())?.GetValue(x, null)).ToList()
							: classSections.OrderByDescending(x => x.GetType().GetProperty(sortColumn.ToLower())?.GetValue(x, null)).ToList();
					}
				}

				// Apply pagination
				var data = classSections
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
				_logger.LogError(ex, "Error loading class sections data");
				return Json(new { error = "An error occurred while loading class sections data." });
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

		[HttpPost("ToggleStatus/{id}")]
		public IActionResult ToggleStatus(Guid id )
		{
			try
			{
				var userId = CurrentUserId;
                var result = _service.ToggleStatus(id, userId);
				if (result)
				{
					if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
					{
						return Json(new { success = true, message = "Status updated successfully" });
					}
					return RedirectToAction("Index");
				}
				
				if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
				{
					return Json(new { success = false, message = "Failed to update status" });
				}
				return RedirectToAction("Index");
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error toggling class section status");
				if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
				{
					return Json(new { success = false, message = "An error occurred while updating the status" });
				}
				return View("Error");
			}
		}
	}
}