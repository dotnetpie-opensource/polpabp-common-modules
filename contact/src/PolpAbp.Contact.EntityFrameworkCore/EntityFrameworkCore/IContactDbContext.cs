using DotNetPie.PolpAbp.Contact.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace DotNetPie.PolpAbp.Contact.EntityFrameworkCore;

[ConnectionStringName(ContactDbProperties.ConnectionStringName)]
public interface IContactDbContext : IEfCoreDbContext
{

    DbSet<Address> Addresses { get; }
    DbSet<ContactCard> ContactCards { get; }
}
