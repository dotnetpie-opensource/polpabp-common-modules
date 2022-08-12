using Volo.Abp.Modularity;

namespace PolpAbp.Contact;

[DependsOn(
    typeof(ContactApplicationModule),
    typeof(ContactDomainTestModule)
    )]
public class ContactApplicationTestModule : AbpModule
{

}
