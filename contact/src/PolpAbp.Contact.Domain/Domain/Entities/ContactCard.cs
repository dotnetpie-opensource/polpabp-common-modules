﻿using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace PolpAbp.Contact.Domain.Entities
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

        // Used to tell if a contact is public.
        // A contact with a non-nullable value
        // is not included in the search scope.
        public Guid? OwnerId { get; set; }

        // Like the above, this field is used to
        // divide the contacts into different groups
        // from another angle.
        public int RoleId { get; set; }

        public Guid? TenantId { get; set; }

        public ContactCard(): base()
        {
        }

        public ContactCard(Guid id) : base(id)
        {
            CreationTime = DateTime.UtcNow;
        }
    }
}