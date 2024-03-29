﻿using System;
using PolpAbp.Contact.Dtos;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using System.Threading;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace PolpAbp.Contact.Services
{
    public interface IAddressAppService : IApplicationService
    {
        Task<Guid> CreateAsync(AddressInputDto dto, bool autoSave = false, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, bool autoSave = false, CancellationToken cancellationToken = default);
        Task<AddressOutputDto> FindByIdAsync(Guid id, bool throwException = false, CancellationToken cancellationToken = default);
        Task<List<AddressOutputDto>> SearchAsync(Guid[] ids, string sorting = null, CancellationToken cancellationToken = default);
        Task UpdateAsyc(Guid id, AddressInputDto input, bool autoSave = false, CancellationToken cancellationToken = default);
        Task<PagedResultDto<AddressOutputDto>> SearchAsync(SearchAddressDto input, CancellationToken cancellationToken = default);

    }
}

