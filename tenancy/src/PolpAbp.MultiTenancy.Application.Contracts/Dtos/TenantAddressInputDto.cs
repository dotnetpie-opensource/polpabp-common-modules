﻿using System;
using PolpAbp.Contact.Dtos;

namespace PolpAbp.MultiTenancy.Dtos
{
    public class TenantAddressInputDto
    {
        public Guid AddressId { get; set; }

        public bool IsPrimary { get; set; }

        public bool IsBillingAddress { get; set; }

        public int DisplayOrder { get; set; }
    }
}

