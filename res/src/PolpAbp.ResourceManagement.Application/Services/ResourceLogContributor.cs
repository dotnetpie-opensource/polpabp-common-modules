using PolpAbp.ResourceManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;

namespace PolpAbp.ResourceManagement.Services
{
    public abstract class ResourceLogContributor : IResourceLogContributor
    {
        public IAbpLazyServiceProvider LazyServiceProvider { get; set; }
        
        public abstract string ResourceName { get; }

        protected IRepository<ResourceUsageLog> UsageLogRepository => LazyServiceProvider.LazyGetRequiredService<IRepository<ResourceUsageLog>>();
        protected IRepository<ResourceMonthlyUsage> MonthlyUsageRepository => LazyServiceProvider.LazyGetRequiredService<IRepository<ResourceMonthlyUsage>>();
        protected IRepository<ResourceYearlyUsage> YearlyUsageRepository => LazyServiceProvider.LazyGetRequiredService<IRepository<ResourceYearlyUsage>>();

        protected IRepository<Resource> ResourceRepository => LazyServiceProvider.LazyGetRequiredService<IRepository<Resource>> ();
        protected IGuidGenerator GuidGenerator => LazyServiceProvider.LazyGetRequiredService<IGuidGenerator>();

        public virtual async Task StoreAsync(ResourceLogInfo resourceLogInfo, bool autoSave = false)
        {
            if (resourceLogInfo.ResourceName != ResourceName)
            {
                throw new Exception("Resource not matched");
            }

            var resourceEntry = await ResourceRepository.GetAsync(a => a.Name == ResourceName);

            var entity = new ResourceUsageLog(GuidGenerator.Create())
            {
                ResourceId = resourceEntry.Id,
                Usage = resourceLogInfo.Usage,
                UserId = resourceLogInfo.UserId,
                TenantId = resourceLogInfo.TenantId,
                CreationTime = resourceLogInfo.HappenedOn
            };

            await UsageLogRepository.InsertAsync(entity, autoSave);
        }

        public virtual async Task<long> CountCurrentUsageAsync(Guid? userId, DateTime StartedOn, DateTime? EndedOn)
        {
            var resourceEntry = await ResourceRepository.GetAsync(a => a.Name == ResourceName);

            var query = await UsageLogRepository.GetQueryableAsync();
            var amount = query
                .Where(x => x.ResourceId == resourceEntry.Id && x.CreationTime >= StartedOn)
                .WhereIf(userId.HasValue, w => w.UserId == userId.Value)
                .WhereIf(EndedOn.HasValue, y => y.CreationTime < EndedOn)
                .Select(z => z.Usage)
                .Sum();

            return amount;
        }

        public virtual async Task<long> GetMonthlyUsageAsync(int year, int month)
        {
            var resourceEntry = await ResourceRepository.GetAsync(a => a.Name == ResourceName);

            var query = await MonthlyUsageRepository.GetQueryableAsync();
            var entry = query
                .Where(x => x.ResourceId == resourceEntry.Id && x.Year == year && x.Month == month)
                .FirstOrDefault();

            var sum = 0L;
            if (entry != null)
            {
                sum = entry.Usage;
            }
            else
            {
                var startDate = new DateTime(year, month, 1, 0, 0, 0, DateTimeKind.Utc);
                var endDate = startDate.AddMonths(1);
                sum = await CountCurrentUsageAsync(null, startDate, endDate);
            }
            return sum;
        }

        public virtual async Task<long> GetYearlyUsageAsync(int year)
        {
            var resourceEntry = await ResourceRepository.GetAsync(a => a.Name == ResourceName);

            var query = await YearlyUsageRepository.GetQueryableAsync();
            var entry = query
                .Where(x => x.ResourceId == resourceEntry.Id && x.Year == year)
            .FirstOrDefault();

            var sum = 0L;
            if (entry != null)
            {
                sum = entry.Usage;
            }
            else
            {
                // Find out the
                for (var m = 1; m <= 12; m++)
                {
                    sum += await GetMonthlyUsageAsync(year, m);
                }
            }
            return sum;
        }
    }
}
