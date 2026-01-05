using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Microsoft.Extensions.Logging;
using SchoolPortal.DBAccess;
using SchoolPortal.DTOs;
using SchoolPortal.DTOs.Reports;

namespace SchoolPortal.Services
{
    public class ReportService : IReportService
    {
        private readonly ILogger<ReportService> _logger;

        public ReportService(ILogger<ReportService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<PagedResult<object>> GetEmployeeLeaves(EmployeeLeaveFilterDto filter)
        {
            var result = new PagedResult<object>();
            var items = new List<object>();

            using (Proc p = new Proc("Report_GetEmployeeLeaves"))
            {
                p["PageNumber"] = filter.PageNumber;
                p["PageSize"] = filter.PageSize;
                p["Department"] = DBNull.Value; // No Department property in EmployeeLeaveFilterDto
                p["LeaveType"] = filter.LeaveTypeId.HasValue ? (object)filter.LeaveTypeId.Value : DBNull.Value;
                p["Status"] = (object)filter.Status ?? DBNull.Value;
                p["StartDate"] = (object)filter.FromDate ?? DBNull.Value;
                p["EndDate"] = (object)filter.ToDate ?? DBNull.Value;

                using (var ds = new DataSet())
                {
                    p.Exec(ds);
                    
                    // First table contains the data
                    if (ds.Tables.Count > 0)
                    {
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            items.Add(new
                            {
                                Id = row.Field<int>("Id"),
                                EmployeeId = row.Field<int>("EmployeeId"),
                                EmployeeName = row.Field<string>("EmployeeName"),
                                Department = row.Field<string>("Department"),
                                LeaveType = row.Field<string>("LeaveType"),
                                FromDate = row.Field<DateTime>("FromDate"),
                                ToDate = row.Field<DateTime>("ToDate"),
                                Days = row.Field<int>("Days"),
                                Status = row.Field<string>("Status"),
                                Reason = row.Field<string>("Reason"),
                                ApprovedDate = row.IsNull("ApprovedDate") ? (DateTime?)null : row.Field<DateTime>("ApprovedDate"),
                                ApprovedBy = row.Field<string>("ApprovedBy")
                            });
                        }
                    }

                    // Second table contains the total count
                    if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                    {
                        result.TotalCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
                    }
                }
            }

            result.Items = items;
            result.PageNumber = filter.PageNumber;
            result.PageSize = filter.PageSize;

            return result;
        }

        public async Task<byte[]> ExportEmployeeLeaves(EmployeeLeaveFilterDto filter)
        {
            var items = new List<dynamic>();

            using (Proc p = new Proc("Report_ExportEmployeeLeaves"))
            {
                p["Department"] = DBNull.Value; // No Department property in EmployeeLeaveFilterDto
                p["LeaveType"] = filter.LeaveTypeId.HasValue ? (object)filter.LeaveTypeId.Value : DBNull.Value;
                p["Status"] = (object)filter.Status ?? DBNull.Value;
                p["StartDate"] = (object)filter.FromDate ?? DBNull.Value;
                p["EndDate"] = (object)filter.ToDate ?? DBNull.Value;

                using (var dt = new DataTable())
                {
                    p.Exec(dt);
                    foreach (DataRow row1 in dt.Rows)
                    {
                        items.Add(new
                        {
                            EmployeeId = row1.Field<int>("EmployeeId"),
                            EmployeeName = row1.Field<string>("EmployeeName"),
                            Department = row1.Field<string>("Department"),
                            LeaveType = row1.Field<string>("LeaveType"),
                            FromDate = row1.Field<DateTime>("FromDate").ToString("d"),
                            ToDate = row1.Field<DateTime>("ToDate").ToString("d"),
                            Days = (row1.Field<DateTime>("ToDate") - row1.Field<DateTime>("FromDate")).Days + 1,
                            Status = row1.Field<string>("Status"),
                            Reason = row1.Field<string>("Reason"),
                            ApprovedDate = row1.IsNull("ApprovedDate") ? string.Empty : row1.Field<DateTime>("ApprovedDate").ToString("d"),
                            ApprovedBy = row1.Field<string>("ApprovedBy")
                        });
                    }
                }
            }

            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Employee Leaves");
            
            // Add headers
            var headers = new string[] { "Employee ID", "Employee Name", "Department", "Leave Type", 
                "From Date", "To Date", "Days", "Status", "Reason", "Approved Date", "Approved By" };
            
            for (int i = 0; i < headers.Length; i++)
            {
                worksheet.Cell(1, i + 1).Value = headers[i];
            }

            var headerRange = worksheet.Range(1, 1, 1, headers.Length);
            headerRange.Style.Font.Bold = true;
            headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;
            headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            int row = 2;
            foreach (var item in items)
            {
                worksheet.Cell(row, 1).Value = item.EmployeeId;
                worksheet.Cell(row, 2).Value = item.EmployeeName;
                worksheet.Cell(row, 3).Value = item.Department;
                worksheet.Cell(row, 4).Value = item.LeaveType;
                worksheet.Cell(row, 5).Value = item.FromDate;
                worksheet.Cell(row, 6).Value = item.ToDate;
                worksheet.Cell(row, 7).Value = item.Days;
                worksheet.Cell(row, 8).Value = item.Status;
                worksheet.Cell(row, 9).Value = item.Reason;
                worksheet.Cell(row, 10).Value = item.ApprovedDate;
                worksheet.Cell(row, 11).Value = item.ApprovedBy;
                row++;
            }

            worksheet.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            return stream.ToArray();
        }

        public async Task<PagedResult<object>> GetStudents(StudentReportFilterDto filter)
        {
            // TODO: Implement student report logic
            throw new NotImplementedException();
        }

        public async Task<object> GetFeeCollection(FeeCollectionFilterDto filter)
        {
            // TODO: Implement fee collection report logic
            throw new NotImplementedException();
        }

        public async Task<PagedResult<object>> GetInventoryItems(InventoryItemFilterDto filter)
        {
            // TODO: Implement inventory items report logic
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<object>> GetItemStockMovement(int itemId)
        {
            // TODO: Implement item stock movement report logic
            throw new NotImplementedException();
        }
    }
}