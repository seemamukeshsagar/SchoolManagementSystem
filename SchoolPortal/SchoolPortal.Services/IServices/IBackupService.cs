using System;
using System.Threading.Tasks;

namespace SchoolPortal.Services.IServices
{
    public interface IBackupService
    {
        Task<bool> CreateBackupAsync(string? backupName = null, bool includeMedia = true);
        Task<bool> RestoreBackupAsync(string backupPath);
        Task<bool> DeleteBackupAsync(string backupPath);
        Task<BackupInfo[]> GetAvailableBackupsAsync();
    }

    public class BackupInfo
    {
        public string Name { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public long SizeInBytes { get; set; }
        public string FormattedSize => FormatFileSize(SizeInBytes);

        private string FormatFileSize(long bytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            int order = 0;
            double size = bytes;
            while (size >= 1024 && order < sizes.Length - 1)
            {
                order++;
                size /= 1024;
            }
            return $"{size:0.##} {sizes[order]}";
        }
    }
}
