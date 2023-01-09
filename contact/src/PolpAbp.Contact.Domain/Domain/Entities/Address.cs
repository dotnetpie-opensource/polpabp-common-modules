using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace PolpAbp.Contact.Domain.Entities
{
    public class Address : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public Guid CountryId { get; set; }
        public Guid? StateProvinceId { get; set; }

        /// <summary>
        ///  State name. In replace of StateProvinceId.
        ///  Either StateProvinceId or StateProvince ...
        /// </summary>
        public string StateProvinceName { get; set; }

        public string City { get; set; }

        public string County { get; set; }

        public int StreeNo { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string ZipCode { get; set; }

        public Guid? TenantId { get; set; }

        // Used to tell if an address is public.
        // An address with a non-nullable value
        // is not included in the search scope.
        public Guid? OwnerId { get; set; }

        // Like the above, this field is used to
        // divide the addresses into different groups
        // from another angle.
        public int RoleId { get; set; }

        public Address() : base()
        {
        }

        public Address(Guid id) : base(id) {
            CreationTime = DateTime.UtcNow;
        }
    }
}

