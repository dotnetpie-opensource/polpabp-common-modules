using PolpAbp.Contact.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace PolpAbp.Contact.Permissions;

public class ContactPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(ContactPermissions.GroupName, L("Permission:Contact"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<ContactResource>(name);
    }
}
