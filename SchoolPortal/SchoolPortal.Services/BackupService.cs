using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SchoolPortal.Services.IServices;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolPortal.Services
{
    public class BackupService : IBackupService
    {
        private readonly ILogger<BackupService> _logger;
        private readonly IConfiguration _configuration;
        private readonly string _backupDirectory;

        public BackupService(ILogger<BackupService> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _backupDirectory = _configuration["BackupSettings:BackupPath"] ?? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "SchoolPortal", "Backups");
            
            // Ensure backup directory exists
            if (!Directory.Exists(_backupDirectory))
            {
                Directory.CreateDirectory(_backupDirectory);
            }
        }

        public async Task<bool> CreateBackupAsync(string? backupName = null, bool includeMedia = true)
        {
            try
            {
                _logger.LogInformation("Starting database backup process");

                // Generate backup name if not provided
                if (string.IsNullOrEmpty(backupName))
                {
                    backupName = $"Backup_{DateTime.Now:yyyyMMdd_HHmmss}";
                }

                // Get connection string from configuration
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                if (string.IsNullOrEmpty(connectionString))
                {
                    _logger.LogError("Database connection string not found");
                    return false;
                }

                // Parse connection string to get database name
                var builder = new Microsoft.Data.SqlClient.SqlConnectionStringBuilder(connectionString);
                var databaseName = builder.InitialCatalog;
                var serverName = builder.DataSource;

                // Create backup file path
                var backupFilePath = Path.Combine(_backupDirectory, $"{backupName}.bak");

                // Build SQL backup command
                var backupCommand = $"BACKUP DATABASE [{databaseName}] TO DISK = '{backupFilePath}' WITH INIT, NAME = '{backupName}', STATS = 10";

                // Execute backup command
                var success = await ExecuteSqlBackupCommand(backupCommand);

                if (success)
                {
                    _logger.LogInformation($"Database backup successfully created at: {backupFilePath}");
                    
                    // If media files need to be included, create a zip with database backup
                    if (includeMedia)
                    {
                        await IncludeMediaFiles(backupFilePath, backupName);
                    }

                    return true;
                }
                else
                {
                    _logger.LogError("Database backup failed");
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating database backup");
                return false;
            }
        }

        public async Task<bool> RestoreBackupAsync(string backupPath)
        {
            try
            {
                _logger.LogInformation($"Starting database restore from: {backupPath}");

                if (!File.Exists(backupPath))
                {
                    _logger.LogError($"Backup file not found: {backupPath}");
                    return false;
                }

                // Get connection string
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                var builder = new Microsoft.Data.SqlClient.SqlConnectionStringBuilder(connectionString);
                var databaseName = builder.InitialCatalog;

                // Build SQL restore command
                var restoreCommand = $"RESTORE DATABASE [{databaseName}] FROM DISK = '{backupPath}' WITH REPLACE, STATS = 10";

                // Execute restore command
                var success = await ExecuteSqlBackupCommand(restoreCommand);

                if (success)
                {
                    _logger.LogInformation("Database restore completed successfully");
                    return true;
                }
                else
                {
                    _logger.LogError("Database restore failed");
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error restoring database backup");
                return false;
            }
        }

        public async Task<bool> DeleteBackupAsync(string backupPath)
        {
            try
            {
                if (File.Exists(backupPath))
                {
                    File.Delete(backupPath);
                    _logger.LogInformation($"Backup file deleted: {backupPath}");
                    return true;
                }
                else
                {
                    _logger.LogWarning($"Backup file not found for deletion: {backupPath}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting backup file: {backupPath}");
                return false;
            }
        }

        public async Task<BackupInfo[]> GetAvailableBackupsAsync()
        {
            try
            {
                if (!Directory.Exists(_backupDirectory))
                {
                    return new BackupInfo[0];
                }

                var backupFiles = Directory.GetFiles(_backupDirectory, "*.bak")
                    .Select(file => new FileInfo(file))
                    .Select(info => new BackupInfo
                    {
                        Name = Path.GetFileNameWithoutExtension(info.Name),
                        Path = info.FullName,
                        CreatedDate = info.CreationTime,
                        SizeInBytes = info.Length
                    })
                    .OrderByDescending(b => b.CreatedDate)
                    .ToArray();

                return backupFiles;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving available backups");
                return new BackupInfo[0];
            }
        }

        private async Task<bool> ExecuteSqlBackupCommand(string sqlCommand)
        {
            try
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                var builder = new Microsoft.Data.SqlClient.SqlConnectionStringBuilder(connectionString);
                
                // Use master database for backup/restore operations
                builder.InitialCatalog = "master";
                var masterConnectionString = builder.ToString();

                using (var connection = new Microsoft.Data.SqlClient.SqlConnection(masterConnectionString))
                {
                    await connection.OpenAsync();
                    
                    using (var command = new Microsoft.Data.SqlClient.SqlCommand(sqlCommand, connection))
                    {
                        command.CommandTimeout = 0; // No timeout for backup/restore operations
                        await command.ExecuteNonQueryAsync();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error executing SQL command: {sqlCommand}");
                return false;
            }
        }

        private async Task IncludeMediaFiles(string backupFilePath, string backupName)
        {
            try
            {
                // This is a placeholder for media file backup functionality
                // In a real implementation, you would:
                // 1. Identify media directories (wwwroot/uploads, etc.)
                // 2. Create a zip file containing both database backup and media files
                // 3. Replace the original .bak file with the zip file
                
                _logger.LogInformation("Media files backup inclusion not yet implemented");
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error including media files in backup");
            }
        }
    }
}
