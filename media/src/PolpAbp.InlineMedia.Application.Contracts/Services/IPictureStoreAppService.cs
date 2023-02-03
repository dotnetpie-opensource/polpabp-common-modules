using System;
using PolpAbp.InlineMedia.Dtos;
using System.Threading.Tasks;
using System.Threading;

namespace PolpAbp.InlineMedia.Services
{
    public interface IPictureStoreAppService
    {
        Task<Guid> CreateAsync(PictureInputDto dto, bool autoSave = false, CancellationToken cancellationToken = default);
        Task UpdateAsync(Guid id, PictureInputDto dto, bool autoSave = false, CancellationToken cancellationToken = default);
        Task<PictureOutputDto> GetByIdAsync(Guid id);
        Task<PictureOutputDto> FindByIdAsync(Guid id);
        Task DeleteAsync(Guid id, bool autoSave = false, CancellationToken cancellationToken = default);
        Task<PictureOutputDto> GetByIdBeyondTenantAsync(Guid id);
    }
}

