using DotNetPie.PolpAbp.Directory.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace DotNetPie.PolpAbp.Directory.EntityFrameworkCore;

public static class DirectoryDbContextModelCreatingExtensions
{
    public static void ConfigureDirectory(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        builder.Entity<Country>(b =>
        {
            //Configure table & schema name
            b.ToTable(DirectoryDbProperties.DbTablePrefix + DirectoryDbProperties.CountryTableName, DirectoryDbProperties.DbSchema);

            b.ConfigureByConvention();

            //Properties
            b.Property(q => q.Name).IsRequired().HasMaxLength(DirectoryDomainSharedConsts.MaxCountryNameLength);

            b.HasMany<StateProvince>(x => x.StateProvinces).WithOne().HasForeignKey(p => p.CountryId);
        });

        builder.Entity<StateProvince>(b =>
        {
            //Configure table & schema name
            b.ToTable(DirectoryDbProperties.DbTablePrefix + DirectoryDbProperties.StateProvinceTableName, DirectoryDbProperties.DbSchema);

            b.ConfigureByConvention();

            //Properties
            b.Property(q => q.Name).IsRequired().HasMaxLength(DirectoryDomainSharedConsts.MaxStateProvinceNameLength);
        });
    }
}
