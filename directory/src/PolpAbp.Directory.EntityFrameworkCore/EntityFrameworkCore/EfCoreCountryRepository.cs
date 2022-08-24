using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using PolpAbp.Directory.Domain.Entities;
using PolpAbp.Directory.Domain.Repositories;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;

namespace PolpAbp.Directory.EntityFrameworkCore
{
    public class EfCoreCountryRepository : EfCoreRepository<IDirectoryDbContext, Country, Guid>,
        ICountryRepository
    {

        public EfCoreCountryRepository(IDbContextProvider<IDirectoryDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task AddStateProvincesAsync(Country country, IEnumerable<StateProvince> stateProvinces, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            var context = await GetDbContextAsync();
            country.StateProvinces.AddRange(stateProvinces);
            if (autoSave) {
                await context.SaveChangesAsync(GetCancellationToken(cancellationToken));
            }
        }

        public async Task RemoveStateProvincesAsync(Country country, IEnumerable<StateProvince> stateProvinces, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            var context = await GetDbContextAsync();
            foreach (var a in stateProvinces)
            {
                country.StateProvinces.Remove(a);
            }
            if (autoSave)
            {
                await context.SaveChangesAsync(GetCancellationToken(cancellationToken));
            }
        }
    }
}

