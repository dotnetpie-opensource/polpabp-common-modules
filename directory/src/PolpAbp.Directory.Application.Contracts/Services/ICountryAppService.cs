using PolpAbp.Directory.Dtos;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace PolpAbp.Directory.Services
{
    public interface ICountryAppService : IApplicationService
    {
        Task AddStateProvinceAsync(Guid countryId, StateProvinceInputDto input, CancellationToken cancellationToken = default);
        Task<Guid> CreateAsync(CountryInputDto dto, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<CountryOutputDto>> ListAsyc(CancellationToken cancellationToken = default);
        Task<IEnumerable<StateProvinceOutputDto>> ListStateProvincesByCountryAsync(Guid countryId, CancellationToken cancellationToken = default);
        Task RemoveStateProvinceAsync(Guid countryId, Guid stateProvinceId, CancellationToken cancellationToken = default);
        Task UpdateAsyc(Guid id, CountryInputDto input, CancellationToken cancellationToken = default);
    }
}

