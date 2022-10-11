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
    public class EfCoreTenantAddOnRepository : EfCoreRepository<IMultiTenancyDbContext, TenantAddOn, Guid>,
        ITenantAddOnRepository
    {
        public EfCoreTenantAddOnRepository(IDbContextProvider<IMultiTenancyDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task AddAddressesAsync(TenantAddOn addOn, IEnumerable<TenantAddressMap> addressMaps, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            var context = await GetDbContextAsync();
            addOn.AddressMaps.AddRange(addressMaps);
            if (autoSave)
            {
                await context.SaveChangesAsync(GetCancellationToken(cancellationToken));
            }
        }

        public async Task RemoveAddressesAsync(TenantAddOn addOn, IEnumerable<TenantAddressMap> addressMaps, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            var context = await GetDbContextAsync();
            foreach (var a in addressMaps)
            {
                addOn.AddressMaps.Remove(a);
            }
            if (autoSave)
            {
                await context.SaveChangesAsync(GetCancellationToken(cancellationToken));
            }
        }

        public async Task AddContactsAsync(TenantAddOn addOn, IEnumerable<TenantConactMap> contactMaps, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            var context = await GetDbContextAsync();
            addOn.ContactMaps.AddRange(contactMaps);
            if (autoSave)
            {
                await context.SaveChangesAsync(GetCancellationToken(cancellationToken));
            }
        }

        public async Task RemoveContactsAsync(TenantAddOn addOn, IEnumerable<TenantConactMap> contactMaps, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            var context = await GetDbContextAsync();
            foreach (var a in contactMaps)
            {
                addOn.ContactMaps.Remove(a);
            }
            if (autoSave)
            {
                await context.SaveChangesAsync(GetCancellationToken(cancellationToken));
            }
        }

        public async Task AddPicturesAsync(TenantAddOn addOn, IEnumerable<TenantPictureMap> pictureMaps, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            var context = await GetDbContextAsync();
            addOn.PictureMaps.AddRange(pictureMaps);
            if (autoSave)
            {
                await context.SaveChangesAsync(GetCancellationToken(cancellationToken));
            }
        }

        public async Task RemovePicturesAsync(TenantAddOn addOn, IEnumerable<TenantPictureMap> pictureMaps, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            var context = await GetDbContextAsync();
            foreach (var a in pictureMaps)
            {
                addOn.PictureMaps.Remove(a);
            }
            if (autoSave)
            {
                await context.SaveChangesAsync(GetCancellationToken(cancellationToken));
            }
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

