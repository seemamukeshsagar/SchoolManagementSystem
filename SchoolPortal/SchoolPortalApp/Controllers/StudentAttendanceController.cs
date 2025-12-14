// SchoolPortalApp/Controllers/StudentAttendanceController.cs
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using SchoolPortal.Entities.Models;
using SchoolPortal.Services.IServices;
using SchoolPortalApp.Models.Attendance;

namespace SchoolPortalApp.Controllers
{
    [Route("StudentAttendance")]
    public class StudentAttendanceController : BaseController
    {
        private readonly IStudentAttendanceService _service;
        private readonly ILookupService _lookup;
        private new readonly ILogger<StudentAttendanceController> _logger;

        public StudentAttendanceController(
            IStudentAttendanceService service, 
            ILookupService lookup, 
            ILogger<StudentAttendanceController> logger)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _lookup = lookup ?? throw new ArgumentNullException(nameof(lookup));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        private CurrentUser CurrentUser => new CurrentUser
        {
            UserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? Guid.Empty.ToString()),
            SchoolId = Guid.Parse(HttpContext.Session.GetString("SchoolId") ?? Guid.Empty.ToString()),
            CompanyId = Guid.Parse(HttpContext.Session.GetString("CompanyId") ?? Guid.Empty.ToString())
        };

        private async Task PopulateDropdowns(StudentAttendanceViewModel vm)
        {
            if (!Guid.TryParse(HttpContext.Session.GetString("SchoolId"), out Guid schoolId))
            {
                TempData["ErrorMessage"] = "School information not found. Please log in again.";
                return;
            }
            
            // Get all required data asynchronously
            var studentsTask = _lookup.GetStudentsAsync(schoolId);
            var classesTask = _lookup.GetClassesAsync();
            var sectionsTask = _lookup.GetSectionsAsync();
            var reasonsTask = _lookup.GetAttendanceReasonsAsync(schoolId);

            await Task.WhenAll(studentsTask, classesTask, sectionsTask, reasonsTask);

            // Get the results
            var students = await studentsTask;
            var classes = await classesTask;
            var sections = await sectionsTask;
            var reasons = await reasonsTask;

            // Populate dropdowns
            vm.Students = students?.Select(s => new SelectListItem 
            { 
                Value = s.Id.ToString(), 
                Text = $"{s.FirstName} {s.LastName}",
                Selected = s.Id == vm.StudentGUID 
            }).ToList() ?? new List<SelectListItem>();

            vm.Classes = classes?.Select(c => new SelectListItem 
            { 
                Value = c.Id.ToString(), 
                Text = c.Name,
                Selected = c.Id == vm.ClassId 
            }).ToList() ?? new List<SelectListItem>();

            vm.Sections = sections?.Where(s => vm.ClassId == Guid.Empty)
                .Select(s => new SelectListItem 
                { 
                    Value = s.Id.ToString(), 
                    Text = s.Name,
                    Selected = s.Id == vm.SectionId 
                }).ToList() ?? new List<SelectListItem>();

            vm.AttendanceReasons = reasons?.Select(r => new SelectListItem 
            { 
                Value = r.Id.ToString(), 
                Text = r.Name,
                Selected = r.Id == vm.AttendanceReasonId 
            }).ToList() ?? new List<SelectListItem>();
        }

