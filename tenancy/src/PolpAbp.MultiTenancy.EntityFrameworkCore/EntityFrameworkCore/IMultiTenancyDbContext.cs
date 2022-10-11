using Microsoft.EntityFrameworkCore;
using PolpAbp.MultiTenancy.Domain.Entities;
using System.Diagnostics.Metrics;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace PolpAbp.MultiTenancy.EntityFrameworkCore;

[ConnectionStringName(MultiTenancyDbProperties.ConnectionStringName)]
public interface IMultiTenancyDbContext : IEfCoreDbContext
{
    public DbSet<TenantAddOn> TenantAddOns { get; set; }

}
