using System;

namespace PolpAbp.MultiTenancy.Dtos
{
    public class TenantAddressMapInputDto
    {
        public Guid AddressId { get; set; }

        public bool IsPrimary { get; set; }

        public bool IsBillingAddress { get; set; }

        public int DisplayOrder { get; set; }
    }
}

