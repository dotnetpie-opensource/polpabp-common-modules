using PolpAbp.MultiTenancy.Localization;
using Volo.Abp.Application.Services;

namespace PolpAbp.MultiTenancy;

public abstract class MultiTenancyAppService : ApplicationService
{
    protected MultiTenancyAppService()
    {
        LocalizationResource = typeof(MultiTenancyResource);
        ObjectMapperContext = typeof(MultiTenancyApplicationModule);
    }
}
