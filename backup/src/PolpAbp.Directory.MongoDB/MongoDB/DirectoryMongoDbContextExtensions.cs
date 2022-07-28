using Volo.Abp;
using Volo.Abp.MongoDB;

namespace PolpAbp.Directory.MongoDB;

public static class DirectoryMongoDbContextExtensions
{
    public static void ConfigureDirectory(
        this IMongoModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));
    }
}
