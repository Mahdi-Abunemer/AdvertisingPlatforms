using AdvertisingPlatforms.Domain.Entities;
using AdvertisingPlatforms.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisingPlatforms.Application.Interfaces
{
    public interface IAdvertisingPlatformRepository
    {
        void ReplaceAll(IEnumerable<Platform> platforms);

        IReadOnlyCollection<Platform> GetAll();
    }
}