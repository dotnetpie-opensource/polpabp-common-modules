using DotNetPie.PolpAbp.Directory.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.MultiTenancy;

namespace DotNetPie.PolpAbp.Directory.EntityFrameworkCore;

[IgnoreMultiTenancy]
[ConnectionStringName(DirectoryDbProperties.ConnectionStringName)]
public class DirectoryDbContext : AbpDbContext<DirectoryDbContext>, IDirectoryDbContext
{
    public DbSet<Country> Countries { get; set; }

    public DbSet<StateProvince> StateProvinces { get; set; }

    public DirectoryDbContext(DbContextOptions<DirectoryDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureDirectory();
    }
}
