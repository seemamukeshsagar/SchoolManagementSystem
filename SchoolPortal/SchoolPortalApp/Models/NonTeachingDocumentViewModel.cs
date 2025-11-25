using System;

namespace SchoolPortal.Web.Models
{
    public class NonTeachingDocumentViewModel
    {
        public Guid Id { get; set; }
        public Guid NonTeachingId { get; set; }
        public string DocumentType { get; set; }
        public Guid DocumentTypeId { get; set; }
        public string DocumentNumber { get; set; }
        public string DocumentPath { get; set; }
        public DateTime? IssueDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string Remarks { get; set; }
        public bool IsVerified { get; set; }
        public Guid VerifiedBy { get; set; }
        public DateTime? VerifiedOn { get; set; }
        public byte[] FileContent { get; set; }
        public string FileType { get; set; }
        public string FileName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; } = true;
        public Guid CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

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