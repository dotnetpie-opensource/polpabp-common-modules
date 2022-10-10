using System;
using System.Threading.Tasks;
using Xunit;

namespace PolpAbp.InlineMedia.Services
{
    public class PictureStoreAppServiceTests : InlineMediaApplicationTestBase
    {
        private readonly IPictureStoreAppService _appService;

        public PictureStoreAppServiceTests()
        {
            _appService = GetRequiredService<IPictureStoreAppService>();
        }

        [Fact]
        public async Task CreateAsync()
        {
            var id = new Guid();
            var result = await _appService.CreateAsync(new Dtos.PictureInputDto
            {
                PictureBinary = new byte[] { 1, 2, 4},
                SeoFilename = "a.png"
            });

            Assert.NotEqual(id, result);
        }
    }
}

