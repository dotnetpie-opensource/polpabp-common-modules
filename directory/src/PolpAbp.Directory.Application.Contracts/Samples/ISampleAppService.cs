using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace PolpAbp.Directory.Samples;

public interface ISampleAppService : IApplicationService
{
    Task<SampleDto> GetAsync();

    Task<SampleDto> GetAuthorizedAsync();
}
