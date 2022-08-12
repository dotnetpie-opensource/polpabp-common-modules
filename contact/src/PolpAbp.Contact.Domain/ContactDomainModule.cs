using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace DotNetPie.PolpAbp.Contact;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(ContactDomainSharedModule)
)]
public class ContactDomainModule : AbpModule
{

}
