using PolpAbp.Directory.Localization;
using Volo.Abp.Application.Services;

namespace PolpAbp.Directory;

public abstract class DirectoryAppService : ApplicationService
{
    protected DirectoryAppService()
    {
        LocalizationResource = typeof(DirectoryResource);
        ObjectMapperContext = typeof(DirectoryApplicationModule);
    }
}
