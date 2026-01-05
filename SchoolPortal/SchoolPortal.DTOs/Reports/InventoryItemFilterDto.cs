namespace SchoolPortal.DTOs.Reports
{
    public class InventoryItemFilterDto
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int? CategoryId { get; set; }
        public string SearchTerm { get; set; }
        public string Status { get; set; }
    }
}
