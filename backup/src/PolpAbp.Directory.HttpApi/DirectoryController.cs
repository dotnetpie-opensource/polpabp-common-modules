using PolpAbp.Directory.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace PolpAbp.Directory;

public abstract class DirectoryController : AbpControllerBase
{
    protected DirectoryController()
    {
        LocalizationResource = typeof(DirectoryResource);
    }
}
