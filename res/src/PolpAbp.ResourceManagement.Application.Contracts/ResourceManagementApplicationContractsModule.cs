using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace PolpAbp.ResourceManagement;

[DependsOn(
    typeof(ResourceManagementDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
public class ResourceManagementApplicationContractsModule : AbpModule
{

}
