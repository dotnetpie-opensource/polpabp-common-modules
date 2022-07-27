namespace PolpAbp.Directory;

public static class DirectoryDbProperties
{
    public static string DbTablePrefix { get; set; } = "Directory";

    public static string DbSchema { get; set; } = null;

    public const string ConnectionStringName = "Directory";
}
