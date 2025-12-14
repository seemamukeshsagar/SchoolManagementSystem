using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SchoolPortal.Entities.Models;
using System.Data;

namespace SchoolPortal.Services.IServices
{
    public interface IStudentService
    {
        // Existing methods
        Task<List<StudentMaster>> GetAllAsync(Guid? schoolId = null);
        List<StudentMaster> GetAll(Guid? schoolId = null);
        Task<StudentMaster?> GetByIdAsync(Guid id);
        Task<Guid> CreateAsync(StudentMaster student);
        Task<bool> UpdateAsync(StudentMaster student);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> CategoryExistsAsync(Guid categoryId);
        Task<StudentAttendanceDetails?> GetStudentAttendanceByIdAsync(Guid id);
        Task<Guid> CreateStudentAttendanceAsync(StudentAttendanceDetails attendance);
        Task<bool> UpdateStudentAttendanceAsync(StudentAttendanceDetails attendance);
        StudentMaster GetById(Guid id);
        Guid Create(StudentMaster student);
        bool Update(StudentMaster student);
        bool Delete(Guid id);

        // New methods
        Task<IEnumerable<StudentMaster>> SearchStudentsAsync(StudentSearchCriteria criteria);
        Task<StudentStats> GetStudentStatisticsAsync(Guid? schoolId = null);
        Task<bool> BulkUpdateStatusAsync(IEnumerable<Guid> studentIds, bool isActive);

    }

    public class StudentSearchCriteria
    {
        public string? SearchTerm { get; set; }
        public Guid? SchoolId { get; set; }
        public Guid? ClassId { get; set; }
        public bool? IsActive { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }

    public class StudentStats
    {
        public int TotalStudents { get; set; }
        public int ActiveStudents { get; set; }
        public int NewThisMonth { get; set; }
        public int InactiveStudents { get; set; }
        public int GraduatedThisYear { get; set; }
    }
}
