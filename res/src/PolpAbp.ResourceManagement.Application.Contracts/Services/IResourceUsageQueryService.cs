using System.Threading;
using System.Threading.Tasks;

namespace PolpAbp.ResourceManagement.Services
{
    public interface IResourceUsageQueryService
    {
        Task<ResourceUsageCacheItem> GetResourceUsageAsync(string resourceName, string cacheKey, 
            int expiredInMins = 10, bool forceReload = false, CancellationToken cancellationToken = default);
    }
}
