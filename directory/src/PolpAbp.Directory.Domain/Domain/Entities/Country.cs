﻿using System;
using System.Collections.Generic;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;

namespace PolpAbp.Directory.Domain.Entities
{
    public class Country : AggregateRoot<Guid>, IHasCreationTime
    {
        public DateTime CreationTime {get;set;}

        public string Name { get; set; }

        public string TwoLetterIsoCode { get; set; }

        public string ThreeLetterIsoCode { get; set; }

        public int NumbericIsoCode { get; set; }

        public List<StateProvince> StateProvinces { get; set; }


        public Country() : base()
        {
        }

        public Country(Guid id) : base(id)
        {
            CreationTime = DateTime.UtcNow;
        }
    }
}

