using PolpAbp.Directory.Domain.Entities;
using PolpAbp.Directory.Domain.Repositories;
using PolpAbp.Directory.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;

namespace PolpAbp.Directory.Services
{
    [RemoteService(false)]
    public class CountryAppService : DirectoryAppService, ICountryAppService
    {

        private readonly ICountryRepository _countryRepo;

        public CountryAppService(ICountryRepository countryRepo)
        {
            _countryRepo = countryRepo;
        }

        public async Task<Guid> CreateAsync(CountryInputDto dto, CancellationToken cancellationToken = default)
        {
            var target = new Country(GuidGenerator.Create());
            ObjectMapper.Map<CountryInputDto, Country>(dto, target);
            var a = await _countryRepo.InsertAsync(target, cancellationToken:cancellationToken);
            return a.Id;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            await _countryRepo.DeleteAsync(id, cancellationToken:cancellationToken);
        }

        public async Task UpdateAsyc(Guid id, CountryInputDto input, CancellationToken cancellationToken = default)
        {
            var target = await _countryRepo.FindAsync(id, cancellationToken:cancellationToken);
            if (target == null)
            {
                throw new ArgumentException($"No record for {id}");
            }
            ObjectMapper.Map<CountryInputDto, Country>(input, target);
            await _countryRepo.UpdateAsync(target, cancellationToken:cancellationToken);
        }

        public async Task<IEnumerable<CountryOutputDto>> ListAsyc(CancellationToken cancellationToken = default)
        {
            var a = await _countryRepo.GetListAsync(cancellationToken:cancellationToken);
            return a.Select(x => ObjectMapper.Map<Country, CountryOutputDto>(x));
        }

        // Follow DDD, we have to access country first. That's why we need the countryId.
        public async Task<IEnumerable<StateProvinceOutputDto>> ListStateProvincesByCountryAsync(Guid countryId, CancellationToken cancellationToken = default)
        {
            var country = await _countryRepo.GetAsync(countryId, cancellationToken:cancellationToken, includeDetails:true);
            var s = country.StateProvinces;

            return s.Select(x => ObjectMapper.Map<StateProvince, StateProvinceOutputDto>(x));   
        }

        // Follow DDD, we have to access country first. That's why we need the countryId.
        public async Task AddStateProvinceAsync(Guid countryId, StateProvinceInputDto input, CancellationToken cancellationToken = default)
        {
            var item = new StateProvince(GuidGenerator.Create());
            ObjectMapper.Map<StateProvinceInputDto, StateProvince>(input, item);

            await _countryRepo.AddStateProvincesAsync(countryId, new List<StateProvince> { item }, cancellationToken: cancellationToken);
        }

        // Follow DDD, we have to access country first. That's why we need the countryId.
        public async Task RemoveStateProvinceAsync(Guid countryId, Guid stateProvinceId, CancellationToken cancellationToken = default)
        {
            await _countryRepo.RemoveStateProvincesAsync(countryId, new List<Guid> { stateProvinceId }, cancellationToken: cancellationToken);
        }
    }
}

