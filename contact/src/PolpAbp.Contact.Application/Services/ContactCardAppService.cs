using System;
using PolpAbp.Contact.Domain.Entities;
using PolpAbp.Contact.Dtos;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp;
using System.Threading;
using Volo.Abp.Domain.Entities;

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

        public async Task<ContactCardOutputDto> FindByIdAsync(Guid id, bool throwException = false, CancellationToken cancellationToken = default)
        {
            var source = await _contactCardRepo.FindAsync(a => a.Id == id, cancellationToken: cancellationToken);
            if (source != null)
            {
                return ObjectMapper.Map<ContactCard, ContactCardOutputDto>(source);
            }

            if (throwException)
            {
                throw new EntityNotFoundException(typeof(ContactCard), id);
            }

            return null;
        }

        public async Task<Guid> CreateAsync(ContactCardInputDto dto, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            var target = new ContactCard(GuidGenerator.Create());
            ObjectMapper.Map<ContactCardInputDto, ContactCard>(dto, target);
            var a = await _contactCardRepo.InsertAsync(target, autoSave, cancellationToken: cancellationToken);
            return a.Id;
        }

        public async Task DeleteAsync(Guid id, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            await _contactCardRepo.DeleteAsync(a => a.Id == id, autoSave, cancellationToken: cancellationToken);
        }

        public async Task UpdateAsyc(Guid id, ContactCardInputDto input, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            var target = await _contactCardRepo.GetAsync(a => a.Id == id, cancellationToken: cancellationToken);
            ObjectMapper.Map<ContactCardInputDto, ContactCard>(input, target);
            await _contactCardRepo.UpdateAsync(target, autoSave, cancellationToken:cancellationToken);
        }
    }

}
