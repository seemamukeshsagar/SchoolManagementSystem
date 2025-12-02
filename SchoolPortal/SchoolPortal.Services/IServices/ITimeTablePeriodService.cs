using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SchoolPortal.Entities.Models;

namespace SchoolPortal.Services.IServices
{
    public interface ITimeTablePeriodService
    {
        /// <summary>
        /// Gets all timetable periods
        /// </summary>
        /// <returns>Collection of all timetable periods</returns>
        Task<IEnumerable<TimeTableClassPeriodDetails>> GetAllAsync();

        /// <summary>
        /// Gets a specific timetable period by ID (synchronous version)
        /// </summary>
        /// <param name="id">The period ID</param>
        /// <returns>The timetable period or null if not found</returns>
        TimeTableClassPeriodDetails? GetById(Guid id);

        /// <summary>
        /// Creates a new timetable period
        /// </summary>
        /// <param name="period">The period to create</param>
        /// <returns>The created period with generated ID</returns>
        Task<TimeTableClassPeriodDetails> CreateAsync(TimeTableClassPeriodDetails period);

        /// <summary>
        /// Updates an existing timetable period
        /// </summary>
        /// <param name="period">The period to update</param>
        /// <returns>True if update was successful, false otherwise</returns>
        Task<bool> UpdateAsync(TimeTableClassPeriodDetails period);

        /// <summary>
        /// Deletes a timetable period by ID
        /// </summary>
        /// <param name="id">The ID of the period to delete</param>
        /// <returns>True if deletion was successful, false otherwise</returns>
        Task<bool> DeleteAsync(Guid id);
        /// <summary>
        /// Saves a single timetable period
        /// </summary>
        /// <param name="period">The period to save</param>
        /// <returns>Task representing the asynchronous operation</returns>
        Task SaveAsync(TimeTableClassPeriodDetails period);

        /// <summary>
        /// Saves multiple timetable periods in a single transaction
        /// </summary>
        /// <param name="periods">Collection of periods to save</param>
        /// <returns>Task representing the asynchronous operation</returns>
        Task SaveBulkAsync(IEnumerable<TimeTableClassPeriodDetails> periods);

        /// <summary>
        /// Deletes all timetable periods for a specific class, section, and academic year
        /// </summary>
        /// <param name="classId">The class ID</param>
        /// <param name="sectionId">The section ID</param>
        /// <param name="academicYearId">The academic year ID</param>
        /// <returns>Task representing the asynchronous operation</returns>
        Task<bool> DeleteByClassSectionAndAcademicYearAsync(Guid classId, Guid sectionId, Guid academicYearId, Guid userId);

        /// <summary>
        /// Gets all timetable periods for a specific class, section, and academic year
        /// </summary>
        /// <param name="classId">The class ID</param>
        /// <param name="sectionId">The section ID</param>
        /// <param name="academicYearId">The academic year ID</param>
        /// <returns>Collection of timetable periods</returns>
        Task<IEnumerable<TimeTableClassPeriodDetails>> GetByClassSectionAndAcademicYearAsync(
            Guid classId, Guid sectionId, Guid academicYearId);

        /// <summary>
        /// Gets a specific timetable period by ID
        /// </summary>
        /// <param name="id">The period ID</param>
        /// <returns>The timetable period or null if not found</returns>
        Task<TimeTableClassPeriodDetails?> GetByIdAsync(Guid id);

        /// <summary>
        /// Gets all timetable periods for a specific teacher
        /// </summary>
        /// <param name="teacherId">The teacher ID</param>
        /// <returns>Collection of timetable periods</returns>
        Task<IEnumerable<TimeTableClassPeriodDetails>> GetByTeacherIdAsync(Guid teacherId);

        /// <summary>
        /// Gets all timetable periods for a specific subject
        /// </summary>
        /// <param name="subjectId">The subject ID</param>
        /// <returns>Collection of timetable periods</returns>
        Task<IEnumerable<TimeTableClassPeriodDetails>> GetBySubjectIdAsync(Guid subjectId);

        /// <summary>
        /// Checks if a teacher is available at a specific time
        /// </summary>
        /// <param name="teacherId">The teacher ID</param>
        /// <param name="dayOfWeek">The day of week (1-7, where 1 is Monday)</param>
        /// <param name="startTime">The start time</param>
        /// <param name="endTime">The end time</param>
        /// <param name="excludePeriodId">Optional period ID to exclude from the check (for updates)</param>
        /// <returns>True if the teacher is available, false otherwise</returns>
        Task<bool> IsTeacherAvailableAsync(
            Guid teacherId, 
            int dayOfWeek, 
            TimeSpan startTime, 
            TimeSpan endTime, 
            Guid? excludePeriodId = null);

        /// <summary>
        /// Checks if a classroom is available at a specific time
        /// </summary>
        /// <param name="classroomId">The classroom ID</param>
        /// <param name="dayOfWeek">The day of week (1-7, where 1 is Monday)</param>
        /// <param name="startTime">The start time</param>
        /// <param name="endTime">The end time</param>
        /// <param name="excludePeriodId">Optional period ID to exclude from the check (for updates)</param>
        /// <returns>True if the classroom is available, false otherwise</returns>
        Task<bool> IsClassroomAvailableAsync(
            Guid classroomId, 
            int dayOfWeek, 
            TimeSpan startTime, 
            TimeSpan endTime, 
            Guid? excludePeriodId = null);

        /// <summary>
        /// Gets a timetable period by its setup ID
        /// </summary>
        /// <param name="setupId">The setup ID to search for</param>
        /// <returns>The timetable period or null if not found</returns>
        Task<TimeTableClassPeriodDetails> GetBySetupIdAsync(Guid setupId);
    }
}
