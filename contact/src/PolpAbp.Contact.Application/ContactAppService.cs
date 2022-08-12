using PolpAbp.Contact.Localization;
using Volo.Abp.Application.Services;

namespace PolpAbp.Contact;

public abstract class ContactAppService : ApplicationService
{
    protected ContactAppService()
    {
        LocalizationResource = typeof(ContactResource);
        ObjectMapperContext = typeof(ContactApplicationModule);
    }
}
