using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PolpAbp.MultiTenancy.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace PolpAbp.MultiTenancy.Domain.Repositories
{
    public interface ITenantAddOnRepository : IBasicRepository<TenantAddOn, Guid>
    {
        Task AddAddressesAsync(Guid addOnId, IEnumerable<TenantAddressMap> addressMaps, bool autoSave = false, CancellationToken cancellationToken = default);

        Task RemoveAddressesAsync(Guid addOnId, IEnumerable<Guid> addressMapIds, bool autoSave = false, CancellationToken cancellationToken = default);

        Task UpdateAddressMapAsync(Guid addOnId, Guid addressMapId,
           Action<TenantAddressMap> func, bool autoSave = false, CancellationToken cancellationToken = default);

        Task AddContactsAsync(Guid addOnId, IEnumerable<TenantConactMap> contactMaps, bool autoSave = false, CancellationToken cancellationToken = default);

        Task RemoveContactsAsync(Guid addOnId, IEnumerable<Guid> contactMapIds, bool autoSave = false, CancellationToken cancellationToken = default);

        Task UpdateContactMapAsync(Guid addOnId, Guid contactMapId,
            Action<TenantConactMap> func, bool autoSave = false, CancellationToken cancellationToken = default);

        Task AddPicturesAsync(Guid addOnId, IEnumerable<TenantPictureMap> pictureMaps, bool autoSave = false, CancellationToken cancellationToken = default);

        Task RemovePicturesAsync(Guid addOnId, IEnumerable<Guid> pictureMapIds, bool autoSave = false, CancellationToken cancellationToken = default);

        Task UpdatePicutreMapAsync(Guid addOnId, Guid pictureMapId,
            Action<TenantPictureMap> func, bool autoSave = false, CancellationToken cancellationToken = default);

        Task<Tuple<List<TenantAddOn>, int>> SearchTenantsAsync(int skipCount, int maxResultCount, string sorting,
                   bool IsAutoProvisioningEnabled,
                   string keyword = null,
                   bool includeDetails = false,
                   CancellationToken cancellationToken = default);
    }
}

