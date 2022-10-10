using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace PolpAbp.InlineMedia;

[DependsOn(
    typeof(InlineMediaDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
public class InlineMediaApplicationContractsModule : AbpModule
{

}
