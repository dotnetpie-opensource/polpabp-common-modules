using System;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using PolpAbp.Contact.Domain.Entities;
using System.Threading.Tasks;
using PolpAbp.Contact.Dtos;
using System.Threading;

namespace PolpAbp.Contact.Services
{
    [RemoteService(false)]
    public class AddressAppService : ContactAppService, IAddressAppService
    {
        private readonly IRepository<Address> _addressRepo;

        public AddressAppService(IRepository<Address> addressRepo)
        {
            _addressRepo = addressRepo;
        }

        public async Task<AddressOutputDto> FindByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var source = await _addressRepo.FindAsync(a => a.Id == id, cancellationToken:cancellationToken);
            if (source != null)
            {
                return ObjectMapper.Map<Address, AddressOutputDto>(source);
            }

            return null;
        }

        public async Task<Guid> CreateAsync(AddressInputDto dto, CancellationToken cancellationToken = default)
        {
            var target = new Address(GuidGenerator.Create());
            ObjectMapper.Map<AddressInputDto, Address>(dto, target);
            var a = await _addressRepo.InsertAsync(target, cancellationToken: cancellationToken);
            return a.Id;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            await _addressRepo.DeleteAsync(a => a.Id == id, cancellationToken:cancellationToken);
        }

        public async Task UpdateAsyc(Guid id, AddressInputDto input, CancellationToken cancellationToken = default)
        {
            var target = await _addressRepo.FindAsync(a => a.Id == id, cancellationToken:cancellationToken);
            if (target == null)
            {
                throw new ArgumentException($"No record for {id}");
            }
            ObjectMapper.Map<AddressInputDto, Address>(input, target);
            await _addressRepo.UpdateAsync(target, cancellationToken:cancellationToken);
        }


    }
}

