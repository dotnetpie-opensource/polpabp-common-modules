using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PolpAbp.Directory.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace PolpAbp.Directory.Domain.Repositories
{
    public interface ICountryRepository : IBasicRepository<Country, Guid>
    {
        Task AddStateProvincesAsync(Country country,  IEnumerable<StateProvince> stateProvinces, bool autoSave = false, CancellationToken cancellationToken = default);
        Task RemoveStateProvincesAsync(Country country, IEnumerable<StateProvince> stateProvinces, bool autoSave = false, CancellationToken cancellationToken = default);
    }
}

