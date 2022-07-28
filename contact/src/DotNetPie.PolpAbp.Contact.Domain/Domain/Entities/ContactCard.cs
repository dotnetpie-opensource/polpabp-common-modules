using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace DotNetPie.PolpAbp.Contact.Domain.Entities
{
    public class ContactCard : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneCountryCode { get; set; }
        public string PhoneNumber { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool PhoneConfirmed { get; set; }

        public Guid? TenantId { get; set; }
    }
}