using System;
using System.Collections.Generic;
using System.Text;

namespace PolpAbp.ResourceManagement.Services.Dtos
{
    public class PlanCategoryQuotaOutputDto
    {
        public string Category { get; set; }

        public long LimitAcrossTenant { get; set; }
        public long LimitPerUser { get; set; }
    }
}
