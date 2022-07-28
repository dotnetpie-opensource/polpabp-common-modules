using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;

namespace PolpAbp.Directory.Domain.Entities
{
    public class Address : FullAuditedAggregateRoot<Guid>
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

        public Country Country { get; set; }
    }
}

