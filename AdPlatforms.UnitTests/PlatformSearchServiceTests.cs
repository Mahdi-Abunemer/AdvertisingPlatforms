using AdvertisingPlatforms.Application.Services;
using AdvertisingPlatforms.Domain.Entities;
using AdvertisingPlatforms.Domain.ValueObjects;
using System.Collections.Generic;
using Xunit;

namespace AdPlatforms.UnitTests
{
    public class PlatformSearchServiceTests
    {
        [Fact]
        public void FindByLocation_ShouldReturnPlatformsMatchingLocationPrefixes()
        {
            var service = new PlatformSearchService();

            var platform1 = new Platform(
                "Platform1",
                new List<Location> { new Location("New_York"), new Location("NY") }
            );

            var platform2 = new Platform(
                "Platform2",
                new List<Location> { new Location("New_Jersey") }
            );

            var platform3 = new Platform(
                "Platform3",
                new List<Location>()
            );

            service.RebuildIndex(new[] { platform1, platform2, platform3 });

            var results = service.FindByLocation("New_York/City");

            Assert.Contains(platform1, results);
            Assert.DoesNotContain(platform2, results);
            Assert.DoesNotContain(platform3, results);
            Assert.Single(results);
        }

        [Fact]
        public void RebuildIndex_ShouldHandleNullPlatforms()
        {
            var service = new PlatformSearchService();

            service.RebuildIndex(null);

            var results = service.FindByLocation("Any_Location");
            Assert.Empty(results);
        }

        [Fact]
        public void FindByLocation_ShouldReturnEmpty_WhenNoMatch()
        {
            var service = new PlatformSearchService();
            var platform = new Platform("Platform1", new List<Location> { new Location("Paris") });
            service.RebuildIndex(new[] { platform });

            var results = service.FindByLocation("London");

            Assert.Empty(results);
        }

        [Fact]
        public void FindByLocation_ShouldDeduplicatePlatforms()
        {
            var service = new PlatformSearchService();
            var platform = new Platform(
                "Platform1",
                new List<Location> { new Location("New"), new Location("New_York") }
            );
            service.RebuildIndex(new[] { platform });

            var results = service.FindByLocation("New_York/City");

            Assert.Single(results);
            Assert.Contains(platform, results);
        }
    }
}
