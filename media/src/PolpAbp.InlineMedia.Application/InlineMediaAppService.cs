using PolpAbp.InlineMedia.Localization;
using Volo.Abp.Application.Services;

namespace PolpAbp.InlineMedia;

public abstract class InlineMediaAppService : ApplicationService
{
    protected InlineMediaAppService()
    {
        LocalizationResource = typeof(InlineMediaResource);
        ObjectMapperContext = typeof(InlineMediaApplicationModule);
    }
}
