using System;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;

namespace PolpAbp.MultiTenancy.Domain.Entities
{
    public class TenantAddressMap : Entity<Guid>, IHasCreationTime, IHasDeletionTime, IHasModificationTime
    {

        // May refer to another datatable.
        public Guid AddressId { get; set; }

        public bool IsPrimary { get; set; }

        public bool IsBillingAddress { get; set; }

        public int DisplayOrder { get; set; }

        public DateTime CreationTime { get; set; }

        public DateTime? DeletionTime { get; set; }

        public bool IsDeleted { get; set; }

        // Foreign Key
        public Guid TenantId { get; set; }

        public DateTime? LastModificationTime { get; set; }

        protected TenantAddressMap() : base()
        {
        }

        public TenantAddressMap(Guid id) : base(id)
        {
        }
    }
}

