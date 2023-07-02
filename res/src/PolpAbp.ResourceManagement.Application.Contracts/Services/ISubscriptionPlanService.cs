using System;
using System.Threading;
using System.Threading.Tasks;

namespace PolpAbp.ResourceManagement.Services
{
    public interface ISubscriptionPlanService
    {
        Task<Tuple<DateTime, DateTime?>> GetCurrentBillingPeriodAsync(string resourceName, CancellationToken cancellationToken);
        Task<long> GetQuotaAsync(string resourceName, bool isTenantLevel, CancellationToken cancellationToken);
    }
}
