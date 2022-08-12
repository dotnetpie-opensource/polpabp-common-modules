using Volo.Abp.Reflection;

namespace PolpAbp.Contact.Permissions;

public class ContactPermissions
{
    public const string GroupName = "Contact";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(ContactPermissions));
    }
}
