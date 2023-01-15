using System;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using PolpAbp.Contact.Domain.Entities;
using System.Threading.Tasks;
using PolpAbp.Contact.Dtos;
using System.Threading;
using System.Linq.Dynamic.Core;
using System.Linq;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

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

        public async Task<AddressOutputDto> FindByIdAsync(Guid id, bool throwException = false, CancellationToken cancellationToken = default)
        {
            var source = await _addressRepo.FindAsync(a => a.Id == id, cancellationToken:cancellationToken);
            if (source != null)
            {
                return ObjectMapper.Map<Address, AddressOutputDto>(source);
            }

            if (throwException)
            {
                throw new EntityNotFoundException(typeof(Address), id);
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
            var target = await _addressRepo.GetAsync(a => a.Id == id, cancellationToken:cancellationToken);
            ObjectMapper.Map<AddressInputDto, Address>(input, target);
            await _addressRepo.UpdateAsync(target, cancellationToken:cancellationToken);
        }

        public async Task<List<AddressOutputDto>> SearchAsync(Guid[] ids, string sorting = null, CancellationToken cancellationToken = default)
        {
            var query = await _addressRepo.GetQueryableAsync();
            query = query.Where(x => ids.Contains(x.Id));
            var sortKey = string.IsNullOrEmpty(sorting) ? "CountryId ASC, StateProvinceId ASC, City ASC, Address1 ASC" : sorting;
            var sorted = query.OrderBy(sortKey);

            cancellationToken.ThrowIfCancellationRequested();

            return sorted.Select(y => ObjectMapper.Map<Address, AddressOutputDto>(y)).ToList();
        }

        // Search the public addresses.
        public async Task<PagedResultDto<AddressOutputDto>> SearchAsync(SearchAddressDto input, CancellationToken cancellationToken = default)
        {
            var query = await _addressRepo.GetQueryableAsync();
            query = query.Where(x => x.RoleId == input.RoleId);
            query = query.Where(x => x.OwnerId == null);
            query = query.WhereIf(input.Keyword != null, x => x.Address1.Contains(input.Keyword) || x.City.Contains(input.Keyword));

            cancellationToken.ThrowIfCancellationRequested();
            var total = query.Count();

            var sortKey = string.IsNullOrEmpty(input.Sorting) ? "CountryId ASC, StateProvinceId ASC, City ASC, Address1 ASC" : input.Sorting;
            var sorted = query.OrderBy(sortKey);

            var range = sorted.Skip(input.SkipCount).Take(input.MaxResultCount);

            cancellationToken.ThrowIfCancellationRequested();
            var items = range.Select(y => ObjectMapper.Map<Address, AddressOutputDto>(y)).ToList();

            var ret = new PagedResultDto<AddressOutputDto>
            {
                Items = items,
                TotalCount = total
            };

            return ret;
        }
    }
}

