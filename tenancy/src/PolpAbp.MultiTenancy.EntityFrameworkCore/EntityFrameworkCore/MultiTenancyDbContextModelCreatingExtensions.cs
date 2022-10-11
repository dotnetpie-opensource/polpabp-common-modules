using Microsoft.EntityFrameworkCore;
using PolpAbp.MultiTenancy.Domain.Entities;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace PolpAbp.MultiTenancy.EntityFrameworkCore;

public static class MultiTenancyDbContextModelCreatingExtensions
{
    public static void ConfigureMultiTenancy(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        builder.Entity<TenantAddOn>(b =>
        {
            //Configure table & schema name
            b.ToTable(MultiTenancyDbProperties.DbTablePrefix + MultiTenancyDbProperties.TenantAddOnTableName, MultiTenancyDbProperties.DbSchema);

            b.ConfigureByConvention();

            //Properties
            b.Property(q => q.DisplayName).HasMaxLength(MultiTenancyDbProperties.TenantDisplayNameMaxLen);
            b.Property(q => q.Description).HasMaxLength(MultiTenancyDbProperties.TenantDescriptionMaxLen);

            b.HasIndex(q => q.TenantId).IsUnique();

            b.HasMany<TenantAddressMap>(x => x.AddressMaps).WithOne().HasForeignKey(p => p.TenantId);
            b.HasMany<TenantConactMap>(x => x.ContactMaps).WithOne().HasForeignKey(p => p.TenantId);
            b.HasMany<TenantPictureMap>(x => x.PictureMaps).WithOne().HasForeignKey(p => p.TenantId);
        });

        builder.Entity<TenantAddressMap>(b =>
        {
            //Configure table & schema name
            b.ToTable(MultiTenancyDbProperties.DbTablePrefix + MultiTenancyDbProperties.TenantAddressMapTableName, MultiTenancyDbProperties.DbSchema);

            b.ConfigureByConvention();

            // todo: Can AddressId be unique
            b.HasIndex(x => x.AddressId).IsUnique();
        });

        builder.Entity<TenantConactMap>(b =>
        {
            //Configure table & schema name
            b.ToTable(MultiTenancyDbProperties.DbTablePrefix + MultiTenancyDbProperties.TenantContactMapTableName, MultiTenancyDbProperties.DbSchema);

            b.ConfigureByConvention();

            // Can ContactId be unique
            b.HasIndex(x => x.ContactId).IsUnique();
        });

        builder.Entity<TenantPictureMap>(b =>
        {
            //Configure table & schema name
            b.ToTable(MultiTenancyDbProperties.DbTablePrefix + MultiTenancyDbProperties.TenantPictureMapTableName, MultiTenancyDbProperties.DbSchema);

            b.ConfigureByConvention();

            // Can pictureId be unique
            b.HasIndex(x => x.PictureId).IsUnique();
        });

    }
}
