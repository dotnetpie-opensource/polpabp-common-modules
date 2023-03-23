using Volo.Abp.DependencyInjection;

namespace PolpAbp.ResourceManagement.Services
{
    [ExposeServices(typeof(SmsResourceLogContributor))]
    public class SmsResourceLogContributor : ResourceLogContributor, ITransientDependency
    {
        public override string ResourceName => ResourceManagementTestConsts.SmsResourceName;
    }
}
