using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;
using PolpAbp.Framework;

namespace PolpAbp.Directory;

[DependsOn(
    typeof(AbpAutoMapperModule),
    typeof(AbpDddApplicationModule),
    typeof(DirectoryDomainModule),
    typeof(DirectoryApplicationContractsModule),
    typeof(PolpAbpFrameworkAbpExtensionsModule)
    )]
public class DirectoryApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<DirectoryApplicationModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<DirectoryApplicationModule>(validate: true);
        });
    }
}
