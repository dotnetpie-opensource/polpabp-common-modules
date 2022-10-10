using Volo.Abp.Reflection;

namespace PolpAbp.InlineMedia.Permissions;

public class InlineMediaPermissions
{
    public const string GroupName = "InlineMedia";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(InlineMediaPermissions));
    }
}
