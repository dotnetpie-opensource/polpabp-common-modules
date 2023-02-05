using System;
using System.Threading.Tasks;
using Volo.Abp.MultiTenancy;
using Xunit;

namespace PolpAbp.MultiTenancy.Services
{
    public class TenantAddOnAppServiceTests : MultiTenancyApplicationTestBase
    {
        private readonly ITenantAddOnAppService _appService;
        private readonly ICurrentTenant _currentTenant;

        public TenantAddOnAppServiceTests() : base()
        {
            _appService = GetRequiredService<ITenantAddOnAppService>();
            _currentTenant = GetRequiredService<ICurrentTenant>();
        }

        [Fact]
        public async Task CanCreateAsync()
        {
            await WithUnitOfWorkAsync(async () => {
                using (_currentTenant.Change(MultiTenancyTestConsts.TenantId))
                {
                    var oldId = Guid.NewGuid();
                    var newId = await _appService.CreateOrUpdateAsync(new Dtos.TenantAddOnInputDto
                    {
                        DisplayName = "hello",
                        Description = "world"
                    });

                    Assert.NotEqual(oldId, newId);
                }
            });
        }
    }
}

