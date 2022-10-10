using System;
using PolpAbp.InlineMedia.Dtos;
using System.Threading.Tasks;

namespace PolpAbp.InlineMedia.Services
{
    public interface IPictureWebUrlAppService
    {
        Task<string> BuildPictureFileAsync(PictureOutputDto picture,
            string relativePath,
            int targetSize = 0);
    }
}

