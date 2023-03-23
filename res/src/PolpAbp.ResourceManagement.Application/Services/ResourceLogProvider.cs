using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace PolpAbp.ResourceManagement.Services
{
    public class ResourceLogProvider : IResourceLogProvider, ITransientDependency
    {
        public IAbpLazyServiceProvider LazyServiceProvider { get; set; }

        protected ResourceLogOptions Options; 

        public ResourceLogProvider(IOptions<ResourceLogOptions> options)
        {
            Options = options.Value;
        }

        public async Task StoreAsync(ResourceLogInfo resourceLogInfo, bool autoSave = false)
        {
            if (Options.IsEnabled)
            {
                // todo: Exception handling
                var contributor = Options.Contributors.First(a => a.Key == resourceLogInfo.ResourceName);
                var instance = LazyServiceProvider.LazyGetRequiredService(contributor.Value) as IResourceLogContributor;

                await instance.StoreAsync(resourceLogInfo, autoSave);
            }
        }

        public virtual async Task<long> CountCurrentUsageAsync(string resourceName, Guid? userId, DateTime StartedOn, DateTime? EndedOn)
        {
            var contributor = Options.Contributors.First(a => a.Key == resourceName);
            var instance = LazyServiceProvider.LazyGetRequiredService(contributor.Value) as IResourceLogContributor;

                return await instance.CountCurrentUsageAsync(userId, StartedOn, EndedOn);
        }

        public virtual async Task<long> GetMonthlyUsageAsync(string resourceName, int year, int month)
        {
            var contributor = Options.Contributors.First(a => a.Key == resourceName);
            var instance = LazyServiceProvider.LazyGetRequiredService(contributor.Value) as IResourceLogContributor;

            return await instance.GetMonthlyUsageAsync(year, month);
        }

        public virtual async Task<long> GetYearlyUsageAsync(string resourceName, int year)
        {
            var contributor = Options.Contributors.First(a => a.Key == resourceName);
            var instance = LazyServiceProvider.LazyGetRequiredService(contributor.Value) as IResourceLogContributor;

            return await instance.GetYearlyUsageAsync(year);
        }
    }
}
