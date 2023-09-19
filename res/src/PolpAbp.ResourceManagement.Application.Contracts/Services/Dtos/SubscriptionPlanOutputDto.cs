using System;
using System.Collections.Generic;
using System.Text;

namespace PolpAbp.ResourceManagement.Services.Dtos
{
    public class SubscriptionPlanOutputDto
    {
        // From tenant subscription
        public DateTime EffectiveOn { get; set; }
        public bool IsTerminated { get; set; }
        public DateTime? TerminatedOn { get; set; }

        public DateTime BillingCycleOn { get; set; }

        public int Quantity { get; set; }
        public Guid PlanId { get; set; }

        // Inferred properties
        public DateTime CurrentBillingStartDate { get; set; }
        public DateTime? CurrentBillingEndDate { get; set; }

        // From plan
        public string Name { get; set; }
        public string Description { get; set; }

        public int BillingCycleId { get; set; }

        // Plan breakdown 
        public List<PlanBreakdownOutputDto> Breakdowns { get; set; }

        public SubscriptionPlanOutputDto() { 
            Breakdowns = new List<PlanBreakdownOutputDto>();
        }
    }
}
