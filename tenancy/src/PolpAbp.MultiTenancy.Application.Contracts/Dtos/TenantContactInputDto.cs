using System;
namespace PolpAbp.MultiTenancy.Dtos
{
    public class TenantContactInputDto
    {
        public Guid ContactId { get; set; }
        public bool IsPrimary { get; set; }
        public int DisplayOrder { get; set; }

    }
}

