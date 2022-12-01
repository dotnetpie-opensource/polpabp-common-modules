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
        Task AddStateProvincesAsync(Guid countryId,  IEnumerable<StateProvince> stateProvinces, bool autoSave = false, CancellationToken cancellationToken = default);
        Task RemoveStateProvincesAsync(Guid countryId, IEnumerable<Guid> stateProvinceIds, bool autoSave = false, CancellationToken cancellationToken = default);
        Task UpdateStateProvinceAsync(Guid countryId, Guid stateProvinceId,
            Action<StateProvince> func, bool autoSave = false, CancellationToken cancellationToken = default);
    }
}

