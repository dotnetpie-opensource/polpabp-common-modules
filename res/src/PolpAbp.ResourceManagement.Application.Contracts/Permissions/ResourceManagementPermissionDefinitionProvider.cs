using PolpAbp.ResourceManagement.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace PolpAbp.ResourceManagement.Permissions;

public class ResourceManagementPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(ResourceManagementPermissions.GroupName, L("Permission:ResourceManagement"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<ResourceManagementResource>(name);
    }
}
