using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace PolpAbp.Directory.EntityFrameworkCore;

[ConnectionStringName(DirectoryDbProperties.ConnectionStringName)]
public interface IDirectoryDbContext : IEfCoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
}
