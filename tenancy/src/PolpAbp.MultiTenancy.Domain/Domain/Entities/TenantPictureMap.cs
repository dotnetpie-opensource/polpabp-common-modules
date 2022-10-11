using System;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;
using PolpAbp.MultiTenancy.Shared;

namespace PolpAbp.MultiTenancy.Domain.Entities
{
    public class TenantPictureMap : Entity<Guid>, IHasCreationTime, IHasDeletionTime, IHasModificationTime
    {
        public DateTime CreationTime { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }

        public int DisplayOrder { get; set; }

        public int RoleId { get; set; }

        // Helper
        public TenantPictureRoleEnum PictureRole => (TenantPictureRoleEnum)RoleId;

        // Foreign key, picutre Id.
        public Guid PictureId { get; set; }

        // Foreign Key, not multitenancy Id.
        public Guid TenantId { get; set; }

        protected TenantPictureMap() : base()
        {
        }

        public TenantPictureMap(Guid id) : base(id)
        {
        }
    }
}

