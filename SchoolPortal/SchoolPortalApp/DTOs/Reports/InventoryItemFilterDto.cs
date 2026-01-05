namespace SchoolPortalApp.DTOs.Reports
{
    public class InventoryItemFilterDto : ReportFilterDto
    {
        public int? CategoryId { get; set; }
        public int? SupplierId { get; set; }
        public string Status { get; set; } // in_stock, low_stock, out_of_stock
    }
}