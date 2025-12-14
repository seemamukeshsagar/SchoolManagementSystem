// SchoolPortalApp/Controllers/AttendanceController.cs
using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using SchoolPortal.Entities.Models;
using SchoolPortalApp.Models.Attendance;
using SchoolPortal.Services.IServices;

namespace SchoolPortalApp.Controllers
{
	[Route("Attendance")]
	public class AttendanceController : BaseController
	{
		private readonly IEmpAttendanceService _attendanceService;
        private readonly IEmpService _employeeService;
        private readonly IAttendanceReasonMasterService _attendanceReasonService;
        private readonly ILogger<AttendanceController> _logger;
        public AttendanceController(
            IEmpAttendanceService attendanceService,
            IEmpService employeeService,
            IAttendanceReasonMasterService attendanceReasonService,
            ILogger<AttendanceController> logger)
        {
            _attendanceService = attendanceService ?? throw new ArgumentNullException(nameof(attendanceService));
            _employeeService = employeeService ?? throw new ArgumentNullException(nameof(employeeService));
            _attendanceReasonService = attendanceReasonService ?? throw new ArgumentNullException(nameof(attendanceReasonService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

		[HttpGet]
		[Route("")]
		[Route("Index")]
		public IActionResult Index()
		{
			try
			{
				var attendanceList = _attendanceService.GetAll();
				var employees = _employeeService.GetAll();
				var leaveTypes = _attendanceReasonService.GetAll();

				var viewModel = attendanceList.Select(attendance => {
				var employee = employees.FirstOrDefault(e => e.Id == attendance.EmployeeId);
				var leaveType = attendance.AttendenceLeaveTypeId != Guid.Empty ? 
					leaveTypes.FirstOrDefault(lt => lt.Id == attendance.AttendenceLeaveTypeId) : null;
				return new AttendanceListItemViewModel
				{
					Id = attendance.Id,
					EmployeeId = attendance.EmployeeId,
					EmployeeName = employee != null ? $"{employee.FirstName} {employee.LastName}" : "Unknown",
					AttendanceDate = attendance.AttendenceDate,
					AttendanceMarked = attendance.AttendenceMarked,
					LeaveType = leaveType?.Description ?? "N/A",
					IsHalfDay = attendance.IsHalfDay,
					AttendanceTime = attendance.AttendenceTime,
					Status = attendance.Status
				};
			}).ToList();

				return View(viewModel);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while getting attendance list");
				return View(new List<AttendanceListItemViewModel>());
			}
		}

		[HttpGet]
		[Route("Details/{id}")]
		public IActionResult Details(Guid id)
		{
			try
			{
				var attendance = _attendanceService.GetById(id);
				if (attendance == null)
				{
					return NotFound();
				}

				var employee = _employeeService.GetById(attendance.EmployeeId);
				var leaveType = attendance.AttendenceLeaveTypeId != Guid.Empty ? 
					_attendanceReasonService.GetById(attendance.AttendenceLeaveTypeId) : null;
				var viewModel = new AttendanceDetailsViewModel
				{
					Id = attendance.Id,
					EmployeeId = attendance.EmployeeId,
					EmployeeName = employee != null ? $"{employee.FirstName} {employee.LastName}" : "Unknown",
					AttendanceDate = attendance.AttendenceDate,
					AttendanceMarked = attendance.AttendenceMarked,
					LeaveType = leaveType?.Description ?? "N/A",
					AttendenceLeaveTypeId = attendance.AttendenceLeaveTypeId,
					IsHalfDay = attendance.IsHalfDay,
					AttendanceTime = attendance.AttendenceTime,
					Status = attendance.Status,
					StatusMessage = attendance.StatusMessage,
					CreatedDate = attendance.CreatedDate,
					ModifiedDate = attendance.ModifiedDate,
					CreatedByName = "System", // Replace with actual user lookup if needed
					ModifiedByName = attendance.ModifiedBy.HasValue ? "System" : null, // Replace with actual user lookup if needed
					ModifiedBy = attendance.ModifiedBy
				};

				return View(viewModel);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"Error occurred while getting attendance details for ID: {id}");
				return RedirectToAction("Index");
			}
		}

		[HttpGet]
		[Route("Create")]
		public IActionResult Create()
		{
			var viewModel = new AttendanceViewModel
			{
				AttendanceDate = DateTime.Today,
				AttendanceTime = DateTime.Now.ToString("HH:mm"),
				AttendanceMarked = true
			};
			
			PopulateDropdowns(viewModel);
			return View(viewModel);
		}

		[HttpPost]
		[Route("Create")]
		[ValidateAntiForgeryToken]
		public IActionResult Create(AttendanceViewModel model)
		{
			if (!ModelState.IsValid)
			{
				PopulateDropdowns(model);
				return View(model);
			}

			try
			{
				var userId = CurrentUserId;
				if (!userId.HasValue)
				{
					ModelState.AddModelError(string.Empty, "Please login to create attendance record.");
					PopulateDropdowns(model);
					return View(model);
				}

				var attendance = new EmpAttendanceDetails
				{
					Id = Guid.NewGuid(),
					EmployeeId = model.EmployeeId,
					AttendenceDate = model.AttendanceDate,
					AttendenceMarked = model.AttendanceMarked,
					AttendenceLeaveTypeId = model.LeaveTypeId ?? Guid.Empty,
					IsHalfDay = model.IsHalfDay,
					AttendenceTime = model.AttendanceTime,
					CompanyId = CurrentCompanyId ?? Guid.Empty,
					SchoolId = CurrentSchoolId ?? Guid.Empty,
					IsActive = true,
					IsDeleted = false,
					CreatedBy = userId.Value,
					CreatedDate = DateTime.UtcNow,
					Status = "ACT",
					StatusMessage = "Active"
				};

				var result = _attendanceService.Create(attendance);
				if (result == Guid.Empty)
				{
					ModelState.AddModelError(string.Empty, "Failed to create attendance record.");
					PopulateDropdowns(model);
					return View(model);
				}

				return RedirectToAction("Details", new { id = result });
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while creating attendance record");
				ModelState.AddModelError(string.Empty, "An error occurred while creating the attendance record.");
				PopulateDropdowns(model);
				return View(model);
			}
		}

		[HttpGet]
		[Route("Edit/{id}")]
		public async Task<IActionResult> Edit(Guid id)
		{
			try
			{
				var attendance = await _attendanceService.GetByIdAsync(id);
				if (attendance == null)
				{
					return NotFound();
				}
				var viewModel = new AttendanceViewModel
				{
					Id = attendance.Id,
					EmployeeId = attendance.EmployeeId,
					AttendanceDate = attendance.AttendenceDate,
					AttendanceMarked = attendance.AttendenceMarked,
					LeaveTypeId = attendance.AttendenceLeaveTypeId != Guid.Empty ? attendance.AttendenceLeaveTypeId : (Guid?)null,
					IsHalfDay = attendance.IsHalfDay,
					AttendanceTime = attendance.AttendenceTime
				};
				PopulateDropdowns(viewModel);
				return View(viewModel);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"Error getting attendance for edit. ID: {id}");
				return RedirectToAction("Index");
			}
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Route("Edit/{id}")]
		public async Task<IActionResult> Edit(Guid id, AttendanceViewModel model)
		{
			if (id != model.Id)
			{
				return NotFound();
			}
			if (!ModelState.IsValid)
			{
				PopulateDropdowns(model);
				return View(model);
			}
			try
			{
				var attendance = await _attendanceService.GetByIdAsync(id);
				if (attendance == null)
				{
					return NotFound();
				}
				attendance.EmployeeId = model.EmployeeId;
				attendance.AttendenceDate = model.AttendanceDate;
				attendance.AttendenceMarked = model.AttendanceMarked;
				attendance.AttendenceLeaveTypeId = model.LeaveTypeId ?? Guid.Empty;
				attendance.IsHalfDay = model.IsHalfDay;
				attendance.AttendenceTime = model.AttendanceTime;
				attendance.ModifiedDate = DateTime.UtcNow;
				attendance.ModifiedBy = CurrentUserId;
				var result = await _attendanceService.UpdateAsync(attendance);
				if (!result)
				{
					ModelState.AddModelError(string.Empty, "Failed to update attendance record.");
					PopulateDropdowns(model);
					return View(model);
				}
				return RedirectToAction(nameof(Details), new { id });
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"Error updating attendance record. ID: {id}");
				ModelState.AddModelError(string.Empty, "An error occurred while updating the attendance record.");
				PopulateDropdowns(model);
				return View(model);
			}
		}

