using PolpAbp.MultiTenancy.Domain.Entities;
using PolpAbp.MultiTenancy.Domain.Repositories;
using PolpAbp.MultiTenancy.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.MultiTenancy;

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

        public async Task<TenantAddOnOutputDto> GetBeyondTenantAsync(Guid tenantId, CancellationToken cancellationToken = default)
        {
            using (DataFilter.Disable<IMultiTenant>())
            {
                var id = await _tenantAddOnRepository.EnsureForTenantIdAsync(tenantId, autoSave: true, cancellationToken: cancellationToken);
                var entry = await _tenantAddOnRepository.GetAsync(id, includeDetails: true, cancellationToken: cancellationToken);
                var dto = BuildDto(entry);
                return dto;
            }
        }

        public async Task<Guid> CreateOrUpdateAsync(TenantAddOnInputDto dto, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            var anyRecord = await _tenantAddOnRepository.FindByTenantIdAsync(CurrentTenant.Id.Value);
            if (anyRecord == null)
            {
                var target = ObjectMapper.Map<TenantAddOnInputDto, TenantAddOn>(dto);
                anyRecord = await _tenantAddOnRepository.InsertAsync(target, autoSave, cancellationToken: cancellationToken);
            }
            else
            {
                ObjectMapper.Map<TenantAddOnInputDto, TenantAddOn>(dto, anyRecord);
                await _tenantAddOnRepository.UpdateAsync(anyRecord, autoSave, cancellationToken: cancellationToken);
            }
            return anyRecord.Id;
        }

        public async Task DeleteAsync(Guid id, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            await _tenantAddOnRepository.DeleteAsync(id, autoSave, cancellationToken: cancellationToken);
        }

        public async Task AddOrUpdatePictureMapAsync(TenantPictureMapInputDto dto, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            // Note that we must pass the autosave to be true.
            var id = await _tenantAddOnRepository.EnsureForTenantIdAsync(CurrentTenant.Id.Value, autoSave: true, cancellationToken: cancellationToken);

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

        public async Task DeletePicutreMapAsync(Guid pictureId, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            var id = await _tenantAddOnRepository.EnsureForTenantIdAsync(CurrentTenant.Id.Value, autoSave: true, cancellationToken: cancellationToken);

            await _tenantAddOnRepository.RemovePictureMapsByPictureIdsAsync(id, new List<Guid> { pictureId }, autoSave, cancellationToken);
        }

        public async Task AddOrUpdateAddressMapAsync(TenantAddressMapInputDto dto, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            // Note that we must pass the autosave to be true.
            var id = await _tenantAddOnRepository.EnsureForTenantIdAsync(CurrentTenant.Id.Value, autoSave: true, cancellationToken: cancellationToken);

            var entry = await _tenantAddOnRepository.GetAsync(id, includeDetails: true, cancellationToken: cancellationToken);
            var anyMap = entry.AddressMaps.Where(a => a.AddressId == dto.AddressId).FirstOrDefault();
            if (anyMap != null)
            {
                // Update 
                await _tenantAddOnRepository.UpdateAddressMapAsync(id, anyMap.Id, (record) =>
                {
                    ObjectMapper.Map(dto, record);
                }, autoSave, cancellationToken);
                return;
            }

            // Add a new record 
            var newMap = new TenantAddressMap(GuidGenerator.Create());
            ObjectMapper.Map(dto, newMap);
            await _tenantAddOnRepository.AddAddressMapsAsync(id, new List<TenantAddressMap> { newMap }, autoSave, cancellationToken);
        }

        public async Task DeleteAddressMapAsync(Guid addressId, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            var id = await _tenantAddOnRepository.EnsureForTenantIdAsync(CurrentTenant.Id.Value, autoSave: true, cancellationToken: cancellationToken);

            await _tenantAddOnRepository.RemoveAddressMapsByAddressIdsAsync(id, new List<Guid> { addressId }, autoSave, cancellationToken);
        }

        protected TenantAddOnOutputDto BuildDto(TenantAddOn entity)
        {
            var dto = ObjectMapper.Map<TenantAddOn, TenantAddOnOutputDto>(entity);
            foreach(var a in entity.AddressMaps)
            {
                var b = ObjectMapper.Map<TenantAddressMap, TenantAddressMapOutputDto>(a);
                dto.AddresseMaps.Add(b);
            }
            foreach(var c in entity.ContactMaps)
            {
                var d = ObjectMapper.Map<TenantContactMap, TenantContactMapOutputDto>(c);
                dto.ContactMaps.Add(d);
            }
            foreach(var p in entity.PictureMaps) { 
                var q = ObjectMapper.Map<TenantPictureMap, TenantPictureMapOutputDto>(p);
                dto.PictureMaps.Add(q);
            }

            return dto;
        }
    }
}

