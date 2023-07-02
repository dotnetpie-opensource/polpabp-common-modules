using System;
using System.Threading;
using System.Threading.Tasks;

namespace PolpAbp.ResourceManagement.Services
{
    public interface IResourceLogProvider
    {
        Task StoreAsync(ResourceLogInfo resourceLogInfo, CancellationToken cancellationToken, bool autoSave = false);

        Task<long> CountCurrentUsageAsync(string resourceName, Guid? userId, DateTime StartedOn, DateTime? EndedOn, CancellationToken cancellationToken);
        Task<long> GetMonthlyUsageAsync(string resourceName, int year, int month, CancellationToken cancellationToken);
        Task<long> GetYearlyUsageAsync(string resourceName, int year, CancellationToken cancellationToken);
    }
}
