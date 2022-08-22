using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using PolpAbp.Directory.Domain.Entities;
using PolpAbp.Directory.Domain.Repositories;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore;

namespace PolpAbp.Directory.EntityFrameworkCore
{
    public class EfCoreCountryRepository : EfCoreRepository<IDirectoryDbContext, Country, Guid>,
        ICountryRepository
    {

        public EfCoreCountryRepository(IDbContextProvider<IDirectoryDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task AddStateProvinceAsync(Country country, StateProvince stateProvince)
        {
            country.StateProvinces.Add(stateProvince);
            var context = await GetDbContextAsync();
            await context.SaveChangesAsync();
        }

        public async Task RemoveStateProvinceAsync(Country country, StateProvince stateProvince)
        {
            country.StateProvinces.Remove(stateProvince);
            var context = await GetDbContextAsync();
            await context.SaveChangesAsync();
        }
    }
}

