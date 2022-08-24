using System;
using PolpAbp.Contact.Dtos;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using System.Threading;

namespace PolpAbp.Contact.Services
{
    public interface IAddressAppService : IApplicationService
    {
        Task<Guid> CreateAsync(AddressInputDto dto, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task<AddressOutputDto> FindByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task UpdateAsyc(Guid id, AddressInputDto input, CancellationToken cancellationToken = default);
    }
}

