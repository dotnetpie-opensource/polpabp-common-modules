using Localization.Resources.AbpUi;
using PolpAbp.Directory.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace PolpAbp.Directory;

[DependsOn(
    typeof(DirectoryApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class DirectoryHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(DirectoryHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<DirectoryResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
