using AutoMapper;
using PolpAbp.Directory.Domain.Entities;
using PolpAbp.Directory.Dtos;

namespace PolpAbp.Directory;

public class DirectoryApplicationAutoMapperProfile : Profile
{
    public DirectoryApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<CountryInputDto, Country>()
        .IgnoreSourceMissingProperties();

        CreateMap<Country, CountryOutputDto>();

        CreateMap<CountryOutputDto, CountryInputDto>()
            .IgnoreSourceMissingProperties();

        CreateMap<StateProvinceInputDto, StateProvince>()
        .IgnoreSourceMissingProperties();

        CreateMap<StateProvince, StateProvinceOutputDto>();

        CreateMap<StateProvinceOutputDto, StateProvinceInputDto>()
            .IgnoreSourceMissingProperties();

    }
}
