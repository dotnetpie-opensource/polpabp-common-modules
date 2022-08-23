using PolpAbp.Directory.Domain.Entities;
using PolpAbp.Directory.Domain.Repositories;
using PolpAbp.Directory.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace PolpAbp.Directory.Services
{
    public class CountryAppService : DirectoryAppService, ICountryAppService
    {

        private readonly ICountryRepository _countryRepo;

        public CountryAppService(ICountryRepository countryRepo)
        {
            _countryRepo = countryRepo;
        }

        public async Task<Guid> CreateAsync(CountryInputDto dto)
        {
            var target = new Country(GuidGenerator.Create());
            ObjectMapper.Map<CountryInputDto, Country>(dto, target);
            var a = await _countryRepo.InsertAsync(target);
            return a.Id;
        }

        public async Task DeleteAsync(Guid id)
        {
            await _countryRepo.DeleteAsync(id);
        }

        public async Task UpdateAsyc(Guid id, CountryInputDto input)
        {
            var target = await _countryRepo.FindAsync(id);
            if (target == null)
            {
                throw new ArgumentException($"No record for {id}");
            }
            ObjectMapper.Map<CountryInputDto, Country>(input, target);
            await _countryRepo.UpdateAsync(target);
        }

        public async Task<IEnumerable<CountryOutputDto>> ListAsyc()
        {
            var a = await _countryRepo.GetListAsync();
            return a.Select(x => ObjectMapper.Map<Country, CountryOutputDto>(x));
        }

        // Follow DDD, we have to access country first. That's why we need the countryId.
        public async Task<IEnumerable<StateProvinceOutputDto>> ListStateProvincesByCountryAsync(Guid countryId)
        {
            var country = await _countryRepo.GetAsync(countryId);
            var s = country.StateProvinces;

            return s.Select(x => ObjectMapper.Map<StateProvince, StateProvinceOutputDto>(x));   
        }

        // Follow DDD, we have to access country first. That's why we need the countryId.
        public async Task AddStateProvinceAsync(Guid countryId, StateProvinceInputDto input)
        {
            var country = await _countryRepo.GetAsync(countryId);

            var item = new StateProvince(GuidGenerator.Create());
            ObjectMapper.Map<StateProvinceInputDto, StateProvince>(input, item);

            await _countryRepo.AddStateProvincesAsync(country, new List<StateProvince> { item });
        }

        // Follow DDD, we have to access country first. That's why we need the countryId.
        public async Task RemoveStateProvinceAsync(Guid countryId, Guid stateProvinceId)
        {
            var country = await _countryRepo.GetAsync(countryId);

            var item = country.StateProvinces.Find(a => a.Id == stateProvinceId);
            if (item != null)
            {
                await _countryRepo.RemoveStateProvincesAsync(country, new List<StateProvince> { item });
            }
        }
    }
}

