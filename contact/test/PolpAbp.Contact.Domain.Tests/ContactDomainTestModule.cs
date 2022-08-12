using PolpAbp.Contact.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace PolpAbp.Contact;

/* Domain tests are configured to use the EF Core provider.
 * You can switch to MongoDB, however your domain tests should be
 * database independent anyway.
 */
[DependsOn(
    typeof(ContactEntityFrameworkCoreTestModule)
    )]
public class ContactDomainTestModule : AbpModule
{

}
