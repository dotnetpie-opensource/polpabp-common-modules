﻿using System;
using PolpAbp.Contact.Dtos;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using System.Threading;
using System.Collections.Generic;

namespace PolpAbp.Contact.Services
{
    public interface IAddressAppService : IApplicationService
    {
        Task<Guid> CreateAsync(AddressInputDto dto, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task<AddressOutputDto> FindByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<List<AddressOutputDto>> SearchAsync(Guid[] ids, string sorting = null);
        Task UpdateAsyc(Guid id, AddressInputDto input, CancellationToken cancellationToken = default);
    }
}