		[HttpGet]
		[Route("Delete/{id}")]
		public IActionResult Delete(Guid id)
		{
			try
			{
				var attendance = _attendanceService.GetById(id);
				if (attendance == null)
				{
					return NotFound();
				}

				var employee = _employeeService.GetById(attendance.EmployeeId);
				var leaveType = attendance.AttendenceLeaveTypeId != Guid.Empty ? 
					_attendanceReasonService.GetById(attendance.AttendenceLeaveTypeId) : null;

				var viewModel = new AttendanceDetailsViewModel
				{
					Id = attendance.Id,
					EmployeeId = attendance.EmployeeId,
					EmployeeName = employee != null ? $"{employee.FirstName} {employee.LastName}" : "Unknown",
					AttendanceDate = attendance.AttendenceDate,
					AttendanceMarked = attendance.AttendenceMarked,
					LeaveType = leaveType?.Description ?? "N/A",
					IsHalfDay = attendance.IsHalfDay,
					AttendanceTime = attendance.AttendenceTime,
					Status = attendance.Status,
					StatusMessage = attendance.StatusMessage,
					CreatedDate = attendance.CreatedDate,
					ModifiedDate = attendance.ModifiedDate
				};

				return View(viewModel);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"Error occurred while getting attendance for delete. ID: {id}");
				return RedirectToAction("Index");
			}
		}

