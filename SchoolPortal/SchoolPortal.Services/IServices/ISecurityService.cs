using System.Threading.Tasks;

namespace SchoolPortal.Services.IServices
{
    public interface ISecurityService
    {
        Task<SecurityScanResult> RunSecurityAuditAsync();
        Task<bool> CheckUserPermissionsAsync();
        Task<bool> CheckDatabaseSecurityAsync();
        Task<bool> CheckApplicationSecurityAsync();
        Task<bool> CheckPasswordPoliciesAsync();
        Task<bool> CheckSessionSecurityAsync();
        Task<SecurityReport> GetSecurityReportAsync();
    }

    public class SecurityScanResult
    {
        public bool OverallSuccess { get; set; }
        public string ScanTimestamp { get; set; } = string.Empty;
        public SecurityCheckResult[] CheckResults { get; set; } = new SecurityCheckResult[0];
        public SecurityIssue[] IssuesFound { get; set; } = new SecurityIssue[0];
        public string TotalScanTime { get; set; } = string.Empty;
        public int CriticalIssues { get; set; }
        public int WarningIssues { get; set; }
        public int InfoIssues { get; set; }
    }

    public class SecurityCheckResult
    {
        public string CheckName { get; set; } = string.Empty;
        public bool Passed { get; set; }
        public string Status { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string[] Recommendations { get; set; } = new string[0];
    }

    public class SecurityIssue
    {
        public string Title { get; set; } = string.Empty;
        public string Severity { get; set; } = string.Empty; // Critical, Warning, Info
        public string Description { get; set; } = string.Empty;
        public string AffectedArea { get; set; } = string.Empty;
        public string Recommendation { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
    }

    public class SecurityReport
    {
        public string LastScanDate { get; set; } = string.Empty;
        public string OverallSecurityStatus { get; set; } = string.Empty;
        public int TotalIssues { get; set; }
        public int CriticalIssues { get; set; }
        public int WarningIssues { get; set; }
        public SecurityIssue[] RecentIssues { get; set; } = new SecurityIssue[0];
        public string[] SecurityRecommendations { get; set; } = new string[0];
    }
}
