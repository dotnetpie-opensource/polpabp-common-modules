using System;
using PolpAbp.InlineMedia.Domain.Entities;
using System.IO;
using System.Linq;
using System.Threading;
using Volo.Abp;
using PolpAbp.InlineMedia.Dtos;
using Polpware.NetStd.Framework.IO;
using PolpAbp.InlineMedia.Utilities;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using Microsoft.Extensions.FileProviders;
using System.Threading.Tasks;

namespace PolpAbp.InlineMedia.Services
{
    [RemoteService(false)]
    public class PictureWebUrlAppService : InlineMediaAppService, IPictureWebUrlAppService
    {
        private readonly INopFileProvider _nopFileProvider;

        public PictureWebUrlAppService(INopFileProvider nopFileProvider)
        {
            _nopFileProvider = nopFileProvider;
        }


        /// <summary>
        /// Builds a picture. If successful, 
        /// </summary>
        /// <param name="picture">Picture instance</param>
        /// <param name="targetSize">The target picture size (longest side)</param>
        /// <param name="relativePath">Relative Path, such as "~/wwwroot/", or "~/sxxx"</param>
        /// <returns>Picture Full Path</returns>
        public virtual Task<string> BuildPictureFileAsync(PictureOutputDto picture,
            string relativePath,
            int targetSize = 0)
        {
            var seoFileName = picture.SeoFilename; // = GetPictureSeName(picture.SeoFilename); //just for sure

            var lastPart = GetFileExtensionFromMimeType(picture.MimeType);
            string thumbFileName;
            if (targetSize == 0)
            {
                thumbFileName = !string.IsNullOrEmpty(seoFileName)
                    ? $"{picture.Id}_{seoFileName}.{lastPart}"
                    : $"{picture.Id}.{lastPart}";
            }
            else
            {
                thumbFileName = !string.IsNullOrEmpty(seoFileName)
                    ? $"{picture.Id}_{seoFileName}_{targetSize}.{lastPart}"
                    : $"{picture.Id}_{targetSize}.{lastPart}";
            }
            var thumbFilePath = GetThumbLocalPath(relativePath, thumbFileName);

            //the named mutex helps to avoid creating the same files in different threads,
            //and does not decrease performance significantly, because the code is blocked only for the specific file.
            using (var mutex = new Mutex(false, thumbFileName))
            {
                if (!GeneratedThumbExists(thumbFilePath, thumbFileName))
                {
                    try
                    {
                        mutex.WaitOne();

                        //check, if the file was created, while we were waiting for the release of the mutex.
                        if (!GeneratedThumbExists(thumbFilePath, thumbFileName))
                        {
                            using (var image = Image.Load(picture.PictureBinary))
                            {
                                //resizing required
                                if (targetSize != 0)
                                {
                                    var newSize = CalculateDimensions(image.Size(), targetSize);

                                    int width = newSize.Width / 2;
                                    int height = newSize.Height / 2;
                                    image.Mutate(x => x.Resize(width, height));
                                }

                                image.Save(thumbFilePath);
                            }
                        }
                    }
                    finally
                    {
                        mutex.ReleaseMutex();
                    }
                }
            }

            return Task.FromResult(thumbFilePath);
        }

        /// <summary>
        /// Returns the file extension from mime type.
        /// </summary>
        /// <param name="mimeType">Mime type</param>
        /// <returns>File extension</returns>
        protected virtual string GetFileExtensionFromMimeType(string mimeType)
        {
            if (mimeType == null)
                return null;

            //TODO use FileExtensionContentTypeProvider to get file extension

            var parts = mimeType.Split('/');
            var lastPart = parts[parts.Length - 1];
            switch (lastPart)
            {
                case "pjpeg":
                    lastPart = "jpg";
                    break;
                case "x-png":
                    lastPart = "png";
                    break;
                case "x-icon":
                    lastPart = "ico";
                    break;
            }
            return lastPart;
        }

        /// <summary>
        /// Get picture (thumb) local path
        /// </summary>
        /// <param name="thumbFileName">Filename</param>
        /// <returns>Local picture thumb path</returns>
        protected virtual string GetThumbLocalPath(string relativePath, string thumbFileName)
        {
            var thumbsDirectoryPath = _nopFileProvider.GetAbsolutePath(relativePath);

            var thumbFilePath = _nopFileProvider.Combine(thumbsDirectoryPath, thumbFileName);
            return thumbFilePath;
        }


        /// <summary>
        /// Get a value indicating whether some file already exists
        /// </summary>
        /// <param name="filePath">file path</param>
        /// <param name="fileName">file name</param>
        /// <returns>Result</returns>
        protected virtual bool GeneratedThumbExists(string filePath, string fileName)
        {
            return _nopFileProvider.FileExists(filePath);
        }

        /// <summary>
        /// Calculates picture dimensions whilst maintaining aspect
        /// </summary>
        /// <param name="originalSize">The original picture size</param>
        /// <param name="targetSize">The target picture size (longest side)</param>
        /// <param name="resizeType">Resize type</param>
        /// <param name="ensureSizePositive">A value indicatingh whether we should ensure that size values are positive</param>
        /// <returns></returns>
        protected virtual Size CalculateDimensions(Size originalSize, int targetSize,
            ResizeType resizeType = ResizeType.LongestSide, bool ensureSizePositive = true)
        {
            float width, height;

            switch (resizeType)
            {
                case ResizeType.LongestSide:
                    if (originalSize.Height > originalSize.Width)
                    {
                        // portrait
                        width = originalSize.Width * (targetSize / (float)originalSize.Height);
                        height = targetSize;
                    }
                    else
                    {
                        // landscape or square
                        width = targetSize;
                        height = originalSize.Height * (targetSize / (float)originalSize.Width);
                    }
                    break;
                case ResizeType.Width:
                    width = targetSize;
                    height = originalSize.Height * (targetSize / (float)originalSize.Width);
                    break;
                case ResizeType.Height:
                    width = originalSize.Width * (targetSize / (float)originalSize.Height);
                    height = targetSize;
                    break;
                default:
                    throw new Exception("Not supported ResizeType");
            }

            if (ensureSizePositive)
            {
                if (width < 1)
                    width = 1;
                if (height < 1)
                    height = 1;
            }

            //we invoke Math.Round to ensure that no white background is rendered - https://www.nopcommerce.com/boards/t/40616/image-resizing-bug.aspx
            return new Size((int)Math.Round(width), (int)Math.Round(height));
        }

    }


}

