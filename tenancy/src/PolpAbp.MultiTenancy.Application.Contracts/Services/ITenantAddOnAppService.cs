using System;
using PolpAbp.MultiTenancy.Dtos;
using System.Threading;
using System.Threading.Tasks;

namespace PolpAbp.MultiTenancy.Services
{
    public interface ITenantAddOnAppService
    {
        Task AddOrUpdatePictureMapAsync(TenantPictureMapInputDto dto, bool autoSave = false, CancellationToken cancellationToken = default);
        Task<Guid> CreateOrUpdateAsync(TenantAddOnInputDto dto, bool autoSave = false, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, bool autoSave = false, CancellationToken cancellationToken = default);
        Task DeletePicutreMapAsync(Guid pictureId, bool autoSave = false, CancellationToken cancellationToken = default);
        Task<TenantAddOnOutputDto> GetAsync(Guid tenantId, CancellationToken cancellationToken = default);
    }
}

