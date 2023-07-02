using PolpAbp.ResourceManagement.Services;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus;

namespace PolpAbp.ResourceManagement.EventHandlers
{
    public class ResourceLogInfoHandler : ILocalEventHandler<ResourceLogInfo>,
          ITransientDependency
    {
        private readonly IResourceLogProvider _resourceLogProvider;

        public ResourceLogInfoHandler(IResourceLogProvider resourceLogProvider)
        {
            _resourceLogProvider = resourceLogProvider;
        }

        public async Task HandleEventAsync(ResourceLogInfo eventData)
        {
            // todo: Is there a way to get the cancellation token ...
            await _resourceLogProvider.StoreAsync(eventData, default);
        }
    }
}
