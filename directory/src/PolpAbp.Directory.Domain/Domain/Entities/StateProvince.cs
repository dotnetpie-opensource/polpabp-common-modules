using System;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Auditing;

namespace DotNetPie.PolpAbp.Directory.Domain.Entities
{
    public class StateProvince : Entity<Guid>, IHasCreationTime
    {
        public DateTime CreationTime { get; set; }

        public Guid CountryId { get; set; }

        public string Name { get; set; }

        public string Abbreviation { get; set; }
    }
}

