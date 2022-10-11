using System.Diagnostics.Metrics;
using AutoMapper;
using PolpAbp.MultiTenancy.Domain.Entities;
using PolpAbp.MultiTenancy.Dtos;

namespace PolpAbp.MultiTenancy;

public class MultiTenancyApplicationAutoMapperProfile : Profile
{
    public MultiTenancyApplicationAutoMapperProfile()
    {
        CreateMap<TenantAddOnInputDto, TenantAddOn>()
            .IgnoreSourceMissingProperties();

        CreateMap<TenantAddOn, TenantAddOnOutputDto>();

        CreateMap<TenantAddOnOutputDto, TenantAddOnInputDto>();

        CreateMap<TenantAddressMap, TenantAddressOutputDto>();

        CreateMap<TenantAddressInputDto, TenantAddressMap>()
            .IgnoreSourceMissingProperties();

        CreateMap<TenantContactInputDto, TenantConactMap>()
            .IgnoreSourceMissingProperties();

        CreateMap<TenantConactMap, TenantContactOutputDto>();

        CreateMap<TenantPictureMapInputDto, TenantPictureMap>()
            .ForMember(dst => dst.PictureRole, o => o.Ignore());

        CreateMap<TenantPictureMap, TenantPictureMapOutputDto>()
            .ForMember(dst => dst.PictureRole, o => o.Ignore())
            .IgnoreSourceMissingProperties();
    }
}
