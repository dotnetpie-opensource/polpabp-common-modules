using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PolpAbp.ResourceManagement.Domain.Entities;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.DependencyInjection;
using Volo.Abp.Modularity;

namespace PolpAbp.ResourceManagement.EntityFrameworkCore;

[DependsOn(
    typeof(ResourceManagementDomainModule),
    typeof(AbpEntityFrameworkCoreModule)
)]
public class ResourceManagementEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<ResourceManagementDbContext>(options =>
        {
            options.AddDefaultRepositories(includeAllEntities: false);
        });

        Configure<AbpEntityOptions>(options =>
        {
            options.Entity<TenantSubscription>(a =>
            {
                a.DefaultWithDetailsFunc = (q) =>
                {
                    return q.Include(c => c.Plan)
                    .ThenInclude(d => d.Breakdowns)
                    .Include(c => c.Plan)
                    .ThenInclude(d => d.CategoryQuotas);
                };
            });
            options.Entity<Plan>(a =>
            {
                a.DefaultWithDetailsFunc = (q) =>
                {
                    return q.Include(c => c.Breakdowns)
                    .Include(d => d.CategoryQuotas);
                };
            });
        });
    }
}
