using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;
using PolpAbp.Directory.Domain.Repositories;
using PolpAbp.Directory.Domain.Entities;
using PolpAbp.Extensions.EntityFrameworkCore;

namespace PolpAbp.Directory.EntityFrameworkCore;

[DependsOn(
    typeof(DirectoryDomainModule),
    typeof(PolpAbpExtensionsEntityFrameworkCoreModule)
)]
public class DirectoryEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<DirectoryDbContext>(options =>
        {
            // Only include the aggregated ones.
            options.AddRepository<Country, EfCoreCountryRepository>();
        });
    }
}
