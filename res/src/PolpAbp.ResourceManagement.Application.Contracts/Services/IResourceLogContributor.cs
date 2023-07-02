using System;
using System.Threading;
using System.Threading.Tasks;

namespace PolpAbp.ResourceManagement.Services
{
    public interface IResourceLogContributor
    {
        string ResourceName { get; }

        Task<long> CountCurrentUsageAsync(Guid? userId, DateTime StartedOn, DateTime? EndedOn, CancellationToken cancellationToken);
        Task<long> GetMonthlyUsageAsync(int year, int month, CancellationToken cancellationToken);
        Task<long> GetYearlyUsageAsync(int year, CancellationToken cancellationToken);

        // Storing 
        Task StoreAsync(ResourceLogInfo resourceLogInfo, CancellationToken cancellationToken, bool autoSave = false);
    }
}
