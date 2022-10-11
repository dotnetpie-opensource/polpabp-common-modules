using Volo.Abp.Reflection;

namespace PolpAbp.MultiTenancy.Permissions;

public class MultiTenancyPermissions
{
    public const string GroupName = "MultiTenancy";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(MultiTenancyPermissions));
    }
}