		[HttpPost]
		[Route("Delete/{id}")]
		[ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult ConfirmDelete(Guid id)
		{
			try
			{
				var success = _attendanceService.Delete(id);
				if (!success)
				{
					TempData["ErrorMessage"] = "Failed to delete attendance record.";
					return RedirectToAction("Delete", new { id });
				}

				return RedirectToAction("Index");
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"Error occurred while deleting attendance record. ID: {id}");
				TempData["ErrorMessage"] = "An error occurred while deleting the attendance record.";
				return RedirectToAction("Delete", new { id });
			}
		}

		private void PopulateDropdowns(AttendanceViewModel model)
		{
			// Populate employees dropdown
			var employees = _employeeService.GetAll()
				.OrderBy(e => e.FirstName)
				.ThenBy(e => e.LastName)
				.Select(e => new SelectListItem
				{
					Value = e.Id.ToString(),
					Text = $"{e.FirstName} {e.LastName}",
					Selected = e.Id == model.EmployeeId
				})
				.ToList();

			// Populate attendance reasons dropdown
			var leaveTypes = _attendanceReasonService.GetAll()
				.OrderBy(lt => lt.Description)
				.Select(lt => new SelectListItem
				{
					Value = lt.Id.ToString(),
					Text = lt.Description,
					Selected = lt.Id == model.LeaveTypeId
				})
				.ToList();

			// Add default empty option
			leaveTypes.Insert(0, new SelectListItem { Value = "", Text = "-- Select Leave Type --" });

			model.Employees = employees;
			model.LeaveTypes = leaveTypes;
		}
	}
}