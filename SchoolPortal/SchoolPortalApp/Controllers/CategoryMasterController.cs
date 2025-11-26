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
	[Route("CategoryMaster")]
	public class CategoryMasterController : BaseController
	{
		private readonly ICategoryMasterService _service;
		private readonly ILogger<CategoryMasterController> _logger;

		public CategoryMasterController(ICategoryMasterService service, ILogger<CategoryMasterController> logger)
		{
			_service = service;
			_logger = logger;
		}

		[HttpGet]
		[Route("")]
		[Route("Index")]
		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		[Route("GetCategoriesData")]
		public IActionResult GetCategoriesData()
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

				// Get all categories
				var categories = _service.GetAll().Select(item => new CategoryMasterListItemViewModel
				{
					Id = item.Id,
					Name = item.Name,
					IsActive = item.IsActive
				}).ToList();

				// Apply search
				if (!string.IsNullOrEmpty(searchValue))
				{
					categories = categories.Where(c => 
						(!string.IsNullOrEmpty(c.Name) && c.Name.Contains(searchValue, StringComparison.OrdinalIgnoreCase)) ||
						(c.IsActive.ToString().Contains(searchValue, StringComparison.OrdinalIgnoreCase))
					).ToList();
				}

				// Get total count
				recordsTotal = categories.Count;

				// Apply sorting
				if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortColumnDirection))
				{
					var propertyInfo = typeof(CategoryMasterListItemViewModel).GetProperty(sortColumn, 
						System.Reflection.BindingFlags.IgnoreCase | 
						System.Reflection.BindingFlags.Public | 
						System.Reflection.BindingFlags.Instance);

					if (propertyInfo != null)
					{
						categories = sortColumnDirection.ToLower() == "asc"
							? categories.OrderBy(x => propertyInfo.GetValue(x, null)).ToList()
							: categories.OrderByDescending(x => propertyInfo.GetValue(x, null)).ToList();
					}
				}

				// Apply pagination
				var data = categories
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
				_logger.LogError(ex, "Error loading categories data");
				return Json(new { error = "An error occurred while loading categories data." });
			}
		}

		[HttpGet]
		[Route("Details/{id}")]
		public IActionResult Details(Guid id)
		{
			var item = _service.GetById(id);
			if (item == null) return NotFound();

			var vm = new CategoryMasterDetailsViewModel
			{
				Id = item.Id,
				Name = item.Name,
				IsActive = item.IsActive,
				Status = item.Status ?? string.Empty,
				StatusMessage = item.StatusMessage ?? string.Empty
			};
			return View(vm);
		}

		[HttpGet]
		[Route("Create")]
		public IActionResult Create()
		{
			var vm = new CategoryMasterViewModel();
			return View(vm);
		}

		[HttpPost]
		[Route("Create")]
		[ValidateAntiForgeryToken]
		public IActionResult Create(CategoryMasterViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var userId = CurrentUserId;
			if (!userId.HasValue)
			{
				ModelState.AddModelError(string.Empty, "Please login to create category.");
				return View(model);
			}

			var entity = new CategoryMaster
			{
				Id = Guid.Empty,
				Name = model.Name,
				IsActive = model.IsActive,
				IsDeleted = false,
				CreatedBy = userId.Value,
				CreatedDate = DateTime.UtcNow,
				Status = "ACT",
				StatusMessage = "Active"
			};

			var newId = _service.Create(entity);
			if (newId == Guid.Empty)
			{
				ModelState.AddModelError(string.Empty, "Failed to create category.");
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

			var vm = new CategoryMasterViewModel
			{
				Id = item.Id,
				Name = item.Name,
				IsActive = item.IsActive
			};
			return View(vm);
		}

		[HttpPost]
		[Route("Edit/{id}")]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Guid id, CategoryMasterViewModel model)
		{
			if (id != model.Id) return BadRequest();
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var userId = CurrentUserId;
			if (!userId.HasValue)
			{
				ModelState.AddModelError(string.Empty, "Please login to update category.");
				return View(model);
			}

			var entity = new CategoryMaster
			{
				Id = id,
				Name = model.Name,
				IsActive = model.IsActive,
				ModifiedBy = userId.Value,
				ModifiedDate = DateTime.UtcNow
			};

			if (!_service.Update(entity))
			{
				ModelState.AddModelError(string.Empty, "Failed to update category.");
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
				TempData["ErrorMessage"] = "Failed to delete category.";
				return RedirectToAction("Delete", new { id });
			}
			return RedirectToAction("Index");
		}
	}
}
