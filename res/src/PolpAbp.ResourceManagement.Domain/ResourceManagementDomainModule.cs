using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace PolpAbp.ResourceManagement;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(ResourceManagementDomainSharedModule)
)]
public class ResourceManagementDomainModule : AbpModule
{

}
