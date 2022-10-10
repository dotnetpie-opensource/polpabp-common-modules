using PolpAbp.InlineMedia.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace PolpAbp.InlineMedia.Permissions;

public class InlineMediaPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(InlineMediaPermissions.GroupName, L("Permission:InlineMedia"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<InlineMediaResource>(name);
    }
}
