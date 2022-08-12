using System;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using PolpAbp.Contact.Domain.Entities;
using System.Threading.Tasks;
using PolpAbp.Contact.Dtos;

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

        public async Task<AddressOutputDto> FindByIdAsync(Guid id)
        {
            var source = await _addressRepo.FindAsync(a => a.Id == id);
            if (source != null)
            {
                return ObjectMapper.Map<Address, AddressOutputDto>(source);
            }

            return null;
        }

        public async Task<Guid> CreateAsync(AddressInputDto dto)
        {
            var target = new Address(GuidGenerator.Create());
            ObjectMapper.Map<AddressInputDto, Address>(dto, target);
            var a = await _addressRepo.InsertAsync(target);
            return a.Id;
        }

        public async Task DeleteAsync(Guid id)
        {
            await _addressRepo.DeleteAsync(a => a.Id == id);
        }

        public async Task UpdateAsyc(Guid id, AddressInputDto input)
        {
            var target = await _addressRepo.FindAsync(a => a.Id == id);
            if (target == null)
            {
                throw new ArgumentException($"No record for {id}");
            }
            ObjectMapper.Map<AddressInputDto, Address>(input, target);
            await _addressRepo.UpdateAsync(target);
        }


    }
}

