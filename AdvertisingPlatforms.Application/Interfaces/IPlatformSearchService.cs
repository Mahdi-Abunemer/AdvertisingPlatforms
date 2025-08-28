using AdvertisingPlatforms.Domain.Entities;
using AdvertisingPlatforms.Domain.ValueObjects;
using System.Collections.Generic;

namespace AdvertisingPlatforms.Application.Interfaces
{
    /// <summary>
    /// Application-level search service: builds its own index (on RebuildIndex)
    /// and serves fast read queries. 
    /// </summary>
    public interface IPlatformSearchService
    {
        /// <summary>
        /// Rebuild internal search index from the provided platforms.
        /// Called after ReplaceAll (file load).
        /// </summary>
        void RebuildIndex(IEnumerable<Platform> platforms);

        /// <summary>
        /// Accept raw input (string) — application handles normalization & validation.
        /// </summary>
        IReadOnlyCollection<Platform> FindByLocation(string rawLocation);
    }
}
