using System;
using PolpAbp.InlineMedia.Dtos;
using System.Threading.Tasks;

namespace PolpAbp.InlineMedia.Services
{
    public interface IPictureStoreAppService
    {
        Task<Guid> InsertPictureAsync(PictureInputDto dto);
        Task UpdatePictureAsync(Guid id, PictureInputDto dto);
        Task<PictureOutputDto> GetPictureByIdAsync(Guid id);
        Task<PictureOutputDto> FindPictureByIdAsync(Guid id);
        Task DeletePictureAsync(Guid id);
    }
}

