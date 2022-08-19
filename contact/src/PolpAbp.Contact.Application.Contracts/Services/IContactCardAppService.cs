using PolpAbp.Contact.Dtos;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace PolpAbp.Contact.Services
{
    public interface IContactCardAppService : IApplicationService
    {
        Task<Guid> CreateAsync(ContactCardInputDto dto);
        Task DeleteAsync(Guid id);
        Task<ContactCardOutputDto> FindByIdAsync(Guid id);
        Task UpdateAsyc(Guid id, ContactCardInputDto input);
    }
}

