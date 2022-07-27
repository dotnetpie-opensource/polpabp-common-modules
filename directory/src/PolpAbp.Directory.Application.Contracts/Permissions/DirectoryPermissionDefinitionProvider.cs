using PolpAbp.Directory.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace PolpAbp.Directory.Permissions;

public class DirectoryPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(DirectoryPermissions.GroupName, L("Permission:Directory"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<DirectoryResource>(name);
    }
}
