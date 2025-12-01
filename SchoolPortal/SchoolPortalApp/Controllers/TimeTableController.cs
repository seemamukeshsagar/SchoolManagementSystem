using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using SchoolPortal.Entities.Models;
using SchoolPortal.Services.IServices;
using SchoolPortalApp.Models;

namespace SchoolPortalApp.Controllers
{
    [Route("TimeTable")]
    public class TimeTableController : BaseController
    {
        private readonly ITimeTablePeriodService _timeTablePeriodService;
        private readonly ITimeTablePeriodMasterService _timeTablePeriodMasterService;
        private readonly IClassService _classService;
        private readonly ISectionService _sectionService;
        private readonly IAcademicYearService _academicYearService;
        private readonly ISubjectService _subjectService;
        private readonly ITeacherService _teacherService;
        private readonly ITimeTableSetupDetailsService _timeTableSetupService;
        private readonly ILogger<TimeTableController> _logger;

        public TimeTableController(
            ITimeTablePeriodService timeTablePeriodService,
            ITimeTablePeriodMasterService timeTablePeriodMasterService,
            IClassService classService,
            ISectionService sectionService,
            IAcademicYearService academicYearService,
            ISubjectService subjectService,
            ITeacherService teacherService,
            ITimeTableSetupDetailsService timeTableSetupService,
            ILogger<TimeTableController> logger)
        {
            _timeTablePeriodService = timeTablePeriodService;
            _timeTablePeriodMasterService = timeTablePeriodMasterService;
            _classService = classService;
            _sectionService = sectionService;
            _academicYearService = academicYearService;
            _subjectService = subjectService;
            _teacherService = teacherService;
            _timeTableSetupService = timeTableSetupService;
            _logger = logger;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            try
            {
                var model = new TimeTableViewModel
                {
                    Classes = _classService.GetAll().Where(c => c.IsActive && !c.IsDeleted)
                        .Select(c => new SelectListItem 
                        { 
                            Value = c.Id.ToString(), 
                            Text = c.Name 
                        }).ToList(),
                    AcademicYears = _academicYearService.GetAll().Where(a => a.IsActive && !a.IsDeleted)
                        .Select(a => new SelectListItem 
                        { 
                            Value = a.Id.ToString(), 
                            Text = a.AcademicYearName 
                        }).ToList()
                };

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading timetable index");
                TempData["ErrorMessage"] = "An error occurred while loading the page. Please try again.";
                return View(new TimeTableViewModel());
            }
        }

        [HttpGet]
        [Route("GetSectionsByClass/{classId}")]
        public IActionResult GetSectionsByClass(Guid classId)
        {
            try
            {
                var sections = _sectionService.GetByClassId(classId)
                    .Where(s => s.IsActive && !s.IsDeleted)
                    .Select(s => new SelectListItem 
                    { 
                        Value = s.Id.ToString(), 
                        Text = s.Name 
                    }).ToList();

                return Json(sections);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting sections for class {classId}");
                return Json(new List<SelectListItem>());
            }
        }

