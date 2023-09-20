using System;
using PolpAbp.MultiTenancy.Dtos;
using System.Threading;
using System.Threading.Tasks;

namespace PolpAbp.MultiTenancy.Services
{
    public interface ITenantAddOnAppService
    {
        Task AddOrUpdateAddressMapAsync(TenantAddressMapInputDto dto, bool autoSave = false, CancellationToken cancellationToken = default);
        Task AddOrUpdatePictureMapAsync(TenantPictureMapInputDto dto, bool autoSave = false, CancellationToken cancellationToken = default);
        Task<Guid> CreateOrUpdateAsync(TenantAddOnInputDto dto, bool autoSave = false, CancellationToken cancellationToken = default);
        Task DeleteAddressMapAsync(Guid addressId, bool autoSave = false, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, bool autoSave = false, CancellationToken cancellationToken = default);
        Task DeletePicutreMapAsync(Guid pictureId, bool autoSave = false, CancellationToken cancellationToken = default);
        /// <summary>
        /// Loads the given tenant information. 
        /// Note that the method is not run in the scope of the current tenant. 
        /// </summary>
        /// <param name="tenantId">Tenant Id</param>
        /// <param name="cancellationToken">Token</param>
        /// <returns>Task</returns>
        Task<TenantAddOnOutputDto> GetBeyondTenantAsync(Guid tenantId, CancellationToken cancellationToken = default);
    }
}