        [HttpGet]
        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            try
            {
                var schoolIdString = HttpContext.Session.GetString("SchoolId");
                if (string.IsNullOrEmpty(schoolIdString) || !Guid.TryParse(schoolIdString, out Guid schoolId))
                {
                    TempData["ErrorMessage"] = "School information not found. Please log in again.";
                    return RedirectToAction("Login", "Account");
                }
        
                var attendances = _service.GetAll();
                var students = _lookup.GetStudents(schoolId);
                var classes = _lookup.GetClasses();
                var sections = _lookup.GetSections();
                var result = attendances.Select(attendance =>
                {
                    var student = students?.FirstOrDefault(s => s.Id == attendance.StudentGUID);
                    var classInfo = classes?.FirstOrDefault(c => c.Id == attendance.ClassId);
                    var section = sections?.FirstOrDefault(s => s.Id == attendance.SectionId);
                    return new StudentAttendanceListItemViewModel
                    {
                        Id = attendance.Id,
                        StudentName = student != null ? $"{student.FirstName} {student.LastName}" : "N/A",
                        ClassName = classInfo?.Name ?? "N/A",
                        SectionName = section?.Name ?? "N/A",
                        AttendenceDate = attendance.AttendenceDate,
                        AttendenceStatus = attendance.AttendenceStatus,
                        Status = attendance.Status
                    };
                }).OrderByDescending(a => a.AttendenceDate).ToList();
                return View(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting student attendance list");
                TempData["ErrorMessage"] = "An error occurred while retrieving attendance records.";
                return View(new List<StudentAttendanceListItemViewModel>());
            }
        }

        [HttpGet]
        [Route("Details/{id}")]
        public async Task<IActionResult> Details(Guid id)
        {
            try
            {
                if (!Guid.TryParse(HttpContext.Session.GetString("SchoolId"), out Guid schoolId))
                {
                    TempData["ErrorMessage"] = "School information not found. Please log in again.";
                    return RedirectToAction("Login", "Account");
                }

                var attendance = await _service.GetByIdAsync(id);
                if (attendance == null)
                {
                    return NotFound();
                }

                var studentsTask = _lookup.GetStudentsAsync(schoolId);
                var classesTask = _lookup.GetClassesAsync();
                var sectionsTask = _lookup.GetSectionsAsync();
                var reasonsTask = _lookup.GetAttendanceReasonsAsync(schoolId);

                await Task.WhenAll(studentsTask, classesTask, sectionsTask, reasonsTask);

                var students = await studentsTask;
                var classes = await classesTask;
                var sections = await sectionsTask;
                var reasons = await reasonsTask;

                var student = students?.FirstOrDefault(s => s.Id == attendance.StudentGUID);
                var classInfo = classes?.FirstOrDefault(c => c.Id == attendance.ClassId);
                var section = sections?.FirstOrDefault(s => s.Id == attendance.SectionId);
                var reason = reasons?.FirstOrDefault(r => r.Id == attendance.AttendanceReasonId);

                var vm = new StudentAttendanceDetailsViewModel
                {
                    Id = attendance.Id,
                    StudentGUID = attendance.StudentGUID,
                    StudentName = student != null ? $"{student.FirstName} {student.LastName}" : "N/A",
                    ClassId = attendance.ClassId,
                    ClassName = classInfo?.Name ?? "N/A",
                    SectionId = attendance.SectionId,
                    SectionName = section?.Name ?? "N/A",
                    Month = attendance.Month,
                    Year = attendance.Year,
                    AttendenceDate = attendance.AttendenceDate,
                    AttendenceStatus = attendance.AttendenceStatus,
                    AttendanceReasonId = attendance.AttendanceReasonId,
                    AttendanceReason = reason?.Name ?? "N/A",
                    AttendenceTime = attendance.AttendenceTime,
                    Status = attendance.Status,
                    StatusMessage = attendance.StatusMessage
                };

                return View(vm);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while getting student attendance details for ID: {id}");
                TempData["ErrorMessage"] = "An error occurred while retrieving attendance details.";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        [Route("Create")]
        public IActionResult Create()
        {
            var vm = new StudentAttendanceViewModel
            {
                AttendenceDate = DateTime.Today,
                AttendenceStatus = true,
                AttendenceTime = DateTime.Now.ToString("HH:mm")
            };

            _ = PopulateDropdowns(vm);
            return View(vm);
        }

        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudentAttendanceViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await PopulateDropdowns(model);
                return View(model);
            }

            try
            {
                var attendance = new StudentAttendanceDetails
                {
                    Id = Guid.NewGuid(),
                    StudentGUID = model.StudentGUID,
                    ClassId = model.ClassId,
                    SectionId = model.SectionId,
                    Month = model.Month,
                    Year = model.Year,
                    AttendenceDate = model.AttendenceDate,
                    AttendenceStatus = model.AttendenceStatus,
                    AttendanceReasonId = model.AttendanceReasonId,
                    AttendenceTime = model.AttendenceTime,
                    CompanyId = CurrentUser.CompanyId,
                    SchoolId = CurrentUser.SchoolId,
                    IsActive = true,
                    IsDeleted = false,
                    CreatedBy = CurrentUser.UserId,
                    CreatedDate = DateTime.UtcNow,
                    Status = "ACT",
                    StatusMessage = "Active"
                };

                var result = await _service.CreateAsync(attendance);
                if (result != Guid.Empty)
                {
                    TempData["SuccessMessage"] = "Attendance record created successfully.";
                    return RedirectToAction(nameof(Details), new { id = result });
                }

                ModelState.AddModelError(string.Empty, "Failed to create attendance record.");
                await PopulateDropdowns(model);
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating student attendance");
                ModelState.AddModelError(string.Empty, "An error occurred while creating the attendance record.");
                await PopulateDropdowns(model);
                return View(model);
            }
        }

