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
	[Route("BookCategory")]
	public class BookCategoryMasterController : BaseController
	{
		private readonly IBookCategoryService _service;
		private readonly ILogger<BookCategoryMasterController> _logger;

		public BookCategoryMasterController(IBookCategoryService service, ILogger<BookCategoryMasterController> logger)
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
			var result = list.Select(item => new BookCategoryListItemViewModel
			{
				Id = item.Id,
				Name = item.Name,
				Description = item.Description,
				IsActive = item.IsActive,
			}).ToList();
			return View(result);
		}

		[HttpGet]
		[Route("Details/{id}")]
		public IActionResult Details(Guid id)
		{
			var item = _service.GetById(id);
			if (item == null) return NotFound();

			var vm = new BookCategoryDetailsViewModel
			{
				Id = item.Id,
				Name = item.Name,
				Description = item.Description ?? string.Empty,
				IsActive = item.IsActive,
			};
			return View(vm);
		}

		[HttpGet]
		[Route("Create")]
		public IActionResult Create()
		{
			var vm = new BookCategoryViewModel();
			return View(vm);
		}

		[HttpPost]
		[Route("Create")]
		[ValidateAntiForgeryToken]
		public IActionResult Create(BookCategoryViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}
			
			var userId = CurrentUserId;
			var companyId = CurrentCompanyId;
			var schoolId = CurrentSchoolId;
			if (!userId.HasValue)
			{
				ModelState.AddModelError(string.Empty, "Please login to create book category.");
				return View(model);
			}
			if (!companyId.HasValue)
			{
				ModelState.AddModelError(string.Empty, "Company information not found in session.");
				return View(model);
			}
			if (!schoolId.HasValue)
			{
				ModelState.AddModelError(string.Empty, "School information not found in session.");
				return View(model);
			}
		
			var entity = new BookCategoryMaster
			{
				Id = Guid.Empty,
				Name = model.Name,
				Description = model.Description ?? string.Empty,
				IsActive = model.IsActive,
				CompanyId = companyId.Value,
				SchoolId = schoolId.Value,
				CreatedBy = userId.Value,
				CreatedDate = DateTime.UtcNow,
			};

			var newId = _service.Create(entity);
			if (newId == Guid.Empty)
			{
				ModelState.AddModelError(string.Empty, "Failed to create book category.");
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
			
			var vm = new BookCategoryViewModel
			{
				Id = item.Id,
				Name = item.Name,
				Description = item.Description,
				IsActive = item.IsActive,
			};
			return View(vm);
		}

		[HttpPost]
		[Route("Edit/{id}")]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Guid id, BookCategoryViewModel model)
		{
			if (id != model.Id) return BadRequest();
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var userId = CurrentUserId;
			var companyId = CurrentCompanyId;
			var schoolId = CurrentSchoolId;
			if (!userId.HasValue)
			{
				ModelState.AddModelError(string.Empty, "Please login to update book category.");
				return View(model);
			}
			if (!companyId.HasValue)
			{
				ModelState.AddModelError(string.Empty, "Company information not found in session.");
				return View(model);
			}
			if (!schoolId.HasValue)
			{
				ModelState.AddModelError(string.Empty, "School information not found in session.");
				return View(model);
			}
		
			var entity = new BookCategoryMaster
			{
				Id = id,
				Name = model.Name,
				Description = model.Description ?? string.Empty,
				IsActive = model.IsActive,
				CompanyId = companyId.Value,
				SchoolId = schoolId.Value,
				ModifiedBy = userId.Value,
				ModifiedDate = DateTime.UtcNow,
			};

			if (!_service.Update(entity))
			{
				ModelState.AddModelError(string.Empty, "Failed to update book category.");
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
				TempData["ErrorMessage"] = "Failed to delete book category.";
				return RedirectToAction("Delete", new { id });
			}
			return RedirectToAction("Index");
		}
	}
}