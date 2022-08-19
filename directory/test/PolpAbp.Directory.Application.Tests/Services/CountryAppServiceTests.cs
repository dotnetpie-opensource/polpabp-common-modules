using System;
using System.Threading.Tasks;
using Xunit;

namespace PolpAbp.Directory.Services
{
    public class CountryAppServiceTests : DirectoryApplicationTestBase
    {
        private readonly ICountryAppService _appService;

        public CountryAppServiceTests()
        {
            _appService = GetRequiredService<ICountryAppService>();
        }

        [Fact]
        public async Task CreateAsync()
        {
            var id = new Guid();
            var result = await _appService.CreateAsync(new Dtos.CountryInputDto
            {
                Name = "Name",
                TwoLetterIsoCode = "us",
                ThreeLetterIsoCode = "usa"
            });

            Assert.NotEqual(id, result);
        }
    }
}
