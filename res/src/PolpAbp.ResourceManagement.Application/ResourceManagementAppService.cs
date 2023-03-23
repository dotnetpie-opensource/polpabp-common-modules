using PolpAbp.ResourceManagement.Localization;
using Volo.Abp.Application.Services;

namespace PolpAbp.ResourceManagement;

public abstract class ResourceManagementAppService : ApplicationService
{
    protected ResourceManagementAppService()
    {
        LocalizationResource = typeof(ResourceManagementResource);
        ObjectMapperContext = typeof(ResourceManagementApplicationModule);
    }
}
