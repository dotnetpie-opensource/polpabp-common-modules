using PolpAbp.ResourceManagement.Core;
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

        public async Task<Tuple<DateTime, DateTime?>> GetCurrentBillingPeriodAsync(string resourceName)
        {
            var resourceEntry = await _resourceRepository.GetAsync(a => a.Name == resourceName);

            // Next 
            var query = await _subscriptionRepository.WithDetailsAsync();
            var now = DateTime.UtcNow;
            var entry = query
                .Where(a => !a.IsTerminated && a.EffectiveOn < now && a.Plan.Breakdowns.Any(b => b.ResourceId == resourceEntry.Id))
                .FirstOrDefault();

            if (entry != null )
            {
                return ComputeBillingDateRange(entry.BillingCycleOn, entry.Plan.BillingCycle, now);
            }

            throw new Exception("Plan not defined yet");
        }

        private Tuple<DateTime, DateTime?> ComputeBillingDateRange(DateTime cycleOn, BillingCycleEnum cycle, DateTime referenceTime)
        {
            var maxLoop = 0;
            // a < x < b
            var rangeStart = cycleOn;
            while (maxLoop < 365 * 100)
            {
                var rangeEnd = GetRangeEndDate(cycle, rangeStart);
                // Case 1:
                if (rangeEnd == null)
                {
                    return Tuple.Create<DateTime, DateTime?>(rangeStart, null);
                }
                // Case 2:
                if (referenceTime >= rangeStart && referenceTime < rangeEnd)
                {
                    return Tuple.Create(rangeStart, rangeEnd);
                }

                rangeStart = rangeEnd!.Value;
                maxLoop ++;
            }

            throw new Exception("Wrong cycle");
        } 

        private DateTime? GetRangeEndDate(BillingCycleEnum cycle, DateTime startDate)
        {            
            switch (cycle)
            {
                case BillingCycleEnum.Day:
                    return startDate.AddDays(1);
                case BillingCycleEnum.Month:
                    return startDate.AddMonths(1);
                case BillingCycleEnum.Year:
                    return startDate.AddYears(1);
                case BillingCycleEnum.Forever:
                    return null;
            }

            return null;
        }
    }
}
