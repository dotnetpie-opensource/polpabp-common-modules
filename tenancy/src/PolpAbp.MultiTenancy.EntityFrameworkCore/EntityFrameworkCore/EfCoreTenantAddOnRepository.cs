using Microsoft.EntityFrameworkCore;
using PolpAbp.MultiTenancy.Domain.Entities;
using PolpAbp.MultiTenancy.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
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

        // Used when tenant is not set.
        public async Task<TenantAddOn> FindByTenantIdAsync(Guid tenantId)
        {
            var query = await WithDetailsAsync();
            var entity = query.Where(x => x.TenantId == tenantId).FirstOrDefault();
            return entity;
        }

        // Used when tenant is not set.
        public async Task<Guid> EnsureForTenantIdAsync(Guid tenantId, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            var query = await GetQueryableAsync();
            var entity = query.Where(x => x.TenantId == tenantId).FirstOrDefault();
            if (entity == null)
            {
                entity = new TenantAddOn(GuidGenerator.Create()) { TenantId = tenantId };
                await InsertAsync(entity, autoSave, cancellationToken);
            }
            return entity.Id;
        }

        public async Task AddAddressMapsAsync(Guid addOnId, IEnumerable<TenantAddressMap> addressMaps, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            await AddOrRemoveChildItemsAsync(addOnId, a => a.AddressMaps, (entity) =>
            {
                entity.AddressMaps.AddRange(addressMaps);
            }, autoSave, cancellationToken);
        }

        public async Task RemoveAddressMapsByMapIdsAsync(Guid addOnId, IEnumerable<Guid> mapIds, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            await AddOrRemoveChildItemsAsync(addOnId, a => a.AddressMaps, (entity) =>
            {
                var candidates = entity.AddressMaps.Where(b => mapIds.Contains(b.Id)).ToList();
                foreach (var c in candidates)
                {
                    entity.AddressMaps.Remove(c);
                }
            }, autoSave, cancellationToken);
        }

        public async Task RemoveAddressMapsByAddressIdsAsync(Guid addOnId, IEnumerable<Guid> addressIds, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            await AddOrRemoveChildItemsAsync(addOnId, a => a.AddressMaps, (entity) =>
            {
                var candidates = entity.AddressMaps.Where(b => addressIds.Contains(b.AddressId)).ToList();
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

        public async Task AddContactMapsAsync(Guid addOnId, IEnumerable<TenantContactMap> contactMaps, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            await AddOrRemoveChildItemsAsync(addOnId, a => a.ContactMaps, (entity) =>
            {
                entity.ContactMaps.AddRange(contactMaps);
            }, autoSave, cancellationToken);
        }

        public async Task RemoveContactMapsByMapIdsAsync(Guid addOnId, IEnumerable<Guid> mapIds, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            await AddOrRemoveChildItemsAsync(addOnId, a => a.ContactMaps, (entity) =>
            {
                var candidates = entity.ContactMaps.Where(b => mapIds.Contains(b.Id)).ToList();
                foreach (var c in candidates)
                {
                    entity.ContactMaps.Remove(c);
                }
            }, autoSave, cancellationToken);
        }

        public async Task RemoveContactMapsByContactIdsAsync(Guid addOnId, IEnumerable<Guid> contactIds, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            await AddOrRemoveChildItemsAsync(addOnId, a => a.ContactMaps, (entity) =>
            {
                var candidates = entity.ContactMaps.Where(b => contactIds.Contains(b.ContactId)).ToList();
                foreach (var c in candidates)
                {
                    entity.ContactMaps.Remove(c);
                }
            }, autoSave, cancellationToken);
        }

        public async Task UpdateContactMapAsync(Guid addOnId, Guid contactMapId,
            Action<TenantContactMap> func, bool autoSave = false, CancellationToken cancellationToken = default)
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

        public async Task RemovePictureMapsByMapIdsAsync(Guid addOnId, IEnumerable<Guid> mapIds, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            await AddOrRemoveChildItemsAsync(addOnId, a => a.PictureMaps, (entity) =>
            {
                var candidates = entity.PictureMaps.Where(b => mapIds.Contains(b.Id)).ToList();
                foreach (var c in candidates)
                {
                    entity.PictureMaps.Remove(c);
                }
            }, autoSave, cancellationToken);
        }

        public async Task RemovePictureMapsByPictureIdsAsync(Guid addOnId, IEnumerable<Guid> pictureIds, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            await AddOrRemoveChildItemsAsync(addOnId, a => a.PictureMaps, (entity) =>
            {
                var candidates = entity.PictureMaps.Where(b => pictureIds.Contains(b.PictureId)).ToList();
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

