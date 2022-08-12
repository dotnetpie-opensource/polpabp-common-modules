using DotNetPie.PolpAbp.Directory.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace DotNetPie.PolpAbp.Directory.EntityFrameworkCore;

[ConnectionStringName(DirectoryDbProperties.ConnectionStringName)]
public interface IDirectoryDbContext : IEfCoreDbContext
{
    DbSet<Country> Countries { get; }
    DbSet<StateProvince> StateProvinces { get; }
}
