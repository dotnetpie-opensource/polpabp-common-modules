using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using PolpAbp.MultiTenancy.Shared;

namespace PolpAbp.MultiTenancy.Domain.Entities
{
    public class TenantAddOn : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        protected TenantAddOn() : base()
        {
        }

        public TenantAddOn(Guid id) : base(id)
        {
        }

        public Guid? TenantId { get; set; }

        /// <summary>
        /// Is the SCIM-based auto provising enabled?  
        /// </summary>
        public bool IsAutoProvisioningEnabled { get; set; }

        /// <summary>
        /// A display name for a tenant,
        /// rather than the name, which must be a valid identifier in
        /// the system.
        /// </summary>
        public string DisplayName { get; set; }

        public string Description { get; set; }

        /// <summary>
        /// The type of a company.
        /// Maybe country specific.
        /// </summary>
        public int CompanyTypeId { get; set; }

        /// <summary>
        /// The size of a company.
        /// </summary>
        public int CompanySize { get; set; }

        /// <summary>
        /// A standardize industry classification code.
        /// Maybe country specific.
        /// </summary>
        public int IndustryCodeId { get; set; }

        public TenantCompanyTypeEnum CompanyType => (TenantCompanyTypeEnum)CompanyTypeId;

        public TenantIndustryCodeEnum IndustryCode => (TenantIndustryCodeEnum)IndustryCodeId;


        #region Contact

        #endregion


        #region Plan
        public Guid? OrderId { get; set; }

        public Guid? PlanId { get; set; }

        #endregion

        // Lazy load
        public virtual List<TenantContactMap> ContactMaps { get; set; }
        public virtual List<TenantAddressMap> AddressMaps { get; set; }
        public virtual List<TenantPictureMap> PictureMaps { get; set; }

    }
}

