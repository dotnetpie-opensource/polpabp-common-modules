using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace PolpAbp.ResourceManagement.Domain.Entities
{
    public class CategoryRestriction : AuditedEntity<Guid>
    {
        public Guid PlanId { get;set;}  

        public string Name { get;set;}

        public long LimitAcrossTenant { get; set;}
        public long LimitPerUser { get; set;}

        protected CategoryRestriction() :base() { }

        public CategoryRestriction(Guid id) : base(id) { }

    }
}
