using System.Threading.Tasks;

namespace SchoolPortal.Services.IServices
{
    public interface IMaintenanceService
    {
        Task<bool> RunSystemMaintenanceAsync();
        Task<bool> OptimizeDatabaseAsync();
        Task<bool> CleanUpOrphanedRecordsAsync();
        Task<bool> UpdateStatisticsAsync();
        Task<bool> CheckSystemHealthAsync();
        Task<MaintenanceReport> GetMaintenanceReportAsync();
    }

    public class MaintenanceReport
    {
        public string DatabaseOptimizationStatus { get; set; } = string.Empty;
        public string OrphanedRecordsCleaned { get; set; } = string.Empty;
        public string StatisticsUpdated { get; set; } = string.Empty;
        public string SystemHealthStatus { get; set; } = string.Empty;
        public string LastMaintenanceRun { get; set; } = string.Empty;
        public string TotalExecutionTime { get; set; } = string.Empty;
        public bool OverallSuccess { get; set; }
        public string[] PerformedTasks { get; set; } = new string[0];
        public string[] Errors { get; set; } = new string[0];
    }
}
