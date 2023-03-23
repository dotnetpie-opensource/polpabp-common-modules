using System;
using System.Collections.Generic;
using System.Text;

namespace PolpAbp.ResourceManagement.Services
{
    public class ResourceLogInfo
    {
        public string ResourceName { get; set; }

        public Guid UserId { get; set; }
        public Guid TenantId { get; set; }

        public int Usage { get; set; }

        public DateTime HappenedOn { get; set; }
    }
}
