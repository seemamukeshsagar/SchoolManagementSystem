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
	public class CategoryMasterController : Controller
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
			var list = _service.GetAll();
			var result = list.Select(item => new CategoryMasterListItemViewModel
			{
				Id = item.Id,
				Name = item.Name,
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

			var userIdStr = HttpContext.Session.GetString("UserId");
			if (string.IsNullOrWhiteSpace(userIdStr) || !Guid.TryParse(userIdStr, out var userId))
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
				CreatedBy = userId,
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

			var userIdStr = HttpContext.Session.GetString("UserId");
			if (string.IsNullOrWhiteSpace(userIdStr) || !Guid.TryParse(userIdStr, out var userId))
			{
				ModelState.AddModelError(string.Empty, "Please login to update category.");
				return View(model);
			}

			var entity = new CategoryMaster
			{
				Id = id,
				Name = model.Name,
				IsActive = model.IsActive,
				ModifiedBy = userId,
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
