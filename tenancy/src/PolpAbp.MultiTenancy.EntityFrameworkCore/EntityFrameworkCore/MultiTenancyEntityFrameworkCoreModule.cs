using Microsoft.Extensions.DependencyInjection;
using PolpAbp.MultiTenancy.Domain.Entities;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace PolpAbp.MultiTenancy.EntityFrameworkCore;

[DependsOn(
    typeof(MultiTenancyDomainModule),
    typeof(AbpEntityFrameworkCoreModule)
)]
public class MultiTenancyEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<MultiTenancyDbContext>(options =>
        {
            options.AddRepository<TenantAddOn, EfCoreTenantAddOnRepository>();
        });
    }
}
