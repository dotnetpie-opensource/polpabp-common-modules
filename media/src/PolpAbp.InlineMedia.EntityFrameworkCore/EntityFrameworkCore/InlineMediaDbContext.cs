using Microsoft.EntityFrameworkCore;
using PolpAbp.InlineMedia.Domain.Entities;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace PolpAbp.InlineMedia.EntityFrameworkCore;

[ConnectionStringName(InlineMediaDbProperties.ConnectionStringName)]
public class InlineMediaDbContext : AbpDbContext<InlineMediaDbContext>, IInlineMediaDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     */
    public DbSet<Picture> Pictures { get; set; }

    public InlineMediaDbContext(DbContextOptions<InlineMediaDbContext> options)
        : base(options)
    {

    }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureInlineMedia();
    }
}
