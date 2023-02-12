using PolpAbp.InlineMedia.Domain.Entities;
using PolpAbp.InlineMedia.Dtos;
using Polpware.AspNetCore.Framework;
using System;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.MultiTenancy;

namespace PolpAbp.InlineMedia.Services
{
    [RemoteService(false)]
    public class PictureStoreAppService : InlineMediaAppService, IPictureStoreAppService
    {
        protected readonly IRepository<Picture, Guid> _pictureRepository;

        public PictureStoreAppService(IRepository<Picture, Guid> pictureRepository)
        {
            _pictureRepository = pictureRepository;
        }

        /// <summary>
        /// Inserts a picture
        /// </summary>
        /// <param name="dto">The picture input dto</param>
        /// <returns>Id</returns>
        public virtual async Task<Guid> CreateAsync(PictureInputDto dto, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            dto.MimeType = CommonHelper.EnsureNotNull(dto.MimeType);
            dto.MimeType = CommonHelper.EnsureMaximumLength(dto.MimeType, 20);

            dto.SeoFilename = CommonHelper.EnsureMaximumLength(dto.SeoFilename, 100);

            var picture = ObjectMapper.Map<PictureInputDto, Picture>(dto);
            await _pictureRepository.InsertAsync(picture, autoSave, cancellationToken);

            return picture.Id;
        }


        /// <summary>
        /// Updates the picture
        /// </summary>
        /// <param name="id">The picture identifier</param>
        /// <param name="dto">Dto</param>
        /// <returns>Picture</returns>
        public virtual async Task UpdateAsync(Guid id, PictureInputDto dto, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            // Will throw an exception if no any item is found.
            var picture = await _pictureRepository.GetAsync(x => x.Id == id);

            // Picutre should not be null, otherwise an exception will be thrown.

            // Apply to the dtos.

            dto.MimeType = CommonHelper.EnsureNotNull(dto.MimeType);
            dto.MimeType = CommonHelper.EnsureMaximumLength(dto.MimeType, 20);

            dto.SeoFilename = CommonHelper.EnsureMaximumLength(dto.SeoFilename, 100);

            ObjectMapper.Map<PictureInputDto, Picture>(dto, picture);
            await _pictureRepository.UpdateAsync(picture, autoSave, cancellationToken);
        }

        /// <summary>
        /// Gets a picture just by the given Id.
        /// </summary>
        /// <param name="id">Picture identifier</param>
        /// <returns>Picture</returns>
        public virtual async Task<PictureOutputDto> GetByIdBeyondTenantAsync(Guid id, CancellationToken cancellationToken = default)
        {
            using (DataFilter.Disable<IMultiTenant>())
            {
                // Will throw an exception if no any item is found.
                var picture = await _pictureRepository.GetAsync(x => x.Id == id, cancellationToken: cancellationToken);

                var ret = ObjectMapper.Map<Picture, PictureOutputDto>(picture);

                return ret;
            }
        }


        /// <summary>
        /// Gets a picture
        /// </summary>
        /// <param name="id">Picture identifier</param>
        /// <returns>Picture</returns>
        public virtual async Task<PictureOutputDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            // Will throw an exception if no any item is found.
            var picture = await _pictureRepository.GetAsync(x => x.Id == id, cancellationToken: cancellationToken);

            var ret = ObjectMapper.Map<Picture, PictureOutputDto>(picture);

            return ret;
        }

        /// <summary>
        /// Gets a picture
        /// </summary>
        /// <param name="id">Picture identifier</param>
        /// <returns>Picture</returns>
        public virtual async Task<PictureOutputDto> FindByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            // Will throw an exception if no any item is found.
            var picture = await _pictureRepository.FindAsync(x => x.Id == id, cancellationToken: cancellationToken);

            if (picture == null)
            {
                return null;
            }

            var ret = ObjectMapper.Map<Picture, PictureOutputDto>(picture);

            return ret;
        }

        /// <summary>
        /// Deletes a picture
        /// </summary>
        /// <param name="id">Picture</param>
        public virtual async Task DeleteAsync(Guid id, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            var picture = await _pictureRepository.FindAsync(x => x.Id == id);

            if (picture != null)
            {
                await _pictureRepository.DeleteAsync(picture, autoSave, cancellationToken);
            }

        }
    }
}

