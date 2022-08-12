using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace PolpAbp.Contact;

[DependsOn(
    typeof(ContactDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
public class ContactApplicationContractsModule : AbpModule
{

}
