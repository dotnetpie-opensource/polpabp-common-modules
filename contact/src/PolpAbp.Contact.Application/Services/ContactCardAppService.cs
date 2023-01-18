using System;
using PolpAbp.Contact.Domain.Entities;
using PolpAbp.Contact.Dtos;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp;
using System.Threading;
using Volo.Abp.Domain.Entities;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Linq.Dynamic.Core;

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

        public async Task<List<ContactCardOutputDto>> SearchAsync(Guid[] ids, string sorting = null, CancellationToken cancellationToken = default)
        {
            var query = await _contactCardRepo.GetQueryableAsync();
            query = query.Where(x => ids.Contains(x.Id));
            var sortKey = string.IsNullOrEmpty(sorting) ? "LastName ASC, FirstName ASC" : sorting;
            var sorted = query.OrderBy(sortKey);

            cancellationToken.ThrowIfCancellationRequested();

            return sorted.Select(y => ObjectMapper.Map<ContactCard, ContactCardOutputDto>(y)).ToList();
        }

        // Search the public addresses.
        public async Task<PagedResultDto<ContactCardOutputDto>> SearchAsync(SearchContactCardDto input, CancellationToken cancellationToken = default)
        {
            var query = await _contactCardRepo.GetQueryableAsync();
            query = query.Where(x => x.RoleId == input.RoleId);
            query = query.Where(x => x.OwnerId == null);
            query = query.WhereIf(input.Keyword != null, x => x.FirstName.Contains(input.Keyword) || x.LastName.Contains(input.Keyword));

            cancellationToken.ThrowIfCancellationRequested();
            var total = query.Count();

            var sortKey = string.IsNullOrEmpty(input.Sorting) ? "LastName ASC, FirstName ASC" : input.Sorting;
            var sorted = query.OrderBy(sortKey);

            var range = sorted.Skip(input.SkipCount).Take(input.MaxResultCount);

            cancellationToken.ThrowIfCancellationRequested();
            var items = range.Select(y => ObjectMapper.Map<ContactCard, ContactCardOutputDto>(y)).ToList();

            var ret = new PagedResultDto<ContactCardOutputDto>
            {
                Items = items,
                TotalCount = total
            };

            return ret;
        }
    }

}
