using DotNetPie.PolpAbp.Contact.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace DotNetPie.PolpAbp.Contact.EntityFrameworkCore;

public static class ContactDbContextModelCreatingExtensions
{
    public static void ConfigureContact(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        builder.Entity<Address>(b =>
        {
            //Configure table & schema name
            b.ToTable(ContactDbProperties.DbTablePrefix + ContactDbProperties.AddressTableName, ContactDbProperties.DbSchema);

            b.ConfigureByConvention();

            //Properties
            b.Property(q => q.City).IsRequired().HasMaxLength(ContactDomainSharedConsts.MaxCityLength);
            b.Property(q => q.Address1).IsRequired().HasMaxLength(ContactDomainSharedConsts.MaxStreetNameLength);
            b.Property(q => q.Address2).HasMaxLength(ContactDomainSharedConsts.MaxStreetNameLength);

            // Relations
            // We do not have relations??
        });

        builder.Entity<ContactCard>(b =>
        {
            //Configure table & schema name
            b.ToTable(ContactDbProperties.DbTablePrefix + ContactDbProperties.ContactTableName, ContactDbProperties.DbSchema);

            b.ConfigureByConvention();

            //Properties
            b.Property(q => q.FirstName).HasMaxLength(ContactDomainSharedConsts.MaxFirstNameLength);
            b.Property(q => q.LastName).HasMaxLength(ContactDomainSharedConsts.MaxLastNameLength);
            b.Property(q => q.Email).HasMaxLength(ContactDomainSharedConsts.MaxEmailLength);
            b.Property(q => q.PhoneCountryCode).HasMaxLength(ContactDomainSharedConsts.MaxPhoneCountryCodeLength);
            b.Property(q => q.PhoneNumber).HasMaxLength(ContactDomainSharedConsts.MaxPhoneNumberLength);

        });
    }
}
