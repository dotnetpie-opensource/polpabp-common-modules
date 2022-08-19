using System;
using System.ComponentModel.DataAnnotations;

namespace PolpAbp.Directory.Dtos
{
    public class CountryInputDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string TwoLetterIsoCode { get; set; }

        [Required]
        public string ThreeLetterIsoCode { get; set; }

        // todo: Require > 0
        public int NumbericIsoCode { get; set; }
    }
}

