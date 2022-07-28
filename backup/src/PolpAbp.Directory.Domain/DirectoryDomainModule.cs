using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace PolpAbp.Directory;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(DirectoryDomainSharedModule)
)]
public class DirectoryDomainModule : AbpModule
{

}
