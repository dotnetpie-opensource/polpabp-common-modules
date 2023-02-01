using PolpAbp.MultiTenancy.Domain.Entities;
using PolpAbp.MultiTenancy.Domain.Repositories;
using PolpAbp.MultiTenancy.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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

        public async Task<Guid> CreateAsync(TenantAddOnInputDto dto, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            var target = ObjectMapper.Map<TenantAddOnInputDto, TenantAddOn>(dto);
            var a = await _tenantAddOnRepository.InsertAsync(target, autoSave, cancellationToken: cancellationToken);
            return a.Id;
        }

        public async Task DeleteAsync(Guid id, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            await _tenantAddOnRepository.DeleteAsync(id, autoSave, cancellationToken: cancellationToken);
        }

        public async Task UpdateAsyc(Guid id, TenantAddOnInputDto input, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            var target = await _tenantAddOnRepository.GetAsync(id, cancellationToken: cancellationToken);
            ObjectMapper.Map<TenantAddOnInputDto, TenantAddOn>(input, target);
            await _tenantAddOnRepository.UpdateAsync(target, autoSave, cancellationToken: cancellationToken);
        }

        public async Task AddOrUpdatePictureMapAsync(Guid id, TenantPictureMapInputDto dto, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            var entry = await _tenantAddOnRepository.GetAsync(id, includeDetails:true, cancellationToken: cancellationToken);
                var anyMap = entry.PictureMaps.Where(a => a.PictureId == dto.PictureId).FirstOrDefault();
            if (anyMap != null)
            {
                // Update 
                await _tenantAddOnRepository.UpdatePicutreMapAsync(id, anyMap.Id, (record) =>
                {
                    ObjectMapper.Map(dto, record);
                }, autoSave, cancellationToken);
                return;
            }

            // Add a new record 
            var newMap = new TenantPictureMap(GuidGenerator.Create());
            ObjectMapper.Map(dto, newMap);
            await _tenantAddOnRepository.AddPictureMapsAsync(id, new List<TenantPictureMap> { newMap }, autoSave, cancellationToken);
        }

        public async Task DeletePicutreMapAsync(Guid id, Guid pictureId, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            await _tenantAddOnRepository.RemovePictureMapsByPictureIdsAsync(id, new List<Guid> { pictureId }, autoSave, cancellationToken);
        }

    }
}

