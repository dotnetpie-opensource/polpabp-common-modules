using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace PolpAbp.Directory;

[DependsOn(
    typeof(DirectoryDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
public class DirectoryApplicationContractsModule : AbpModule
{

}
