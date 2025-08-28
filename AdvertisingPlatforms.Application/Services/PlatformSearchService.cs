using AdvertisingPlatforms.Application.Interfaces;
using AdvertisingPlatforms.Domain.Entities;
using AdvertisingPlatforms.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdvertisingPlatforms.Application.Services
{
    public class PlatformSearchService : IPlatformSearchService
    {
        // index: declared location -> list of platforms
        // assignment to _index is atomic (replace whole dictionary on rebuild)
        private Dictionary<string, List<Platform>> _index = new(StringComparer.OrdinalIgnoreCase);
        private readonly object _sync = new();

        public void RebuildIndex(IEnumerable<Platform> platforms)
        {
            var newIndex = new Dictionary<string, List<Platform>>(StringComparer.OrdinalIgnoreCase);

            if (platforms == null)
            {
                lock (_sync)
                {
                    _index = newIndex;
                }
                return;
            }

            foreach (var platform in platforms)
            {
                if (platform?.Locations == null) continue;

                foreach (var loc in platform.Locations)
                {
                    if (loc == null) continue;
                    var key = loc.Value;
                    if (!newIndex.TryGetValue(key, out var list))
                    {
                        list = new List<Platform>();
                        newIndex[key] = list;
                    }
                    if (!list.Contains(platform))
                        list.Add(platform);
                }
            }

            lock (_sync)
            {
                _index = newIndex;
            }
        }

        public IReadOnlyCollection<Platform> FindByLocation(string rawLocation)
        {
            var location = new Location(rawLocation);

            var prefixes = location.GetPrefixes();
            var snapshot = _index;

            var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var result = new List<Platform>();

            foreach (var prefix in prefixes)
            {
                if (snapshot.TryGetValue(prefix, out var platformsForPrefix))
                {
                    foreach (var p in platformsForPrefix)
                    {
                        if (p?.Name == null) continue;
                        if (seen.Add(p.Name)) result.Add(p);
                    }
                }
            }

            return result;
        }
    }
}