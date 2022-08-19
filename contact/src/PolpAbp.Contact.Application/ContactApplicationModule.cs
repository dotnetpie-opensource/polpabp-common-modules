using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;
using PolpAbp.Framework;

namespace PolpAbp.Contact;

[DependsOn(
    typeof(AbpDddApplicationModule),
    typeof(AbpAutoMapperModule),
    typeof(ContactDomainModule),
    typeof(ContactApplicationContractsModule),
    typeof(PolpAbpFrameworkAbpExtensionsModule)
    )]
public class ContactApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<ContactApplicationModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<ContactApplicationModule>(validate: true);
        });
    }
}
