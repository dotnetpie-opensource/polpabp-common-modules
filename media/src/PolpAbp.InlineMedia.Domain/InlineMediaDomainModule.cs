using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace PolpAbp.InlineMedia;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(InlineMediaDomainSharedModule)
)]
public class InlineMediaDomainModule : AbpModule
{

}
