using System;
using Xunit;
using AdvertisingPlatforms.Domain.ValueObjects;
using AdvertisingPlatforms.Domain.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace AdPlatforms.UnitTests
{
    public class LocationValueObjectTests
    {
        [Fact]
        public void Normalize_TrimsAndCollapsesAndLowercases()
        {
            var raw = "  /RU/svrd//revda/  ";
            var loc = new Location(raw);
            Assert.Equal("/ru/svrd/revda", loc.Value);
        }

        [Fact]
        public void GetPrefixes_ReturnsOrderedPrefixes()
        {
            var loc = new Location("/ru/svrd/revda");
            var prefixes = loc.GetPrefixes();
            var expected = new List<string> { "/ru", "/ru/svrd", "/ru/svrd/revda" };
            Assert.Equal(expected, prefixes.ToList());
        }

        [Fact]
        public void RootLocation_ReturnsSingleSlashPrefix()
        {
            var loc = new Location("/");
            var prefixes = loc.GetPrefixes();
            Assert.Single(prefixes);
            Assert.Equal("/", prefixes.First());
        }

        [Theory]
        [InlineData("")]
        [InlineData("    ")]
        [InlineData(null)]
        public void Constructor_InvalidOrEmpty_Throws(string raw)
        {
            Assert.Throws<DomainValidationException>(() => new Location(raw));
        }

        [Theory]
        [InlineData("/inva lid")]
        [InlineData("/bad!chars")]
        [InlineData("/ok/also$bad")]
        public void Constructor_InvalidCharactersInSegment_Throws(string raw)
        {
            Assert.Throws<DomainValidationException>(() => new Location(raw));
        }

        [Fact]
        public void Equals_IsCaseInsensitive()
        {
            var a = new Location("/RU/SVRD");
            var b = new Location("/ru/svrd");
            Assert.Equal(a, b);
            Assert.True(a.Equals(b));
        }
    }
}
