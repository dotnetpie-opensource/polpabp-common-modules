using Microsoft.EntityFrameworkCore;
using PolpAbp.InlineMedia.Domain.Entities;
using System.Net;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace PolpAbp.InlineMedia.EntityFrameworkCore;

[ConnectionStringName(InlineMediaDbProperties.ConnectionStringName)]
public interface IInlineMediaDbContext : IEfCoreDbContext
{
    DbSet<Picture> Pictures { get; set; }
}
