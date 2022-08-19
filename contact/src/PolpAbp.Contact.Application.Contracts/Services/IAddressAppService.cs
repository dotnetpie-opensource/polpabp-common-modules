using System;
using PolpAbp.Contact.Dtos;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace PolpAbp.Contact.Services
{
    public interface IAddressAppService : IApplicationService
    {
        Task<Guid> CreateAsync(AddressInputDto dto);
        Task DeleteAsync(Guid id);
        Task<AddressOutputDto> FindByIdAsync(Guid id);
        Task UpdateAsyc(Guid id, AddressInputDto input);
    }
}

