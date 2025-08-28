using AdvertisingPlatforms.Application.Exceptions;
using AdvertisingPlatforms.Application.Interfaces;
using AdvertisingPlatforms.Domain.Entities;
using AdvertisingPlatforms.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisingPlatforms.Application.Services
{
    public class FileUploadService
    {
        private readonly IAdvertisingPlatformRepository _repository;

        public FileUploadService(IAdvertisingPlatformRepository repository)
        {
            _repository = repository;
        }

        public void LoadFromStream(Stream fileStream)
        {
            if (fileStream == null || !fileStream.CanRead)
                throw new FileParseException("Invalid file stream.");

            using var reader = new StreamReader(fileStream);
            var platforms = new List<Platform>();

            int lineNumber = 0;

            while (!reader.EndOfStream)
            {
                lineNumber++;
                var line = reader.ReadLine();

                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var parts = line.Split(':', 2);
                if (parts.Length != 2)
                    throw new FileParseException($"Invalid format at line {lineNumber}");

                var name = parts[0].Trim();
                if (string.IsNullOrEmpty(name))
                    throw new FileParseException($"Missing platform name at line {lineNumber}");

                var rawLocations = parts[1].Split(',', System.StringSplitOptions.RemoveEmptyEntries)
                                           .Select(x => x.Trim())
                                           .Where(x => !string.IsNullOrWhiteSpace(x))
                                           .ToList();

                if (!rawLocations.Any())
                    throw new FileParseException($"Platform {name} must have at least one location (line {lineNumber})");

                var locations = rawLocations.Select(l => new Location(l)).ToList();

                var platform = new Platform(name, locations);
                platforms.Add(platform);
            }

            _repository.ReplaceAll(platforms);
        }
    }
}