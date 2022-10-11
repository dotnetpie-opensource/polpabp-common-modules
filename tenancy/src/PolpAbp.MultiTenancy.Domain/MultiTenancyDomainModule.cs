using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace PolpAbp.MultiTenancy;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(MultiTenancyDomainSharedModule)
)]
public class MultiTenancyDomainModule : AbpModule
{

}
