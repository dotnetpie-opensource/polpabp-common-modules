using System.Collections.Generic;
using System.Security.Claims;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Security.Claims;

namespace PolpAbp.ResourceManagement.Security;

[Dependency(ReplaceServices = true)]
public class FakeCurrentPrincipalAccessor : ThreadCurrentPrincipalAccessor
{
    protected override ClaimsPrincipal GetClaimsPrincipal()
    {
        return GetPrincipal();
    }

    private ClaimsPrincipal _principal;

    private ClaimsPrincipal GetPrincipal()
    {
        if (_principal == null)
        {
            lock (this)
            {
                if (_principal == null)
                {
                    _principal = new ClaimsPrincipal(
                        new ClaimsIdentity(
                            new List<Claim>
                            {
                                    new Claim(AbpClaimTypes.UserId, ResourceManagementTestConsts.AdminId.ToString()),
                                    new Claim(AbpClaimTypes.UserName,"admin"),
                                    new Claim(AbpClaimTypes.Email,ResourceManagementTestConsts.AdminEmail),
                                    new Claim(AbpClaimTypes.TenantId,ResourceManagementTestConsts.TenantId.ToString())
                            }
                        )
                    );
                }
            }
        }

        return _principal;
    }
}
