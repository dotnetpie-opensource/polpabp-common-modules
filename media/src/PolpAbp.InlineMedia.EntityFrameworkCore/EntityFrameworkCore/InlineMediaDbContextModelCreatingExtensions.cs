using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using PolpAbp.InlineMedia.Domain.Entities;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace PolpAbp.InlineMedia.EntityFrameworkCore;

public static class InlineMediaDbContextModelCreatingExtensions
{
    public static void ConfigureInlineMedia(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));


        builder.Entity<Picture>(b =>
        {
            //Configure table & schema name
            b.ToTable(InlineMediaDbProperties.DbTablePrefix + InlineMediaDbProperties.PictureTableName, InlineMediaDbProperties.DbSchema);

            b.ConfigureByConvention();

            //Properties
            // We do not restrict the size of a file;
            // so that the same db can serve for different purposes.
            // It's the client to enforce some constraints.
            b.Property(q => q.MimeType).IsRequired().HasMaxLength(InlineMediaDomainSharedConsts.MaxMimeTypeLength);
            b.Property(q => q.SeoFilename).HasMaxLength(InlineMediaDomainSharedConsts.MaxFileNameLength);
            b.Property(q => q.AltAttribute).HasMaxLength(InlineMediaDomainSharedConsts.MaxAttributeLength);
            b.Property(q => q.TitleAttribute).HasMaxLength(InlineMediaDomainSharedConsts.MaxAttributeLength);
        });

    }
}