        [HttpGet]
        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            try
            {
                var attendance = await _service.GetByIdAsync(id);
                if (attendance == null)
                {
                    return NotFound();
                }

                var vm = new StudentAttendanceViewModel
                {
                    Id = attendance.Id,
                    StudentGUID = attendance.StudentGUID,
                    ClassId = attendance.ClassId,
                    SectionId = attendance.SectionId,
                    Month = attendance.Month,
                    Year = attendance.Year,
                    AttendenceDate = attendance.AttendenceDate,
                    AttendenceStatus = attendance.AttendenceStatus,
                    AttendanceReasonId = attendance.AttendanceReasonId,
                    AttendenceTime = attendance.AttendenceTime,
                    Status = attendance.Status,
                    StatusMessage = attendance.StatusMessage
                };

                await PopulateDropdowns(vm);
                return View(vm);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while getting student attendance for edit. ID: {id}");
                TempData["ErrorMessage"] = "An error occurred while retrieving the attendance record for editing.";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [Route("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, StudentAttendanceViewModel model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                await PopulateDropdowns(model);
                return View(model);
            }

            try
            {
                var existing = await _service.GetByIdAsync(id);
                if (existing == null)
                {
                    return NotFound();
                }

                existing.StudentGUID = model.StudentGUID;
                existing.ClassId = model.ClassId;
                existing.SectionId = model.SectionId;
                existing.Month = model.Month;
                existing.Year = model.Year;
                existing.AttendenceDate = model.AttendenceDate;
                existing.AttendenceStatus = model.AttendenceStatus;
                existing.AttendanceReasonId = model.AttendanceReasonId;
                existing.AttendenceTime = model.AttendenceTime;
                existing.ModifiedBy = CurrentUser.UserId;
                existing.ModifiedDate = DateTime.UtcNow;

                var result = await _service.UpdateAsync(existing);
                if (result)
                {
                    TempData["SuccessMessage"] = "Attendance record updated successfully.";
                    return RedirectToAction(nameof(Details), new { id });
                }

                ModelState.AddModelError(string.Empty, "Failed to update attendance record.");
                await PopulateDropdowns(model);
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while updating student attendance. ID: {id}");
                ModelState.AddModelError(string.Empty, "An error occurred while updating the attendance record.");
                await PopulateDropdowns(model);
                return View(model);
            }
        }

        [HttpPost]
        [Route("Delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var result = await _service.DeleteAsync(id);
                if (result)
                {
                    TempData["SuccessMessage"] = "Attendance record deleted successfully.";
                }
                else
                {
                    TempData["ErrorMessage"] = "Failed to delete attendance record or record not found.";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting student attendance. ID: {id}");
                TempData["ErrorMessage"] = "An error occurred while deleting the attendance record.";
            }

            return RedirectToAction(nameof(Index));
        }
    }

    public class CurrentUser
    {
        public Guid UserId { get; set; }
        public Guid SchoolId { get; set; }
        public Guid CompanyId { get; set; }
    }
}