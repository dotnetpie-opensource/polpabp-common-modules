using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using PolpAbp.Directory.Domain.Entities;
using PolpAbp.Directory.Domain.Repositories;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore;
using System.Collections.Generic;

namespace PolpAbp.Directory.EntityFrameworkCore
{
    public class EfCoreCountryRepository : EfCoreRepository<IDirectoryDbContext, Country, Guid>,
        ICountryRepository
    {

        public EfCoreCountryRepository(IDbContextProvider<IDirectoryDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task AddStateProvincesAsync(Country country, IEnumerable<StateProvince> stateProvinces)
        {
            country.StateProvinces.AddRange(stateProvinces);
            var context = await GetDbContextAsync();
            await context.SaveChangesAsync();
        }

        public async Task RemoveStateProvincesAsync(Country country, IEnumerable<StateProvince> stateProvinces)
        {
            foreach (var a in stateProvinces)
            {
                country.StateProvinces.Remove(a);
            }
            var context = await GetDbContextAsync();
            await context.SaveChangesAsync();
        }
    }
}

