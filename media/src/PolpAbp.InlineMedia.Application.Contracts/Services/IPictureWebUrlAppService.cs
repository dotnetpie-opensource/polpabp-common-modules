using System;
using PolpAbp.InlineMedia.Dtos;
using System.Threading.Tasks;

namespace PolpAbp.InlineMedia.Services
{
    public interface IPictureWebUrlAppService
    {
        /// <summary>
        /// Builds a picture. If successful, 
        /// </summary>
        /// <param name="picture">Picture instance</param>
        /// <param name="targetSize">The target picture size (longest side)</param>
        /// <param name="relativePath">Relative Path, such as "~/wwwroot/", or "~/sxxx"</param>
        /// <returns>Picture Full Path</returns>
        Task<string> BuildPictureFileAsync(PictureOutputDto picture,
            string relativePath,
            int targetSize = 0);
    }
}

