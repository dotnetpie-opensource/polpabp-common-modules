using Microsoft.EntityFrameworkCore;
using PolpAbp.Directory.Domain.Entities;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace PolpAbp.Directory.EntityFrameworkCore;

[ConnectionStringName(DirectoryDbProperties.ConnectionStringName)]
public class DirectoryDbContext : AbpDbContext<DirectoryDbContext>, IDirectoryDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * public DbSet<Question> Questions { get; set; }
     */

    public DirectoryDbContext(DbContextOptions<DirectoryDbContext> options)
        : base(options)
    {

    }

    public DbSet<Country> Countries { get; set; }

    public DbSet<StateProvince> StateProvinces { get; set; }

    public DbSet<Address> Addresses { get; set; }

    public DbSet<Contact> Contacts { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureDirectory();
    }
}
