using System.Threading.Tasks;

namespace SchoolPortal.Services.IServices
{
    public interface ICacheService
    {
        Task<bool> ClearSystemCacheAsync();
        Task<bool> ClearTemporaryFilesAsync();
        Task<CacheInfo> GetCacheInfoAsync();
    }

    public class CacheInfo
    {
        public string MemoryCacheSize { get; set; } = string.Empty;
        public string TemporaryFilesSize { get; set; } = string.Empty;
        public string SessionCount { get; set; } = string.Empty;
        public string LastCleared { get; set; } = string.Empty;
    }
}
