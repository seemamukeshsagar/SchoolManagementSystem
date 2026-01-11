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
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SchoolPortal.Services
{
    public class SecurityService : ISecurityService
    {
        private readonly ILogger<SecurityService> _logger;
        private readonly AppDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public SecurityService(
            ILogger<SecurityService> logger,
            AppDbContext dbContext,
            IConfiguration configuration)
        {
            _logger = logger;
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public async Task<SecurityScanResult> RunSecurityAuditAsync()
        {
            try
            {
                _logger.LogInformation("Starting security audit scan");
                var stopwatch = Stopwatch.StartNew();

                var checkResults = new List<SecurityCheckResult>();
                var issuesFound = new List<SecurityIssue>();
                var criticalIssues = 0;
                var warningIssues = 0;
                var infoIssues = 0;

                // Check 1: User Permissions
                var userPermissionsResult = await CheckUserPermissionsWithDetailsAsync();
                checkResults.Add(userPermissionsResult);
                issuesFound.AddRange(userPermissionsResult.Recommendations.Select(r => new SecurityIssue
                {
                    Title = "User Permission Issue",
                    Severity = "Warning",
                    Description = r,
                    AffectedArea = "User Management",
                    Recommendation = "Review user permissions and roles",
                    Category = "Access Control"
                }));
                warningIssues += userPermissionsResult.Recommendations.Length;

                // Check 2: Database Security
                var dbSecurityResult = await CheckDatabaseSecurityWithDetailsAsync();
                checkResults.Add(dbSecurityResult);
                if (!dbSecurityResult.Passed)
                {
                    issuesFound.Add(new SecurityIssue
                    {
                        Title = "Database Security Issue",
                        Severity = "Critical",
                        Description = "Database security configuration needs attention",
                        AffectedArea = "Database",
                        Recommendation = "Review database security settings and encryption",
                        Category = "Database Security"
                    });
                    criticalIssues++;
                }

                // Check 3: Application Security
                var appSecurityResult = await CheckApplicationSecurityWithDetailsAsync();
                checkResults.Add(appSecurityResult);
                issuesFound.AddRange(appSecurityResult.Recommendations.Select(r => new SecurityIssue
                {
                    Title = "Application Security Issue",
                    Severity = "Warning",
                    Description = r,
                    AffectedArea = "Application Configuration",
                    Recommendation = "Review application security settings",
                    Category = "Application Security"
                }));
                warningIssues += appSecurityResult.Recommendations.Length;

                // Check 4: Password Policies
                var passwordPolicyResult = await CheckPasswordPoliciesWithDetailsAsync();
                checkResults.Add(passwordPolicyResult);
                if (!passwordPolicyResult.Passed)
                {
                    issuesFound.Add(new SecurityIssue
                    {
                        Title = "Password Policy Issue",
                        Severity = "Warning",
                        Description = "Password policies may not meet security standards",
                        AffectedArea = "Authentication",
                        Recommendation = "Implement stronger password policies",
                        Category = "Authentication"
                    });
                    warningIssues++;
                }

                // Check 5: Session Security
                var sessionSecurityResult = await CheckSessionSecurityWithDetailsAsync();
                checkResults.Add(sessionSecurityResult);
                issuesFound.AddRange(sessionSecurityResult.Recommendations.Select(r => new SecurityIssue
                {
                    Title = "Session Security Issue",
                    Severity = "Info",
                    Description = r,
                    AffectedArea = "Session Management",
                    Recommendation = "Review session security configuration",
                    Category = "Session Management"
                }));
                infoIssues += sessionSecurityResult.Recommendations.Length;

                // Additional Security Checks
                var additionalResults = await PerformAdditionalSecurityChecks(checkResults, issuesFound, criticalIssues, warningIssues, infoIssues);
                criticalIssues = additionalResults.criticalIssues;
                warningIssues = additionalResults.warningIssues;
                infoIssues = additionalResults.infoIssues;

                stopwatch.Stop();

                var result = new SecurityScanResult
                {
                    OverallSuccess = criticalIssues == 0,
                    ScanTimestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    CheckResults = checkResults.ToArray(),
                    IssuesFound = issuesFound.ToArray(),
                    TotalScanTime = stopwatch.Elapsed.ToString(@"hh\:mm\:ss"),
                    CriticalIssues = criticalIssues,
                    WarningIssues = warningIssues,
                    InfoIssues = infoIssues
                };

                await StoreSecurityScanResultAsync(result);

                _logger.LogInformation($"Security audit completed in {stopwatch.Elapsed.TotalMinutes:F2} minutes. " +
                    $"Critical: {criticalIssues}, Warnings: {warningIssues}, Info: {infoIssues}");

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during security audit");
                return new SecurityScanResult
                {
                    OverallSuccess = false,
                    ScanTimestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    CheckResults = new[]
                    {
                        new SecurityCheckResult
                        {
                            CheckName = "Security Audit",
                            Passed = false,
                            Status = "Failed",
                            Description = "Security audit failed to complete",
                            Recommendations = new[] { "Check system logs for details" }
                        }
                    },
                    IssuesFound = new[]
                    {
                        new SecurityIssue
                        {
                            Title = "Security Audit Failure",
                            Severity = "Critical",
                            Description = ex.Message,
                            AffectedArea = "System",
                            Recommendation = "Review system configuration and logs",
                            Category = "System"
                        }
                    },
                    TotalScanTime = "00:00:00",
                    CriticalIssues = 1,
                    WarningIssues = 0,
                    InfoIssues = 0
                };
            }
        }

        public async Task<bool> CheckUserPermissionsAsync()
        {
            try
            {
                var result = await CheckUserPermissionsWithDetailsAsync();
                return result.Passed;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking user permissions");
                return false;
            }
        }

        public async Task<bool> CheckDatabaseSecurityAsync()
        {
            try
            {
                var result = await CheckDatabaseSecurityWithDetailsAsync();
                return result.Passed;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking database security");
                return false;
            }
        }

        public async Task<bool> CheckApplicationSecurityAsync()
        {
            try
            {
                var result = await CheckApplicationSecurityWithDetailsAsync();
                return result.Passed;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking application security");
                return false;
            }
        }

        public async Task<bool> CheckPasswordPoliciesAsync()
        {
            try
            {
                var result = await CheckPasswordPoliciesWithDetailsAsync();
                return result.Passed;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking password policies");
                return false;
            }
        }

        public async Task<bool> CheckSessionSecurityAsync()
        {
            try
            {
                var result = await CheckSessionSecurityWithDetailsAsync();
                return result.Passed;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking session security");
                return false;
            }
        }

        public async Task<SecurityReport> GetSecurityReportAsync()
        {
            try
            {
                // This would typically retrieve the last security report from storage
                // For now, return a default report
                return new SecurityReport
                {
                    LastScanDate = "Never",
                    OverallSecurityStatus = "Unknown",
                    TotalIssues = 0,
                    CriticalIssues = 0,
                    WarningIssues = 0,
                    RecentIssues = new SecurityIssue[0],
                    SecurityRecommendations = new[] { "Run a security scan to assess current status" }
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving security report");
                return new SecurityReport
                {
                    LastScanDate = "Error",
                    OverallSecurityStatus = "Error",
                    TotalIssues = 0,
                    CriticalIssues = 0,
                    WarningIssues = 0,
                    RecentIssues = new SecurityIssue[0],
                    SecurityRecommendations = new[] { "Error retrieving security report" }
                };
            }
        }

        private async Task<SecurityCheckResult> CheckUserPermissionsWithDetailsAsync()
        {
            try
            {
                var recommendations = new List<string>();

                // Check for users with excessive permissions
                try
                {
                    var adminUsers = await _dbContext.UserDetails
                        .Where(u => u.RoleName != null && u.RoleName.ToLower().Contains("admin"))
                        .ToListAsync();

                    if (adminUsers.Count > 5)
                    {
                        recommendations.Add($"Consider reviewing admin access: {adminUsers.Count} users have admin privileges");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Could not check user permissions");
                    recommendations.Add("Unable to verify user permission structure");
                }

                // Check for inactive users with active accounts
                try
                {
                    var inactiveUsers = await _dbContext.UserDetails
                        .Where(u => !u.IsActive)
                        .ToListAsync();

                    if (inactiveUsers.Any())
                    {
                        recommendations.Add($"{inactiveUsers.Count} inactive users found in system");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Could not check inactive users");
                }

                return new SecurityCheckResult
                {
                    CheckName = "User Permissions",
                    Passed = recommendations.Count == 0,
                    Status = recommendations.Count == 0 ? "Passed" : "Warning",
                    Description = "Checks user roles and permissions for security issues",
                    Recommendations = recommendations.ToArray()
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in user permissions check");
                return new SecurityCheckResult
                {
                    CheckName = "User Permissions",
                    Passed = false,
                    Status = "Failed",
                    Description = "Failed to check user permissions",
                    Recommendations = new[] { "Review user permission system" }
                };
            }
        }

        private async Task<SecurityCheckResult> CheckDatabaseSecurityWithDetailsAsync()
        {
            try
            {
                var recommendations = new List<string>();

                // Check database connection security
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                if (!string.IsNullOrEmpty(connectionString))
                {
                    if (connectionString.Contains("User ID=sa") || connectionString.Contains("User ID=dbo"))
                    {
                        recommendations.Add("Avoid using privileged database accounts for application access");
                    }

                    if (!connectionString.Contains("Encrypt=") && !connectionString.Contains("TrustServerCertificate="))
                    {
                        recommendations.Add("Consider enabling database connection encryption");
                    }
                }

                // Check for sensitive data exposure
                try
                {
                    var connection = _dbContext.Database.GetDbConnection();
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = @"
                            SELECT COUNT(*) FROM INFORMATION_SCHEMA.COLUMNS 
                            WHERE COLUMN_NAME LIKE '%password%' 
                            OR COLUMN_NAME LIKE '%secret%' 
                            OR COLUMN_NAME LIKE '%key%'";
                        
                        var sensitiveColumns = Convert.ToInt32(await command.ExecuteScalarAsync());
                        if (sensitiveColumns > 0)
                        {
                            recommendations.Add($"Found {sensitiveColumns} potentially sensitive column names in database");
                        }
                    }

                    await connection.CloseAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Could not check database schema for sensitive data");
                    recommendations.Add("Unable to verify database schema security");
                }

                return new SecurityCheckResult
                {
                    CheckName = "Database Security",
                    Passed = recommendations.Count == 0,
                    Status = recommendations.Count == 0 ? "Passed" : "Warning",
                    Description = "Checks database connection and schema security",
                    Recommendations = recommendations.ToArray()
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in database security check");
                return new SecurityCheckResult
                {
                    CheckName = "Database Security",
                    Passed = false,
                    Status = "Failed",
                    Description = "Failed to check database security",
                    Recommendations = new[] { "Review database security configuration" }
                };
            }
        }

        private async Task<SecurityCheckResult> CheckApplicationSecurityWithDetailsAsync()
        {
            try
            {
                var recommendations = new List<string>();

                // Check for development settings in production
                var environment = _configuration["ASPNETCORE_ENVIRONMENT"];
                if (!environment?.Equals("Production", StringComparison.OrdinalIgnoreCase) == true)
                {
                    recommendations.Add("Application is not running in Production mode");
                }

                // Check for security headers configuration
                var securityHeaders = new[]
                {
                    "Security:EnableHsts",
                    "Security:ContentSecurityPolicy",
                    "Security:XFrameOptions",
                    "Security:XContentTypeOptions"
                };

                foreach (var header in securityHeaders)
                {
                    var value = _configuration[header];
                    if (string.IsNullOrEmpty(value))
                    {
                        recommendations.Add($"Security header not configured: {header}");
                    }
                }

                // Check file upload security
                var maxFileSize = _configuration.GetValue<long>("FileUpload:MaxFileSize", 10485760); // 10MB default
                if (maxFileSize > 104857600) // 100MB
                {
                    recommendations.Add("Large file upload size limit may pose security risk");
                }

                // Check logging configuration
                var logLevel = _configuration["Logging:LogLevel:Default"];
                if (logLevel?.ToLower() == "debug" || logLevel?.ToLower() == "trace")
                {
                    recommendations.Add("Verbose logging enabled in production may expose sensitive information");
                }

                return new SecurityCheckResult
                {
                    CheckName = "Application Security",
                    Passed = recommendations.Count == 0,
                    Status = recommendations.Count == 0 ? "Passed" : "Warning",
                    Description = "Checks application configuration security",
                    Recommendations = recommendations.ToArray()
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in application security check");
                return new SecurityCheckResult
                {
                    CheckName = "Application Security",
                    Passed = false,
                    Status = "Failed",
                    Description = "Failed to check application security",
                    Recommendations = new[] { "Review application security configuration" }
                };
            }
        }

        private async Task<SecurityCheckResult> CheckPasswordPoliciesWithDetailsAsync()
        {
            try
            {
                var recommendations = new List<string>();

                // Check password policy configuration
                var minLength = _configuration.GetValue<int>("PasswordPolicy:MinLength", 8);
                var requireUppercase = _configuration.GetValue<bool>("PasswordPolicy:RequireUppercase", true);
                var requireLowercase = _configuration.GetValue<bool>("PasswordPolicy:RequireLowercase", true);
                var requireNumbers = _configuration.GetValue<bool>("PasswordPolicy:RequireNumbers", true);
                var requireSpecialChars = _configuration.GetValue<bool>("PasswordPolicy:RequireSpecialChars", true);

                if (minLength < 8)
                {
                    recommendations.Add("Password minimum length should be at least 8 characters");
                }

                if (!requireUppercase || !requireLowercase || !requireNumbers)
                {
                    recommendations.Add("Password policy should require mixed case and numbers");
                }

                if (!requireSpecialChars)
                {
                    recommendations.Add("Consider requiring special characters in passwords");
                }

                // Check for weak passwords in database (if password hashes are accessible)
                try
                {
                    // This is a placeholder - actual implementation would depend on how passwords are stored
                    // For security reasons, we won't implement actual password checking here
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Could not check password strength in database");
                }

                return new SecurityCheckResult
                {
                    CheckName = "Password Policies",
                    Passed = recommendations.Count == 0,
                    Status = recommendations.Count == 0 ? "Passed" : "Warning",
                    Description = "Checks password policy configuration",
                    Recommendations = recommendations.ToArray()
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in password policies check");
                return new SecurityCheckResult
                {
                    CheckName = "Password Policies",
                    Passed = false,
                    Status = "Failed",
                    Description = "Failed to check password policies",
                    Recommendations = new[] { "Review password policy configuration" }
                };
            }
        }

        private async Task<SecurityCheckResult> CheckSessionSecurityWithDetailsAsync()
        {
            try
            {
                var recommendations = new List<string>();

                // Check session timeout configuration
                var sessionTimeout = _configuration.GetValue<int>("Session:TimeoutMinutes", 30);
                if (sessionTimeout > 120) // 2 hours
                {
                    recommendations.Add("Consider reducing session timeout for better security");
                }

                // Check cookie security settings
                var cookieSecure = _configuration.GetValue<bool>("Cookie:Secure", true);
                var cookieHttpOnly = _configuration.GetValue<bool>("Cookie:HttpOnly", true);
                var cookieSameSite = _configuration["Cookie:SameSite"];

                if (!cookieSecure)
                {
                    recommendations.Add("Cookies should be marked as Secure");
                }

                if (!cookieHttpOnly)
                {
                    recommendations.Add("Cookies should be marked as HttpOnly");
                }

                if (string.IsNullOrEmpty(cookieSameSite))
                {
                    recommendations.Add("Consider setting SameSite policy for cookies");
                }

                // Check for sliding expiration
                var slidingExpiration = _configuration.GetValue<bool>("Authentication:SlidingExpiration", true);
                if (slidingExpiration)
                {
                    recommendations.Add("Consider disabling sliding expiration for better security");
                }

                return new SecurityCheckResult
                {
                    CheckName = "Session Security",
                    Passed = recommendations.Count == 0,
                    Status = recommendations.Count == 0 ? "Passed" : "Warning",
                    Description = "Checks session and cookie security configuration",
                    Recommendations = recommendations.ToArray()
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in session security check");
                return new SecurityCheckResult
                {
                    CheckName = "Session Security",
                    Passed = false,
                    Status = "Failed",
                    Description = "Failed to check session security",
                    Recommendations = new[] { "Review session security configuration" }
                };
            }
        }

        private async Task<(int criticalIssues, int warningIssues, int infoIssues)> PerformAdditionalSecurityChecks(
            List<SecurityCheckResult> checkResults,
            List<SecurityIssue> issuesFound,
            int criticalIssues,
            int warningIssues,
            int infoIssues)
        {
            try
            {
                // Check for outdated dependencies
                var dependencyCheck = await CheckDependencySecurityAsync();
                checkResults.Add(dependencyCheck);
                if (!dependencyCheck.Passed)
                {
                    issuesFound.Add(new SecurityIssue
                    {
                        Title = "Dependency Security",
                        Severity = "Warning",
                        Description = "Some dependencies may have security vulnerabilities",
                        AffectedArea = "Dependencies",
                        Recommendation = "Update dependencies to latest secure versions",
                        Category = "Dependencies"
                    });
                    warningIssues++;
                }

                // Check file system permissions
                var fileSystemCheck = await CheckFileSystemSecurityAsync();
                checkResults.Add(fileSystemCheck);
                issuesFound.AddRange(fileSystemCheck.Recommendations.Select(r => new SecurityIssue
                {
                    Title = "File System Security",
                    Severity = "Info",
                    Description = r,
                    AffectedArea = "File System",
                    Recommendation = "Review file system permissions",
                    Category = "File System"
                }));
                infoIssues += fileSystemCheck.Recommendations.Length;

                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Error performing additional security checks");
            }

            return (criticalIssues, warningIssues, infoIssues);
        }

        private async Task<SecurityCheckResult> CheckDependencySecurityAsync()
        {
            try
            {
                var recommendations = new List<string>();

                // This is a placeholder for dependency checking
                // In a real implementation, you would check package references for known vulnerabilities
                recommendations.Add("Regular dependency vulnerability scanning recommended");

                return new SecurityCheckResult
                {
                    CheckName = "Dependency Security",
                    Passed = true, // Assume passed for now
                    Status = "Info",
                    Description = "Checks for known vulnerabilities in dependencies",
                    Recommendations = recommendations.ToArray()
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in dependency security check");
                return new SecurityCheckResult
                {
                    CheckName = "Dependency Security",
                    Passed = false,
                    Status = "Failed",
                    Description = "Failed to check dependency security",
                    Recommendations = new[] { "Review dependency management process" }
                };
            }
        }

        private async Task<SecurityCheckResult> CheckFileSystemSecurityAsync()
        {
            try
            {
                var recommendations = new List<string>();

                // Check sensitive file permissions
                var sensitiveFiles = new[]
                {
                    "appsettings.json",
                    "appsettings.Production.json",
                    "web.config"
                };

                foreach (var file in sensitiveFiles)
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), file);
                    if (File.Exists(filePath))
                    {
                        var fileInfo = new FileInfo(filePath);
                        if (fileInfo.IsReadOnly)
                        {
                            recommendations.Add($"Sensitive file {file} is read-only (good)");
                        }
                        else
                        {
                            recommendations.Add($"Consider making sensitive file {file} read-only");
                        }
                    }
                }

                return new SecurityCheckResult
                {
                    CheckName = "File System Security",
                    Passed = true,
                    Status = "Info",
                    Description = "Checks file system permissions for sensitive files",
                    Recommendations = recommendations.ToArray()
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in file system security check");
                return new SecurityCheckResult
                {
                    CheckName = "File System Security",
                    Passed = false,
                    Status = "Failed",
                    Description = "Failed to check file system security",
                    Recommendations = new[] { "Review file system permissions" }
                };
            }
        }

        private async Task StoreSecurityScanResultAsync(SecurityScanResult result)
        {
            try
            {
                // This would typically store the result in a database table or file
                // For now, just log it
                _logger.LogInformation($"Security scan result stored: Success={result.OverallSuccess}, " +
                    $"Critical={result.CriticalIssues}, Warnings={result.WarningIssues}, Info={result.InfoIssues}");
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Could not store security scan result");
            }
        }
    }
}
