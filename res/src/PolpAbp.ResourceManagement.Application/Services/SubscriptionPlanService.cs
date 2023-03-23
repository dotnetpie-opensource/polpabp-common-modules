using PolpAbp.ResourceManagement.Domain.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace PolpAbp.ResourceManagement.Services
{
    [RemoteService(false)]
    public class SubscriptionPlanService : ISubscriptionPlanService, ITransientDependency
    {
        private readonly IRepository<TenantSubscription> _subscriptionRepository;
        private readonly IRepository<Resource> _resourceRepository;

        public SubscriptionPlanService(IRepository<TenantSubscription> subscriptionRepository, IRepository<Resource> resourceRepository)
        {
            _subscriptionRepository = subscriptionRepository;
            _resourceRepository = resourceRepository;
        }

        public async Task<long?> GetQuotaAsync(string resourceName, bool isTenantLevel)
        {
            // Require the resource id 
            var resourceEntry = await _resourceRepository.GetAsync(a => a.Name == resourceName);

            // Next 
            var query = await _subscriptionRepository.WithDetailsAsync();

            var counter = query.Count();

            var now = DateTime.UtcNow;
            var entries = query
                .Where(a => !a.IsTerminated && a.EffectiveOn < now && a.Plan.Breakdowns.Any(b => b.ResourceId == resourceEntry.Id))
                .ToList();

            if (entries.Count > 0 )
            {
                // Find out the breakdown ... 
                // Should be only one 
                var effectiveSubr = entries.First();
                var breakdown = effectiveSubr.Plan.Breakdowns.First(c => c.ResourceId == resourceEntry.Id);
                return isTenantLevel ? breakdown.LimitAcrossTenant : breakdown.LimitPerUser;
            }

            return null;
        }
    }
}
