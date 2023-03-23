using Microsoft.EntityFrameworkCore;
using PolpAbp.ResourceManagement.Domain.Entities;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace PolpAbp.ResourceManagement.EntityFrameworkCore;

[ConnectionStringName(ResourceManagementDbProperties.ConnectionStringName)]
public class ResourceManagementDbContext : AbpDbContext<ResourceManagementDbContext>, IResourceManagementDbContext
{
    public DbSet<Resource> Resources { get; set; }
    public DbSet<ResourceUsageLog> UsageLogs { get; set; }
    public DbSet<ResourceMonthlyUsage> ResourceMonthlyUsages { get; set; }
    public DbSet<ResourceYearlyUsage> ResourceYearlyUsages { get; set; }

    public DbSet<Plan> Plans { get; set; }
    public DbSet<PlanBreakdown> Breakdowns { get; set; }
    public DbSet<TenantSubscription> TenantSubscriptions { get; set; }

    public ResourceManagementDbContext(DbContextOptions<ResourceManagementDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureResourceManagement();
    }
}
