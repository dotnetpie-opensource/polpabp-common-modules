using AutoMapper;
using PolpAbp.ResourceManagement.Domain.Entities;
using PolpAbp.ResourceManagement.Services.Dtos;

namespace PolpAbp.ResourceManagement;

public class ResourceManagementApplicationAutoMapperProfile : Profile
{
    public ResourceManagementApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<TenantSubscription, SubscriptionPlanOutputDto>()
            .ForMember(dst => dst.Name, o => o.Ignore())
            .ForMember(dst => dst.Description, o => o.Ignore())
            .ForMember(dst => dst.BillingCycleId, o => o.Ignore())
            .ForMember(dst => dst.Breakdowns, o => o.Ignore())
            .ForMember(dst => dst.CurrentBillingStartDate, o => o.Ignore())
            .ForMember(dst => dst.CurrentBillingEndDate, o => o.Ignore())
            .IgnoreSourceMissingProperties();

        CreateMap<PlanBreakdown, PlanBreakdownOutputDto>()
            .ForMember(dst => dst.Name, o => o.Ignore())
            .ForMember(dst => dst.Description, o => o.Ignore())
            .ForMember(dst => dst.Category, o => o.Ignore());
    }
}
