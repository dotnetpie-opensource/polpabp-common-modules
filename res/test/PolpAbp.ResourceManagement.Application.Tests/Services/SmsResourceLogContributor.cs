using PolpAbp.ResourceManagement.Domain.Entities;
using System;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace PolpAbp.ResourceManagement.Services
{
    [ExposeServices(typeof(SmsResourceLogContributor))]
    public class SmsResourceLogContributor : ResourceLogContributor, ITransientDependency
    {
        public SmsResourceLogContributor(IRepository<ResourceUsageLog, Guid> usageLogRepo, 
            IRepository<ResourceMonthlyUsage, Guid> monthlyUsageRepo, 
            IRepository<ResourceYearlyUsage, Guid> yearlyUsageRepo) 
            : base(usageLogRepo, monthlyUsageRepo, yearlyUsageRepo)
        {
        }

        public override string ResourceName => ResourceManagementTestConsts.SmsResourceName;
    }
}
