namespace PolpAbp.Contact;

public static class ContactDbProperties
{
    public static string DbTablePrefix { get; set; } = "PolpAbpContact";

    public static string DbSchema { get; set; } = null;

    public const string ConnectionStringName = "PolpAbpContact";

    public const string ContactTableName = "Contacts";
    public const string AddressTableName = "Addresses";
}
