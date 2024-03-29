﻿using PolpAbp.InlineMedia.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace PolpAbp.InlineMedia;

/* Domain tests are configured to use the EF Core provider.
 * You can switch to MongoDB, however your domain tests should be
 * database independent anyway.
 */
[DependsOn(
    typeof(InlineMediaEntityFrameworkCoreTestModule)
    )]
public class InlineMediaDomainTestModule : AbpModule
{

}
