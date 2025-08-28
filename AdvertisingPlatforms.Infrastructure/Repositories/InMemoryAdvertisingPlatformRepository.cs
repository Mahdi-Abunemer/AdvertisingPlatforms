using AdvertisingPlatforms.Application.Interfaces;
using AdvertisingPlatforms.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisingPlatforms.Infrastructure.Repositories
{
    public class InMemoryAdvertisingPlatformRepository : IAdvertisingPlatformRepository
    {
        private readonly List<Platform> _platforms = new();

        public void ReplaceAll(IEnumerable<Platform> platforms)
        {
            _platforms.Clear();
            _platforms.AddRange(platforms);
        }

        public IReadOnlyCollection<Platform> GetAll() => _platforms.ToList();
    }
}