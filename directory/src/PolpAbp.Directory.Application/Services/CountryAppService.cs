using PolpAbp.Directory.Domain.Entities;
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

        private readonly IRepository<Country> _countryRepo;

        public CountryAppService(IRepository<Country> countryRepo)
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
            await _countryRepo.DeleteAsync(a => a.Id == id);
        }

        public async Task UpdateAsyc(Guid id, CountryInputDto input)
        {
            var target = await _countryRepo.FindAsync(a => a.Id == id);
            if (target == null)
            {
                throw new ArgumentException($"No record for {id}");
            }
            ObjectMapper.Map<CountryInputDto, Country>(input, target);
            await _countryRepo.UpdateAsync(target);
        }

        public async Task<IEnumerable<CountryOutputDto>> ListAsyc()
        {
            var a = await _countryRepo.ToListAsync();
            return a.Select(x => ObjectMapper.Map<Country, CountryOutputDto>(x));
        }

        // Follow DDD, we have to access country first. That's why we need the countryId.
        public async Task<IEnumerable<StateProvinceOutputDto>> ListStateProvincesByCountryAsync(Guid countryId)
        {
            var country = await _countryRepo.GetAsync(a => a.Id == countryId);
            var s = country.StateProvinces;

            return s.Select(x => ObjectMapper.Map<StateProvince, StateProvinceOutputDto>(x));   
        }

        // Follow DDD, we have to access country first. That's why we need the countryId.
        public async Task AddStateProvinceAsync(Guid countryId, StateProvinceInputDto input)
        {
            var country = await _countryRepo.GetAsync(a => a.Id == countryId);

            var item = new StateProvince(GuidGenerator.Create());
            ObjectMapper.Map<StateProvinceInputDto, StateProvince>(input, item);
            country.StateProvinces.Add(item);

            await _countryRepo.UpdateAsync(country);
        }

        // Follow DDD, we have to access country first. That's why we need the countryId.
        public async Task RemoveStateProvinceAsync(Guid countryId, Guid stateProvinceId)
        {
            var country = await _countryRepo.GetAsync(a => a.Id == countryId);

            var item = country.StateProvinces.Find(a => a.Id == stateProvinceId);
            if (item != null)
            {
                country.StateProvinces.Remove(item);
                await _countryRepo.UpdateAsync(country);
            }
        }
    }
}

