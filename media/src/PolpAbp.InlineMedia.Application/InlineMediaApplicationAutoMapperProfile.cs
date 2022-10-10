using System.Net;
using AutoMapper;
using PolpAbp.InlineMedia.Domain.Entities;
using PolpAbp.InlineMedia.Dtos;

namespace PolpAbp.InlineMedia;

public class InlineMediaApplicationAutoMapperProfile : Profile
{
    public InlineMediaApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<PictureInputDto, Picture>()
            .IgnoreSourceMissingProperties();

        CreateMap<Picture, PictureOutputDto>();
    }
}
