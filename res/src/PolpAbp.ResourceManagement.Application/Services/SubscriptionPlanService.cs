using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using PolpAbp.ResourceManagement.Core;
using PolpAbp.ResourceManagement.Domain.Entities;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace PolpAbp.ResourceManagement.Services
{
    [RemoteService(false)]
    public class SubscriptionPlanService : ISubscriptionPlanService, ITransientDependency
    {
        private readonly IRepository<TenantSubscription, Guid> _subscriptionRepository;
        private readonly IRepository<Resource, Guid> _resourceRepository;
        private readonly ResourceLogOptions _options;

        public SubscriptionPlanService(IRepository<TenantSubscription, Guid> subscriptionRepository, 
            IRepository<Resource, Guid> resourceRepository,
            IOptions<ResourceLogOptions> options)
        {
            _subscriptionRepository = subscriptionRepository;
            _resourceRepository = resourceRepository;
            _options = options.Value;
        }

        public async Task<long> GetQuotaAsync(string resourceName, bool isTenantLevel, CancellationToken cancellationToken)
        {
            // Require the resource id 
            var resourceEntry = await _resourceRepository.GetAsync(a => a.Name == resourceName, cancellationToken: cancellationToken);

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

            // Read the configuration 
            var anyCfg = _options.FreeUsageLimit.FirstOrDefault(x => x.ResourceName == resourceName);
            if (anyCfg != null)
            {
                return isTenantLevel ? anyCfg.LimitAcrossTenant : anyCfg.LimitPerUser;
            }

            return 0;
        }

        public async Task<Tuple<DateTime, DateTime?>> GetCurrentBillingPeriodAsync(string resourceName, CancellationToken cancellationToken)
        {
            var resourceEntry = await _resourceRepository.GetAsync(a => a.Name == resourceName, cancellationToken: cancellationToken);

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

            // Fall back to the configured monthly plan.
            // Get the calendar month 
            var startingMonth = new DateTime(now.Year, now.Month, 1);
            var endMonth = startingMonth.AddMonths(1);

            return Tuple.Create<DateTime, DateTime?>(startingMonth, endMonth);
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
