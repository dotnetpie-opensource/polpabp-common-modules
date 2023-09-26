using System;
using System.Collections.Generic;
using System.Text;

namespace PolpAbp.ResourceManagement.Services.Dtos
{
    public class SubscriptionPlanInputDto
    {
        // From tenant subscription
        public DateTime EffectiveOn { get; set; }
        public bool IsTerminated { get; set; }
        public DateTime? TerminatedOn { get; set; }

        public DateTime BillingCycleOn { get; set; }

        public int Quantity { get; set; }
        public Guid PlanId { get; set; }
    }
}
