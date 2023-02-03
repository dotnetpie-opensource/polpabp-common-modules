using System;
namespace PolpAbp.MultiTenancy.Dtos
{
    public class TenantContactMapInputDto
    {
        public Guid ContactId { get; set; }
        public bool IsPrimary { get; set; }
        public int DisplayOrder { get; set; }

    }
}

