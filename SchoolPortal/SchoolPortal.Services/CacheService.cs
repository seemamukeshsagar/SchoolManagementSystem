using Microsoft.Extensions.Caching.Memory;
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
    public class CacheService : ICacheService
    {
        private readonly ILogger<CacheService> _logger;
        private readonly IMemoryCache _memoryCache;
        private readonly IConfiguration _configuration;

        public CacheService(
            ILogger<CacheService> logger,
            IMemoryCache memoryCache,
            IConfiguration configuration)
        {
            _logger = logger;
            _memoryCache = memoryCache;
            _configuration = configuration;
        }

        public async Task<bool> ClearSystemCacheAsync()
        {
            try
            {
                _logger.LogInformation("Starting system cache clearing");

                // Clear in-memory cache
                ClearMemoryCache();

                // Clear application cache directories
                await ClearApplicationCacheAsync();

                // Clear browser cache (if applicable)
                await ClearBrowserCacheAsync();

                _logger.LogInformation("System cache cleared successfully");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error clearing system cache");
                return false;
            }
        }

        public async Task<bool> ClearTemporaryFilesAsync()
        {
            try
            {
                _logger.LogInformation("Starting temporary files cleanup");

                // Clear system temp files
                await ClearSystemTempFilesAsync();

                // Clear application temp files
                await ClearApplicationTempFilesAsync();

                // Clear log files older than specified days
                await ClearOldLogFilesAsync();

                _logger.LogInformation("Temporary files cleanup completed successfully");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error clearing temporary files");
                return false;
            }
        }

        public async Task<CacheInfo> GetCacheInfoAsync()
        {
            try
            {
                var cacheInfo = new CacheInfo
                {
                    MemoryCacheSize = await GetMemoryCacheSizeAsync(),
                    TemporaryFilesSize = await GetTemporaryFilesSizeAsync(),
                    SessionCount = GetActiveSessionCount(),
                    LastCleared = GetLastClearedTime()
                };

                return cacheInfo;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving cache information");
                return new CacheInfo
                {
                    MemoryCacheSize = "Unknown",
                    TemporaryFilesSize = "Unknown",
                    SessionCount = "Unknown",
                    LastCleared = "Unknown"
                };
            }
        }

        private void ClearMemoryCache()
        {
            try
            {
                // Clear specific cache entries
                var keysToRemove = new[] { "UserCache", "PermissionCache", "LookupCache" };
                foreach (var key in keysToRemove)
                {
                    _memoryCache.Remove(key);
                }

                _logger.LogInformation("Memory cache cleared");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error clearing memory cache");
            }
        }

        private async Task ClearApplicationCacheAsync()
        {
            try
            {
                var wwwrootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                var cacheDirectories = new[]
                {
                    Path.Combine(wwwrootPath, "cache"),
                    Path.Combine(wwwrootPath, "temp"),
                    Path.Combine(wwwrootPath, "uploads", "temp"),
                    Path.Combine(Directory.GetCurrentDirectory(), "App_Data", "cache")
                };

                foreach (var dir in cacheDirectories)
                {
                    if (Directory.Exists(dir))
                    {
                        var files = Directory.GetFiles(dir, "*", SearchOption.AllDirectories);
                        foreach (var file in files)
                        {
                            try
                            {
                                var fileInfo = new FileInfo(file);
                                if (fileInfo.Exists && fileInfo.LastWriteTime < DateTime.Now.AddHours(-1))
                                {
                                    fileInfo.IsReadOnly = false;
                                    fileInfo.Delete();
                                }
                            }
                            catch (Exception ex)
                            {
                                _logger.LogWarning(ex, $"Could not delete cache file: {file}");
                            }
                        }
                    }
                }

                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error clearing application cache");
            }
        }

        private async Task ClearBrowserCacheAsync()
        {
            try
            {
                // This would clear browser-related cache if the application manages any
                // For now, this is a placeholder as browser cache is typically managed by the client
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error clearing browser cache");
            }
        }

        private async Task ClearSystemTempFilesAsync()
        {
            try
            {
                var tempPath = Path.GetTempPath();
                var applicationTempPath = Path.Combine(tempPath, "SchoolPortal");

                if (Directory.Exists(applicationTempPath))
                {
                    var files = Directory.GetFiles(applicationTempPath, "*", SearchOption.AllDirectories);
                    foreach (var file in files)
                    {
                        try
                        {
                            var fileInfo = new FileInfo(file);
                            if (fileInfo.Exists && fileInfo.LastWriteTime < DateTime.Now.AddDays(-1))
                            {
                                fileInfo.IsReadOnly = false;
                                fileInfo.Delete();
                            }
                        }
                        catch (Exception ex)
                        {
                            _logger.LogWarning(ex, $"Could not delete temp file: {file}");
                        }
                    }
                }

                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error clearing system temp files");
            }
        }

        private async Task ClearApplicationTempFilesAsync()
        {
            try
            {
                var tempDirectories = new[]
                {
                    Path.Combine(Directory.GetCurrentDirectory(), "temp"),
                    Path.Combine(Directory.GetCurrentDirectory(), "logs", "temp"),
                    Path.Combine(Directory.GetCurrentDirectory(), "App_Data", "temp")
                };

                foreach (var dir in tempDirectories)
                {
                    if (Directory.Exists(dir))
                    {
                        var files = Directory.GetFiles(dir, "*", SearchOption.AllDirectories);
                        foreach (var file in files)
                        {
                            try
                            {
                                var fileInfo = new FileInfo(file);
                                if (fileInfo.Exists && fileInfo.LastWriteTime < DateTime.Now.AddHours(-2))
                                {
                                    fileInfo.IsReadOnly = false;
                                    fileInfo.Delete();
                                }
                            }
                            catch (Exception ex)
                            {
                                _logger.LogWarning(ex, $"Could not delete application temp file: {file}");
                            }
                        }
                    }
                }

                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error clearing application temp files");
            }
        }

        private async Task ClearOldLogFilesAsync()
        {
            try
            {
                var logDirectories = new[]
                {
                    Path.Combine(Directory.GetCurrentDirectory(), "logs"),
                    Path.Combine(Directory.GetCurrentDirectory(), "App_Data", "logs")
                };

                var retentionDays = _configuration.GetValue<int>("Logging:RetentionDays", 30);

                foreach (var dir in logDirectories)
                {
                    if (Directory.Exists(dir))
                    {
                        var files = Directory.GetFiles(dir, "*.log", SearchOption.AllDirectories)
                            .Concat(Directory.GetFiles(dir, "*.txt", SearchOption.AllDirectories));

                        foreach (var file in files)
                        {
                            try
                            {
                                var fileInfo = new FileInfo(file);
                                if (fileInfo.Exists && fileInfo.LastWriteTime < DateTime.Now.AddDays(-retentionDays))
                                {
                                    fileInfo.IsReadOnly = false;
                                    fileInfo.Delete();
                                }
                            }
                            catch (Exception ex)
                            {
                                _logger.LogWarning(ex, $"Could not delete old log file: {file}");
                            }
                        }
                    }
                }

                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error clearing old log files");
            }
        }

        private async Task<string> GetMemoryCacheSizeAsync()
        {
            try
            {
                // Memory cache size is not directly available, so we'll estimate
                // This is a simplified approach
                await Task.CompletedTask;
                return "~" + GC.GetTotalMemory(false).ToString("N0") + " bytes";
            }
            catch
            {
                return "Unknown";
            }
        }

        private async Task<string> GetTemporaryFilesSizeAsync()
        {
            try
            {
                var tempDirectories = new[]
                {
                    Path.GetTempPath(),
                    Path.Combine(Directory.GetCurrentDirectory(), "temp"),
                    Path.Combine(Directory.GetCurrentDirectory(), "App_Data", "temp")
                };

                long totalSize = 0;
                foreach (var dir in tempDirectories)
                {
                    if (Directory.Exists(dir))
                    {
                        var files = Directory.GetFiles(dir, "*", SearchOption.AllDirectories);
                        foreach (var file in files)
                        {
                            try
                            {
                                var fileInfo = new FileInfo(file);
                                if (fileInfo.Exists)
                                {
                                    totalSize += fileInfo.Length;
                                }
                            }
                            catch
                            {
                                // Ignore files that can't be accessed
                            }
                        }
                    }
                }

                return FormatFileSize(totalSize);
            }
            catch
            {
                return "Unknown";
            }
        }

        private string GetActiveSessionCount()
        {
            try
            {
                // This would require session management integration
                // For now, return an estimated value
                return "Unknown";
            }
            catch
            {
                return "Unknown";
            }
        }

        private string GetLastClearedTime()
        {
            try
            {
                // This would require storing the last clear time
                // For now, return a default value
                return "Not recorded";
            }
            catch
            {
                return "Unknown";
            }
        }

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
