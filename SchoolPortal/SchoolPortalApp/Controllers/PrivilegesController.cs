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
    [Route("Privileges")]
    public class PrivilegesController : Controller
    {
        private readonly IPrivilegeService _service;
        private readonly ILogger<PrivilegesController> _logger;

        public PrivilegesController(IPrivilegeService service, ILogger<PrivilegesController> logger)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            try
            {
                var privileges = _service.GetAll()
                    .Select(p => new PrivilegeListItemViewModel
                    {
                        Id = p.Id,
                        Name = p.PrivilegeName,
                        IsActive = p.IsActive
                    })
                    .OrderBy(x => x.Name)
                    .ToList();

                return View(privileges);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting privileges list");
                return View("Error", new ErrorViewModel { RequestId = HttpContext.TraceIdentifier });
            }
        }

        [HttpGet]
        [Route("Details/{id}")]
        public IActionResult Details(Guid id)
        {
            try
            {
                var privilege = _service.GetById(id);
                if (privilege == null)
                {
                    return NotFound();
                }

                var vm = new PrivilegeDetailsViewModel
                {
                    Id = privilege.Id,
                    Name = privilege.PrivilegeName,
                    IsActive = privilege.IsActive,
                    CreatedBy = privilege.CreatedBy.ToString(),
                    CreatedDate = privilege.CreatedDate,
                    ModifiedBy = privilege.ModifiedBy?.ToString(),
                    ModifiedDate = privilege.ModifiedDate
                };

                return View(vm);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while getting privilege details for ID: {id}");
                return View("Error", new ErrorViewModel { RequestId = HttpContext.TraceIdentifier });
            }
        }

        [HttpGet]
        [Route("Create")]
        public IActionResult Create()
        {
            var vm = new PrivilegeViewModel();
            return View(vm);
        }

        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PrivilegeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var userIdStr = HttpContext.Session.GetString("UserId");
                var companyIdStr = HttpContext.Session.GetString("CompanyId");
                var schoolIdStr = HttpContext.Session.GetString("SchoolId");

                if (string.IsNullOrWhiteSpace(userIdStr) || !Guid.TryParse(userIdStr, out var userId) ||
                    string.IsNullOrWhiteSpace(companyIdStr) || !Guid.TryParse(companyIdStr, out var companyId) ||
                    string.IsNullOrWhiteSpace(schoolIdStr) || !Guid.TryParse(schoolIdStr, out var schoolId))
                {
                    ModelState.AddModelError(string.Empty, "Please login to create a privilege.");
                    return View(model);
                }

                var entity = new Privileges
                {
                    Id = Guid.Empty,
                    PrivilegeName = model.Name?.Trim() ?? string.Empty,
                    IsActive = model.IsActive,
                    CreatedBy = userId,
                    CreatedDate = DateTime.UtcNow
                };

                var newId = _service.Create(entity);
                if (newId == Guid.Empty)
                {
                    ModelState.AddModelError(string.Empty, "Failed to create privilege. A privilege with this name may already exist.");
                    return View(model);
                }

                return RedirectToAction("Details", new { id = newId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating privilege");
                ModelState.AddModelError(string.Empty, "An error occurred while creating the privilege.");
                return View(model);
            }
        }

        [HttpGet]
        [Route("Edit/{id}")]
        public IActionResult Edit(Guid id)
        {
            try
            {
                var privilege = _service.GetById(id);
                if (privilege == null)
                {
                    return NotFound();
                }

                var vm = new PrivilegeViewModel
                {
                    Id = privilege.Id,
                    Name = privilege.PrivilegeName,
                    IsActive = privilege.IsActive
                };

                return View(vm);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while getting privilege for edit, ID: {id}");
                return View("Error", new ErrorViewModel { RequestId = HttpContext.TraceIdentifier });
            }
        }

        [HttpPost]
        [Route("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, PrivilegeViewModel model)
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
                var userIdStr = HttpContext.Session.GetString("UserId");
                var companyIdStr = HttpContext.Session.GetString("CompanyId");
                var schoolIdStr = HttpContext.Session.GetString("SchoolId");

                if (string.IsNullOrWhiteSpace(userIdStr) || !Guid.TryParse(userIdStr, out var userId))
                {
                    ModelState.AddModelError(string.Empty, "Please login to update privilege.");
                    return View(model);
                }

                var entity = new Privileges
                {
                    Id = model.Id,
                    PrivilegeName = model.Name?.Trim() ?? string.Empty,
                    IsActive = model.IsActive,
                    ModifiedBy = userId,
                    ModifiedDate = DateTime.UtcNow
                };

                var result = _service.Update(entity);
                if (!result)
                {
                    ModelState.AddModelError(string.Empty, "Failed to update privilege. The privilege may have been modified or deleted.");
                    return View(model);
                }

                return RedirectToAction("Details", new { id = model.Id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while updating privilege, ID: {id}");
                ModelState.AddModelError(string.Empty, "An error occurred while updating the privilege.");
                return View(model);
            }
        }

        [HttpGet]
        [Route("Delete/{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                var privilege = _service.GetById(id);
                if (privilege == null)
                {
                    return NotFound();
                }

                var vm = new PrivilegeDetailsViewModel
                {
                    Id = privilege.Id,
                    Name = privilege.PrivilegeName,
                    IsActive = privilege.IsActive
                };

                return View(vm);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while getting privilege for delete, ID: {id}");
                return View("Error", new ErrorViewModel { RequestId = HttpContext.TraceIdentifier });
            }
        }

        [HttpPost, ActionName("Delete")]
        [Route("Delete/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            try
            {
                var result = _service.Delete(id);
                if (!result)
                {
                    return NotFound();
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting privilege, ID: {id}");
                return View("Error", new ErrorViewModel { RequestId = HttpContext.TraceIdentifier });
            }
        }
    }
}