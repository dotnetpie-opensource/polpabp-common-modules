namespace PolpAbp.MultiTenancy;

public static class MultiTenancyDbProperties
{
    public static string DbTablePrefix { get; set; } = "PolpAbpMultiTenancy";

    public static string DbSchema { get; set; } = null;

    public const string ConnectionStringName = "PolpAbpMultiTenancy";


    public const string TenantAddOnTableName = "TenantAddOn";
    public const string TenantPictureMapTableName = "TenantPictureMap";
    public const string TenantContactMapTableName = "TenantContactMap";
    public const string TenantAddressMapTableName = "TenantAddressMap";

    public const int TenantDisplayNameMaxLen = 256;
    public const int TenantDescriptionMaxLen = 4096;

}
