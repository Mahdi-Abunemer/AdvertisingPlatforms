using AdvertisingPlatforms.Domain.Exceptions;
using AdvertisingPlatforms.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace AdvertisingPlatforms.Domain.Entities
{
    /// <summary>
    /// Domain entity representing an advertising platform.
    /// Immutable-ish: name and locations set during construction.
    /// </summary>
    public sealed class Platform
    {
        public string Name { get; }
        public IReadOnlyCollection<Location> Locations { get; }

        public Platform(string name, IEnumerable<Location> locations)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainValidationException("Platform name must be non-empty.");

            if (locations == null)
                throw new DomainValidationException("Locations collection cannot be null.");

            Name = name.Trim();
            
            var uniq = new Dictionary<string, Location>(StringComparer.OrdinalIgnoreCase);
            foreach (var loc in locations)
            {
                if (loc == null) continue;
                uniq[loc.Value] = loc;
            }

            Locations = new ReadOnlyCollection<Location>(uniq.Values.ToList());
        }
    }
}
