using System;
using System.Threading.Tasks;

namespace PolpAbp.ResourceManagement.Services
{
    public interface IResourceLogContributor
    {
        string ResourceName { get; }

        Task<long> CountCurrentUsageAsync(Guid? userId, DateTime StartedOn, DateTime? EndedOn);
        Task<long> GetMonthlyUsageAsync(int year, int month);
        Task<long> GetYearlyUsageAsync(int year);

        // Storing 
        Task StoreAsync(ResourceLogInfo resourceLogInfo, bool autoSave = false);
    }
}
