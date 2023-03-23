using System.Threading.Tasks;

namespace PolpAbp.ResourceManagement.Services
{
    public interface ISubscriptionPlanService
    {
        Task<long?> GetQuotaAsync(string resourceName, bool isTenantLevel);
    }
}
