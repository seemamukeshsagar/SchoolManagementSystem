using System;

namespace SchoolPortal.Web.Models
{
    public class NonTeachingDocumentViewModel
    {
        public Guid Id { get; set; } = Guid.Empty;
        public Guid NonTeachingId { get; set; } = Guid.Empty;
        public string DocumentType { get; set; } = string.Empty;
        public Guid DocumentTypeId { get; set; } = Guid.Empty;
        public string DocumentNumber { get; set; } = string.Empty;
        public string DocumentPath { get; set; } = string.Empty;
        public DateTime? IssueDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string Remarks { get; set; } = string.Empty;
        public bool IsVerified { get; set; } = false;
        public Guid VerifiedBy { get; set; } = Guid.Empty;
        public DateTime? VerifiedOn { get; set; }
        public byte[] FileContent { get; set; } = Array.Empty<byte>();
        public string FileType { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public Guid CreatedBy { get; set; } = Guid.Empty;
        public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;
        public Guid ModifiedBy { get; set; } = Guid.Empty;
        public DateTime? ModifiedDate { get; set; } = DateTime.UtcNow;

        // Helper property to display file size
        public string FileSize
        {
            get
            {
                if (FileContent == null) return "0 KB";
                var size = FileContent.Length / 1024f;
                return size < 1024 ? $"{size:0.##} KB" : $"{size / 1024f:0.##} MB";
            }
        }

        // Helper property to get file icon based on file type
        public string FileIcon
        {
            get
            {
                if (string.IsNullOrEmpty(FileType)) return "fa-file";

                return FileType.ToLower() switch
                {
                    var type when type.Contains("pdf") => "fa-file-pdf",
                    var type when type.Contains("word") => "fa-file-word",
                    var type when type.Contains("excel") => "fa-file-excel",
                    var type when type.Contains("image") => "fa-file-image",
                    _ => "fa-file"
                };
            }
        }
    }
}