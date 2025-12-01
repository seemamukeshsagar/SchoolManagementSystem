using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SchoolPortalApp.Models.TimeTable;
using SchoolPortal.Services.IServices;
using SchoolPortal.Entities.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SchoolPortalApp.Controllers
{
    [Route("TimeTable")]
    public class TimeTableController : BaseController
    {
        private readonly ITimeTableSetupDetailsService _timeTableSetupService;
        private readonly ITimeTablePeriodMasterService _timeTablePeriodService;
        private readonly IClassService _classService;
        private readonly ISubjectService _subjectService;
        private readonly IAcademicYearService _academicYearService;
        private readonly ISectionService _sectionService;
        private readonly ITeacherService _teacherService;
        private readonly ILogger<TimeTableController> _logger;

        public TimeTableController(
            ITimeTableSetupDetailsService timeTableSetupService,
            ITimeTablePeriodMasterService timeTablePeriodService,
            IClassService classService,
            ISubjectService subjectService,
            IAcademicYearService academicYearService,
            ISectionService sectionService,
            ITeacherService teacherService,
            ILogger<TimeTableController> logger)
        {
            _timeTableSetupService = timeTableSetupService;
            _timeTablePeriodService = timeTablePeriodService;
            _classService = classService;
            _subjectService = subjectService;
            _academicYearService = academicYearService;
            _sectionService = sectionService;
            _teacherService = teacherService;
            _logger = logger;
        }

        [HttpGet]
        [Route("")]
        [Route("Index")]
        public IActionResult Index(TimeTableFilterViewModel filter)
        {
            var model = new TimeTableViewModel
            {
                Classes = _classService.GetAllActive()
                    .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.ClassName })
                    .ToList(),
                AcademicYears = _academicYearService.GetAllActive()
                    .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.AcademicYearName })
                    .ToList()
            };

            if (filter.ClassId.HasValue)
            {
                model.Sections = _sectionService.GetByClassId(filter.ClassId.Value)
                    .Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.SectionName })
                    .ToList();
            }

            return View(model);
        }

        [HttpGet]
        [Route("GetSectionsByClass")]
        public IActionResult GetSectionsByClass(Guid classId)
        {
            var sections = _sectionService.GetByClassId(classId)
                .Select(s => new { Value = s.Id.ToString(), Text = s.SectionName })
                .ToList();
            return Json(sections);
        }

        [HttpGet]
        [Route("Generate")]
        public IActionResult Generate(Guid classId, Guid sectionId, Guid academicYearId)
        {
            var timeTable = new TimeTableViewModel
            {
                ClassId = classId,
                SectionId = sectionId,
                AcademicYearId = academicYearId,
                ClassName = _classService.GetById(classId)?.ClassName,
                SectionName = _sectionService.GetById(sectionId)?.SectionName,
                AcademicYearName = _academicYearService.GetById(academicYearId)?.AcademicYearName,
                EffectiveFrom = DateTime.Today,
                IsActive = true
            };

            // Get the latest timetable setup
            var setup = _timeTableSetupService.GetAll()
                .OrderByDescending(s => s.CreatedOn)
                .FirstOrDefault();

            if (setup == null)
            {
                TempData["ErrorMessage"] = "No timetable setup found. Please create a timetable setup first.";
                return RedirectToAction("Index");
            }

            // Get all periods for the setup
            var periods = _timeTablePeriodService.GetBySetupId(setup.Id)
                .OrderBy(p => p.PeriodNumber)
                .ToList();

            if (!periods.Any())
            {
                TempData["ErrorMessage"] = "No periods found for the timetable setup. Please configure periods first.";
                return RedirectToAction("Index");
            }

            // Initialize days (Monday to Friday)
            var days = new[] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" };
            for (int i = 0; i < days.Length; i++)
            {
                var day = new TimeTableDayViewModel
                {
                    DayId = i + 1,
                    DayName = days[i],
                    Periods = periods.Select(p => new TimeTablePeriodViewModel
                    {
                        Id = p.Id,
                        PeriodNumber = p.PeriodNumber,
                        StartTime = p.StartTime,
                        EndTime = p.EndTime,
                        IsBreak = p.IsBreak,
                        BreakName = p.BreakName
                    }).ToList()
                };

                timeTable.Days.Add(day);
            }

            // Get subjects for the class
            ViewBag.Subjects = _subjectService.GetByClassId(classId)
                .Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.SubjectName })
                .ToList();

            // Get teachers
            ViewBag.Teachers = _teacherService.GetAllActive()
                .Select(t => new SelectListItem { Value = t.Id.ToString(), Text = $"{t.FirstName} {t.LastName}" })
                .ToList();

            return View(timeTable);
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
                // Get current user ID (you'll need to implement this based on your authentication)
                var currentUserId = GetCurrentUserId();
                var currentDate = DateTime.UtcNow;

                // Get the current session ID
                var currentSession = _academicYearService.GetById(model.AcademicYearId);
                if (currentSession == null)
                {
                    TempData["ErrorMessage"] = "No active academic session found.";
                    return RedirectToAction("Index");
                }

                // Get company and school IDs (you'll need to implement these based on your setup)
                var companyId = GetCurrentCompanyId();
                var schoolId = GetCurrentSchoolId();

                // Delete existing timetable for this class, section and academic year
                await _timeTablePeriodService.DeleteByClassSectionAndAcademicYearAsync(
                    model.ClassId, model.SectionId, model.AcademicYearId);

                // Save the timetable periods
                foreach (var day in model.Days)
                {
                    foreach (var period in day.Periods)
                    {
                        if (!period.IsBreak && period.SubjectId.HasValue && period.TeacherId.HasValue)
                        {
                            var timeTablePeriod = new TimeTableClassPeriodDetails
                            {
                                Id = Guid.NewGuid(),
                                ClassId = model.ClassId,
                                SectionId = model.SectionId,
                                SubjectId = period.SubjectId.Value,
                                PeriodId = period.Id,
                                DayOfWeek = day.DayId,
                                SessionId = currentSession.Id,
                                CompanyId = companyId,
                                SchoolId = schoolId,
                                IsActive = model.IsActive,
                                IsDeleted = false,
                                CreatedBy = currentUserId,
                                CreatedDate = currentDate,
                                Status = "COM",
                                StatusMessage = "Completed"
                            };

                            // Save the period
                            await _timeTablePeriodService.SaveAsync(timeTablePeriod);
                        }
                    }
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
                var model = new TimeTableViewModel
                {
                    ClassId = classId,
                    SectionId = sectionId,
                    AcademicYearId = academicYearId,
                    ClassName = _classService.GetById(classId)?.ClassName,
                    SectionName = _sectionService.GetById(sectionId)?.SectionName,
                    AcademicYearName = _academicYearService.GetById(academicYearId)?.AcademicYearName,
                    Days = new List<TimeTableDayViewModel>()
                };

                // Get the latest timetable setup
                var setup = _timeTableSetupService.GetAll()
                    .OrderByDescending(s => s.CreatedOn)
                    .FirstOrDefault();

                if (setup == null)
                {
                    TempData["ErrorMessage"] = "No timetable setup found.";
                    return RedirectToAction("Index");
                }

                // Get all periods for the setup
                var periods = _timeTablePeriodService.GetBySetupId(setup.Id)
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
                            SubjectName = savedPeriod?.Subject?.SubjectName,
                            TeacherId = savedPeriod?.TeacherId,
                            TeacherName = savedPeriod?.Teacher != null 
                                ? $"{savedPeriod.Teacher.FirstName} {savedPeriod.Teacher.LastName}" 
                                : null,
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
                SectionName = _sectionService.GetById(sectionId)?.SectionName,
                AcademicYearName = _academicYearService.GetById(academicYearId)?.AcademicYearName
            };

            return View(model);
        }
    }
}
