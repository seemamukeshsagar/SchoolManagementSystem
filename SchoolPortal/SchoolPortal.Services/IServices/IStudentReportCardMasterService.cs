using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SchoolPortal.Entities.Models;

namespace SchoolPortal.Services.IServices
{
    public interface IStudentReportCardMasterService
    {
        List<StudentReportCardMaster> GetAll();
        StudentReportCardMaster? GetById(Guid id);
        Task<StudentReportCardMaster?> GetByIdAsync(Guid id);
        Guid Create(StudentReportCardMaster reportCard);
        Task<Guid> CreateAsync(StudentReportCardMaster reportCard);
        bool Update(StudentReportCardMaster reportCard);
        Task<bool> UpdateAsync(StudentReportCardMaster reportCard);
        bool Delete(Guid id);
        Task<bool> DeleteAsync(Guid id);
    }
}
