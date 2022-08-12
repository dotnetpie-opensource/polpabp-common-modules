using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace DotNetPie.PolpAbp.Directory;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(DirectoryDomainSharedModule)
)]
public class DirectoryDomainModule : AbpModule
{

}
