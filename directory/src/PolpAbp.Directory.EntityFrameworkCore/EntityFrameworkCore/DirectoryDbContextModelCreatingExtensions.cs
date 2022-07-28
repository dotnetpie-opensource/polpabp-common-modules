using Microsoft.EntityFrameworkCore;
using PolpAbp.Directory.Domain.Entities;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace PolpAbp.Directory.EntityFrameworkCore;

public static class DirectoryDbContextModelCreatingExtensions
{
    public static void ConfigureDirectory(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        /* Configure all entities here. Example:

        builder.Entity<Question>(b =>
        {
            //Configure table & schema name
            b.ToTable(DirectoryDbProperties.DbTablePrefix + "Questions", DirectoryDbProperties.DbSchema);

            b.ConfigureByConvention();

            //Properties
            b.Property(q => q.Title).IsRequired().HasMaxLength(QuestionConsts.MaxTitleLength);

            //Relations
            b.HasMany(question => question.Tags).WithOne().HasForeignKey(qt => qt.QuestionId);

            //Indexes
            b.HasIndex(q => q.CreationTime);
        });
        */

        builder.Entity<Country>(b =>
        {
            //Configure table & schema name
            b.ToTable(DirectoryDbProperties.DbTablePrefix + DirectoryDbProperties.CountryTableName, DirectoryDbProperties.DbSchema);

            b.ConfigureByConvention();

            //Properties
            b.Property(q => q.Name).IsRequired().HasMaxLength(DirectoryDomainConsts.MaxCountryNameLength);

            b.HasMany<StateProvince>(x => x.StateProvinces).WithOne().HasForeignKey(p => p.CountryId);
        });

        builder.Entity<StateProvince>(b =>
        {
            //Configure table & schema name
            b.ToTable(DirectoryDbProperties.DbTablePrefix + DirectoryDbProperties.StateProvinceTableName, DirectoryDbProperties.DbSchema);

            b.ConfigureByConvention();

            //Properties
            b.Property(q => q.Name).IsRequired().HasMaxLength(DirectoryDomainConsts.MaxStateProvinceNameLength);
        });

        builder.Entity<Address>(b =>
        {
            //Configure table & schema name
            b.ToTable(DirectoryDbProperties.DbTablePrefix + DirectoryDbProperties.AddressTableName, DirectoryDbProperties.DbSchema);

            b.ConfigureByConvention();

            //Properties
            b.Property(q => q.City).IsRequired().HasMaxLength(DirectoryDomainConsts.MaxCityLength);
            b.Property(q => q.Address1).IsRequired().HasMaxLength(DirectoryDomainConsts.MaxStreetNameLength);
            b.Property(q => q.Address2).HasMaxLength(DirectoryDomainConsts.MaxStreetNameLength);

            // Relations
            b.HasOne<Country>(x => x.Country).WithMany().HasForeignKey(y => y.CountryId);
            b.HasOne<StateProvince>().WithMany().HasForeignKey(y => y.StateProvinceId);
        });

        builder.Entity<Contact>(b =>
        {
            //Configure table & schema name
            b.ToTable(DirectoryDbProperties.DbTablePrefix + DirectoryDbProperties.ContactTableName, DirectoryDbProperties.DbSchema);

            b.ConfigureByConvention();

            //Properties
            b.Property(q => q.FirstName).HasMaxLength(DirectoryDomainConsts.MaxFirstNameLength);
            b.Property(q => q.LastName).HasMaxLength(DirectoryDomainConsts.MaxLastNameLength);
            b.Property(q => q.Email).HasMaxLength(DirectoryDomainConsts.MaxEmailLength);
            b.Property(q => q.PhoneCountryCode).HasMaxLength(DirectoryDomainConsts.MaxPhoneCountryCodeLength);
            b.Property(q => q.PhoneNumber).HasMaxLength(DirectoryDomainConsts.MaxPhoneNumberLength);

        });

    }
}
