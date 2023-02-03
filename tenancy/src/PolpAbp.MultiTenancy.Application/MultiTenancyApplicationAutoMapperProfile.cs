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

        CreateMap<TenantAddOn, TenantAddOnOutputDto>()
            .ForMember(dst => dst.AddresseMaps, o => o.Ignore())
            .ForMember(dst => dst.ContactMaps, o => o.Ignore())
            .ForMember(dst => dst.PictureMaps, o => o.Ignore());

        CreateMap<TenantAddOnOutputDto, TenantAddOnInputDto>();

        CreateMap<TenantAddressMap, TenantAddressMapOutputDto>();

        CreateMap<TenantAddressMapInputDto, TenantAddressMap>()
            .IgnoreSourceMissingProperties();

        CreateMap<TenantContactMap, TenantContactMapOutputDto>();

        CreateMap<TenantContactMapInputDto, TenantContactMap>()
            .IgnoreSourceMissingProperties();

        CreateMap<TenantPictureMapInputDto, TenantPictureMap>()
            .ForMember(dst => dst.PictureRole, o => o.Ignore())
            .IgnoreSourceMissingProperties();

        CreateMap<TenantPictureMap, TenantPictureMapOutputDto>()
            .ForMember(dst => dst.PictureRole, o => o.Ignore());
    }
}
