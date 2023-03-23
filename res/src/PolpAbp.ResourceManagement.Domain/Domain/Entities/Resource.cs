using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace PolpAbp.ResourceManagement.Domain.Entities
{
    public class Resource : FullAuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }

        protected Resource() : base() { }

        public Resource(Guid id) : base(id) { }
    }
}
