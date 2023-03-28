namespace PolpAbp.ResourceManagement.Services
{
    public class ResourceUsageLimit
    {
        public string ResourceName { get; set; }
        public long LimitAcrossTenant { get; set; }
        public long LimitPerUser { get; set; }
    }
}
