using Microsoft.EntityFrameworkCore;
using PolpAbp.ResourceManagement.Domain.Entities;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace PolpAbp.ResourceManagement.EntityFrameworkCore;

[ConnectionStringName(ResourceManagementDbProperties.ConnectionStringName)]
public interface IResourceManagementDbContext : IEfCoreDbContext
{
    DbSet<Resource> Resources { get; }
    DbSet<ResourceUsageLog> UsageLogs { get; }
    DbSet<ResourceMonthlyUsage> ResourceMonthlyUsages { get; }
    DbSet<ResourceYearlyUsage> ResourceYearlyUsages { get; }

    DbSet<Plan> Plans { get; }
    DbSet<PlanBreakdown> Breakdowns { get; }
    DbSet<TenantSubscription> TenantSubscriptions { get; }
}
