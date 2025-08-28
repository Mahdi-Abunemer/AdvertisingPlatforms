using AdvertisingPlatforms.Application.Interfaces;
using AdvertisingPlatforms.Application.Services;
using AdvertisingPlatforms.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdPlatforms.UnitTests
{
    /// <summary>
    /// Using the real repository instead of Moq to make sure data is really stored in memory
    /// </summary>
    public class InMemoryAdvertisingPlatformRepositoryTests
    {
        private Stream CreateStreamFromString(string content)
        {
            return new MemoryStream(System.Text.Encoding.UTF8.GetBytes(content));
        }

        [Fact]
        public void ReplaceAll_ShouldStoreDataInMemory()
        {
            var repo = new InMemoryAdvertisingPlatformRepository();
            var _platformService = new PlatformSearchService();
            var service = new FileUploadService(repo , _platformService);
            var content = "PlatformOne:/ru/svrd/revda\nPlatformTwo:/ru/msk,/ru/permobl";
            var stream = CreateStreamFromString(content);

            service.LoadFromStream(stream);

            var allPlatforms = repo.GetAll();

            Assert.Equal(2, allPlatforms.Count);

            var first = allPlatforms.First(p => p.Name == "PlatformOne");
            Assert.Single(first.Locations);
            Assert.Equal("/ru/svrd/revda", first.Locations.First().Value);

            var second = allPlatforms.First(p => p.Name == "PlatformTwo");
            Assert.Equal(2, second.Locations.Count);
            Assert.Contains(second.Locations, l => l.Value == "/ru/msk");
            Assert.Contains(second.Locations, l => l.Value == "/ru/permobl");
        }
    }
}
