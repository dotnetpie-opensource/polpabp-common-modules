using Localization.Resources.AbpUi;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;
using PolpAbp.Contact.Localization;

namespace PolpAbp.Contact;

[DependsOn(
    typeof(ContactApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class ContactHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(ContactHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<ContactResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}

