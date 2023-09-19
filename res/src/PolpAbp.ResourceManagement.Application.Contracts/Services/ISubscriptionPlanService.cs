using PolpAbp.ResourceManagement.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PolpAbp.ResourceManagement.Services
{
    public interface ISubscriptionPlanService
    {
        Task<Tuple<DateTime, DateTime?>> GetCurrentBillingPeriodAsync(string resourceName, CancellationToken cancellationToken);
        Task<long> GetQuotaAsync(string resourceName, bool isTenantLevel, CancellationToken cancellationToken);
        Task<List<SubscriptionPlanOutputDto>> LoadEffectivePlansAsync(DateTime referenceTime, CancellationToken cancellationToken);
    }
}
