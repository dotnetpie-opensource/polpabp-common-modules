using PolpAbp.Contact.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace PolpAbp.Contact.EntityFrameworkCore;

[ConnectionStringName(ContactDbProperties.ConnectionStringName)]
public class ContactDbContext : AbpDbContext<ContactDbContext>, IContactDbContext
{
    public DbSet<Address> Addresses { get; set; }

    public DbSet<ContactCard> ContactCards { get; set; }

    public ContactDbContext(DbContextOptions<ContactDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureContact();
    }
}
