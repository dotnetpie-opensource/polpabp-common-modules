using Volo.Abp.Modularity;

namespace PolpAbp.Directory;

[DependsOn(
    typeof(DirectoryApplicationModule),
    typeof(DirectoryDomainTestModule)
    )]
public class DirectoryApplicationTestModule : AbpModule
{

}
