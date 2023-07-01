using System;
using System.Collections.Generic;

namespace PolpAbp.ResourceManagement.Services
{
    public class ResourceLogOptions
    {
        public bool IsEnabled { get; set; }

        public Dictionary<string, Type> Contributors { get; }

        public List<ResourceUsageLimit> FreeUsageLimit { get; set; }

        public ResourceLogOptions() {
            IsEnabled = true;
            Contributors = new Dictionary<string, Type>();
            FreeUsageLimit = new List<ResourceUsageLimit>();
        }
    }
}
