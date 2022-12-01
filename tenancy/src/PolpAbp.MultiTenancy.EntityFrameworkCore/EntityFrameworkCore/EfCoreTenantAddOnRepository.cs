using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PolpAbp.MultiTenancy.Domain.Entities;
using PolpAbp.MultiTenancy.Domain.Repositories;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace PolpAbp.MultiTenancy.EntityFrameworkCore
{
    public class EfCoreTenantAddOnRepository : PolpAbpEfCoreRepository<IMultiTenancyDbContext, TenantAddOn>,
        ITenantAddOnRepository
    {
        public EfCoreTenantAddOnRepository(IDbContextProvider<IMultiTenancyDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task AddAddressMapsAsync(Guid addOnId, IEnumerable<TenantAddressMap> addressMaps, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            await AddOrRemoveChildItemsAsync(addOnId, a => a.AddressMaps, (entity) =>
            {
                entity.AddressMaps.AddRange(addressMaps);
            }, autoSave, cancellationToken);
        }

        public async Task RemoveAddressMapsAsync(Guid addOnId, IEnumerable<Guid> addressMapIds, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            await AddOrRemoveChildItemsAsync(addOnId, a => a.AddressMaps, (entity) =>
            {
                var candidates = entity.AddressMaps.Where(b => addressMapIds.Contains(b.Id));
                foreach (var c in candidates)
                {
                    entity.AddressMaps.Remove(c);
                }
            }, autoSave, cancellationToken);
        }

        public async Task UpdateAddressMapAsync(Guid addOnId, Guid addressMapId,
            Action<TenantAddressMap> func, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            await UpdateChildItemAsync(addOnId, a => a.AddressMaps, (entity) =>
            {
                var candidate = entity.AddressMaps.Find(b => b.Id == addressMapId);
                func(candidate);
            }, autoSave, cancellationToken);
        }

        public async Task AddContactMapsAsync(Guid addOnId, IEnumerable<TenantConactMap> contactMaps, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            await AddOrRemoveChildItemsAsync(addOnId, a => a.ContactMaps, (entity) =>
            {
                entity.ContactMaps.AddRange(contactMaps);
            }, autoSave, cancellationToken);
        }

        public async Task RemoveContactMapsAsync(Guid addOnId, IEnumerable<Guid> contactMapIds, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            await AddOrRemoveChildItemsAsync(addOnId, a => a.ContactMaps, (entity) =>
            {
                var candidates = entity.ContactMaps.Where(b => contactMapIds.Contains(b.Id));
                foreach (var c in candidates)
                {
                    entity.ContactMaps.Remove(c);
                }
            }, autoSave, cancellationToken);
        }

        public async Task UpdateContactMapAsync(Guid addOnId, Guid contactMapId,
            Action<TenantConactMap> func, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            await UpdateChildItemAsync(addOnId, a => a.ContactMaps, (entity) =>
            {
                var candidate = entity.ContactMaps.Find(b => b.Id == contactMapId);
                func(candidate);
            }, autoSave, cancellationToken);
        }

        public async Task AddPictureMapsAsync(Guid addOnId, IEnumerable<TenantPictureMap> pictureMaps, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            await AddOrRemoveChildItemsAsync(addOnId, a => a.PictureMaps, (entity) =>
            {
                entity.PictureMaps.AddRange(pictureMaps);
            }, autoSave, cancellationToken);
        }

        public async Task RemovePictureMapsAsync(Guid addOnId, IEnumerable<Guid> pictureMapIds, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            await AddOrRemoveChildItemsAsync(addOnId, a => a.PictureMaps, (entity) =>
            {
                var candidates = entity.PictureMaps.Where(b => pictureMapIds.Contains(b.Id));
                foreach (var c in candidates)
                {
                    entity.PictureMaps.Remove(c);
                }
            }, autoSave, cancellationToken);
        }

        public async Task UpdatePicutreMapAsync(Guid addOnId, Guid pictureMapId,
            Action<TenantPictureMap> func, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            await UpdateChildItemAsync(addOnId, a => a.PictureMaps, (entity) =>
            {
                var candidate = entity.PictureMaps.Find(b => b.Id == pictureMapId);
                func(candidate);
            }, autoSave, cancellationToken);
        }

        public async Task<Tuple<List<TenantAddOn>, int>> SearchTenantsAsync(int skipCount, int maxResultCount, string sorting,
            bool IsAutoProvisioningEnabled,
            string keyword = null,
            bool includeDetails = false,
            CancellationToken cancellationToken = default)
        {
            IQueryable<TenantAddOn> queryable;
            if (includeDetails)
            {
                queryable = await WithDetailsAsync();
            }
            else
            {
                queryable = await GetQueryableAsync();
            }

            if (IsAutoProvisioningEnabled)
            {
                queryable = queryable.Where(a => a.IsAutoProvisioningEnabled);
            }

            if (!string.IsNullOrEmpty(keyword))
            {
                queryable = queryable.Where(a => a.DisplayName.Contains(keyword) || a.Description.Contains(keyword));
            }

            queryable = queryable.OrderBy(a => a.CreationTime);

            var total = await queryable.CountAsync(GetCancellationToken(cancellationToken));

            var items = await queryable.ToListAsync(GetCancellationToken(cancellationToken));

            return Tuple.Create<List<TenantAddOn>, int>(items, total);
        }
    }
}

