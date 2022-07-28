namespace DotNetPie.PolpAbp.Directory;

public static class DirectoryDbProperties
{
    public static string DbTablePrefix { get; set; } = "PolpAbpDirectory";

    public static string DbSchema { get; set; } = null;

    public const string ConnectionStringName = "PolpAbpDirectory";

    public const string CountryTableName = "Countries";
    public const string StateProvinceTableName = "StateProvinces";
}