        [HttpGet]
        [Route("Generate")]
        public async Task<IActionResult> Generate(TimeTableViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            try
            {
                // Get the class and section names
                var classInfo = _classService.GetById(model.ClassId);
                var sectionInfo = _sectionService.GetById(model.SectionId);
                var academicYear = _academicYearService.GetById(model.AcademicYearId);

                if (classInfo == null || sectionInfo == null || academicYear == null)
                {
                    TempData["ErrorMessage"] = "Invalid class, section, or academic year selected.";
                    return RedirectToAction("Index");
                }

                // Get the latest timetable setup
                var setup = _timeTableSetupService.GetAll()
                    .OrderByDescending(s => s.CreatedDate)
                    .FirstOrDefault();

                if (setup == null)
                {
                    TempData["ErrorMessage"] = "No timetable setup found. Please set up timetable periods first.";
                    return RedirectToAction("Index");
                }

                // Get all periods for the setup
                var periods = _timeTablePeriodMasterService.GetBySetupId(setup.Id)
                    .OrderBy(p => p.PeriodNumber)
                    .ToList();

                if (!periods.Any())
                {
                    TempData["ErrorMessage"] = "No periods found for the timetable setup.";
                    return RedirectToAction("Index");
                }

                // Get saved timetable periods
                var savedPeriods = await _timeTablePeriodService.GetByClassSectionAndAcademicYearAsync(
                    model.ClassId, model.SectionId, model.AcademicYearId);

                // Get subjects for the selected class
                var subjects = _subjectService.GetByClassId(model.ClassId)
                    .Where(s => s.IsActive && !s.IsDeleted)
                    .Select(s => new SelectListItem 
                    { 
                        Value = s.Id.ToString(), 
                        Text = s.SubjectName 
                    }).ToList();

                // Get all active teachers
                var teachers = _teacherService.GetAll()
                    .Where(t => t.IsActive && !t.IsDeleted)
                    .Select(t => new SelectListItem 
                    { 
                        Value = t.Id.ToString(), 
                        Text = $"{t.FirstName} {t.LastName}" 
                    }).ToList();

                // Initialize the view model
                var viewModel = new TimeTableViewModel
                {
                    ClassId = model.ClassId,
                    ClassName = classInfo.Name,
                    SectionId = model.SectionId,
                    SectionName = sectionInfo.Name,
                    AcademicYearId = model.AcademicYearId,
                    AcademicYearName = academicYear.AcademicYearName,
                    EffectiveFrom = model.EffectiveFrom,
                    EffectiveTo = model.EffectiveTo,
                    IsActive = model.IsActive,
                    Days = new List<TimeTableDayViewModel>()
                };

                // Add subjects and teachers to ViewBag for dropdowns
                ViewBag.Subjects = subjects;
                ViewBag.Teachers = teachers;

                // Initialize days (Monday to Friday)
                var days = new[] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" };
                
                for (int i = 0; i < days.Length; i++)
                {
                    var day = new TimeTableDayViewModel
                    {
                        DayId = i + 1,
                        DayName = days[i],
                        Periods = new List<TimeTablePeriodViewModel>()
                    };

                    foreach (var period in periods)
                    {
                        var savedPeriod = savedPeriods.FirstOrDefault(p => 
                            p.PeriodId == period.Id && p.DayOfWeek == day.DayId);

                        day.Periods.Add(new TimeTablePeriodViewModel
                        {
                            Id = period.Id,
                            PeriodNumber = int.Parse(period.PeriodNumber),
                            StartTime = period.StartTime,
                            EndTime = period.EndTime,
                            SubjectId = savedPeriod?.SubjectId,
                            SubjectName = savedPeriod?.Subject?.SubjectName ?? string.Empty,
                            TeacherId = savedPeriod?.TeacherId,
                            TeacherName = savedPeriod?.Teacher != null 
                                ? $"{savedPeriod.Teacher.FirstName} {savedPeriod.Teacher.LastName}" 
                                : string.Empty,
                            IsBreak = period.IsBreak,
                            BreakName = period.BreakName
                        });
                    }

                    viewModel.Days.Add(day);
                }

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating timetable");
                TempData["ErrorMessage"] = "An error occurred while generating the timetable. Please try again.";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [Route("Save")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(TimeTableViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Invalid data. Please check the form and try again.";
                return RedirectToAction("Index");
            }

            try
            {
                // Get current user ID
                var currentUserId = GetCurrentUserId();
                var currentDate = DateTime.UtcNow;

                // Get company and school IDs
                var companyId = GetCurrentCompanyId();
                var schoolId = GetCurrentSchoolId();

                // Delete existing timetable for this class, section and academic year
                var userId = GetCurrentUserId(); // Assuming you have a method to get current user ID
                await _timeTablePeriodService.DeleteByClassSectionAndAcademicYearAsync(model.ClassId, model.SectionId, model.AcademicYearId, userId);

                // Save the timetable periods
                var periodsToSave = new List<TimeTableClassPeriodDetails>();
                
                foreach (var day in model.Days)
                {
                    foreach (var period in day.Periods)
                    {
                        if (!period.IsBreak && period.SubjectId.HasValue && period.TeacherId.HasValue)
                        {
                            periodsToSave.Add(new TimeTableClassPeriodDetails
                            {
                                Id = Guid.NewGuid(),
                                ClassId = model.ClassId,
                                SectionId = model.SectionId,
                                SubjectId = period.SubjectId.Value,
                                TeacherId = period.TeacherId,
                                PeriodId = period.Id,
                                DayOfWeek = day.DayId,
                                SessionId = model.AcademicYearId,
                                CompanyId = companyId,
                                SchoolId = schoolId,
                                IsActive = true,
                                IsDeleted = false,
                                CreatedBy = currentUserId,
                                CreatedDate = currentDate,
                                Status = "ACT",
                                StatusMessage = "Active"
                            });
                        }
                    }
                }

                // Save all periods in a single transaction
                if (periodsToSave.Any())
                {
                    await _timeTablePeriodService.SaveBulkAsync(periodsToSave);
                }

                TempData["SuccessMessage"] = "Timetable saved successfully.";
                return RedirectToAction("ViewTimetable", new { 
                    classId = model.ClassId, 
                    sectionId = model.SectionId, 
                    academicYearId = model.AcademicYearId 
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving timetable");
                TempData["ErrorMessage"] = "An error occurred while saving the timetable. Please try again.";
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        [Route("ViewTimetable")]
        public async Task<IActionResult> ViewTimetable(Guid classId, Guid sectionId, Guid academicYearId)
        {
            try
            {
                var classInfo = _classService.GetById(classId);
                var sectionInfo = _sectionService.GetById(sectionId);
                var academicYear = _academicYearService.GetById(academicYearId);

                if (classInfo == null || sectionInfo == null || academicYear == null)
                {
                    TempData["ErrorMessage"] = "Invalid class, section, or academic year selected.";
                    return RedirectToAction("Index");
                }

                var model = new TimeTableViewModel
                {
                    ClassId = classId,
                    ClassName = classInfo.Name,
                    SectionId = sectionId,
                    SectionName = sectionInfo.Name,
                    AcademicYearId = academicYearId,
                    AcademicYearName = academicYear.AcademicYearName,
                    Days = new List<TimeTableDayViewModel>()
                };

                // Get the latest timetable setup
                var setup = _timeTableSetupService.GetAll()
                    .OrderByDescending(s => s.CreatedDate)
                    .FirstOrDefault();

                if (setup == null)
                {
                    TempData["ErrorMessage"] = "No timetable setup found.";
                    return RedirectToAction("Index");
                }

                // Get all periods for the setup
                var periods = _timeTablePeriodMasterService.GetBySetupId(setup.Id)
                    .OrderBy(p => p.PeriodNumber)
                    .ToList();

                if (!periods.Any())
                {
                    TempData["ErrorMessage"] = "No periods found for the timetable setup.";
                    return RedirectToAction("Index");
                }

                // Get saved timetable periods
                var savedPeriods = await _timeTablePeriodService.GetByClassSectionAndAcademicYearAsync(
                    classId, sectionId, academicYearId);

                // Initialize days (Monday to Friday)
                var days = new[] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" };
                
                for (int i = 0; i < days.Length; i++)
                {
                    var day = new TimeTableDayViewModel
                    {
                        DayId = i + 1,
                        DayName = days[i],
                        Periods = new List<TimeTablePeriodViewModel>()
                    };

                    foreach (var period in periods)
                    {
                        var savedPeriod = savedPeriods.FirstOrDefault(p => 
                            p.PeriodId == period.Id && p.DayOfWeek == day.DayId);

                        day.Periods.Add(new TimeTablePeriodViewModel
                        {
                            Id = period.Id,
                            PeriodNumber = int.Parse(period.PeriodNumber),
                            StartTime = period.StartTime,
                            EndTime = period.EndTime,
                            SubjectId = savedPeriod?.SubjectId,
                            SubjectName = savedPeriod?.Subject?.SubjectName ?? string.Empty,
                            TeacherId = savedPeriod?.TeacherId,
                            TeacherName = savedPeriod?.Teacher != null 
                                ? $"{savedPeriod.Teacher.FirstName} {savedPeriod.Teacher.LastName}" 
                                : string.Empty,
                            IsBreak = period.IsBreak,
                            BreakName = period.BreakName
                        });
                    }

                    model.Days.Add(day);
                }

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error viewing timetable");
                TempData["ErrorMessage"] = "An error occurred while loading the timetable. Please try again.";
                return RedirectToAction("Index");
            }
        }

        private Guid GetCurrentUserId()
        {
            // Implement this method to get the current user's ID
            // Example: return User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Guid.Empty; // Replace with actual implementation
        }

        private Guid GetCurrentCompanyId()
        {
            // Implement this method to get the current company ID
            // This could come from user claims, session, or configuration
            return Guid.Empty; // Replace with actual implementation
        }

        private Guid GetCurrentSchoolId()
        {
            // Implement this method to get the current school ID
            // This could come from user claims, session, or configuration
            return Guid.Empty; // Replace with actual implementation
        }
    }
}