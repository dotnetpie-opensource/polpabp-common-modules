using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace PolpAbp.Directory.Domain.Entities
{
    public class Contact : FullAuditedAggregateRoot<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneCountryCode { get; set; }
        public string PhoneNumber { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool PhoneConfirmed { get; set; }
    }
}