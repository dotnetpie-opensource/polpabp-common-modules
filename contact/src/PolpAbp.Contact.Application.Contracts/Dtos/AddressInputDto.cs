using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace PolpAbp.Contact.Dtos
{
    public class AddressInputDto
    {
        public Guid CountryId { get; set; }
        public Guid? StateProvinceId { get; set; }

        /// <summary>
        ///  State name. In replace of StateProvinceId.
        ///  Either StateProvinceId or StateProvince ...
        /// </summary>
        public string StateProvinceName { get; set; }

        [Required]
        public string City { get; set; }

        public string County { get; set; }

        public int StreeNo { get; set; }

        [Required]
        public string Address1 { get; set; }

        public string Address2 { get; set; }

        [Required]
        public string ZipCode { get; set; }

    }
}

