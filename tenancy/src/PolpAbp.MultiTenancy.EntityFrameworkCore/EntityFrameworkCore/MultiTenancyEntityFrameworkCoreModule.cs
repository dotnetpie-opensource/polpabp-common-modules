using Microsoft.Extensions.DependencyInjection;
using PolpAbp.MultiTenancy.Domain.Entities;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;
using PolpAbp.Extensions.EntityFrameworkCore;

namespace PolpAbp.MultiTenancy.EntityFrameworkCore;

[DependsOn(
    typeof(MultiTenancyDomainModule),
    typeof(PolpAbpExtensionsEntityFrameworkCoreModule)
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
