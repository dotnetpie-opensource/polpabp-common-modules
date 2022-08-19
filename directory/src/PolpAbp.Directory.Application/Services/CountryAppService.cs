using System;
using System.Net;
using System.Threading.Tasks;
using PolpAbp.Directory.Domain.Entities;
using PolpAbp.Directory.Dtos;
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

    }
}

