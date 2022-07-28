namespace PolpAbp.Directory;

public static class DirectoryDbProperties
{
    public static string DbTablePrefix { get; set; } = "Directory";

    public static string DbSchema { get; set; } = null;

    public const string ConnectionStringName = "Directory";

    public const string CountryTableName = "Countries";
    public const string StateProvinceTableName = "StateProvinces";
    public const string ContactTableName = "Contacts";
    public const string AddressTableName = "Addresses";
}
