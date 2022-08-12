using System;
using PolpAbp.Contact.Domain.Entities;
using PolpAbp.Contact.Dtos;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp;

namespace PolpAbp.Contact.Services
{
    [RemoteService(false)]
    public class ContactCardAppService : ContactAppService, IContactCardAppService
    {
        private readonly IRepository<ContactCard> _contactCardRepo;

        public ContactCardAppService(IRepository<ContactCard> contactCardRepo)
        {
            _contactCardRepo = contactCardRepo;
        }

        public async Task<ContactCardOutputDto> FindByIdAsync(Guid id)
        {
            var source = await _contactCardRepo.FindAsync(a => a.Id == id);
            if (source != null)
            {
                return ObjectMapper.Map<ContactCard, ContactCardOutputDto>(source);
            }

            return null;
        }

        public async Task<Guid> CreateAsync(ContactCardInputDto dto)
        {
            var target = new ContactCard(GuidGenerator.Create());
            ObjectMapper.Map<ContactCardInputDto, ContactCard>(dto, target);
            var a = await _contactCardRepo.InsertAsync(target);
            return a.Id;
        }

        public async Task DeleteAsync(Guid id)
        {
            await _contactCardRepo.DeleteAsync(a => a.Id == id);
        }

        public async Task UpdateAsyc(Guid id, ContactCardInputDto input)
        {
            var target = await _contactCardRepo.FindAsync(a => a.Id == id);
            if (target == null)
            {
                throw new ArgumentException($"No record for {id}");
            }
            ObjectMapper.Map<ContactCardInputDto, ContactCard>(input, target);
            await _contactCardRepo.UpdateAsync(target);
        }
    }

}
