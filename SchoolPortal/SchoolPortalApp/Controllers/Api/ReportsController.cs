using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolPortal.Services;
using SchoolPortal.DTOs.Reports;
using System;
using System.Threading.Tasks;

namespace SchoolPortalApp.Controllers.Api
{
    [Authorize]
    [Route("api/reports")]
    [ApiController]
    public class ReportsController : BaseApiController
    {
        private readonly IReportService _reportService;
        
        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpPost("employee-leaves")]
        public async Task<IActionResult> GetEmployeeLeaves([FromBody] EmployeeLeaveFilterDto filter)
        {
            var result = await _reportService.GetEmployeeLeaves(filter);
            return HandlePagedResult(result);
        }

        [HttpGet("export-employee-leaves")]
        public async Task<IActionResult> ExportEmployeeLeaves([FromQuery] EmployeeLeaveFilterDto filter)
        {
            var content = await _reportService.ExportEmployeeLeaves(filter);
            return File(
                content,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", 
                $"EmployeeLeaves_{DateTime.Now:yyyyMMddHHmmss}.xlsx"
            );
        }

        [HttpPost("students")]
        public async Task<IActionResult> GetStudents([FromBody] StudentReportFilterDto filter)
        {
            var result = await _reportService.GetStudents(filter);
            return HandlePagedResult(result);
        }

        [HttpPost("fee-collection")]
        public async Task<IActionResult> GetFeeCollection([FromBody] FeeCollectionFilterDto filter)
        {
            var result = await _reportService.GetFeeCollection(filter);
            return Ok(result);
        }

        [HttpGet("inventory/items")]
        public async Task<IActionResult> GetInventoryItems([FromQuery] InventoryItemFilterDto filter)
        {
            var result = await _reportService.GetInventoryItems(filter);
            return HandlePagedResult(result);
        }

        [HttpGet("inventory/items/{itemId}/stock-movement")]
        public async Task<IActionResult> GetItemStockMovement(int itemId)
        {
            var result = await _reportService.GetItemStockMovement(itemId);
            return Ok(result);
        }
    }
}