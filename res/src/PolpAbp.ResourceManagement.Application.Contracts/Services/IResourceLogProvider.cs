using System;
using System.Threading.Tasks;

namespace PolpAbp.ResourceManagement.Services
{
    public interface IResourceLogProvider
    {
        Task StoreAsync(ResourceLogInfo resourceLogInfo, bool autoSave = false);

        Task<long> CountCurrentUsageAsync(string resourceName, Guid? userId, DateTime StartedOn, DateTime? EndedOn);
        Task<long> GetMonthlyUsageAsync(string resourceName, int year, int month);
        Task<long> GetYearlyUsageAsync(string resourceName, int year);
    }
}
