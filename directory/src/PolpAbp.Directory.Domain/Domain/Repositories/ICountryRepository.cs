using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PolpAbp.Directory.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace PolpAbp.Directory.Domain.Repositories
{
    public interface ICountryRepository : IBasicRepository<Country, Guid>
    {
        Task AddStateProvinceAsync(Country country,  StateProvince stateProvince);
        Task RemoveStateProvinceAsync(Country country, StateProvince stateProvince);
    }
}

