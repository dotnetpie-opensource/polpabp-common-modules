using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace PolpAbp.InlineMedia.Services
{
    public class PictureWebUrlAppServiceTests : InlineMediaApplicationTestBase
    {
        private readonly IPictureWebUrlAppService _appService;
        private readonly IPictureStoreAppService _storeAppService;

        public PictureWebUrlAppServiceTests()
        {
            _appService = GetRequiredService<IPictureWebUrlAppService>();
            _storeAppService = GetRequiredService<IPictureStoreAppService>();
        }

        [Fact]
        public async Task BuildPictureFileTestAsync()
        {
            var bytes = File.ReadAllBytes("test.png");

            var id = new Guid();

            var pictureId = await _storeAppService.CreateAsync(new Dtos.PictureInputDto
            {
                PictureBinary = bytes,
                MimeType = "image/png",
                SeoFilename = "apple"
            });

            var savedPicture = await _storeAppService.GetByIdAsync(pictureId);

            var result = await _appService.BuildPictureFileAsync(savedPicture, string.Empty);

            Assert.NotEmpty(result);

        }


    }
}

