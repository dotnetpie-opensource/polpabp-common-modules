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
                /* Add custom repositories here. Example:
                 * options.AddRepository<Question, EfCoreQuestionRepository>();
                 */
        });
    }
}
