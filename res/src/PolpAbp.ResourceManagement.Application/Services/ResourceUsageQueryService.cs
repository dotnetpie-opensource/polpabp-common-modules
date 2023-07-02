using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;

namespace PolpAbp.ResourceManagement.Services
{
    [RemoteService(false)]
    public class ResourceUsageQueryService : IResourceUsageQueryService, ITransientDependency
    {
        private readonly IResourceLogProvider _resourceLogProvider;
        private readonly ISubscriptionPlanService _subscriptionPlanService;
        private readonly IDistributedCache<ResourceUsageCacheItem> _cache;

        public ResourceUsageQueryService(IResourceLogProvider resourceLogProvider,
            ISubscriptionPlanService subscriptionPlanService,
            IDistributedCache<ResourceUsageCacheItem> cache)
        {
            _resourceLogProvider = resourceLogProvider;
            _subscriptionPlanService = subscriptionPlanService;
            _cache = cache;
        }

        public async Task<ResourceUsageCacheItem> GetResourceUsageAsync(string resourceName, 
            string cacheKey,
            int expiredInMins = 10, bool forceReload = false,
            CancellationToken cancellationToken = default)
        {
            if (forceReload)
            {
                await _cache.RemoveAsync(cacheKey, token: cancellationToken);
            }

            return await _cache.GetOrAddAsync(
              cacheKey,
              async () =>
              {
                  var quota = await _subscriptionPlanService.GetQuotaAsync(resourceName, true, cancellationToken: cancellationToken);
                  // Get the usage 
                  var curBillingCycle = await _subscriptionPlanService.GetCurrentBillingPeriodAsync(resourceName, cancellationToken: cancellationToken);
                  var usage = await _resourceLogProvider.CountCurrentUsageAsync(resourceName, null, curBillingCycle.Item1, curBillingCycle.Item2, cancellationToken: cancellationToken);

                  return new ResourceUsageCacheItem
                  {
                      Usage = usage,
                      Quota = quota
                  };
              },
              () => new DistributedCacheEntryOptions
              {
                  SlidingExpiration = new TimeSpan(0, expiredInMins, 0)
              },
              token: cancellationToken
          );
        }
    }
}
