﻿using PolpAbp.Contact.Dtos;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace PolpAbp.Contact.Services
{
    public interface IContactCardAppService : IApplicationService
    {
        Task<Guid> CreateAsync(ContactCardInputDto dto, bool autoSave = false, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, bool autoSave = false, CancellationToken cancellationToken = default);
        Task<ContactCardOutputDto> FindByIdAsync(Guid id, bool throwException = false, CancellationToken cancellationToken = default);
        Task UpdateAsyc(Guid id, ContactCardInputDto input, bool autoSave = false, CancellationToken cancellationToken = default);
        Task<List<ContactCardOutputDto>> SearchAsync(Guid[] ids, string sorting = null, CancellationToken cancellationToken = default);
        Task<PagedResultDto<ContactCardOutputDto>> SearchAsync(SearchContactCardDto input, CancellationToken cancellationToken = default);
    }
}

