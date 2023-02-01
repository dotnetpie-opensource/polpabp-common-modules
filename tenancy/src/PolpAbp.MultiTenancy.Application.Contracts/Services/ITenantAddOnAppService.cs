using System;
using PolpAbp.MultiTenancy.Dtos;
using System.Threading;
using System.Threading.Tasks;

namespace PolpAbp.MultiTenancy.Services
{
    public interface ITenantAddOnAppService
    {
        Task AddOrUpdatePictureMapAsync(Guid id, TenantPictureMapInputDto dto, bool autoSave = false, CancellationToken cancellationToken = default);
        Task<Guid> CreateAsync(TenantAddOnInputDto dto, bool autoSave = false, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, bool autoSave = false, CancellationToken cancellationToken = default);
        Task DeletePicutreMapAsync(Guid id, Guid pictureId, bool autoSave = false, CancellationToken cancellationToken = default);
        Task UpdateAsyc(Guid id, TenantAddOnInputDto input, bool autoSave = false, CancellationToken cancellationToken = default);
    }
}

