using System.Threading.Tasks;

namespace SchoolPortal.Services.IServices
{
    public interface IAuditLogger
    {
        Task LogAsync(string action, string description, string userId, string ipAddress);
    }
}
