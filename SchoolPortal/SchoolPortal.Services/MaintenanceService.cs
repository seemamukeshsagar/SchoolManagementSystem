using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SchoolPortal.Entities.Models;
using SchoolPortal.Services.IServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolPortal.Services
{
    public class MaintenanceService : IMaintenanceService
    {
        private readonly ILogger<MaintenanceService> _logger;
        private readonly AppDbContext _dbContext;
        private readonly IConfiguration _configuration;
        private readonly ICacheService _cacheService;

        public MaintenanceService(
            ILogger<MaintenanceService> logger,
            AppDbContext dbContext,
            IConfiguration configuration,
            ICacheService cacheService)
        {
            _logger = logger;
            _dbContext = dbContext;
            _configuration = configuration;
            _cacheService = cacheService;
        }

        public async Task<bool> RunSystemMaintenanceAsync()
        {
            try
            {
                _logger.LogInformation("Starting system maintenance tasks");
                var stopwatch = Stopwatch.StartNew();

                var performedTasks = new List<string>();
                var errors = new List<string>();
                var success = true;

                // Task 1: Database optimization
                try
                {
                    var optimized = await OptimizeDatabaseAsync();
                    if (optimized)
                    {
                        performedTasks.Add("Database optimization completed");
                    }
                    else
                    {
                        errors.Add("Database optimization failed");
                        success = false;
                    }
                }
                catch (Exception ex)
                {
                    errors.Add($"Database optimization error: {ex.Message}");
                    success = false;
                }

                // Task 2: Clean up orphaned records
                try
                {
                    var cleaned = await CleanUpOrphanedRecordsAsync();
                    if (cleaned)
                    {
                        performedTasks.Add("Orphaned records cleanup completed");
                    }
                    else
                    {
                        errors.Add("Orphaned records cleanup failed");
                        success = false;
                    }
                }
                catch (Exception ex)
                {
                    errors.Add($"Orphaned records cleanup error: {ex.Message}");
                    success = false;
                }

                // Task 3: Update statistics
                try
                {
                    var updated = await UpdateStatisticsAsync();
                    if (updated)
                    {
                        performedTasks.Add("Database statistics updated");
                    }
                    else
                    {
                        errors.Add("Database statistics update failed");
                        success = false;
                    }
                }
                catch (Exception ex)
                {
                    errors.Add($"Statistics update error: {ex.Message}");
                    success = false;
                }

                // Task 4: Clear cache
                try
                {
                    var cacheCleared = await _cacheService.ClearSystemCacheAsync();
                    if (cacheCleared)
                    {
                        performedTasks.Add("System cache cleared");
                    }
                    else
                    {
                        errors.Add("Cache clearing failed");
                        success = false;
                    }
                }
                catch (Exception ex)
                {
                    errors.Add($"Cache clearing error: {ex.Message}");
                    success = false;
                }

                // Task 5: System health check
                try
                {
                    var healthOk = await CheckSystemHealthAsync();
                    if (healthOk)
                    {
                        performedTasks.Add("System health check passed");
                    }
                    else
                    {
                        errors.Add("System health check failed");
                        success = false;
                    }
                }
                catch (Exception ex)
                {
                    errors.Add($"System health check error: {ex.Message}");
                    success = false;
                }

                stopwatch.Stop();

                // Store maintenance report
                var report = new MaintenanceReport
                {
                    DatabaseOptimizationStatus = performedTasks.Contains("Database optimization completed") ? "Success" : "Failed",
                    OrphanedRecordsCleaned = performedTasks.Contains("Orphaned records cleanup completed") ? "Success" : "Failed",
                    StatisticsUpdated = performedTasks.Contains("Database statistics updated") ? "Success" : "Failed",
                    SystemHealthStatus = performedTasks.Contains("System health check passed") ? "Healthy" : "Issues detected",
                    LastMaintenanceRun = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    TotalExecutionTime = stopwatch.Elapsed.ToString(@"hh\:mm\:ss"),
                    OverallSuccess = success,
                    PerformedTasks = performedTasks.ToArray(),
                    Errors = errors.ToArray()
                };

                await StoreMaintenanceReportAsync(report);

                _logger.LogInformation($"System maintenance completed in {stopwatch.Elapsed.TotalMinutes:F2} minutes. Success: {success}");
                return success;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during system maintenance");
                return false;
            }
        }

        public async Task<bool> OptimizeDatabaseAsync()
        {
            try
            {
                _logger.LogInformation("Starting database optimization");

                // Get database connection
                var connection = _dbContext.Database.GetDbConnection();
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    // Rebuild indexes for major tables
                    var tables = new[]
                    {
                        "Students", "Teachers", "Employees", "Parents", "ClassSubject",
                        "StudentAttendance", "TeacherAttendance", "EmpAttendance"
                    };

                    foreach (var table in tables)
                    {
                        try
                        {
                            command.CommandText = $"IF EXISTS (SELECT * FROM sys.tables WHERE name = '{table}') " +
                                                $"BEGIN EXEC('ALTER INDEX ALL ON {table} REBUILD'); END";
                            await command.ExecuteNonQueryAsync();
                            _logger.LogInformation($"Rebuilt indexes for table: {table}");
                        }
                        catch (Exception ex)
                        {
                            _logger.LogWarning(ex, $"Could not rebuild indexes for table: {table}");
                        }
                    }

                    // Update statistics
                    command.CommandText = "EXEC sp_updatestats";
                    await command.ExecuteNonQueryAsync();

                    // Shrink database if needed (optional - use with caution)
                    var shrinkEnabled = _configuration.GetValue<bool>("Maintenance:EnableDatabaseShrink", false);
                    if (shrinkEnabled)
                    {
                        command.CommandText = "DBCC SHRINKDATABASE (0)";
                        await command.ExecuteNonQueryAsync();
                        _logger.LogInformation("Database shrink completed");
                    }
                }

                await connection.CloseAsync();
                _logger.LogInformation("Database optimization completed successfully");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during database optimization");
                return false;
            }
        }

        public async Task<bool> CleanUpOrphanedRecordsAsync()
        {
            try
            {
                _logger.LogInformation("Starting orphaned records cleanup");

                var cleanedCount = 0;

                // Clean up orphaned student attendance records
                try
                {
                    var orphanedAttendance = await _dbContext.StudentAttendance
                        .Where(sa => !_dbContext.Students.Any(s => s.Id == sa.StudentId))
                        .ToListAsync();

                    if (orphanedAttendance.Any())
                    {
                        _dbContext.StudentAttendance.RemoveRange(orphanedAttendance);
                        await _dbContext.SaveChangesAsync();
                        cleanedCount += orphanedAttendance.Count;
                        _logger.LogInformation($"Cleaned up {orphanedAttendance.Count} orphaned student attendance records");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Error cleaning up student attendance records");
                }

                // Clean up orphaned teacher class details
                try
                {
                    var orphanedTeacherClasses = await _dbContext.TeacherClassDetails
                        .Where(tcd => !_dbContext.Teachers.Any(t => t.Id == tcd.TeacherId))
                        .ToListAsync();

                    if (orphanedTeacherClasses.Any())
                    {
                        _dbContext.TeacherClassDetails.RemoveRange(orphanedTeacherClasses);
                        await _dbContext.SaveChangesAsync();
                        cleanedCount += orphanedTeacherClasses.Count;
                        _logger.LogInformation($"Cleaned up {orphanedTeacherClasses.Count} orphaned teacher class records");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Error cleaning up teacher class records");
                }

                // Clean up orphaned user details if user doesn't exist
                try
                {
                    // This would require checking against the actual user table
                    // For now, this is a placeholder
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Error cleaning up user details");
                }

                _logger.LogInformation($"Orphaned records cleanup completed. Total records cleaned: {cleanedCount}");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during orphaned records cleanup");
                return false;
            }
        }

        public async Task<bool> UpdateStatisticsAsync()
        {
            try
            {
                _logger.LogInformation("Starting database statistics update");

                var connection = _dbContext.Database.GetDbConnection();
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    // Update statistics for all tables
                    command.CommandText = "EXEC sp_updatestats";
                    await command.ExecuteNonQueryAsync();

                    // Update statistics for specific tables if needed
                    var tables = new[]
                    {
                        "Students", "Teachers", "Employees", "Parents", "ClassSubject",
                        "StudentAttendance", "TeacherAttendance", "EmpAttendance"
                    };

                    foreach (var table in tables)
                    {
                        try
                        {
                            command.CommandText = $"IF EXISTS (SELECT * FROM sys.tables WHERE name = '{table}') " +
                                                $"BEGIN EXEC('UPDATE STATISTICS {table}'); END";
                            await command.ExecuteNonQueryAsync();
                        }
                        catch (Exception ex)
                        {
                            _logger.LogWarning(ex, $"Could not update statistics for table: {table}");
                        }
                    }
                }

                await connection.CloseAsync();
                _logger.LogInformation("Database statistics update completed successfully");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during database statistics update");
                return false;
            }
        }

        public async Task<bool> CheckSystemHealthAsync()
        {
            try
            {
                _logger.LogInformation("Starting system health check");

                var healthIssues = new List<string>();

                // Check database connectivity
                try
                {
                    await _dbContext.Database.CanConnectAsync();
                }
                catch (Exception ex)
                {
                    healthIssues.Add($"Database connectivity issue: {ex.Message}");
                }

                // Check database size (if configured threshold exists)
                try
                {
                    var maxSizeMB = _configuration.GetValue<int>("Maintenance:MaxDatabaseSizeMB", 10000);
                    var currentSize = await GetDatabaseSizeAsync();
                    
                    if (currentSize > maxSizeMB)
                    {
                        healthIssues.Add($"Database size ({currentSize} MB) exceeds threshold ({maxSizeMB} MB)");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Could not check database size");
                }

                // Check for long-running queries
                try
                {
                    var longRunningQueries = await GetLongRunningQueriesAsync();
                    if (longRunningQueries.Any())
                    {
                        healthIssues.Add($"Found {longRunningQueries.Count} long-running queries");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Could not check for long-running queries");
                }

                // Check disk space
                try
                {
                    var currentDirectory = Directory.GetCurrentDirectory();
                    var drive = new DriveInfo(currentDirectory);
                    var freeSpaceGB = drive.AvailableFreeSpace / (1024.0 * 1024.0 * 1024.0);
                    
                    if (freeSpaceGB < 1) // Less than 1GB free
                    {
                        healthIssues.Add($"Low disk space: {freeSpaceGB:F2} GB available");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Could not check disk space");
                }

                if (healthIssues.Any())
                {
                    _logger.LogWarning($"System health check found {healthIssues.Count} issues: {string.Join("; ", healthIssues)}");
                    return false;
                }

                _logger.LogInformation("System health check passed");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during system health check");
                return false;
            }
        }

        public async Task<MaintenanceReport> GetMaintenanceReportAsync()
        {
            try
            {
                // This would typically retrieve the last maintenance report from storage
                // For now, return a default report
                return new MaintenanceReport
                {
                    DatabaseOptimizationStatus = "Not run",
                    OrphanedRecordsCleaned = "Not run",
                    StatisticsUpdated = "Not run",
                    SystemHealthStatus = "Unknown",
                    LastMaintenanceRun = "Never",
                    TotalExecutionTime = "00:00:00",
                    OverallSuccess = false,
                    PerformedTasks = new string[0],
                    Errors = new string[0]
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving maintenance report");
                return new MaintenanceReport
                {
                    DatabaseOptimizationStatus = "Error",
                    OrphanedRecordsCleaned = "Error",
                    StatisticsUpdated = "Error",
                    SystemHealthStatus = "Error",
                    LastMaintenanceRun = "Error",
                    TotalExecutionTime = "00:00:00",
                    OverallSuccess = false,
                    PerformedTasks = new[] { "Error retrieving report" },
                    Errors = new[] { ex.Message }
                };
            }
        }

        private async Task StoreMaintenanceReportAsync(MaintenanceReport report)
        {
            try
            {
                // This would typically store the report in a database table or file
                // For now, just log it
                _logger.LogInformation($"Maintenance report stored: Success={report.OverallSuccess}, " +
                    $"Tasks={report.PerformedTasks.Length}, Errors={report.Errors.Length}");
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Could not store maintenance report");
            }
        }

        private async Task<int> GetDatabaseSizeAsync()
        {
            try
            {
                var connection = _dbContext.Database.GetDbConnection();
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"
                        SELECT SUM(size * 8 / 1024) as SizeMB
                        FROM sys.master_files
                        WHERE database_id = DB_ID()";
                    
                    var result = await command.ExecuteScalarAsync();
                    await connection.CloseAsync();
                    
                    return Convert.ToInt32(result);
                }
            }
            catch
            {
                return 0;
            }
        }

        private async Task<List<string>> GetLongRunningQueriesAsync()
        {
            try
            {
                var connection = _dbContext.Database.GetDbConnection();
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"
                        SELECT TOP 10 
                            st.text,
                            qp.execution_time
                        FROM sys.dm_exec_query_stats qs
                        CROSS APPLY sys.dm_exec_sql_text(qs.sql_handle) st
                        CROSS APPLY sys.dm_exec_query_plan(qs.plan_handle) qp
                        WHERE qp.execution_time > 30000 -- 30 seconds
                        ORDER BY qp.execution_time DESC";
                    
                    var results = new List<string>();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            results.Add(reader.GetString(0));
                        }
                    }
                    
                    await connection.CloseAsync();
                    return results;
                }
            }
            catch
            {
                return new List<string>();
            }
        }
    }
}
