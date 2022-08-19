using System;
using System.Threading.Tasks;
using Xunit;

namespace PolpAbp.Contact.Services
{
    public class AddressAppServiceTests : ContactApplicationTestBase
    {
        private readonly IAddressAppService _appService;

        public AddressAppServiceTests()
        {
            _appService = GetRequiredService<IAddressAppService>();
        }

        [Fact]
        public async Task CreateTestAsync()
        {
            var id = new Guid();
            var result = await _appService.CreateAsync(new Dtos.AddressInputDto
            {
                 CountryId = id,
                 City = "City",
                 Address1 = "Address1",
                 ZipCode = "Zipcode"
            });

            Assert.NotEqual(id, result);
        }
    }
}

