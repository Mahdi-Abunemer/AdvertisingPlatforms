using AdvertisingPlatforms.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdvertisingPlatforms.Domain.ValueObjects
{
    /// <summary>
    /// Value object representing a normalized location path (example: "/ru/svrd/revda").
    /// Normalization rules:
    /// - Trim whitespace
    /// - Collapse duplicate slashes
    /// - Ensure leading slash
    /// - Convert to lower-case (case-insensitive matching)
    /// - Prohibit empty segments (unless the whole path is "/")
    /// </summary>
    public sealed class Location : IEquatable<Location>
    {
        private static readonly Regex MultipleSlashes = new Regex("/{2,}", RegexOptions.Compiled);

        public string Value { get; }

        public Location(string raw)
        {
            if (raw is null)
                throw new DomainValidationException("Location cannot be null.");

            var s = raw.Trim();

            s = s.Replace('\\', '/');

            s = MultipleSlashes.Replace(s, "/");

            if (!s.StartsWith("/"))
                s = "/" + s;

            if (s == "/")
            {
                Value = s;
                return;
            }

            if (s.Length > 1 && s.EndsWith("/"))
                s = s.TrimEnd('/');

            s = s.ToLowerInvariant();

            var segments = s.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            if (segments.Length == 0)
                throw new DomainValidationException($"Location '{raw}' is invalid after normalization.");

            foreach (var seg in segments)
            {
                if (string.IsNullOrWhiteSpace(seg))
                    throw new DomainValidationException($"Location '{raw}' contains an empty segment.");

                if (!IsAllowedSegment(seg))
                    throw new DomainValidationException($"Location segment '{seg}' contains invalid characters.");
            }

            Value = "/" + string.Join("/", segments);
        }

        private static bool IsAllowedSegment(string segment)
        {
            foreach (var ch in segment)
            {
                if (char.IsLetterOrDigit(ch) || ch == '-' || ch == '_')
                    continue;
                return false;
            }
            return true;
        }

        /// <summary>
        /// Returns ordered list of prefixes for this location.
        /// Example: "/ru/svrd/revda" => ["/ru", "/ru/svrd", "/ru/svrd/revda"]
        /// Root "/" returns ["/"].
        /// </summary>
        public IReadOnlyList<string> GetPrefixes()
        {
            if (Value == "/")
                return new List<string> { "/" };

            var segments = Value.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            var result = new List<string>(segments.Length);
            for (int i = 0; i < segments.Length; i++)
            {
                var prefix = "/" + string.Join("/", segments.Take(i + 1));
                result.Add(prefix);
            }

            return result;
        }

        public override string ToString() => Value;

        public bool Equals(Location other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Value, other.Value, StringComparison.OrdinalIgnoreCase);
        }

        public override bool Equals(object obj) => Equals(obj as Location);

        public override int GetHashCode() => StringComparer.OrdinalIgnoreCase.GetHashCode(Value);
    }
}
