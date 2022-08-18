using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace PolpAbp.Contact.EntityFrameworkCore;

[DependsOn(
    typeof(ContactDomainModule),
    typeof(AbpEntityFrameworkCoreModule)
)]
public class ContactEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<ContactDbContext>(options =>
        {
            // Only include the aggregated ones.
            options.AddDefaultRepositories(includeAllEntities: false);
        });
    }
}
