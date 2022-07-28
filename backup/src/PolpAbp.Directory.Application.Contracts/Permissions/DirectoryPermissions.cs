using Volo.Abp.Reflection;

namespace PolpAbp.Directory.Permissions;

public class DirectoryPermissions
{
    public const string GroupName = "Directory";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(DirectoryPermissions));
    }
}
