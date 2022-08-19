using PolpAbp.Directory.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace PolpAbp.Directory.Services
{
    public interface ICountryAppService : IApplicationService
    {
        Task AddStateProvinceAsync(Guid countryId, StateProvinceInputDto input);
        Task<Guid> CreateAsync(CountryInputDto dto);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<CountryOutputDto>> ListAsyc();
        Task<IEnumerable<StateProvinceOutputDto>> ListStateProvincesByCountryAsync(Guid countryId);
        Task RemoveStateProvinceAsync(Guid countryId, Guid stateProvinceId);
        Task UpdateAsyc(Guid id, CountryInputDto input);
    }
}

