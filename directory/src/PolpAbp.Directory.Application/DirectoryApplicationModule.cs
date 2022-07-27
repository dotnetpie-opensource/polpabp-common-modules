using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;

namespace PolpAbp.Directory;

[DependsOn(
    typeof(DirectoryDomainModule),
    typeof(DirectoryApplicationContractsModule),
    typeof(AbpDddApplicationModule),
    typeof(AbpAutoMapperModule)
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
