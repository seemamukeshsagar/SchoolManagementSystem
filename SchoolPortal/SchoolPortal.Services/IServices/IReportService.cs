using SchoolPortal.DTOs;
using SchoolPortal.DTOs.Reports;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolPortal.Services
{
    public interface IReportService
    {
        Task<PagedResult<object>> GetEmployeeLeaves(EmployeeLeaveFilterDto filter);
        Task<PagedResult<object>> GetStudents(StudentReportFilterDto filter);
        Task<object> GetFeeCollection(FeeCollectionFilterDto filter);
        Task<PagedResult<object>> GetInventoryItems(InventoryItemFilterDto filter);
        Task<IEnumerable<object>> GetItemStockMovement(int itemId);
        Task<byte[]> ExportEmployeeLeaves(EmployeeLeaveFilterDto filter);
    }
}