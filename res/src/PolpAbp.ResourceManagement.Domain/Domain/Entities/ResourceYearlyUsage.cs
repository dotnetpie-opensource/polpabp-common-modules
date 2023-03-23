using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace PolpAbp.ResourceManagement.Domain.Entities
{
    public class ResourceYearlyUsage : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public Guid ResourceId { get; set; }
        public Guid? TenantId { get; set; }

        public int Year { get; set; }
        public long Usage { get; set; }

        // Resource navigation
        public virtual Resource Resource { get; set; }

        protected ResourceYearlyUsage() : base() { }

        public ResourceYearlyUsage(Guid id) : base(id) { }
    }
}
