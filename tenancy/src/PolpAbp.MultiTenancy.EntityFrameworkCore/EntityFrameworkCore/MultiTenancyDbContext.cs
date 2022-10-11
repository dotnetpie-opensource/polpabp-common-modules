using Microsoft.EntityFrameworkCore;
using PolpAbp.MultiTenancy.Domain.Entities;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace PolpAbp.MultiTenancy.EntityFrameworkCore;

[ConnectionStringName(MultiTenancyDbProperties.ConnectionStringName)]
public class MultiTenancyDbContext : AbpDbContext<MultiTenancyDbContext>, IMultiTenancyDbContext
{
    public DbSet<TenantAddOn> TenantAddOns { get; set; }

    public MultiTenancyDbContext(DbContextOptions<MultiTenancyDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureMultiTenancy();
    }
}
