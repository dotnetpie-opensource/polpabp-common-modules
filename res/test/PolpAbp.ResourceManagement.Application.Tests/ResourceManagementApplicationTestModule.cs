using PolpAbp.ResourceManagement.Core;
using PolpAbp.ResourceManagement.Services;
using Volo.Abp.Auditing;
using Volo.Abp.Modularity;

namespace PolpAbp.ResourceManagement;

[DependsOn(
    typeof(ResourceManagementApplicationModule),
    typeof(ResourceManagementDomainTestModule)
    )]
public class ResourceManagementApplicationTestModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {

        Configure<ResourceLogOptions>(options =>
        {
            options.Contributors.Add(ResourceManagementTestConsts.SmsResourceName, typeof(SmsResourceLogContributor));
        });
    }
}
