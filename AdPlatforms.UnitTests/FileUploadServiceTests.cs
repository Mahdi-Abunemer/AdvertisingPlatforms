using AdvertisingPlatforms.Application.Exceptions;
using AdvertisingPlatforms.Application.Interfaces;
using AdvertisingPlatforms.Application.Services;
using AdvertisingPlatforms.Domain.Entities;
using AdvertisingPlatforms.Domain.Exceptions;
using AdvertisingPlatforms.Domain.ValueObjects;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace AdvertisingPlatforms.Tests.Application
{
    public class FileUploadServiceTests
    {
        private readonly Mock<IAdvertisingPlatformRepository> _repositoryMock;
        private readonly FileUploadService _service;

        public FileUploadServiceTests()
        {
            _repositoryMock = new Mock<IAdvertisingPlatformRepository>();
            _service = new FileUploadService(_repositoryMock.Object);
        }

        private Stream CreateStreamFromString(string content)
        {
            return new MemoryStream(System.Text.Encoding.UTF8.GetBytes(content));
        }

        [Fact]
        public void LoadFromStream_ValidFile_CallsRepositoryWithPlatforms()
        {
            var content = "PlatformOne:/ru/svrd/revda\nPlatformTwo:/ru/msk,/ru/permobl";
            var stream = CreateStreamFromString(content);

            _service.LoadFromStream(stream);

            _repositoryMock.Verify(r => r.ReplaceAll(It.Is<List<Platform>>(list =>
                list.Count == 2 &&
                list[0].Name == "PlatformOne" &&
                list[0].Locations.First().Value == "/ru/svrd/revda" &&
                list[1].Name == "PlatformTwo" &&
                list[1].Locations.Count == 2
            )), Times.Once);
        }

        [Fact]
        public void LoadFromStream_NullStream_ThrowsFileParseException()
        {
            Assert.Throws<FileParseException>(() => _service.LoadFromStream(null));
        }

        [Fact]
        public void LoadFromStream_InvalidLineFormat_ThrowsFileParseException()
        {
            var stream = CreateStreamFromString("InvalidLineWithoutColon");
            Assert.Throws<FileParseException>(() => _service.LoadFromStream(stream));
        }

        [Fact]
        public void LoadFromStream_MissingPlatformName_ThrowsFileParseException()
        {
            var stream = CreateStreamFromString("   :/ru/svrd/revda");
            Assert.Throws<FileParseException>(() => _service.LoadFromStream(stream));
        }

        [Fact]
        public void LoadFromStream_NoLocations_ThrowsFileParseException()
        {
            var stream = CreateStreamFromString("PlatformOne:   ");
            Assert.Throws<FileParseException>(() => _service.LoadFromStream(stream));
        }

        [Fact]
        public void LoadFromStream_InvalidLocation_ThrowsDomainValidationException()
        {
            var stream = CreateStreamFromString("PlatformOne:/ru/invalid path");
            Assert.Throws<DomainValidationException>(() => _service.LoadFromStream(stream));
        }
    }
}
