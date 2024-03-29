﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PolpAbp.MultiTenancy.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace PolpAbp.MultiTenancy.Domain.Repositories
{
    public interface ITenantAddOnRepository : IBasicRepository<TenantAddOn, Guid>
    {
        Task AddAddressMapsAsync(Guid addOnId, IEnumerable<TenantAddressMap> addressMaps, bool autoSave = false, CancellationToken cancellationToken = default);

        Task RemoveAddressMapsByMapIdsAsync(Guid addOnId, IEnumerable<Guid> addressMapIds, bool autoSave = false, CancellationToken cancellationToken = default);

        Task UpdateAddressMapAsync(Guid addOnId, Guid addressMapId,
           Action<TenantAddressMap> func, bool autoSave = false, CancellationToken cancellationToken = default);

        Task AddContactMapsAsync(Guid addOnId, IEnumerable<TenantContactMap> contactMaps, bool autoSave = false, CancellationToken cancellationToken = default);

        Task RemoveContactMapsByMapIdsAsync(Guid addOnId, IEnumerable<Guid> contactMapIds, bool autoSave = false, CancellationToken cancellationToken = default);

        Task UpdateContactMapAsync(Guid addOnId, Guid contactMapId,
            Action<TenantContactMap> func, bool autoSave = false, CancellationToken cancellationToken = default);

        Task AddPictureMapsAsync(Guid addOnId, IEnumerable<TenantPictureMap> pictureMaps, bool autoSave = false, CancellationToken cancellationToken = default);

        Task RemovePictureMapsByMapIdsAsync(Guid addOnId, IEnumerable<Guid> pictureMapIds, bool autoSave = false, CancellationToken cancellationToken = default);

        Task UpdatePicutreMapAsync(Guid addOnId, Guid pictureMapId,
            Action<TenantPictureMap> func, bool autoSave = false, CancellationToken cancellationToken = default);

        Task<Tuple<List<TenantAddOn>, int>> SearchTenantsAsync(int skipCount, int maxResultCount, string sorting,
                   bool IsAutoProvisioningEnabled,
                   string keyword = null,
                   bool includeDetails = false,
                   CancellationToken cancellationToken = default);
        Task RemoveAddressMapsByAddressIdsAsync(Guid addOnId, IEnumerable<Guid> addressIds, bool autoSave = false, CancellationToken cancellationToken = default);
        Task RemoveContactMapsByContactIdsAsync(Guid addOnId, IEnumerable<Guid> contactIds, bool autoSave = false, CancellationToken cancellationToken = default);
        Task RemovePictureMapsByPictureIdsAsync(Guid addOnId, IEnumerable<Guid> pictureIds, bool autoSave = false, CancellationToken cancellationToken = default);
        Task<TenantAddOn> FindByTenantIdAsync(Guid tenantId);
        Task<Guid> EnsureForTenantIdAsync(Guid tenantId, bool autoSave = false, CancellationToken cancellationToken = default);
    }
}