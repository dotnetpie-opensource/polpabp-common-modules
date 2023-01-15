using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Threading;
using System.Threading.Tasks;
using PolpAbp.MultiTenancy.Domain.Entities;
using PolpAbp.MultiTenancy.Domain.Repositories;
using PolpAbp.MultiTenancy.Dtos;
using Volo.Abp;

namespace PolpAbp.MultiTenancy.Services
{
    [RemoteService(false)]
    public class TenantAddOnAppService : MultiTenancyAppService, ITenantAddOnAppService
    {
        protected readonly ITenantAddOnRepository _tenantAddOnRepository;

        public TenantAddOnAppService(ITenantAddOnRepository tenantAddOnRepository)
        {
            _tenantAddOnRepository = tenantAddOnRepository;
        }

        public async Task<Guid> CreateAsync(TenantAddOnInputDto dto, CancellationToken cancellationToken = default)
        {
            var target = ObjectMapper.Map<TenantAddOnInputDto, TenantAddOn>(dto);
            var a = await _tenantAddOnRepository.InsertAsync(target, cancellationToken: cancellationToken);
            return a.Id;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            await _tenantAddOnRepository.DeleteAsync(id, cancellationToken: cancellationToken);
        }

        public async Task UpdateAsyc(Guid id, TenantAddOnInputDto input, CancellationToken cancellationToken = default)
        {
            var target = await _tenantAddOnRepository.GetAsync(id, cancellationToken: cancellationToken);
            ObjectMapper.Map<TenantAddOnInputDto, TenantAddOn>(input, target);
            await _tenantAddOnRepository.UpdateAsync(target, cancellationToken: cancellationToken);
        }
    }
}

