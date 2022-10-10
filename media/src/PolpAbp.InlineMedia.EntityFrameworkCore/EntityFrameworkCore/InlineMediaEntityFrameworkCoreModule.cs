using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace PolpAbp.InlineMedia.EntityFrameworkCore;

[DependsOn(
    typeof(InlineMediaDomainModule),
    typeof(AbpEntityFrameworkCoreModule)
)]
public class InlineMediaEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<InlineMediaDbContext>(options =>
        {
            options.AddDefaultRepositories(includeAllEntities: false);
        });
    }
}
