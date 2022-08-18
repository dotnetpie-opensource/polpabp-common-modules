using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace PolpAbp.Directory.EntityFrameworkCore;

[DependsOn(
    typeof(DirectoryDomainModule),
    typeof(AbpEntityFrameworkCoreModule)
)]
public class DirectoryEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<DirectoryDbContext>(options =>
        {
            // Only include the aggregated ones.
            options.AddDefaultRepositories(includeAllEntities: false);
        });
    }
}
