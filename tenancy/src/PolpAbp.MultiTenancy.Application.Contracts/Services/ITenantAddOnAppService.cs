using System;
using PolpAbp.MultiTenancy.Dtos;
using System.Threading;
using System.Threading.Tasks;

namespace PolpAbp.MultiTenancy.Services
{
    public interface ITenantAddOnAppService
    {
        Task<Guid> CreateAsync(TenantAddOnInputDto dto, bool autoSave = false, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, bool autoSave = false, CancellationToken cancellationToken = default);
        Task UpdateAsyc(Guid id, TenantAddOnInputDto input, bool autoSave = false, CancellationToken cancellationToken = default);
    }
}

