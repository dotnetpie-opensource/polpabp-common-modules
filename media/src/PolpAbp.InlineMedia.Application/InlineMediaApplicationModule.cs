using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;

namespace PolpAbp.InlineMedia;

[DependsOn(
    typeof(InlineMediaDomainModule),
    typeof(InlineMediaApplicationContractsModule),
    typeof(AbpDddApplicationModule),
    typeof(AbpAutoMapperModule),
    typeof(PolpAbp.Framework.PolpAbpFrameworkAbpExtensionsModule)
    )]
public class InlineMediaApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<InlineMediaApplicationModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<InlineMediaApplicationModule>(validate: true);
        });
    }
}
