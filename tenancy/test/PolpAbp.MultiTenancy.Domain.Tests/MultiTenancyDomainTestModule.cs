using PolpAbp.MultiTenancy.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace PolpAbp.MultiTenancy;

/* Domain tests are configured to use the EF Core provider.
 * You can switch to MongoDB, however your domain tests should be
 * database independent anyway.
 */
[DependsOn(
    typeof(MultiTenancyEntityFrameworkCoreTestModule)
    )]
public class MultiTenancyDomainTestModule : AbpModule
{

}
