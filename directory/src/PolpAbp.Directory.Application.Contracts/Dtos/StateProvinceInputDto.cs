using System;
using System.ComponentModel.DataAnnotations;

namespace PolpAbp.Directory.Dtos
{
    public class StateProvinceInputDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Abbreviation { get; set; }
    }
}

