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
        Task AddAddressesAsync(TenantAddOn addOn, IEnumerable<TenantAddressMap> addressMaps, bool autoSave = false, CancellationToken cancellationToken = default);

        Task RemoveAddressesAsync(TenantAddOn addOn, IEnumerable<TenantAddressMap> addressMaps, bool autoSave = false, CancellationToken cancellationToken = default);

        Task AddContactsAsync(TenantAddOn addOn, IEnumerable<TenantConactMap> contactMaps, bool autoSave = false, CancellationToken cancellationToken = default);

        Task RemoveContactsAsync(TenantAddOn addOn, IEnumerable<TenantConactMap> contactMaps, bool autoSave = false, CancellationToken cancellationToken = default);

        Task AddPicturesAsync(TenantAddOn addOn, IEnumerable<TenantPictureMap> pictureMaps, bool autoSave = false, CancellationToken cancellationToken = default);

        Task RemovePicturesAsync(TenantAddOn addOn, IEnumerable<TenantPictureMap> pictureMaps, bool autoSave = false, CancellationToken cancellationToken = default);

        Task<Tuple<List<TenantAddOn>, int>> SearchTenantsAsync(int skipCount, int maxResultCount, string sorting,
                   bool IsAutoProvisioningEnabled,
                   string keyword = null,
                   bool includeDetails = false,
                   CancellationToken cancellationToken = default);
    }
}

