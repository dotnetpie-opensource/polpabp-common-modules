using PolpAbp.MultiTenancy.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace PolpAbp.MultiTenancy.Permissions;

public class MultiTenancyPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(MultiTenancyPermissions.GroupName, L("Permission:MultiTenancy"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<MultiTenancyResource>(name);
    }
}
