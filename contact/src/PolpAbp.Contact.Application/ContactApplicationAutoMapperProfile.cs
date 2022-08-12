using AutoMapper;
using PolpAbp.Contact.Domain.Entities;
using PolpAbp.Contact.Dtos;

namespace PolpAbp.Contact;

public class ContactApplicationAutoMapperProfile : Profile
{
    public ContactApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<AddressInputDto, Address>()
            .IgnoreSourceMissingProperties();

        CreateMap<Address, AddressOutputDto>();

        CreateMap<AddressOutputDto, AddressInputDto>()
            .IgnoreSourceMissingProperties();

        CreateMap<ContactCardInputDto, ContactCard>()
            .IgnoreSourceMissingProperties();

        CreateMap<ContactCard, ContactCardOutputDto>();

        CreateMap<ContactCardOutputDto, ContactCardInputDto>()
            .IgnoreSourceMissingProperties();

    }
}
