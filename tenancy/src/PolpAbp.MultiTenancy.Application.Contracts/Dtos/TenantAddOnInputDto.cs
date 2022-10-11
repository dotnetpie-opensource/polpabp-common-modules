using System;
using PolpAbp.MultiTenancy.Shared;

namespace PolpAbp.MultiTenancy.Dtos
{
    public class TenantAddOnInputDto 
    {
        public bool IsAutoProvisioningEnabled { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }

        public int CompanyTypeId { get; set; }

        public int CompanySize { get; set; }

        public int IndustryCodeId { get; set; }

        public Guid? OrderId { get; set; }

        public Guid? PlanId { get; set; }
    }
}

