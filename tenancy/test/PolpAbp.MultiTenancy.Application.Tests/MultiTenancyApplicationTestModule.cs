using Volo.Abp.Modularity;

namespace PolpAbp.MultiTenancy;

[DependsOn(
    typeof(MultiTenancyApplicationModule),
    typeof(MultiTenancyDomainTestModule)
    )]
public class MultiTenancyApplicationTestModule : AbpModule
{

}
