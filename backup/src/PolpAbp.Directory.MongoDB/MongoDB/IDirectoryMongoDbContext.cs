using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace PolpAbp.Directory.MongoDB;

[ConnectionStringName(DirectoryDbProperties.ConnectionStringName)]
public interface IDirectoryMongoDbContext : IAbpMongoDbContext
{
    /* Define mongo collections here. Example:
     * IMongoCollection<Question> Questions { get; }
     */
}
