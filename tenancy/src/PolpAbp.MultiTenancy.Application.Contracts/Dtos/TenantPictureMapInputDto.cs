using System;
using PolpAbp.MultiTenancy.Shared;

namespace PolpAbp.MultiTenancy.Dtos
{
    public class TenantPictureMapInputDto
    {
        public int DisplayOrder { get; set; }

        public int RoleId { get; set; }

        // Helper
        public TenantPictureRoleEnum PictureRole => (TenantPictureRoleEnum)RoleId;

        // Foreign key, picutre Id.
        public Guid PictureId { get; set; }
    }
}

