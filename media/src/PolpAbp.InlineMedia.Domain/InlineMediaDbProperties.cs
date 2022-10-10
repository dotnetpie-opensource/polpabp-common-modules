namespace PolpAbp.InlineMedia;

public static class InlineMediaDbProperties
{
    public static string DbTablePrefix { get; set; } = "PolpAbpInlineMedia";

    public static string DbSchema { get; set; } = null;

    public const string ConnectionStringName = "PolpAbpInlineMedia";

    public const string PictureTableName = "Pictures";

}
