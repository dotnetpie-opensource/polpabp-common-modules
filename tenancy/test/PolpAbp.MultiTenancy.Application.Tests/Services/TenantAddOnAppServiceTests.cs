using System;
using System.Threading.Tasks;
using Xunit;

namespace PolpAbp.MultiTenancy.Services
{
    public class TenantAddOnAppServiceTests : MultiTenancyApplicationTestBase
    {
        private readonly ITenantAddOnAppService _appService;

        public TenantAddOnAppServiceTests() : base()
        {
            _appService = GetRequiredService<ITenantAddOnAppService>();
        }

        [Fact]
        async Task CanCreateAsync()
        {
            var oldId = Guid.NewGuid();
            var newId = await _appService.CreateAsync(new Dtos.TenantAddOnInputDto
            {
                DisplayName = "hello",
                Description = "world"
            });

            Assert.NotEqual(oldId, newId);
        }
    }
}

