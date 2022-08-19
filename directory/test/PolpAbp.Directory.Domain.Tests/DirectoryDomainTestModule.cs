using PolpAbp.Directory.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace PolpAbp.Directory;

/* Domain tests are configured to use the EF Core provider.
 * You can switch to MongoDB, however your domain tests should be
 * database independent anyway.
 */
[DependsOn(
    typeof(DirectoryEntityFrameworkCoreTestModule)
    )]
public class DirectoryDomainTestModule : AbpModule
{

}
