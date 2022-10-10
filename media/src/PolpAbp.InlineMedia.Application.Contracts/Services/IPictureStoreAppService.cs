using System;
using PolpAbp.InlineMedia.Dtos;
using System.Threading.Tasks;

namespace PolpAbp.InlineMedia.Services
{
    public interface IPictureStoreAppService
    {
        Task<Guid> CreateAsync(PictureInputDto dto);
        Task UpdateAsync(Guid id, PictureInputDto dto);
        Task<PictureOutputDto> GetByIdAsync(Guid id);
        Task<PictureOutputDto> FindByIdAsync(Guid id);
        Task DeleteAsync(Guid id);
    }
}

