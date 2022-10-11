using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace PolpAbp.MultiTenancy;

[DependsOn(
    typeof(MultiTenancyDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
public class MultiTenancyApplicationContractsModule : AbpModule
{

}
