using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolPortal.Entities.Models
{
    [Table("AuditLogs")]
    public class AuditLog
    {
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Action { get; set; }
        
        [MaxLength(1000)]
        public string Description { get; set; }
        
        [Required]
        public string UserId { get; set; }
        
        [MaxLength(100)]
        public string IpAddress { get; set; }
        
        [Required]
        public DateTime Timestamp { get; set; }
    }
}
