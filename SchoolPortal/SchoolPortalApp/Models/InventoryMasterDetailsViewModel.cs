using System;

namespace SchoolPortalApp.Models
{
    public class InventoryMasterDetailsViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ItemName { get; set; } = string.Empty;
        public string LocationName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal CostPerItem { get; set; }
        public bool IsActive { get; set; }
    }
}