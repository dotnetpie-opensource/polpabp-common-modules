using System;
using PolpAbp.MultiTenancy.Dtos;
using System.Threading;
using System.Threading.Tasks;

namespace PolpAbp.MultiTenancy.Services
{
    public interface ITenantAddOnAppService
    {
        Task<Guid> CreateAsync(TenantAddOnInputDto dto, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task UpdateAsyc(Guid id, TenantAddOnInputDto input, CancellationToken cancellationToken = default);
    }
}

