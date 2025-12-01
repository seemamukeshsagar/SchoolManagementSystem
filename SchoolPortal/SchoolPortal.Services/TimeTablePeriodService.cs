using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SchoolPortal.Entities.Models;
using SchoolPortal.Services.IServices;

namespace SchoolPortal.Services
{
    public class TimeTablePeriodService : ITimeTablePeriodService
    {
        private readonly SchoolPortalDbContext _context;
        private readonly ILogger<TimeTablePeriodService> _logger;

        public TimeTablePeriodService(
            SchoolPortalDbContext context,
            ILogger<TimeTablePeriodService> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task SaveAsync(TimeTableClassPeriodDetails period)
        {
            try
            {
                if (period == null)
                    throw new ArgumentNullException(nameof(period));

                if (period.Id == Guid.Empty)
                {
                    period.Id = Guid.NewGuid();
                    _context.TimeTableClassPeriodDetails.Add(period);
                }
                else
                {
                    _context.TimeTableClassPeriodDetails.Update(period);
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving timetable period");
                throw;
            }
        }

        public async Task SaveBulkAsync(IEnumerable<TimeTableClassPeriodDetails> periods)
        {
            if (periods == null || !periods.Any())
                return;

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                foreach (var period in periods)
                {
                    if (period.Id == Guid.Empty)
                    {
                        period.Id = Guid.NewGuid();
                        _context.TimeTableClassPeriodDetails.Add(period);
                    }
                    else
                    {
                        _context.TimeTableClassPeriodDetails.Update(period);
                    }
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error saving bulk timetable periods");
                throw;
            }
        }

        public async Task DeleteByClassSectionAndAcademicYearAsync(Guid classId, Guid sectionId, Guid academicYearId)
        {
            try
            {
                var periods = await _context.TimeTableClassPeriodDetails
                    .Where(p => p.ClassId == classId && 
                               p.SectionId == sectionId && 
                               p.SessionId == academicYearId)
                    .ToListAsync();

                if (periods.Any())
                {
                    _context.TimeTableClassPeriodDetails.RemoveRange(periods);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting timetable periods");
                throw;
            }
        }

        public async Task<IEnumerable<TimeTableClassPeriodDetails>> GetByClassSectionAndAcademicYearAsync(
            Guid classId, Guid sectionId, Guid academicYearId)
        {
            try
            {
                return await _context.TimeTableClassPeriodDetails
                    .Include(p => p.Subject)
                    .Include(p => p.Teacher)
                    .Where(p => p.ClassId == classId && 
                               p.SectionId == sectionId && 
                               p.SessionId == academicYearId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting timetable periods by class, section and academic year");
                throw;
            }
        }

        public async Task<TimeTableClassPeriodDetails> GetByIdAsync(Guid id)
        {
            try
            {
                return await _context.TimeTableClassPeriodDetails
                    .Include(p => p.Subject)
                    .Include(p => p.Teacher)
                    .FirstOrDefaultAsync(p => p.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting timetable period by ID: {id}");
                throw;
            }
        }

        public async Task<IEnumerable<TimeTableClassPeriodDetails>> GetByTeacherIdAsync(Guid teacherId)
        {
            try
            {
                return await _context.TimeTableClassPeriodDetails
                    .Include(p => p.Subject)
                    .Include(p => p.Teacher)
                    .Where(p => p.TeacherId == teacherId)
                    .OrderBy(p => p.DayOfWeek)
                    .ThenBy(p => p.CreatedDate)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting timetable periods by teacher ID: {teacherId}");
                throw;
            }
        }

        public async Task<IEnumerable<TimeTableClassPeriodDetails>> GetBySubjectIdAsync(Guid subjectId)
        {
            try
            {
                return await _context.TimeTableClassPeriodDetails
                    .Include(p => p.Teacher)
                    .Where(p => p.SubjectId == subjectId)
                    .OrderBy(p => p.DayOfWeek)
                    .ThenBy(p => p.CreatedDate)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting timetable periods by subject ID: {subjectId}");
                throw;
            }
        }

        public async Task<bool> IsTeacherAvailableAsync(
            Guid teacherId, 
            int dayOfWeek, 
            TimeSpan startTime, 
            TimeSpan endTime, 
            Guid? excludePeriodId = null)
        {
            try
            {
                var query = _context.TimeTableClassPeriodDetails
                    .Include(p => p.Period)
                    .Where(p => p.TeacherId == teacherId && 
                               p.DayOfWeek == dayOfWeek &&
                               p.Id != excludePeriodId);

                return !await query.AnyAsync(p => 
                    (startTime >= p.Period.StartTime && startTime < p.Period.EndTime) ||
                    (endTime > p.Period.StartTime && endTime <= p.Period.EndTime) ||
                    (startTime <= p.Period.StartTime && endTime >= p.Period.EndTime));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error checking teacher availability for teacher ID: {teacherId}");
                throw;
            }
        }

        public async Task<bool> IsClassroomAvailableAsync(
            Guid classroomId, 
            int dayOfWeek, 
            TimeSpan startTime, 
            TimeSpan endTime, 
            Guid? excludePeriodId = null)
        {
            try
            {
                // Note: This assumes you have a ClassroomId field in TimeTableClassPeriodDetails
                // If not, you'll need to adjust this query based on your actual schema
                var query = _context.TimeTableClassPeriodDetails
                    .Include(p => p.Period)
                    .Where(p => p.ClassId == classroomId && // Adjust this based on your schema
                               p.DayOfWeek == dayOfWeek &&
                               p.Id != excludePeriodId);

                return !await query.AnyAsync(p => 
                    (startTime >= p.Period.StartTime && startTime < p.Period.EndTime) ||
                    (endTime > p.Period.StartTime && endTime <= p.Period.EndTime) ||
                    (startTime <= p.Period.StartTime && endTime >= p.Period.EndTime));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error checking classroom availability for classroom ID: {classroomId}");
                throw;
            }
        }
    }
}
