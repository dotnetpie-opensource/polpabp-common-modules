using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using PolpAbp.Directory.Domain.Entities;
using PolpAbp.Directory.Domain.Repositories;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Linq;

namespace PolpAbp.Directory.EntityFrameworkCore
{
    public class EfCoreCountryRepository : PolpAbpEfCoreRepository<IDirectoryDbContext, Country>,
        ICountryRepository
    {

        public EfCoreCountryRepository(IDbContextProvider<IDirectoryDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task AddStateProvincesAsync(Guid countryId, IEnumerable<StateProvince> stateProvinces,
            bool autoSave = false, CancellationToken cancellationToken = default)
        {
            await AddOrRemoveChildItemsAsync(countryId, a => a.StateProvinces, (entity) =>
            {
                entity.StateProvinces.AddRange(stateProvinces);
            }, autoSave, cancellationToken);
        }

        public async Task RemoveStateProvincesAsync(Guid countryId, IEnumerable<Guid> stateProvinceIds,
            bool autoSave = false, CancellationToken cancellationToken = default)
        {
            await AddOrRemoveChildItemsAsync(countryId, a => a.StateProvinces, (entity) =>
            {
                var candidates = entity.StateProvinces.Where(b => stateProvinceIds.Contains(b.Id)).ToList();
                foreach (var c in candidates)
                {
                    entity.StateProvinces.Remove(c);
                }
            }, autoSave, cancellationToken);
        }

        public async Task UpdateStateProvinceAsync(Guid countryId, Guid stateProvinceId,
            Action<StateProvince> func, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            await UpdateChildItemAsync(countryId, a => a.StateProvinces, (entity) =>
            {
                var candidate = entity.StateProvinces.Find(b => b.Id == stateProvinceId);
                func(candidate);
            }, autoSave, cancellationToken);
        }
    }
}

