using Microsoft.Extensions.DependencyInjection;
using PolpAbp.MultiTenancy.Domain.Entities;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;
using PolpAbp.Extensions.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.DependencyInjection;
using Microsoft.EntityFrameworkCore;

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

        Configure<AbpEntityOptions>(options =>
        {
            options.Entity<TenantAddOn>(a =>
            {
                a.DefaultWithDetailsFunc = (q) =>
                {
                    return q.Include(c => c.AddressMaps)
                    .Include(c => c.PictureMaps)
                    .Include(c => c.ContactMaps);
                };
            });
        });
    }
}
