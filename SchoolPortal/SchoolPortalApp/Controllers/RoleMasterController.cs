using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SchoolPortal.Entities.Models;
using SchoolPortalApp.Models;
using SchoolPortal.Services.IServices;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SchoolPortalApp.Controllers
{
	[Route("RoleMaster")]
	public class RoleMasterController : BaseController
	{
		private readonly IRoleMasterService _service;
		private readonly ILogger<RoleMasterController> _logger;

		public RoleMasterController(IRoleMasterService service, ILogger<RoleMasterController> logger)
		{
			_service = service ?? throw new ArgumentNullException(nameof(service));
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		[HttpGet]
		[Route("")]
		[Route("Index")]
		public IActionResult Index()
		{
			return View();
			// try
			// {
			// 	var roles = _service.GetAll()
			// 		.Select(r => new RoleMasterListItemViewModel
			// 		{
			// 			Id = r.Id,
			// 			RoleName = r.Name,
			// 			Description = r.Description,
			// 			IsActive = r.IsActive
			// 		})
			// 		.ToList();

			// 	return View(roles);
			// }
			// catch (Exception ex)
			// {
			// 	_logger.LogError(ex, "Error occurred while getting roles list");
			// 	return View("Error", new ErrorViewModel { RequestId = HttpContext.TraceIdentifier });
			// }
		}

		[HttpGet]
		[Route("Details/{id}")]
		public IActionResult Details(Guid id)
		{
			try
			{
				var role = _service.GetById(id);
				if (role == null)
				{
					return NotFound();
				}

				var vm = new RoleMasterDetailsViewModel
				{
					Id = role.Id,
					RoleName = role.Name,
					Description = role.Description,
					IsActive = role.IsActive,
					CreatedBy = role.CreatedBy.ToString(),
					CreatedDate = role.CreatedDate,
					ModifiedBy = role.ModifiedBy?.ToString(),
					ModifiedDate = role.ModifiedDate
				};

				return View(vm);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"Error occurred while getting role details for ID: {id}");
				return View("Error", new ErrorViewModel { RequestId = HttpContext.TraceIdentifier });
			}
		}

		[HttpGet]
		[Route("Create")]
		public IActionResult Create()
		{
			var vm = new RoleMasterViewModel();
			return View(vm);
		}

		[HttpPost]
		[Route("Create")]
		[ValidateAntiForgeryToken]
		public IActionResult Create(RoleMasterViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			try
			{
				var userId = CurrentUserId;
				var companyId = CurrentCompanyId;
				var schoolId = CurrentSchoolId;
				if (!userId.HasValue || !companyId.HasValue || !schoolId.HasValue)
				{
					ModelState.AddModelError(string.Empty, "Please login to create a role.");
					return View(model);
				}

				var entity = new RoleMaster
				{
					Id = Guid.Empty,
					Name = model.RoleName?.Trim() ?? string.Empty,
					Description = model.Description?.Trim(),
					IsActive = model.IsActive,
					CompanyId = companyId.Value,
					SchoolId = schoolId.Value,
					CreatedBy = userId.Value,
					CreatedDate = DateTime.UtcNow
				};

				var newId = _service.Create(entity);
				if (newId == Guid.Empty)
				{
					ModelState.AddModelError(string.Empty, "Failed to create role. A role with this name may already exist.");
					return View(model);
				}

				return RedirectToAction("Details", new { id = newId });
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while creating role");
				ModelState.AddModelError(string.Empty, "An error occurred while creating the role.");
				return View(model);
			}
		}

		[HttpGet]
		[Route("Edit/{id}")]
		public IActionResult Edit(Guid id)
		{
			try
			{
				var role = _service.GetById(id);
				if (role == null)
				{
					return NotFound();
				}

				var vm = new RoleMasterViewModel
				{
					Id = role.Id,
					RoleName = role.Name,
					Description = role.Description,
					IsActive = role.IsActive
				};

				return View(vm);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"Error occurred while getting role for edit, ID: {id}");
				return View("Error", new ErrorViewModel { RequestId = HttpContext.TraceIdentifier });
			}
		}

		[HttpPost]
		[Route("Edit/{id}")]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Guid id, RoleMasterViewModel model)
		{
			if (id != model.Id)
			{
				return BadRequest();
			}

			if (!ModelState.IsValid)
			{
				return View(model);
			}

			try
			{
				var userId = CurrentUserId;
				var companyId = CurrentCompanyId;
				var schoolId = CurrentSchoolId;
				if (!userId.HasValue || !companyId.HasValue || !schoolId.HasValue)
				{
					ModelState.AddModelError(string.Empty, "Please login to update role.");
					return View(model);
				}

				var existingRole = _service.GetById(id);
				if (existingRole == null)
				{
					return NotFound();
				}
				
				// Ensure the role belongs to the same company and school
				if (existingRole.CompanyId != companyId.Value || existingRole.SchoolId != schoolId.Value)
				{
					ModelState.AddModelError(string.Empty, "You don't have permission to update this role.");
					return View(model);
				}

				existingRole.Name = model.RoleName?.Trim() ?? string.Empty;
				existingRole.Description = model.Description?.Trim();
				existingRole.IsActive = model.IsActive;
				existingRole.ModifiedBy = userId.Value;
				existingRole.ModifiedDate = DateTime.UtcNow;

				var success = _service.Update(existingRole);
				if (!success)
				{
					ModelState.AddModelError(string.Empty, "Failed to update role. A role with this name may already exist.");
					return View(model);
				}

				return RedirectToAction("Details", new { id });
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"Error occurred while updating role, ID: {id}");
				ModelState.AddModelError(string.Empty, "An error occurred while updating the role.");
				return View(model);
			}
		}

		[HttpGet]
		[Route("Delete/{id}")]
		public IActionResult Delete(Guid id)
		{
			try
			{
				var role = _service.GetById(id);
				if (role == null)
				{
					return NotFound();
				}

				var vm = new RoleMasterDetailsViewModel
				{
					Id = role.Id,
					RoleName = role.Name,
					Description = role.Description,
					IsActive = role.IsActive,
					CreatedBy = role.CreatedBy.ToString(),
					CreatedDate = role.CreatedDate,
					ModifiedBy = role.ModifiedBy?.ToString(),
					ModifiedDate = role.ModifiedDate
				};

				return View(vm);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"Error occurred while getting role for deletion, ID: {id}");
				return View("Error", new ErrorViewModel { RequestId = HttpContext.TraceIdentifier });
			}
		}

		[HttpPost]
		[Route("Delete/{id}")]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteConfirmed(Guid id)
		{
			try
			{
				var success = _service.Delete(id);
				if (!success)
				{
					return NotFound();
				}

				return RedirectToAction("Index");
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"Error occurred while deleting role, ID: {id}");
				return View("Error", new ErrorViewModel { RequestId = HttpContext.TraceIdentifier });
			}
		}

		[HttpPost]
		public IActionResult GetRoles()
		{
			try
			{
				var draw = Request.Form["draw"].FirstOrDefault();
				var start = Request.Form["start"].FirstOrDefault();
				var length = Request.Form["length"].FirstOrDefault();
				var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
				var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
				var searchValue = Request.Form["search[value]"].FirstOrDefault()?.ToLower();
				int pageSize = length != null ? Convert.ToInt32(length) : 0;
				int skip = start != null ? Convert.ToInt32(start) : 0;
				
				// Get all roles
				var roles = _service.GetAll().AsQueryable();
				
				// Apply search
				if (!string.IsNullOrEmpty(searchValue))
				{
					roles = roles.Where(r => 
						(r.Name != null && r.Name.ToLower().Contains(searchValue)) ||
						(r.Description != null && r.Description.ToLower().Contains(searchValue))
					);
				}
				
				// Get total records count
				int recordsTotal = roles.Count();
				
				// Apply sorting
				if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortColumnDirection))
				{
					if (sortColumn == "Name")
					{
						roles = sortColumnDirection == "asc" 
							? roles.OrderBy(r => r.Name)
							: roles.OrderByDescending(r => r.Name);
					}
					else if (sortColumn == "Description")
					{
						roles = sortColumnDirection == "asc"
							? roles.OrderBy(r => r.Description)
							: roles.OrderByDescending(r => r.Description);
					}
				}
				
				// Apply paging
				var data = roles
					.Skip(skip)
					.Take(pageSize)
					.Select(r => new {
						id = r.Id,
						name = r.Name,
						description = r.Description,
						isActive = r.IsActive
					})
					.ToList();
					
				return Json(new {
					draw = draw,
					recordsFiltered = recordsTotal,
					recordsTotal = recordsTotal,
					data = data
				});
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error getting roles for DataTable");
				return Json(new { error = "An error occurred while processing your request." });
			}
		}
	}
}