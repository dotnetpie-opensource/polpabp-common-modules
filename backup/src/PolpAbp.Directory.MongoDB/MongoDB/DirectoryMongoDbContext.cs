using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace PolpAbp.Directory.MongoDB;

[ConnectionStringName(DirectoryDbProperties.ConnectionStringName)]
public class DirectoryMongoDbContext : AbpMongoDbContext, IDirectoryMongoDbContext
{
    /* Add mongo collections here. Example:
     * public IMongoCollection<Question> Questions => Collection<Question>();
     */

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        modelBuilder.ConfigureDirectory();
    }
}
